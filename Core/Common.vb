Public Module Common

    Public Enum ExemptionEntity
        FileExtension = 1
        FileName = 2
        FilePath = 3
        FileSize = 4
        FolderName = 5
        FolderPath = 6
    End Enum

    Public Enum ExemptionOperator
        Contains = 1
        IsEqualTo = 2
        IsGreaterThan = 3
        IsLessThan = 4
        IsNotEqualTo = 5
        Matches = 6
    End Enum

    Public TASK_OPTIONS As New Dictionary(Of SyncTaskOptions, String)() From { _
        {SyncTaskOptions.AddFiles, "Add files"}, _
        {SyncTaskOptions.ReplaceFiles, "Replace files"}, _
        {SyncTaskOptions.RemoveFiles, "Remove files"}, _
        {SyncTaskOptions.IncludeSubdirectories, "Include subdirectories"}, _
        {SyncTaskOptions.ExcludeHiddenFiles, "Exclude hidden files"} _
    }

    Public EXEMPTION_ENTITIES As New Dictionary(Of ExemptionEntity, String)() From { _
        {ExemptionEntity.FileExtension, "File extension"}, _
        {ExemptionEntity.FileName, "File name"}, _
        {ExemptionEntity.FilePath, "File path"}, _
        {ExemptionEntity.FileSize, "File size"}, _
        {ExemptionEntity.FolderName, "Folder name"}, _
        {ExemptionEntity.FolderPath, "Folder path"} _
    }

    Public EXEMPTION_OPERATORS As New Dictionary(Of ExemptionOperator, String)() From { _
        {ExemptionOperator.Contains, "contains"}, _
        {ExemptionOperator.IsEqualTo, "is equal to"}, _
        {ExemptionOperator.IsGreaterThan, "is greater than"}, _
        {ExemptionOperator.IsLessThan, "is less than"}, _
        {ExemptionOperator.IsNotEqualTo, "is not equal to"}, _
        {ExemptionOperator.Matches, "matches"} _
    }

    Public Function GetExemptionOperators(ByVal entity As ExemptionEntity) As Dictionary(Of ExemptionOperator, String)
        Select Case entity
            Case ExemptionEntity.FileExtension, ExemptionEntity.FileName, ExemptionEntity.FilePath, ExemptionEntity.FolderName, ExemptionEntity.FolderPath
                Return (From x In EXEMPTION_OPERATORS
                        Where x.Key = ExemptionOperator.Contains _
                        OrElse x.Key = ExemptionOperator.IsEqualTo _
                        OrElse x.Key = ExemptionOperator.IsNotEqualTo _
                        OrElse x.Key = ExemptionOperator.Matches
                        Select x).ToDictionary(Function(x) x.Key, Function(x) x.Value)
            Case ExemptionEntity.FileSize
                Return (From x In EXEMPTION_OPERATORS
                        Where x.Key = ExemptionOperator.IsEqualTo _
                        OrElse x.Key = ExemptionOperator.IsGreaterThan _
                        OrElse x.Key = ExemptionOperator.IsLessThan
                        Select x).ToDictionary(Function(x) x.Key, Function(x) x.Value)
            Case Else
                Return EXEMPTION_OPERATORS
        End Select
    End Function

End Module
