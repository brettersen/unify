Public Class RoutineList
    Inherits Collection(Of Routine)

    Public Shared Function GetRoutineList() As RoutineList

        Using sqlcnn As New SqlConnection(Common.ConnectionString)
            Using sqlcmd As New SqlCommand("Select_RoutineList", sqlcnn)
                With sqlcmd
                    .CommandType = CommandType.StoredProcedure
                    .Connection.Open()
                    Using sqlrdr As SqlDataReader = .ExecuteReader()
                        Return ParseResultSet(sqlrdr)
                    End Using
                End With
            End Using
        End Using

    End Function

    Public Shared Function ParseResultSet(ByRef resultSet As SqlDataReader) As RoutineList

        Dim result As Routine
        Dim results As New RoutineList()

        If resultSet.HasRows Then
            While resultSet.Read()
                result = New Routine()
                With result
                    ._routineId = Integer.Parse(resultSet("RoutineId"))
                    ._routineName = resultSet("RoutineName").ToString()
                    ._createdOn = DateTime.Parse(resultSet("CreatedOn"))
                    ._updatedOn = DateTime.Parse(resultSet("UpdatedOn"))
                    ._pendingAction = Action.None
                End With
                results.Add(result)
            End While
        End If

        Return results

    End Function

End Class
