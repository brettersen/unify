Public Class ExemptionEntity

    Friend _exemptionEntityId As Integer
    Friend _entityName As String

    Private Sub New()

    End Sub

    Public ReadOnly Property ExemptionEntityId As Integer
        Get
            Return _exemptionEntityId
        End Get
    End Property

    Public Property EntityName As String
        Get
            Return _entityName
        End Get
        Set(value As String)
            _entityName = value
        End Set
    End Property

    Public Shared Function GetEntities() As List(Of ExemptionEntity)

        Dim result As ExemptionEntity
        Dim results As New List(Of ExemptionEntity)

        Using sqlcnn As New SqlConnection(Common.ConnectionString)
            Using sqlcmd As New SqlCommand("Select_ExemptionEntityList", sqlcnn)
                With sqlcmd
                    .CommandType = CommandType.StoredProcedure
                    .Connection.Open()
                    Using sqlrdr As SqlDataReader = .ExecuteReader()
                        If sqlrdr.HasRows Then
                            While sqlrdr.Read()
                                result = New ExemptionEntity()
                                result._exemptionEntityId = Integer.Parse(sqlrdr("ExemptionEntityId"))
                                result._entityName = sqlrdr("EntityName").ToString()
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
