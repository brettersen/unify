Public Class frmExemptions

#Region "PROPERTIES"

    Public Property Exemptions As Collection(Of SyncTaskExemption)

    Public ReadOnly Property SelectedExemptionIndex As Integer
        Get
            If dgvExemption.SelectedRows.Count > 0 Then
                Return dgvExemption.SelectedRows(0).Index
            Else
                Return -1
            End If
        End Get
    End Property

#End Region

#Region "METHODS"

    Private Sub FormatDataGridView()
        With dgvExemption
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .RowHeadersVisible = False
            .ColumnHeadersVisible = False
            .ReadOnly = True
            .Columns.Add("dgvc0", "Id")
            .Columns.Add("dgvc1", "Entity")
            .Columns.Add("dgvc1", "Operator")
            .Columns.Add("dgvc1", "Value")
            .Columns(0).Visible = False
            .Columns(1).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            .Columns(1).FillWeight = 30
            .Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            .Columns(2).FillWeight = 30
            .Columns(3).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            .Columns(3).FillWeight = 40
        End With
    End Sub

    Private Sub PopulateDataGridView()

        Dim rowIndex As Integer

        dgvExemption.Rows.Clear()

        For Each exemption As SyncTaskExemption In Me.Exemptions
            rowIndex = dgvExemption.Rows.Add()
            With dgvExemption.Rows(rowIndex)
                .Cells(1).Value = EXEMPTION_ENTITIES(exemption.Entity)
                .Cells(2).Value = EXEMPTION_OPERATORS(exemption.Operator)
                .Cells(3).Value = exemption.Value
            End With
        Next

    End Sub

    Private Function CheckSelection() As Boolean

        Dim value As String

        If cboEntity.SelectedIndex < 0 Then
            ShowError("An exemption entity must be selected.", "Add Exemption")
            Return False
        End If

        If cboOperator.SelectedIndex < 0 Then
            ShowError("An exemption operator must be selected.", "Add Exemption")
            Return False
        End If

        value = txtValue.Text.Trim()
        If value.Length = 0 Then
            ShowError("An exemption value must be provided.", "Add Exemption")
            Return False
        End If

        Return True

    End Function

    Private Function GetSelection() As SyncTaskExemption
        Return New SyncTaskExemption(CType(cboEntity.SelectedItem, KeyValuePair(Of ExemptionEntity, String)).Key, _
                                     CType(cboOperator.SelectedItem, KeyValuePair(Of ExemptionOperator, String)).Key, _
                                     txtValue.Text.Trim())
    End Function

#End Region

#Region "EVENTS"

    Private Sub frmExemption_Load(sender As Object, e As EventArgs) Handles Me.Load

        FormatDataGridView()
        PopulateDataGridView()

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

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        If CheckSelection() Then
            Me.Exemptions.Add(GetSelection())
            PopulateDataGridView()
            cboEntity.SelectedIndex = 0
            cboOperator.SelectedIndex = 0
            txtValue.Text = Space(0)
        End If
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click

    End Sub

    Private Sub btnRemove_Click(sender As Object, e As EventArgs) Handles btnRemove.Click
        If Me.SelectedExemptionIndex > -1 Then
            If ShowWarning("Are you sure you want to remove this exemption?", "Remove Exemption?") = Windows.Forms.DialogResult.Yes Then
                Me.Exemptions.RemoveAt(Me.SelectedExemptionIndex)
                PopulateDataGridView()
            End If
        End If
    End Sub

    Private Sub btnPromote_Click(sender As Object, e As EventArgs) Handles btnPromote.Click
        If Me.SelectedExemptionIndex > -1 Then
            Me.Exemptions.Promote(Me.SelectedExemptionIndex)
            PopulateDataGridView()
            dgvExemption_SelectionChanged(sender, e)
        End If
    End Sub

    Private Sub btnDemote_Click(sender As Object, e As EventArgs) Handles btnDemote.Click
        If Me.SelectedExemptionIndex > -1 Then
            Me.Exemptions.Demote(Me.SelectedExemptionIndex)
            PopulateDataGridView()
            dgvExemption_SelectionChanged(sender, e)
        End If
    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
    End Sub

    Private Sub dgvExemption_SelectionChanged(sender As Object, e As EventArgs) Handles dgvExemption.SelectionChanged
        If dgvExemption.SelectedRows.Count > 0 Then
            btnEdit.Enabled = True
            btnRemove.Enabled = True
            btnPromote.Enabled = dgvExemption.SelectedRows(0).Index > 0
            btnDemote.Enabled = dgvExemption.SelectedRows(0).Index < dgvExemption.Rows.Count - 1
        Else
            btnEdit.Enabled = False
            btnRemove.Enabled = False
            btnPromote.Enabled = False
            btnDemote.Enabled = False
        End If
    End Sub

#End Region

End Class