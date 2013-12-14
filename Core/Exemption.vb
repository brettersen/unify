Public Class Exemption

    Friend _exemptionId As Integer
    Friend _taskId As Integer
    Friend _exemptionIndex As Integer
    Friend _exemptionEntityId As Integer
    Friend _exemptionOperatorId As Integer
    Friend _exemptionValue As String
    Friend _pendingAction As Action

    Public Sub New()

        Me.PendingAction = Action.None

    End Sub

    Public ReadOnly Property ExemptionId As Integer
        Get
            Return _exemptionId
        End Get
    End Property

    Public Property TaskId As Integer
        Get
            Return _taskId
        End Get
        Set(value As Integer)
            _taskId = value
        End Set
    End Property

    Public Property ExemptionIndex As Integer
        Get
            Return _exemptionIndex
        End Get
        Set(value As Integer)
            _exemptionIndex = value
        End Set
    End Property

    Public Property ExemptionEntityId As Integer
        Get
            Return _exemptionEntityId
        End Get
        Set(value As Integer)
            _exemptionEntityId = value
        End Set
    End Property

    Public Property ExemptionOperatorId As Integer
        Get
            Return _exemptionOperatorId
        End Get
        Set(value As Integer)
            _exemptionOperatorId = value
        End Set
    End Property

    Public Property ExemptionValue As String
        Get
            Return _exemptionValue
        End Get
        Set(value As String)
            _exemptionValue = value
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

    Public Sub Update()

        Select Case Me.PendingAction
            Case Action.Insert
            Case Action.Update
            Case Action.Delete
        End Select

        Me.PendingAction = Action.None

    End Sub

End Class
