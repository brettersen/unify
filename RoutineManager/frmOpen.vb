Imports BP.Unify.Core

Public Class frmOpen

    Private Property RoutineList As RoutineList

    Private Sub frmOpen_Load(sender As Object, e As EventArgs) Handles Me.Load

        FormatDataGridView(dgvRoutine)

        Me.RoutineList = RoutineList.GetRoutineList()

        For Each r As Routine In Me.RoutineList
            dgvRoutine.Rows.Add(New Object() {r.RoutineId, r.RoutineName, r.CreatedOn.ToShortDateString(), r.UpdatedOn.ToShortDateString()})
        Next

    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click

    End Sub

    Private Sub btnOpen_Click(sender As Object, e As EventArgs) Handles btnOpen.Click

    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click

        Me.Close()

    End Sub

    Private Sub dgvRoutine_SelectionChanged(sender As Object, e As EventArgs) Handles dgvRoutine.SelectionChanged

        btnDelete.Enabled = dgvRoutine.SelectedRows.Count > 0
        btnOpen.Enabled = dgvRoutine.SelectedRows.Count > 0

    End Sub

    Private Sub FormatDataGridView(ByRef dgv As DataGridView)

        With dgv
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .AllowUserToResizeColumns = False
            .AllowUserToOrderColumns = False
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect  
            .RowHeadersVisible = False
            .ReadOnly = True
            .Columns.Add("dgvcRoutineId", "Id")
            .Columns.Add("dgvcRoutineName", "Name")
            .Columns.Add("dgvcCreatedOn", "Created On")
            .Columns.Add("dgvcUpdatedOn", "Updated On")
            .Columns(0).Visible = False
            .Columns(1).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            .Columns(2).Width = 88
            .Columns(3).Width = 88
            .Columns(1).DefaultCellStyle.Padding = New Padding(5, 0, 0, 0)
            .Columns(2).DefaultCellStyle.Padding = New Padding(5, 0, 0, 0)
            .Columns(3).DefaultCellStyle.Padding = New Padding(5, 0, 0, 0)
        End With

    End Sub

End Class