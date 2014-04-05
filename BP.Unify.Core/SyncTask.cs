using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BP.Unify.Core
{
    [Flags]
    public enum SyncTaskOptions
    {
        AddFiles = 1,
        ReplaceFiles = 2,
        RemoveFiles = 4,
        ExcludeSubdirectories = 8,
        ExcludeHiddenFiles = 16,
        CompareFilesInDepth = 32
    }

    [Serializable]
    class SyncTask
    {
        private string _sourceDirectory;
        private string _targetDirectory;
        private SyncTaskOptions _options;
        private List<SyncTaskExemption> _exemptions;

        public event SyncStatusChanged(String status);

    Public Event SyncStatusChanged


        public SyncTask()
        {
            this.Exemptions = new List<SyncTaskExemption>();
        }

#region PROPERTIES

        public string SourceDirectory
        {
            get { return _sourceDirectory; }
            set { _sourceDirectory = value; }
        }

        public string TargetDirectory
        {
            get { return _targetDirectory; }
            set { _targetDirectory = value; }
        }

        public SyncTaskOptions Options
        {
            get { return _options; }
            set { _options = value; }
        }

        public List<SyncTaskExemption> Exemptions
        {
            get { return _exemptions; }
            set { _exemptions = value; }
        }

#endregion

#region METHODS

        private static bool ByteArraysAreEqual(byte[] firstArray, byte[] secondArray)
        {
            if (firstArray is secondArray)
            {
                return true;
            }
            if (firstArray == null || secondArray == null)
            {
                return false;
            }
            if (firstArray.Length != secondArray.Length)
            {
                return false;
            }
            for (int i = 0; i <= firstArray.Length - 1; i++)
            {
                if (firstArray[i] != secondArray[i])
                {
                    return false;
                }
            }
            return true;
        }

        public SyncTask Clone()
        {
            SyncTask item = new SyncTask();
            item.SourceDirectory = this.SourceDirectory;
            item.TargetDirectory = this.TargetDirectory;
            item.Options = this.Options;
            item.Exemptions = this.Exemptions;
            return item;
        }

        private static bool FilesAreEqual(String firstFilePath, String secondFilePath, Boolean compareFilesInDepth)
        {
            return FilesAreEqual(new FileInfo(firstFilePath), new FileInfo(secondFilePath), compareFilesInDepth);
        }

        private static bool FilesAreEqual(FileInfo firstFile, FileInfo secondFile, Boolean compareFilesInDepth)
        {
            if (firstFile.Length == secondFile.Length)
            {
                if (firstFile.LastWriteTime == secondFile.LastWriteTime)
                {
                    return true;
                }
                else
                {
                    if (compareFilesInDepth)
                    {
                        if (ByteArraysAreEqual(GetHash(firstFile.FullName), GetHash(secondFile.FullName)))
                        {
                            return true;
                        }
                        else 
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }
        }

        /*

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
        */

    private static byte[] GetHash(String filePath)
    {
        using (MD5 algorithm = MD5.Create())
        {
            using (FileStream stream = File.OpenRead(filePath))
            {
                return algorithm.ComputeHash(stream);
            }
    
        }
    }

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

        recursive = Not Me.Options.HasFlag(SyncTaskOptions.ExcludeSubdirectories)

        If stopRequested Then Return operations

        RaiseEvent SyncStatusChanged("Determining source files...")
        sourceFiles = sourceDirectoryInfo.GetFiles("*", IIf(recursive, SearchOption.AllDirectories, SearchOption.TopDirectoryOnly))
        For Each f As FileInfo In sourceFiles
            If Not Me.Options.HasFlag(SyncTaskOptions.ExcludeHiddenFiles) OrElse Not f.Attributes.HasFlag(FileAttributes.Hidden) Then
                relativeSourceFilePaths.Add(f.FullName.Replace(sourceDirectoryPath & Path.DirectorySeparatorChar, Space(0)))
            End If
        Next

        If stopRequested Then Return operations

        RaiseEvent SyncStatusChanged("Determining target files...")
        targetFiles = targetDirectoryInfo.GetFiles("*", IIf(recursive, SearchOption.AllDirectories, SearchOption.TopDirectoryOnly))
        For Each f As FileInfo In targetFiles
            If Not Me.Options.HasFlag(SyncTaskOptions.ExcludeHiddenFiles) OrElse Not f.Attributes.HasFlag(FileAttributes.Hidden) Then
                relativeTargetFilePaths.Add(f.FullName.Replace(targetDirectoryPath & Path.DirectorySeparatorChar, Space(0)))
            End If
        Next

        If stopRequested Then Return operations

        RaiseEvent SyncStatusChanged("Determining files to add...")

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

        RaiseEvent SyncStatusChanged("Determining files to replace...")

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

        RaiseEvent SyncStatusChanged("Determining files to remove...")

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

    Public Sub Sync()



    End Sub

#endregion

    }
}
