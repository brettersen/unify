Imports System.Collections.Concurrent
Imports System.IO

Public Class AnalyzedTask

    Private _task As Task
    Private _pendingActions As ConcurrentQueue(Of Action)
    Private _sourceFiles As IEnumerable(Of FileInfo)
    Private _destinationFiles As IEnumerable(Of FileInfo)
    Private _sourcePaths As IEnumerable(Of String)
    Private _destinationPaths As IEnumerable(Of String)

    ' ==============
    '  CONSTRUCTORS
    ' ==============

    Friend Sub New(ByRef t As Task)

        Me.Task = t
        Me.PendingActions = New ConcurrentQueue(Of Action)

        Analyze()

    End Sub

    ' ============
    '  PROPERTIES
    ' ============

    Public Property Task() As Task
        Get
            Return _task
        End Get
        Set(value As Task)
            _task = value
        End Set
    End Property

    Public Property PendingActions() As ConcurrentQueue(Of Action)
        Get
            Return _pendingActions
        End Get
        Set(value As ConcurrentQueue(Of Action))
            _pendingActions = value
        End Set
    End Property

    Public Property SourceFiles() As IEnumerable(Of FileInfo)
        Get
            Return _sourceFiles
        End Get
        Private Set(ByVal value As IEnumerable(Of FileInfo))
            _sourceFiles = value
        End Set
    End Property

    Public Property DestinationFiles() As IEnumerable(Of FileInfo)
        Get
            Return _destinationFiles
        End Get
        Private Set(ByVal value As IEnumerable(Of FileInfo))
            _destinationFiles = value
        End Set
    End Property

    Public Property SourcePaths() As IEnumerable(Of String)
        Get
            Return _sourcePaths
        End Get
        Private Set(ByVal value As IEnumerable(Of String))
            _sourcePaths = value
        End Set
    End Property

    Public Property DestinationPaths() As IEnumerable(Of String)
        Get
            Return _destinationPaths
        End Get
        Private Set(ByVal value As IEnumerable(Of String))
            _destinationPaths = value
        End Set
    End Property

    Public ReadOnly Property NewPaths() As IEnumerable(Of String)
        Get
            Return Me.SourcePaths.Except(Me.DestinationPaths)
        End Get
    End Property

    Public ReadOnly Property OldPaths() As IEnumerable(Of String)
        Get
            Return Me.DestinationPaths.Except(Me.SourcePaths)
        End Get
    End Property

    Public ReadOnly Property ExistingPaths() As IEnumerable(Of String)
        Get
            Return Me.SourcePaths.Intersect(Me.DestinationPaths)
        End Get
    End Property

    ' =========
    '  METHODS
    ' =========

    Private Sub Analyze()

        Dim actions As New List(Of Action)
        Dim sourceDirectory, destinationDirectory As DirectoryInfo

        Dim getFiles = Function(directory As DirectoryInfo) As IEnumerable(Of FileInfo)
                           Return (From file As FileInfo In directory.EnumerateFiles("*", IIf(Me.Task.Recursive, SearchOption.AllDirectories, SearchOption.TopDirectoryOnly))
                                   Where Not file.Attributes.HasFlag(FileAttributes.Directory)
                                   Select file)
                       End Function

        Dim determineRelativePaths = Function(files As IEnumerable(Of FileInfo), parentDirectory As DirectoryInfo) As IEnumerable(Of String)
                                         Return (From f In files
                                                 Select f.FullName.Replace(parentDirectory.FullName, Space(0)))
                                     End Function

        sourceDirectory = Me.Task.SourceDirectory
        destinationDirectory = Me.Task.DestinationDirectory

        If Not sourceDirectory.Exists OrElse Not sourceDirectory.Attributes.HasFlag(FileAttributes.Directory) Then
            Throw New DirectoryNotFoundException("The specified source directory could not be found or is not a directory.")
        End If

        If destinationDirectory.Exists AndAlso Not destinationDirectory.Attributes.HasFlag(FileAttributes.Directory) Then
            Throw New DirectoryNotFoundException("The specifed destination directory could not be found or is not a directory.")
        Else
            Directory.CreateDirectory(destinationDirectory.FullName)
        End If

        Me.SourceFiles = getFiles(sourceDirectory)
        Me.DestinationFiles = getFiles(destinationDirectory)

        Me.SourcePaths = determineRelativePaths(Me.SourceFiles, Me.Task.SourceDirectory)
        Me.DestinationPaths = determineRelativePaths(Me.DestinationFiles, Me.Task.DestinationDirectory)

        For Each p In Me.NewPaths.AsParallel
            Dim a = New Action(Me.Task.SourceDirectory.FullName & p, _
                               Me.Task.DestinationDirectory.FullName & p, _
                               File.GetAttributes(Me.Task.SourceDirectory.FullName & p), _
                               OperationType.Create)
            Me.PendingActions.Enqueue(a)
        Next

        For Each p In Me.ExistingPaths.AsParallel
            Dim sf = (From f In Me.SourceFiles
                      Where f.FullName.Replace(Me.Task.SourceDirectory.FullName, Space(0)).Equals(p)
                      Select f).First()
            Dim df = (From f In Me.DestinationFiles
                      Where f.FullName.Replace(Me.Task.DestinationDirectory.FullName, Space(0)).Equals(p)
                      Select f).First()
            If Not AreIdentical(sf, df) Then
                Me.PendingActions.Enqueue(New Action(sf.FullName, df.FullName, sf.Attributes, OperationType.Update))
            End If
        Next

        For Each p In Me.OldPaths.AsParallel
            Dim dp = Me.Task.DestinationDirectory.FullName & p
            Dim df = (From f In Me.DestinationFiles
                      Where f.FullName = dp
                      Select f).First()
            Me.PendingActions.Enqueue(New Action(Nothing, df.FullName, df.Attributes, OperationType.Delete))
        Next

    End Sub

    Private Shared Function AreIdentical(file1 As FileInfo, file2 As FileInfo) As Boolean

        If file1.Length = file2.Length Then
            If file1.LastWriteTime = file2.LastWriteTime Then
                Return True
            Else
                Return CompareBytes(file1, file2)
            End If
        Else
            Return False
        End If

    End Function

    Private Shared Function CompareBytes(file1 As FileInfo, file2 As FileInfo) As Boolean

        Dim file1Byte, file2Byte As Integer

        Using stream1 As New FileStream(file1.FullName, FileMode.Open, FileAccess.Read)
            Using stream2 As New FileStream(file2.FullName, FileMode.Open, FileAccess.Read)
                Do
                    file1Byte = stream1.ReadByte()
                    file2Byte = stream2.ReadByte()
                Loop While (file1Byte = file2Byte AndAlso file1Byte <> -1)
            End Using
        End Using

        Return (file1Byte = file2Byte)

    End Function

End Class
