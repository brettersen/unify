using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP.Unify.Core
{
	public enum FileOperation
	{
		None = 0,
		Add = 1,
		Replace = 2,
		Remove = 3
	}

	public class SyncOperation
	{
		public delegate void SyncOperationStartedHandler(SyncOperation operation);
		public delegate void SyncOperationProgressedHandler(long bytesTotal, long bytesTransferred);
		public delegate void SyncOperationFinishedHandler(SyncOperation operation, SyncOperationFinishedEventArgs e);

		public event SyncOperationStartedHandler SyncOperationStarted;
		public event SyncOperationProgressedHandler SyncOperationProgressed;
		public event SyncOperationFinishedHandler SyncOperationFinished;

		internal SyncOperation()
		{

		}

		#region PROPERTIES

		public string SourceFilePath { get; set; }
		public string TargetFilePath { get; set; }
		public string RelativeFilePath { get; set; }
		public FileOperation Operation { get; set; }
		public SyncTaskExemption Exemption { get; set; }

		#endregion

		#region PUBLIC METHODS

		public void Execute(ref bool stopRequested)
		{
			try
			{
				this.SyncOperationStarted(this);
				switch(this.Operation)
				{
					case FileOperation.Add:
						CreateParentDirectory();
						Win32API.CopyFileEx(this.SourceFilePath, this.TargetFilePath, CopyProgress, null, ref stopRequested, null);
					case FileOperation.Replace:
						Win32API.CopyFileEx(this.SourceFilePath, this.TargetFilePath, CopyProgress, null, ref stopRequested, null);
					case FileOperation.Remove:
						File.SetAttributes(this.TargetFilePath, FileAttributes.Normal);
						File.Delete(this.TargetFilePath);
						if (Path.GetDirectoryName(this.TargetFilePath) != this.TargetFilePath.Replace(Path.DirectorySeparatorChar + this.RelativeFilePath, ""))
						{
							DeleteParentDirectory();
						}
				}
				if (!stopRequested)
				{ this.SyncOperationFinished(this, new SyncOperationFinishedEventArgs()); }
				else
				{ this.SyncOperationFinished(this, new SyncOperationFinishedEventArgs(true)); }		
			}
			catch (Exception ex)
			{
				this.SyncOperationFinished(this, new SyncOperationFinishedEventArgs(false, true, ex));
			}
		}

		#endregion

		#region PRIVATE METHODS

		private Win32API.CopyProgressResult CopyProgress(long totalFileSize, long totalBytesTransferred, long streamSize, long streamBytesTransferred, uint streamNumber, Win32API.CopyProgressCallbackReason callbackReason, IntPtr sourceFile, IntPtr destinationFile, IntPtr data)
		{
			this.SyncOperationProgressed(totalFileSize, totalBytesTransferred);
			return Win32API.CopyProgressResult.PROGRESS_CONTINUE;
		}

		private void CreateParentDirectory()
		{
			string sourceParentDirectory = Path.GetDirectoryName(this.SourceFilePath);
			string targetParentDirectory = Path.GetDirectoryName(this.TargetFilePath);
			if (!Directory.Exists(targetParentDirectory))
			{
				Directory.CreateDirectory(targetParentDirectory);
				File.SetAttributes(targetParentDirectory, File.GetAttributes(sourceParentDirectory));
			}
		}

		private void DeleteParentDirectory()
		{
			DirectoryInfo parentDirectory = new DirectoryInfo(Path.GetDirectoryName(this.TargetFilePath));
			if (!parentDirectory.EnumerateDirectories().Any() && !parentDirectory.EnumerateFiles().Any())
			{
				parentDirectory.Delete(false);
			}
		}

		#endregion
	}

	public class SyncOperationFinishedEventArgs
	{
		internal SyncOperationFinishedEventArgs(bool canceled = false, bool failed = false, Exception failureReason = null)
		{
			this.Canceled = canceled;
			this.Failed = failed;
			this.FailureReason = failureReason;
		}

		public bool Canceled { get; set; }
		public bool Failed { get; set; }
		public Exception FailureReason { get; set; }
	}
}
