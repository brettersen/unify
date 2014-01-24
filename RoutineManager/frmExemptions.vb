Public Class frmExemptions

#Region "PROPERTIES"

    Public ReadOnly Property Exemptions As Collection(Of Exemption)
        Get
            Return Me.PreviousForm.Task.Exemptions
        End Get
    End Property

    Public Property PreviousForm As frmTask

#End Region

#Region "EVENTS"

    Private Sub frmExemption_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing

        Me.PreviousForm.Enabled = True

    End Sub

    Private Sub frmExemption_Load(sender As Object, e As EventArgs) Handles Me.Load
        With cboEntity
            .DataSource = New BindingSource(EXEMPTION_ENTITIES, Nothing)
            .DisplayMember = "Value"
            .ValueMember = "Key"
        End With
    End Sub

    Private Sub cboEntity_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboEntity.SelectedIndexChanged
        Dim selectedEntity As ExemptionEntity
        If cboEntity.SelectedIndex > -1 Then
            selectedEntity = CType(cboEntity.SelectedItem, KeyValuePair(Of ExemptionEntity, String)).Key
            With cboOperator
                .DataSource = New BindingSource(GetExemptionOperators(selectedEntity), Nothing)
                .DisplayMember = "Value"
                .ValueMember = "Key"
            End With
        End If
    End Sub

    Private Sub txtValue_TextChanged(sender As Object, e As EventArgs) Handles txtValue.TextChanged

    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub FormatDataGridView()

        With dgvExemption

        End With

    End Sub

#End Region

End Class