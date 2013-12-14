Public Class Routine

    Friend _routineId As Integer
    Friend _routineName As String
    Friend _createdOn As DateTime
    Friend _updatedOn As DateTime
    Friend _tasks As Collection(Of Task)
    Friend _pendingAction As Action

    Public Sub New()

        Me.PendingAction = Action.None
        Me.Tasks = New Collection(Of Task)

    End Sub

    Public ReadOnly Property RoutineId As Integer
        Get
            Return _routineId
        End Get
    End Property

    Public Property RoutineName As String
        Get
            Return _routineName
        End Get
        Set(value As String)
            _routineName = value
        End Set
    End Property

    Public ReadOnly Property CreatedOn As DateTime
        Get
            Return _createdOn
        End Get
    End Property

    Public ReadOnly Property UpdatedOn As DateTime
        Get
            Return _updatedOn
        End Get
    End Property

    Public Property Tasks As Collection(Of Task)
        Get
            Return _tasks
        End Get
        Set(value As Collection(Of Task))
            _tasks = value
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

    Public ReadOnly Property HasPendingActions As Boolean
        Get
            If Me.PendingAction <> Action.None Then
                Return True
            End If
            For Each t In Me.Tasks
                If t.PendingAction <> Action.None Then
                    Return True
                End If
                For Each e In t.Exemptions
                    If e.PendingAction <> Action.None Then
                        Return True
                    End If
                Next
            Next
            Return False
        End Get
    End Property

    Public Sub Save()

        Me.Update()
        For Each t In Me.Tasks

        Next

    End Sub

    Public Sub Update()

        Select Case Me.PendingAction
            Case Action.Insert
                Using sqlcnn As New SqlConnection(ConnectionString)
                    Using sqlcmd As New SqlCommand("Insert_Routine", sqlcnn)
                        With sqlcmd
                            .CommandType = CommandType.StoredProcedure
                            .Parameters.Add("@RoutineId", SqlDbType.Int).Direction = ParameterDirection.Output
                            .Parameters.Add("@RoutineName", SqlDbType.VarChar).Value = Me.RoutineName
                            .Parameters.Add("@CreatedOn", SqlDbType.DateTime2).Direction = ParameterDirection.Output
                            .Parameters.Add("@UpdatedOn", SqlDbType.DateTime2).Direction = ParameterDirection.Output
                            .Connection.Open()
                            .ExecuteNonQuery()
                            Me._routineId = Integer.Parse(.Parameters("@RoutineId").Value)
                            Me._createdOn = DateTime.Parse(.Parameters("@CreatedOn").Value)
                            Me._updatedOn = DateTime.Parse(.Parameters("@UpdatedOn").Value)
                        End With
                    End Using
                End Using
            Case Action.Update
                Using sqlcnn As New SqlConnection(ConnectionString)
                    Using sqlcmd As New SqlCommand("Update_Routine", sqlcnn)
                        With sqlcmd
                            .CommandType = CommandType.StoredProcedure
                            .Parameters.Add("@RoutineId", SqlDbType.Int).Value = Me.RoutineId
                            .Parameters.Add("@RoutineName", SqlDbType.VarChar).Value = Me.RoutineName
                            .Parameters.Add("@UpdatedOn", SqlDbType.DateTime2).Direction = ParameterDirection.Output
                            .Connection.Open()
                            .ExecuteNonQuery()
                            Me._updatedOn = DateTime.Parse(.Parameters("@UpdatedOn").Value)
                        End With
                    End Using
                End Using
            Case Action.Delete
                Using sqlcnn As New SqlConnection(ConnectionString)
                    Using sqlcmd As New SqlCommand("Delete_Routine", sqlcnn)
                        With sqlcmd
                            .CommandType = CommandType.StoredProcedure
                            .Parameters.Add("@RoutineId", SqlDbType.Int).Value = Me.RoutineId
                            .Connection.Open()
                            .ExecuteNonQuery()
                        End With
                    End Using
                End Using
        End Select

        Me.PendingAction = Action.None

    End Sub

End Class
