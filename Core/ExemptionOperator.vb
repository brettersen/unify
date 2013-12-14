Public Class ExemptionOperator

    Friend _exemptionOperatorId As Integer
    Friend _operatorName As String

    Private Sub New()

    End Sub

    Public ReadOnly Property ExemptionOperatorId As Integer
        Get
            Return _exemptionOperatorId
        End Get
    End Property

    Public Property OperatorName As String
        Get
            Return _operatorName
        End Get
        Set(value As String)
            _operatorName = value
        End Set
    End Property

    Public Shared Function GetOperators() As List(Of ExemptionOperator)

        Dim result As ExemptionOperator
        Dim results As New List(Of ExemptionOperator)

        Using sqlcnn As New SqlConnection(Common.ConnectionString)
            Using sqlcmd As New SqlCommand("Select_ExemptionOperatorList", sqlcnn)
                With sqlcmd
                    .CommandType = CommandType.StoredProcedure
                    .Connection.Open()
                    Using sqlrdr As SqlDataReader = .ExecuteReader()
                        If sqlrdr.HasRows Then
                            While sqlrdr.Read()
                                result = New ExemptionOperator()
                                result._exemptionOperatorId = Integer.Parse(sqlrdr("ExemptionOperatorId"))
                                result._operatorName = sqlrdr("OperatorName").ToString()
                                results.Add(result)
                            End While
                        End If
                    End Using
                End With
            End Using
        End Using

        Return results

    End Function

    Public Shared Function GetOperatorsByEntityId(ByVal exemptionEntityId As Integer) As List(Of ExemptionOperator)

        Dim result As ExemptionOperator
        Dim results As New List(Of ExemptionOperator)

        Using sqlcnn As New SqlConnection(Common.ConnectionString)
            Using sqlcmd As New SqlCommand("Select_ExemptionOperatorListByEntityId", sqlcnn)
                With sqlcmd
                    .CommandType = CommandType.StoredProcedure
                    .Parameters.Add("@ExemptionEntityId", SqlDbType.Int).Value = exemptionEntityId
                    .Connection.Open()
                    Using sqlrdr As SqlDataReader = .ExecuteReader()
                        If sqlrdr.HasRows Then
                            While sqlrdr.Read()
                                result = New ExemptionOperator()
                                result._exemptionOperatorId = Integer.Parse(sqlrdr("ExemptionOperatorId"))
                                result._operatorName = sqlrdr("OperatorName").ToString()
                                results.Add(result)
                            End While
                        End If
                    End Using
                End With
            End Using
        End Using

        Return results

    End Function

End Class
