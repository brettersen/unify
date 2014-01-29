Imports System.IO
Imports System.Security.Cryptography
Imports System.Text.RegularExpressions

<Flags()> _
Public Enum SyncTaskOptions
    AddFiles = 1
    ReplaceFiles = 2
    RemoveFiles = 4
    IncludeSubdirectories = 8
    ExcludeHiddenFiles = 16
    CompareFilesInDepth = 32
End Enum

Public Enum SyncTaskMilestone
    DeterminingSourceFiles
    DeterminingTargetFiles
    DeterminingFilesToAdd
    DeterminingFilesToReplace
    DeterminingFilesToRemove
End Enum

<Serializable>
Public Class SyncTask

    Private _sourceDirectory As String
    Private _targetDirectory As String
    Private _options As SyncTaskOptions
    Private _exemptions As List(Of SyncTaskExemption)

    Public Event SyncTaskMilestoneReached(ByVal milestone As SyncTaskMilestone)

    Public Sub New()
        Me.Exemptions = New List(Of SyncTaskExemption)
    End Sub

#Region "PROPERTIES"

    Public Property SourceDirectory As String
        Get
            Return _sourceDirectory
        End Get
        Set(value As String)
            _sourceDirectory = value
        End Set
    End Property

    Public Property TargetDirectory As String
        Get
            Return _targetDirectory
        End Get
        Set(value As String)
            _targetDirectory = value
        End Set
    End Property

    Public Property Options As SyncTaskOptions
        Get
            Return _options
        End Get
        Set(value As SyncTaskOptions)
            _options = value
        End Set
    End Property

    Public Property Exemptions As List(Of SyncTaskExemption)
        Get
            Return _exemptions
        End Get
        Set(value As List(Of SyncTaskExemption))
            _exemptions = value
        End Set
    End Property

#End Region

