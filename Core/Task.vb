Imports System.IO
Imports System.Reflection

<Flags()> _
Public Enum OperationTypes
    Create = 1
    Update = 2
    Delete = 4
End Enum

Public Class Task

    Private _sourceDirectory As DirectoryInfo
    Private _destinationDirectory As DirectoryInfo
    Private _recursive As Boolean
    Private _operations As OperationTypes

    ' ==============
    '  CONSTRUCTORS
    ' ==============

    Public Sub New()

    End Sub

    Public Sub New(ByVal sourceDirectory As DirectoryInfo, _
                   ByVal destinationDirectory As DirectoryInfo, _
                   ByVal recursive As Boolean, _
                   ByVal operations As OperationTypes)

        Me.SourceDirectory = sourceDirectory
        Me.DestinationDirectory = destinationDirectory
        Me.Recursive = recursive
        Me.Operations = operations

    End Sub

    ' ============
    '  PROPERTIES
    ' ============

    Public Property SourceDirectory As DirectoryInfo
        Get
            Return _sourceDirectory
        End Get
        Set(value As DirectoryInfo)
            _sourceDirectory = value
        End Set
    End Property

    Public Property DestinationDirectory As DirectoryInfo
        Get
            Return _destinationDirectory
        End Get
        Set(value As DirectoryInfo)
            _destinationDirectory = value
        End Set
    End Property

    Public Property Recursive As Boolean
        Get
            Return _recursive
        End Get
        Set(value As Boolean)
            _recursive = value
        End Set
    End Property

    Public Property Operations As OperationTypes
        Get
            Return _operations
        End Get
        Set(value As OperationTypes)
            _operations = value
        End Set
    End Property

    ' =========
    '  METHODS
    ' =========

    Public Function Analyze() As AnalyzedTask

        Return New AnalyzedTask(Me)

    End Function

    Public Function Clone() As Task

        Dim clonedTask As New Task
        Dim props As PropertyInfo() = Me.GetType().GetProperties()

        For Each prop As PropertyInfo In props
            prop.SetValue(clonedTask, prop.GetValue(Me, Nothing), Nothing)
        Next

        Return clonedTask

    End Function

End Class
