<Serializable>
Public Class SyncTaskExemption

    Private _entity As ExemptionEntity
    Private _operator As ExemptionOperator
    Private _value As String

    Public Sub New()

    End Sub

    Public Sub New(ByVal entity As ExemptionEntity, _
                   ByVal [operator] As ExemptionOperator, _
                   ByVal value As String)
        Me.Entity = entity
        Me.Operator = [operator]
        Me.Value = value
    End Sub

#Region "PROPERTIES"

    Public Property Entity As ExemptionEntity
        Get
            Return _entity
        End Get
        Set(value As ExemptionEntity)
            _entity = value
        End Set
    End Property

    Public Property [Operator] As ExemptionOperator
        Get
            Return _operator
        End Get
        Set(value As ExemptionOperator)
            _operator = value
        End Set
    End Property

    Public Property Value As String
        Get
            Return _value
        End Get
        Set(value As String)
            _value = value
        End Set
    End Property

#End Region

#Region "METHODS"

    Public Function Clone() As SyncTaskExemption
        Dim item As New SyncTaskExemption
        With item
            .Entity = Me.Entity
            .Operator = Me.Operator
            .Value = Me.Value
        End With
        Return item
    End Function

#End Region

End Class
