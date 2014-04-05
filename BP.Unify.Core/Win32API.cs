using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BP.Unify.Core
{
	class Win32API
	{
		[Flags]
		public enum CopyFileFlags : uint
		{
			COPY_FILE_ALLOW_DECRYPTED_DESTINATION = 0x8,
			COPY_FILE_COPY_SYMLINK = 0x800,
			COPY_FILE_FAIL_IF_EXISTS = 0x1,
			COPY_FILE_NO_BUFFERING = 0x1000,
			COPY_FILE_OPEN_SOURCE_FOR_WRITE = 0x4,
			COPY_FILE_RESTARTABLE = 0x2
		}

		public enum CopyProgressCallbackReason : uint
		{
			CALLBACK_CHUNK_FINISHED = 0x0,
			CALLBACK_STREAM_SWITCH = 0x1
		}

		public enum CopyProgressResult : uint
		{
			PROGRESS_CANCEL = 1,
			PROGRESS_CONTINUE = 0,
			PROGRESS_STOP = 2,
			PROGRESS_QUIET = 3
		}

		public delegate CopyProgressResult CopyProgressRoutine(long totalFileSize,
															   long totalBytesTransferred,
															   long streamSize,
															   long streamBytesTransferred,
															   uint streamNumber,
															   CopyProgressCallbackReason callbackReason,
															   IntPtr sourceFile,
															   IntPtr destinationFile,
															   IntPtr data);

		[DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool CopyFileEx(string existingFileName, 
											 string newFileName,
											 CopyProgressResult progressRoutine,
											 IntPtr data,
											 bool cancel,
											 CopyFileFlags copyFlags);
	}
}
