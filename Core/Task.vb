Imports System.Reflection

Public Class Task

    Friend _taskId As Integer
    Friend _routineId As Integer
    Friend _taskIndex As Integer
    Friend _sourceDirectory As String
    Friend _destinationDirectory As String
    Friend _addFiles As Boolean
    Friend _replaceFiles As Boolean
    Friend _removeFiles As Boolean
    Friend _searchRecursively As Boolean
    Friend _excludedHiddenFiles As Boolean
    Friend _exemptions As Collection(Of Exemption)
    Friend _pendingAction As Action

    ' ==============
    '  CONSTRUCTORS
    ' ==============

    Public Sub New()

        Me.PendingAction = Action.None
        Me.Exemptions = New Collection(Of Exemption)

    End Sub

    ' ============
    '  PROPERTIES
    ' ============

    Public ReadOnly Property TaskId As Integer
        Get
            Return _taskId
        End Get
    End Property

    Public Property RoutineId As Integer
        Get
            Return _routineId
        End Get
        Set(value As Integer)
            value = _routineId
        End Set
    End Property

    Public Property TaskIndex As Integer
        Get
            Return _taskIndex
        End Get
        Set(value As Integer)
            value = _taskIndex
        End Set
    End Property

    Public Property SourceDirectory As String
        Get
            Return _sourceDirectory
        End Get
        Set(value As String)
            _sourceDirectory = value
        End Set
    End Property

    Public Property DestinationDirectory As String
        Get
            Return _destinationDirectory
        End Get
        Set(value As String)
            _destinationDirectory = value
        End Set
    End Property

    Public Property AddFiles As Boolean
        Get
            Return _addFiles
        End Get
        Set(value As Boolean)
            _addFiles = value
        End Set
    End Property

    Public Property ReplaceFiles As Boolean
        Get
            Return _replaceFiles
        End Get
        Set(value As Boolean)
            _replaceFiles = value
        End Set
    End Property

    Public Property RemoveFiles As Boolean
        Get
            Return _removeFiles
        End Get
        Set(value As Boolean)
            _removeFiles = value
        End Set
    End Property

    Public Property SearchRecursively As Boolean
        Get
            Return _searchRecursively
        End Get
        Set(value As Boolean)
            _searchRecursively = value
        End Set
    End Property

    Public Property ExcludeHiddenFiles As Boolean
        Get
            Return _excludedHiddenFiles
        End Get
        Set(value As Boolean)
            _excludedHiddenFiles = value
        End Set
    End Property

    Public Property Exemptions As Collection(Of Exemption)
        Get
            Return _exemptions
        End Get
        Set(value As Collection(Of Exemption))
            _exemptions = value
        End Set
    End Property

    Public Property PendingAction As Action
        Get
            Return _pendingAction
        End Get
        Set(value As Action)
            _pendingAction = value
        End Set
    End Property

    ' =========
    '  METHODS
    ' =========

    'Public Function Analyze() As AnalyzedTask

    '    Return New AnalyzedTask(Me)

    'End Function

    Public Function Clone() As Task

        Dim clonedTask As New Task
        Dim props As PropertyInfo() = Me.GetType().GetProperties()

        For Each prop As PropertyInfo In props
            prop.SetValue(clonedTask, prop.GetValue(Me, Nothing), Nothing)
        Next

        Return clonedTask

    End Function

    Public Sub Update()

        Select Case Me.PendingAction
            Case Action.Insert
            Case Action.Update
            Case Action.Delete
        End Select

        Me.PendingAction = Action.None

    End Sub

End Class
