Public Class Routine

    Private _routineId As Integer
    Private _routineName As String
    Private _routineDescription As String
    Private _createdOn As DateTime
    Private _updatedOn As DateTime
    Private _pendingAction As Action

    Public Sub New()

        Me.PendingAction = Action.Insert

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

    Public Property RoutineDescription As String
        Get
            Return _routineDescription
        End Get
        Set(value As String)
            _routineDescription = value
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

    Public Property PendingAction As Action
        Get
            Return _pendingAction
        End Get
        Set(value As Action)
            _pendingAction = value
        End Set
    End Property

    Public Sub Update()

        Select Case Me.PendingAction
            Case Action.Insert
                Using sqlcnn As New SqlConnection(ConnectionString)
                    Using sqlcmd As New SqlCommand("Insert_Routine", sqlcnn)
                        With sqlcmd
                            .CommandType = CommandType.StoredProcedure
                            .Parameters.Add("@RoutineId", SqlDbType.Int).Direction = ParameterDirection.Output
                            .Parameters.Add("@RoutineName", SqlDbType.VarChar).Value = Me.RoutineName
                            .Parameters.Add("@RoutineDescription", SqlDbType.VarChar).Value = Me.RoutineDescription
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
                            .Parameters.Add("@RoutineDescription", SqlDbType.VarChar).Value = Me.RoutineDescription
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
