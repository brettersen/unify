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
End Enum

<Serializable>
Public Class SyncTask

    Private _sourceDirectory As String
    Private _targetDirectory As String
    Private _options As SyncTaskOptions
    Private _exemptions As List(Of SyncTaskExemption)

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

    Public Function GetOperations() As List(Of SyncOperation)

        Dim sourceDirectoryPath, targetDirectoryPath As String
        Dim sourceDirectoryInfo, targetDirectoryInfo As DirectoryInfo
        Dim sourceFiles, targetFiles As FileInfo()
        Dim sourceRelativeFilePaths, targetRelativeFilePaths As New List(Of String)
        Dim filesToAdd, filesToReplace, filesToRemove, filesToIgnore As IEnumerable(Of String)
        Dim recursive As Boolean
        Dim operation As SyncOperation
        Dim operations As New List(Of SyncOperation)
        Dim sourceFile As FileInfo

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

        sourceFiles = sourceDirectoryInfo.GetFiles("*", IIf(recursive, SearchOption.AllDirectories, SearchOption.TopDirectoryOnly))
        For Each f As FileInfo In sourceFiles
            sourceRelativeFilePaths.Add(f.FullName.Replace(sourceDirectoryPath & Path.DirectorySeparatorChar, Space(0)))
        Next

        targetFiles = targetDirectoryInfo.GetFiles("*", IIf(recursive, SearchOption.AllDirectories, SearchOption.TopDirectoryOnly))
        For Each f As FileInfo In targetFiles
            targetRelativeFilePaths.Add(f.FullName.Replace(targetDirectoryPath & Path.DirectorySeparatorChar, Space(0)))
        Next

        If Me.Options.HasFlag(SyncTaskOptions.AddFiles) Then
            filesToAdd = sourceRelativeFilePaths.Except(targetRelativeFilePaths)
            For Each fileToAdd In filesToAdd
                sourceFile = GetSourceFile(fileToAdd)
                If Not Me.Options.HasFlag(SyncTaskOptions.ExcludeHiddenFiles) OrElse _
                    Not sourceFile.Attributes.HasFlag(FileAttributes.Hidden) Then
                    operation = New SyncOperation()
                    With operation
                        .SourceFilePath = sourceDirectoryPath & Path.DirectorySeparatorChar & fileToAdd
                        .TargetFilePath = targetDirectoryPath & Path.DirectorySeparatorChar & fileToAdd
                        .Attributes = .Attributes
                        .Operation = FileOperation.Add
                        .IsExempt = IsExempt(sourceFile)
                    End With
                    operations.Add(operation)
                End If
            Next
        End If

        If Me.Options.HasFlag(SyncTaskOptions.ReplaceFiles) Then
            filesToIgnore = sourceRelativeFilePaths.Intersect(targetRelativeFilePaths)
            filesToReplace = (From matchedFile In filesToIgnore
                              Where Not FilesAreEqual(GetSourceFile(matchedFile), GetTargetFile(matchedFile))
                              Select matchedFile)
        End If

        If Me.Options.HasFlag(SyncTaskOptions.RemoveFiles) Then
            filesToRemove = targetRelativeFilePaths.Except(sourceRelativeFilePaths)
        End If

        Return operations

    End Function

    Private Shared Function FilesAreEqual(ByVal firstFilePath As String, ByVal secondFilePath As String) As Boolean
        Return FilesAreEqual(New FileInfo(firstFilePath), New FileInfo(secondFilePath))
    End Function

    Private Shared Function FilesAreEqual(ByVal firstFile As FileInfo, ByVal secondFile As FileInfo) As Boolean
        If firstFile.Length = secondFile.Length Then
            If firstFile.LastWriteTime = secondFile.LastWriteTime Then
                If ByteArraysAreEqual(GetHash(firstFile.FullName), GetHash(secondFile.FullName)) Then
                    Return True
                Else
                    Return False
                End If
            Else
                Return False
            End If
        Else
            Return False
        End If
    End Function

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

    Private Shared Function GetHash(ByVal filePath As String) As Byte()
        Using algorithm = MD5.Create()
            Using [fileStream] = File.OpenRead(filePath)
                Return algorithm.ComputeHash([fileStream])
            End Using
        End Using
    End Function

    Private Function IsExempt(ByVal suspectFile As FileInfo) As Boolean

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
                            If suspectFile.Length = Long.Parse(exemption.Value) Then
                                Return True
                            End If
                        Case ExemptionOperator.IsGreaterThan
                            If suspectFile.Length > Long.Parse(exemption.Value) Then
                                Return True
                            End If
                        Case ExemptionOperator.IsLessThan
                            If suspectFile.Length < Long.Parse(exemption.Value) Then
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

        Return False

    End Function

#End Region

End Class
