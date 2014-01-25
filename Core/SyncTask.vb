Imports System.IO

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
    Private _exemptions As Collection(Of SyncTaskExemption)

    Public Sub New()
        Me._exemptions = New Collection(Of SyncTaskExemption)
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

    Public ReadOnly Property SourceDirectoryInfo() As DirectoryInfo
        Get
            Return New DirectoryInfo(_sourceDirectory)
        End Get
    End Property

    Public Property TargetDirectory As String
        Get
            Return _targetDirectory
        End Get
        Set(value As String)
            _targetDirectory = value
        End Set
    End Property

    Public ReadOnly Property TargetDirectoryInfo() As DirectoryInfo
        Get
            Return New DirectoryInfo(_targetDirectory)
        End Get
    End Property

    Public Property Options As SyncTaskOptions
        Get
            Return _options
        End Get
        Set(value As SyncTaskOptions)
            _options = value
        End Set
    End Property

    Public Property Exemptions As Collection(Of SyncTaskExemption)
        Get
            Return _exemptions
        End Get
        Set(value As Collection(Of SyncTaskExemption))
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
            .Exemptions = Me.Exemptions
        End With
        Return item
    End Function

#End Region

End Class
