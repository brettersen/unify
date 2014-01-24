Imports BP.Unify.Core
Imports System.IO

Public Class frmOpen

#Region "METHODS"

    Private Sub FormatDataGridView()

        With dgvRoutine
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .AllowUserToResizeColumns = False
            .AllowUserToResizeRows = False
            .AllowUserToOrderColumns = False
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .RowHeadersVisible = False
            .ReadOnly = True
            .Columns.Add("dgvc0", "File Path")
            .Columns.Add("dgvc1", "Files")
            .Columns(0).Visible = False
            .Columns(1).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            .Columns(1).DefaultCellStyle.Padding = New Padding(5, 0, 0, 0)
        End With

    End Sub

    Private Sub PopulateDataGridView()

        Dim rowIndex As Integer
        Dim routineFiles As IEnumerable(Of FileInfo) = From f In New DirectoryInfo(SaveFilePath).GetFiles("*." & APP_FILE_EXTENSION, SearchOption.TopDirectoryOnly)
                                                       Order By f.Name Ascending
                                                       Select f

        dgvRoutine.Rows.Clear()

        For Each routineFile As FileInfo In routineFiles
            rowIndex = dgvRoutine.Rows.Add()
            With dgvRoutine.Rows(rowIndex)
                .Cells(0).Value = routineFile.FullName
                .Cells(1).Value = routineFile.Name
            End With
        Next

    End Sub

#End Region

#Region "EVENTS"

    Private Sub frmOpen_Load(sender As Object, e As EventArgs) Handles Me.Load
        FormatDataGridView()
        PopulateDataGridView()
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If MessageBox.Show("Are you sure you want to delete this routine?", _
                           "Delete Routine", _
                           MessageBoxButtons.YesNo, _
                           MessageBoxIcon.Warning) = Windows.Forms.DialogResult.Yes Then
            File.Delete(dgvRoutine.SelectedRows(0).Cells(0).Value.ToString())
            PopulateDataGridView()
        End If
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

#End Region

End Class