Public Class Exemption

    Private _entity As ExemptionEntity
    Private _operator As ExemptionOperator
    Private _value As String

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

End Class
