Public Class frmExemption

#Region "PROPERTIES"

    Public ReadOnly Property Exemptions As Collection(Of Exemption)
        Get
            Return Me.PreviousForm.Task.Exemptions
        End Get
    End Property

    Public Property PreviousForm As frmTask

#End Region

    Private Sub frmExemption_Load(sender As Object, e As EventArgs) Handles Me.Load

        With cboEntity
            .DataSource = ExemptionEntity.GetEntities()
            .DisplayMember = "EntityName"
            .ValueMember = "ExemptionEntityId"
        End With

    End Sub

    Private Sub cboEntity_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboEntity.SelectedIndexChanged

        If cboEntity.SelectedIndex > 0 Then
            With cboOperator
                .DataSource = ExemptionOperator.GetOperatorsByEntityId(CInt(cboEntity.SelectedValue))
                .DisplayMember = "OperatorName"
                .ValueMember = "ExemptionOperatorId"
            End With
        End If

    End Sub

    Private Sub FormatDataGridView()

        With dgvExemption

        End With

    End Sub

End Class