#Region "METHODS"

    Private Shared Function ByteArraysAreEqual(ByVal firstByteArray As Byte(), ByVal secondByteArray As Byte()) As Boolean
        If (firstByteArray Is secondByteArray) Then
            Return True
        End If
        If (firstByteArray Is Nothing OrElse secondByteArray Is Nothing) Then
            Return False
        End If
        If (firstByteArray.Length <> secondByteArray.Length) Then
            Return False
        End If
        For i As Integer = 0 To firstByteArray.Length - 1
            If (firstByteArray(i) <> secondByteArray(i)) Then
                Return False
            End If
        Next i
        Return True
    End Function

    Public Function Clone() As SyncTask
        Dim item As New SyncTask
        With item
            .SourceDirectory = Me.SourceDirectory
            .TargetDirectory = Me.TargetDirectory
            .Options = Me.Options
            .Exemptions.AddRange(Me.Exemptions)
        End With
        Return item
    End Function

    Private Shared Function FilesAreEqual(ByVal firstFilePath As String, ByVal secondFilePath As String, ByVal compareFilesInDepth As Boolean) As Boolean
        Return FilesAreEqual(New FileInfo(firstFilePath), New FileInfo(secondFilePath), compareFilesInDepth)
    End Function

    Private Shared Function FilesAreEqual(ByVal firstFile As FileInfo, ByVal secondFile As FileInfo, ByVal compareFilesInDepth As Boolean) As Boolean
        If firstFile.Length = secondFile.Length Then
            If firstFile.LastWriteTime = secondFile.LastWriteTime Then
                Return True
            Else
                If compareFilesInDepth Then
                    If ByteArraysAreEqual(GetHash(firstFile.FullName), GetHash(secondFile.FullName)) Then
                        Return True
                    Else
                        Return False
                    End If
                Else
                    Return False
                End If
            End If
        Else
            Return False
            End If
    End Function

    Private Function GetChildPaths(ByVal parentPath As String, ByVal recursive As Boolean) As List(Of String)
        ' Returns relative paths and fails if an UnauthorizedAccessException is thrown.
        Dim childPaths As New List(Of String)
        Dim parentDirectory As New DirectoryInfo(parentPath)
        If recursive Then
            childPaths.AddRange(From p In parentDirectory.EnumerateFiles("*.*", SearchOption.AllDirectories)
                                Select p.FullName.Replace(parentPath, Space(0)))
            childPaths.AddRange(From p In parentDirectory.EnumerateDirectories("*", SearchOption.AllDirectories)
                                Select p.FullName.Replace(parentPath, Space(0)))
        Else
            childPaths.AddRange(From p In parentDirectory.EnumerateFiles("*.*", SearchOption.TopDirectoryOnly)
                                Select p.FullName.Replace(parentPath, Space(0)))
        End If
        Return childPaths
    End Function

    Private Function GetChildPaths(ByVal parentPath As String, ByVal recursive As Boolean, ByVal firstCall As Boolean) As IEnumerable(Of String)
        ' Returns absolute paths and skips any directories that throw an UnauthorizedAccessException
        Dim childPaths As IEnumerable(Of String)
        Try
            childPaths = Enumerable.Empty(Of String)()
            If recursive Then
                childPaths = Directory.EnumerateDirectories(parentPath).SelectMany(Function(x) GetChildPaths(x, recursive, False))
            End If
            If firstCall Then
                Return childPaths.Concat(Directory.EnumerateFiles(parentPath, "*.*"))
            Else
                Return childPaths.Concat(Directory.EnumerateFiles(parentPath, "*.*")).Concat(New String() {parentPath})
            End If
        Catch ex As UnauthorizedAccessException
            Return Enumerable.Empty(Of String)()
        End Try
    End Function

    Private Shared Function GetHash(ByVal filePath As String) As Byte()
        Using algorithm = MD5.Create()
            Using [fileStream] = File.OpenRead(filePath)
                Return algorithm.ComputeHash([fileStream])
            End Using
        End Using
    End Function

    Public Function GetOperations(ByRef stopRequested As Boolean) As List(Of SyncOperation)

        Dim sourceDirectoryPath, targetDirectoryPath As String
        Dim sourceDirectoryInfo, targetDirectoryInfo As DirectoryInfo
        Dim sourceFiles, targetFiles As FileInfo()
        Dim relativeSourceFilePaths, relativeTargetFilePaths As New List(Of String)
        Dim filesToAdd, filesToReplace, filesToRemove, filesToIgnore As IEnumerable(Of String)
        Dim recursive As Boolean
        Dim operation As SyncOperation
        Dim operations As New List(Of SyncOperation)
        Dim sourceFile, targetFile As FileInfo
        Dim determinedExemption As SyncTaskExemption

        Dim GetSourceFile = Function(relativeFilePath As String) As FileInfo
                                Return (From f In sourceFiles
                                        Where f.FullName = (sourceDirectoryPath & Path.DirectorySeparatorChar & relativeFilePath)
                                        Select f).First()
                            End Function

        Dim GetTargetFile = Function(relativeFilePath As String) As FileInfo
                                Return (From f In targetFiles
                                        Where f.FullName = (targetDirectoryPath & Path.DirectorySeparatorChar & relativeFilePath)
                                        Select f).First()
                            End Function

        sourceDirectoryPath = Me.SourceDirectory.TrimEnd(Path.DirectorySeparatorChar)
        targetDirectoryPath = Me.TargetDirectory.TrimEnd(Path.DirectorySeparatorChar)
        sourceDirectoryInfo = New DirectoryInfo(sourceDirectoryPath)
        targetDirectoryInfo = New DirectoryInfo(targetDirectoryPath)

        recursive = Me.Options.HasFlag(SyncTaskOptions.IncludeSubdirectories)

        If stopRequested Then Return operations

        RaiseEvent SyncTaskMilestoneReached(SyncTaskMilestone.DeterminingSourceFiles)
        sourceFiles = sourceDirectoryInfo.GetFiles("*", IIf(recursive, SearchOption.AllDirectories, SearchOption.TopDirectoryOnly))
        For Each f As FileInfo In sourceFiles
            If Not Me.Options.HasFlag(SyncTaskOptions.ExcludeHiddenFiles) OrElse Not f.Attributes.HasFlag(FileAttributes.Hidden) Then
                relativeSourceFilePaths.Add(f.FullName.Replace(sourceDirectoryPath & Path.DirectorySeparatorChar, Space(0)))
            End If
        Next

        If stopRequested Then Return operations

        RaiseEvent SyncTaskMilestoneReached(SyncTaskMilestone.DeterminingTargetFiles)
        targetFiles = targetDirectoryInfo.GetFiles("*", IIf(recursive, SearchOption.AllDirectories, SearchOption.TopDirectoryOnly))
        For Each f As FileInfo In targetFiles
            If Not Me.Options.HasFlag(SyncTaskOptions.ExcludeHiddenFiles) OrElse Not f.Attributes.HasFlag(FileAttributes.Hidden) Then
                relativeTargetFilePaths.Add(f.FullName.Replace(targetDirectoryPath & Path.DirectorySeparatorChar, Space(0)))
            End If
        Next

        If stopRequested Then Return operations

        RaiseEvent SyncTaskMilestoneReached(SyncTaskMilestone.DeterminingFilesToAdd)

        If Me.Options.HasFlag(SyncTaskOptions.AddFiles) Then
            If stopRequested Then Return operations
            filesToAdd = relativeSourceFilePaths.Except(relativeTargetFilePaths)
            If stopRequested Then Return operations
            For Each fileToAdd In filesToAdd
                If stopRequested Then Return operations
                sourceFile = GetSourceFile(fileToAdd)
                operation = New SyncOperation()
                With operation
                    .SourceFilePath = sourceDirectoryPath & Path.DirectorySeparatorChar & fileToAdd
                    .TargetFilePath = targetDirectoryPath & Path.DirectorySeparatorChar & fileToAdd
                    .RelativeFilePath = fileToAdd
                    If Not IsExempt(sourceFile, determinedExemption) Then
                        .Operation = FileOperation.Add
                    Else
                        .Operation = FileOperation.None
                        .Exemption = determinedExemption
                    End If
                End With
                operations.Add(operation)
            Next
        End If

        RaiseEvent SyncTaskMilestoneReached(SyncTaskMilestone.DeterminingFilesToReplace)

        If Me.Options.HasFlag(SyncTaskOptions.ReplaceFiles) Then
            If stopRequested Then Return operations
            filesToIgnore = relativeSourceFilePaths.Intersect(relativeTargetFilePaths)
            If stopRequested Then Return operations
            filesToReplace = (From matchedFile In filesToIgnore
                              Where Not FilesAreEqual(GetSourceFile(matchedFile), GetTargetFile(matchedFile), Me.Options.HasFlag(SyncTaskOptions.CompareFilesInDepth))
                              Select matchedFile)
            If stopRequested Then Return operations
            For Each fileToReplace In filesToReplace
                If stopRequested Then Return operations
                sourceFile = GetSourceFile(fileToReplace)
                operation = New SyncOperation()
                With operation
                    .SourceFilePath = sourceDirectoryPath & Path.DirectorySeparatorChar & fileToReplace
                    .TargetFilePath = targetDirectoryPath & Path.DirectorySeparatorChar & fileToReplace
                    .RelativeFilePath = fileToReplace
                    If Not IsExempt(sourceFile, determinedExemption) Then
                        .Operation = FileOperation.Replace
                    Else
                        .Operation = FileOperation.None
                        .Exemption = determinedExemption
                    End If
                End With
                operations.Add(operation)
            Next
        End If

        RaiseEvent SyncTaskMilestoneReached(SyncTaskMilestone.DeterminingFilesToRemove)

        If Me.Options.HasFlag(SyncTaskOptions.RemoveFiles) Then
            If stopRequested Then Return operations
            filesToRemove = relativeTargetFilePaths.Except(relativeSourceFilePaths).OrderByDescending(Function(x) x)
            If stopRequested Then Return operations
            For Each fileToRemove In filesToRemove
                If stopRequested Then Return operations
                targetFile = GetTargetFile(fileToRemove)
                operation = New SyncOperation()
                With operation
                    .TargetFilePath = targetDirectoryPath & Path.DirectorySeparatorChar & fileToRemove
                    .RelativeFilePath = fileToRemove
                    If Not IsExempt(targetFile, determinedExemption) Then
                        .Operation = FileOperation.Remove
                    Else
                        .Operation = FileOperation.None
                        .Exemption = determinedExemption
                    End If
                End With
                operations.Add(operation)
            Next
        End If

        Return operations

    End Function

    Private Function IsExempt(ByVal suspectFile As FileInfo, ByRef determinedExemption As SyncTaskExemption) As Boolean

        Dim suspectFileExtension As String
        Dim suspectFileFolderName As String
        Dim exemptionRegex As Regex

        For Each exemption As SyncTaskExemption In Me.Exemptions
            Try
                If exemption.Operator = ExemptionOperator.Matches Then
                    exemptionRegex = New Regex(exemption.Value)
                End If
            Catch ex As ArgumentException
                Continue For
            End Try
            determinedExemption = exemption
            Select Case exemption.Entity
                Case ExemptionEntity.FileExtension
                    suspectFileExtension = Path.GetExtension(suspectFile.Name).TrimStart(".")
                    If suspectFileExtension IsNot Nothing AndAlso Not suspectFileExtension.Equals(String.Empty) Then
                        Select Case exemption.Operator
                            Case ExemptionOperator.Contains
                                If suspectFileExtension.Contains(exemption.Value) Then
                                    Return True
                                End If
                            Case ExemptionOperator.IsEqualTo
                                If suspectFileExtension.Equals(exemption.Value) Then
                                    Return True
                                End If
                            Case ExemptionOperator.IsNotEqualTo
                                If Not suspectFileExtension.Equals(exemption.Value) Then
                                    Return True
                                End If
                            Case ExemptionOperator.Matches
                                If exemptionRegex.IsMatch(suspectFileExtension) Then
                                    Return True
                                End If
                        End Select
                    End If
                Case ExemptionEntity.FileName
                    Select Case exemption.Operator
                        Case ExemptionOperator.Contains
                            If suspectFile.Name.Contains(exemption.Value) Then
                                Return True
                            End If
                        Case ExemptionOperator.IsEqualTo
                            If suspectFile.Name.Equals(exemption.Value) Then
                                Return True
                            End If
                        Case ExemptionOperator.IsNotEqualTo
                            If Not suspectFile.Name.Equals(exemption.Value) Then
                                Return True
                            End If
                        Case ExemptionOperator.Matches
                            If exemptionRegex.IsMatch(suspectFile.Name) Then
                                Return True
                            End If
                    End Select
                Case ExemptionEntity.FilePath
                    Select Case exemption.Operator
                        Case ExemptionOperator.Contains
                            If suspectFile.FullName.Contains(exemption.Value) Then
                                Return True
                            End If
                        Case ExemptionOperator.IsEqualTo
                            If suspectFile.FullName.Equals(exemption.Value) Then
                                Return True
                            End If
                        Case ExemptionOperator.IsNotEqualTo
                            If Not suspectFile.FullName.Equals(exemption.Value) Then
                                Return True
                            End If
                        Case ExemptionOperator.Matches
                            If exemptionRegex.IsMatch(suspectFile.FullName) Then
                                Return True
                            End If
                    End Select
                Case ExemptionEntity.FileSize
                    Select Case exemption.Operator
                        Case ExemptionOperator.IsEqualTo
                            If (suspectFile.Length / 1024) = Long.Parse(exemption.Value) Then
                                Return True
                            End If
                        Case ExemptionOperator.IsGreaterThan
                            If (suspectFile.Length / 1024) > Long.Parse(exemption.Value) Then
                                Return True
                            End If
                        Case ExemptionOperator.IsLessThan
                            If (suspectFile.Length / 1024) < Long.Parse(exemption.Value) Then
                                Return True
                            End If
                    End Select
                Case ExemptionEntity.FolderName
                    suspectFileFolderName = suspectFile.DirectoryName
                    If suspectFileFolderName.Contains(Path.DirectorySeparatorChar) Then
                        suspectFileFolderName = suspectFileFolderName.Split(Path.DirectorySeparatorChar).Last()
                    End If
                    Select Case exemption.Operator
                        Case ExemptionOperator.Contains
                            If suspectFileFolderName.Contains(exemption.Value) Then
                                Return True
                            End If
                        Case ExemptionOperator.IsEqualTo
                            If suspectFileFolderName.Equals(exemption.Value) Then
                                Return True
                            End If
                        Case ExemptionOperator.IsNotEqualTo
                            If Not suspectFileFolderName.Equals(exemption.Value) Then
                                Return True
                            End If
                        Case ExemptionOperator.Matches
                            If exemptionRegex.IsMatch(suspectFileFolderName) Then
                                Return True
                            End If
                    End Select
                Case ExemptionEntity.FolderPath
                    Select Case exemption.Operator
                        Case ExemptionOperator.Contains
                            If suspectFile.DirectoryName.Contains(exemption.Value) Then
                                Return True
                            End If
                        Case ExemptionOperator.IsEqualTo
                            If suspectFile.DirectoryName.Equals(exemption.Value) Then
                                Return True
                            End If
                        Case ExemptionOperator.IsNotEqualTo
                            If Not suspectFile.DirectoryName.Equals(exemption.Value) Then
                                Return True
                            End If
                        Case ExemptionOperator.Matches
                            If exemptionRegex.IsMatch(suspectFile.DirectoryName) Then
                                Return True
                            End If
                    End Select
            End Select
        Next
        determinedExemption = Nothing
        Return False

    End Function

#End Region

End Class
