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
        public delegate void SyncStatusChangedHandler(string status);

        public event SyncStatusChangedHandler SyncStatusChanged;

        public SyncTask()
        {
            this.Exemptions = new List<SyncTaskExemption>();
        }

#region PROPERTIES

        public string SourceDirectory { get; set; }
        public string TargetDirectory { get; set; }
        public SyncTaskOptions Options { get; set; }
        public List<SyncTaskExemption> Exemptions { get; set; }

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

    private bool IsExempt(FileInfo suspectFile, ref SyncTaskExemption determinedExemption)
    {
        string suspectFileExtension;
        string suspectFileFolderName;
        Regex exemptionRegex;
   
        foreach (SyncTaskExemption exemption in this.Exemptions)
        {
            try
            {
                if (exemption.Operator == ExemptionOperator.Matches)
                {
                    exemptionRegex = new Regex(exemption.Value);
                }
            }
            catch (ArgumentException ex)
            {
                continue;
            }
            determinedExemption = exemption;
            switch (exemption.Entity)
            {
                case ExemptionEntity.FileExtension:
                    suspectFileExtension = Path.GetExtension(suspectFile.Name).TrimStart(".");
                    if (suspectFileExtension != null && !suspectFileExtension.Equals(string.Empty))
                    {
                        switch (exemption.Operator)
                        {
                            case ExemptionOperator.Contains:
                                if (suspectFileExtension.Contains(exemption.Value)) { return true; }
                            case ExemptionOperator.IsEqualTo:
                                if (suspectFileExtension.Equals(exemption.Value)) { return true; }
                            case ExemptionOperator.IsNotEqualTo:
                                if (!suspectFileExtension.Equals(exemption.Value)) { return true; }
                            case ExemptionOperator.Matches:
                                if (exemptionRegex.IsMatch(suspectFileExtension)) { return true; }
                        }
                    }
                case ExemptionEntity.FileName:
                    switch (exemption.Operator)
                    {
                        case ExemptionOperator.Contains:
                            if (suspectFile.Name.Contains(exemption.Value)) { return true; }
                        case ExemptionOperator.IsEqualTo:
                            if (suspectFile.Name.Equals(exemption.Value)) { return true; }
                        case ExemptionOperator.IsNotEqualTo:
                            if (!suspectFile.Name.Equals(exemption.Value)) { return true; }
                        case ExemptionOperator.Matches:
                            if (exemptionRegex.IsMatch(suspectFile.Name)) { return true; }
                    }
                case ExemptionEntity.FilePath:
                    switch (exemption.Operator)
                    {
                        case ExemptionOperator.Contains:
                            if (suspectFile.FullName.Contains(exemption.Value)) { return true; }
                        case ExemptionOperator.IsEqualTo:
                            if (suspectFile.FullName.Equals(exemption.Value)) { return true; }
                        case ExemptionOperator.IsNotEqualTo:
                            if (!suspectFile.FullName.Equals(exemption.Value)) { return true; }
                        case ExemptionOperator.Matches:
                            if (exemptionRegex.IsMatch(suspectFile.FullName)) { return true; }
                    }
                case ExemptionEntity.FileSize:
                    switch (exemption.Operator)
                    {
                        case ExemptionOperator.IsEqualTo:
                            if (long.Parse(suspectFile.Length / 1024) == long.Parse(exemption.Value)) { return true; }
                        case ExemptionOperator.IsGreaterThan:
                            if (long.Parse(suspectFile.Length / 1024) > long.Parse(exemption.Value)) { return true; }
                        case ExemptionOperator.IsLessThan:
                            if (long.Parse(suspectFile.Length / 1024) < long.Parse(exemption.Value)) { return true; }
                    }
                case ExemptionEntity.FolderName:
                    suspectFileFolderName = suspectFile.DirectoryName;
                    if (suspectFileFolderName.Contains(Path.DirectorySeparatorChar))
                    {
                        suspectFileFolderName = suspectFileFolderName.Split(Path.DirectorySeparatorChar).Last();
                    }
                    switch (exemption.Operator)
                    {
                        case ExemptionOperator.Contains:
                            if (suspectFileFolderName.Contains(exemption.Value)) { return true; }
                        case ExemptionOperator.IsEqualTo:
                            if (suspectFileFolderName.Equals(exemption.Value)) { return true; }
                        case ExemptionOperator.IsNotEqualTo:
                            if (!suspectFileFolderName.Equals(exemption.Value)) { return true; }
                        case ExemptionOperator.Matches:
                            if (exemptionRegex.IsMatch(suspectFileFolderName)) { return true; }
                    }
                case ExemptionEntity.FolderPath:
                    switch (exemption.Operator)
                    {
                        case ExemptionOperator.Contains:
                            if (suspectFile.DirectoryName.Contains(exemption.Value)) { return true; }
                        case ExemptionOperator.IsEqualTo:
                            if (suspectFile.DirectoryName.Equals(exemption.Value)) { return true; }
                        case ExemptionOperator.IsNotEqualTo:
                            if (!suspectFile.DirectoryName.Equals(exemption.Value)) { return true; }
                        case ExemptionOperator.Matches:
                            if (exemptionRegex.IsMatch(suspectFile.DirectoryName)) { return true; }
                    }
            }
            determinedExemption = null;
            return false;
        }
    }

#endregion

    }
}
