Imports System.IO

Public Class Routine

    Friend _name As String
    Friend _tasks As Collection(Of SyncTask)

    Public Sub New()
        Me._tasks = New Collection(Of SyncTask)
    End Sub

#Region "PROPERTIES"

    Public Property Name As String
        Get
            Return _name
        End Get
        Set(value As String)
            _name = value
        End Set
    End Property

    Public ReadOnly Property FilePath As String
        Get

        End Get
    End Property

    Public Property Tasks As Collection(Of SyncTask)
        Get
            Return _tasks
        End Get
        Set(value As Collection(Of SyncTask))
            _tasks = value
        End Set
    End Property

#End Region

#Region "METHODS"

    Public Shared Function GetRoutineNames(ByVal saveDirectory As String, ByVal fileExtension As String) As List(Of String)
        Return From f In New DirectoryInfo(saveDirectory).GetFiles("*." & fileExtension, SearchOption.TopDirectoryOnly)
               Order By f.Name Ascending
               Select f.Name
    End Function

    Public Sub Save()

    End Sub

#End Region

End Class
