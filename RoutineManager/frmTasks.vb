Public Class frmTasks

#Region "PROPERTIES"

    Public Property PreviousForm As frmMain

    Public ReadOnly Property SelectedTask As Task
        Get
            Return Me.Tasks(dgvTask.SelectedRows(0).Index)
        End Get
    End Property

    Public ReadOnly Property Tasks As Collection(Of Task)
        Get
            Return Me.PreviousForm.Routine.Tasks
        End Get
    End Property

#End Region

#Region "CONSTRUCTORS"

    Public Sub New(ByRef previousForm As frmMain)

        InitializeComponent()

        Me.PreviousForm = previousForm

    End Sub

#End Region

#Region "EVENTS"

    Private Sub frmTasks_EnabledChanged(sender As Object, e As EventArgs) Handles Me.EnabledChanged

        If Me.Enabled Then
            PopulateDataGridView()
        End If

    End Sub

    Private Sub frmTaskList_Load(sender As Object, e As EventArgs) Handles Me.Load

        FormatDataGridView()
        PopulateDataGridView()

    End Sub

    Private Sub dgvTask_DoubleClick(sender As Object, e As EventArgs) Handles dgvTask.DoubleClick



    End Sub

    Private Sub dgvTask_SelectionChanged(sender As Object, e As EventArgs) Handles dgvTask.SelectionChanged

        Dim enable As Boolean = dgvTask.SelectedRows.Count > 0

        btnEdit.Enabled = enable
        btnRemove.Enabled = enable
        btnPromote.Enabled = enable
        btnDemote.Enabled = enable

    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click

        ShowForm(FormMode.Adding)

    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click

        ShowForm(FormMode.Editing)

    End Sub

    Private Sub btnRemove_Click(sender As Object, e As EventArgs) Handles btnRemove.Click

    End Sub

    Private Sub btnPromote_Click(sender As Object, e As EventArgs) Handles btnPromote.Click

    End Sub

    Private Sub btnDemote_Click(sender As Object, e As EventArgs) Handles btnDemote.Click

    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click

    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click

    End Sub

#End Region

#Region "METHODS"

    Private Sub FormatDataGridView()

        With dgvTask
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .RowHeadersVisible = False
            .ReadOnly = True
            .Columns.Add("dgvc0", "Id")
            .Columns.Add("dgvc1", "Source Directory")
            .Columns.Add("dgvc2", "Destination Directory")
            .Columns.Add("dgvc3", "Add Files")
            .Columns.Add("dgvc4", "Replace Files")
            .Columns.Add("dgvc5", "Remove Files")
            .Columns.Add("dgvc6", "Search Recursively")
            .Columns.Add("dgvc7", "Exclude Hidden Files")
            .Columns(0).Visible = False
            .Columns(1).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            .Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            .Columns(3).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            .Columns(4).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            .Columns(5).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            .Columns(6).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            .Columns(7).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            .Columns(1).FillWeight = 30
            .Columns(2).FillWeight = 30
            .Columns(3).FillWeight = 10
            .Columns(4).FillWeight = 10
            .Columns(5).FillWeight = 10
            .Columns(6).FillWeight = 10
            .Columns(7).FillWeight = 10
        End With

    End Sub

    Private Sub PopulateDataGridView()

        Dim newRowIndex As Integer

        dgvTask.Rows.Clear()

        For Each t As Task In Me.PreviousForm.Routine.Tasks
            newRowIndex = dgvTask.Rows.Add()
            With dgvTask.Rows(newRowIndex)
                .Cells(0).Value = t.TaskId
                .Cells(1).Value = t.SourceDirectory
                .Cells(2).Value = t.DestinationDirectory
                .Cells(3).Value = t.AddFiles
                .Cells(4).Value = t.ReplaceFiles
                .Cells(5).Value = t.RemoveFiles
                .Cells(6).Value = t.SearchRecursively
                .Cells(7).Value = t.ExcludeHiddenFiles
            End With
        Next

    End Sub

    Private Sub ShowForm(ByVal mode As FormMode)

        Dim newForm As New frmTask()

        Me.Enabled = False

        With newForm
            .FormMode = mode
            .PreviousForm = Me
            .ShowDialog()
        End With

    End Sub

#End Region

End Class