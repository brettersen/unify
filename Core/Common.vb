Public Enum Action
    None
    Insert
    Update
    Delete
End Enum

Public Module Common

    Public Const ConnectionString As String = "Server=.\SQLEXPRESS;Database=Unify;Trusted_Connection=True;"



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

End Module
