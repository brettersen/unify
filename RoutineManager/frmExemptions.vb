Public Class frmExemptions

    Private _exemptions As List(Of SyncTaskExemption)

#Region "PROPERTIES"

    Public Property Exemptions As List(Of SyncTaskExemption)
        Get
            Return _exemptions
        End Get
        Set(value As List(Of SyncTaskExemption))
            _exemptions = value
        End Set
    End Property

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
            .MultiSelect = False
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .RowHeadersVisible = False
            .ColumnHeadersVisible = False
            .ReadOnly = True
            .Columns.Add("dgvc0", "Id")
            .Columns.Add("dgvc1", "Exemption")
            .Columns(0).Visible = False
            .Columns(1).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With
    End Sub

    Private Sub PopulateDataGridView(ByVal selectedRow As SelectionPosition)

        Dim selectedIndex, rowIndex As Integer

        If selectedRow = SelectionPosition.Current OrElse _
            selectedRow = SelectionPosition.Previous OrElse _
            selectedRow = SelectionPosition.Next Then
            If dgvExemption.SelectedRows.Count > 0 Then
                selectedIndex = dgvExemption.SelectedRows(0).Index
            End If
        End If

        dgvExemption.Rows.Clear()

        For Each exemption As SyncTaskExemption In Me.Exemptions
            rowIndex = dgvExemption.Rows.Add()
            With dgvExemption.Rows(rowIndex)
                .Cells(1).Value = EXEMPTION_ENTITIES.Item(exemption.Entity) & _
                    Space(1) & EXEMPTION_OPERATORS.Item(exemption.Operator) & _
                    Space(1) & exemption.Value
            End With
        Next

        With dgvExemption
            If .Rows.Count > 0 Then
                Select Case selectedRow
                    Case SelectionPosition.None
                        .SelectedRows(0).Selected = False
                    Case SelectionPosition.First
                        .Rows(0).Selected = True
                    Case SelectionPosition.Previous
                        .Rows(selectedIndex - 1).Selected = True
                    Case SelectionPosition.Current
                        .Rows(selectedIndex).Selected = True
                    Case SelectionPosition.Next
                        .Rows(selectedIndex + 1).Selected = True
                    Case SelectionPosition.Last
                        .Rows(.Rows.Count - 1).Selected = True
                End Select
            End If
        End With

    End Sub

    Private Sub ShowForm(ByVal entryMode As FormEntryMode, Optional ByVal exemption As SyncTaskExemption = Nothing)
        Using newForm As New frmExemption(entryMode, exemption)
            With newForm
                If .ShowDialog() = Windows.Forms.DialogResult.OK Then
                    If .EntryMode = FormEntryMode.Add Then
                        Me.Exemptions.Add(.Exemption)
                        PopulateDataGridView(SelectionPosition.Last)
                    Else
                        Me.Exemptions(Me.SelectedExemptionIndex) = .Exemption
                        PopulateDataGridView(SelectionPosition.Current)
                    End If
                End If
                dgvExemption_SelectionChanged(Nothing, Nothing)
            End With
        End Using
    End Sub

#End Region

#Region "EVENTS"

    Private Sub frmExemption_Load(sender As Object, e As EventArgs) Handles Me.Load
        FormatDataGridView()
        PopulateDataGridView(SelectionPosition.First)
        dgvExemption_SelectionChanged(sender, e)
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        ShowForm(FormEntryMode.Add)
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        If Me.SelectedExemptionIndex > -1 Then
            ShowForm(FormEntryMode.Edit, Me.Exemptions(Me.SelectedExemptionIndex).Clone())
        End If
    End Sub

    Private Sub btnRemove_Click(sender As Object, e As EventArgs) Handles btnRemove.Click
        If Me.SelectedExemptionIndex > -1 Then
            If ShowWarning("Are you sure you want to remove this exemption?", "Remove Exemption") = Windows.Forms.DialogResult.Yes Then
                Me.Exemptions.RemoveAt(Me.SelectedExemptionIndex)
                PopulateDataGridView(SelectionPosition.None)
                dgvExemption_SelectionChanged(sender, e)
            End If
        End If
    End Sub

    Private Sub btnPromote_Click(sender As Object, e As EventArgs) Handles btnPromote.Click
        If Me.SelectedExemptionIndex > -1 Then
            Me.Exemptions.Promote(Me.SelectedExemptionIndex)
            PopulateDataGridView(SelectionPosition.Previous)
            dgvExemption_SelectionChanged(sender, e)
        End If
    End Sub

    Private Sub btnDemote_Click(sender As Object, e As EventArgs) Handles btnDemote.Click
        If Me.SelectedExemptionIndex > -1 Then
            Me.Exemptions.Demote(Me.SelectedExemptionIndex)
            PopulateDataGridView(SelectionPosition.Next)
            dgvExemption_SelectionChanged(sender, e)
        End If
    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
    End Sub

    Private Sub dgvExemption_DoubleClick(sender As Object, e As EventArgs) Handles dgvExemption.DoubleClick
        If Me.SelectedExemptionIndex > -1 Then
            btnEdit.PerformClick()
        End If
    End Sub

    Private Sub dgvExemption_SelectionChanged(sender As Object, e As EventArgs) Handles dgvExemption.SelectionChanged
        If Me.SelectedExemptionIndex > -1 Then
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