using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP.Unify.Core
{
		public enum ExemptionEntity
        {
            FileExtension = 1,
            FileName = 2,
            FilePath = 3,
            FileSize = 4,
            FolderName = 5,
            FolderPath = 6
        }

		public enum ExemptionOperator
		{
			Contains = 1,
			IsEqualTo = 2,
			IsGreaterThan = 3,
			IsLessThan = 4,
			IsNotEqualTo = 5,
			Matches = 6
		}

    static class Common
    {
		public Dictionary<SyncTaskOptions, string> FormattedSyncTaskOptions = new Dictionary<SyncTaskOptions,string>() {
			{SyncTaskOptions.AddFiles, "Add files"},
			{SyncTaskOptions.ReplaceFiles, "Replace files"}, 
			{SyncTaskOptions.RemoveFiles, "Remove files"}, 
			{SyncTaskOptions.ExcludeSubdirectories, "Exclude subdirectories"}, 
			{SyncTaskOptions.ExcludeHiddenFiles, "Exclude hidden files"} 
		};

		public Dictionary<ExemptionEntity, string> FormattedExemptionEntities = new Dictionary<ExemptionEntity,string>() {
			{ExemptionEntity.FileExtension, "File extension"}, 
			{ExemptionEntity.FileName, "File name"}, 
			{ExemptionEntity.FilePath, "File path"}, 
			{ExemptionEntity.FileSize, "File size"}, 
			{ExemptionEntity.FolderName, "Folder name"}, 
			{ExemptionEntity.FolderPath, "Folder path"} 
		};

		public Dictionary<ExemptionOperator, string> FormattedExemptionOperators = new Dictionary<ExemptionOperator,string>() {
			{ExemptionOperator.Contains, "contains"}, 
			{ExemptionOperator.IsEqualTo, "is equal to"}, 
			{ExemptionOperator.IsGreaterThan, "is greater than"}, 
			{ExemptionOperator.IsLessThan, "is less than"}, 
			{ExemptionOperator.IsNotEqualTo, "is not equal to"}, 
			{ExemptionOperator.Matches, "matches"} 
		};
			
		public Dictionary<ExemptionOperator, string> GetExemptionOperators(ExemptionEntity entity)
		{
			if (entity == ExemptionEntity.FileSize)
			{
				return (from x in FormattedExemptionOperators
						where x.Key == ExemptionOperator.IsEqualTo 
							  || x.Key == ExemptionOperator.IsGreaterThan 
							  || x.Key == ExemptionOperator.IsLessThan
						select x).ToDictionary(x => x.Key, x => x.Value);
			}
			else
			{
				return (from x in FormattedExemptionOperators
                        where x.Key == ExemptionOperator.Contains
							  || x.Key == ExemptionOperator.IsEqualTo
							  || x.Key == ExemptionOperator.IsNotEqualTo
							  || x.Key == ExemptionOperator.Matches
                        select x).ToDictionary(x => x.Key, x => x.Value);
			}
		}
    }
}
