Imports System.IO
Imports System.Runtime.Serialization
Imports System.Runtime.Serialization.Formatters.Binary

Public Class frmMain

    Private _hasPendingChanges As Boolean
    Private _openFilePath As String
    Private _tasks As List(Of SyncTask)

#Region "PROPERTIES"

    Public Property HasPendingChanges As Boolean
        Get
            Return _hasPendingChanges
        End Get
        Set(value As Boolean)
            tsmiSave.Enabled = value
            _hasPendingChanges = value
        End Set
    End Property

    Public Property OpenFilePath As String
        Get
            Return _openFilePath
        End Get
        Set(value As String)
            If value IsNot Nothing Then
                Me.Text = Path.GetFileName(value) & " - " & Common.APP_NAME
            Else
                Me.Text = Common.APP_NAME
            End If
            Me.HasPendingChanges = False
            _openFilePath = value
        End Set
    End Property

    Public Property Tasks As List(Of SyncTask)
        Get
            Return _tasks
        End Get
        Set(value As List(Of SyncTask))
            _tasks = value
        End Set
    End Property

    Public ReadOnly Property SelectedTaskIndex As Integer
        Get
            If dgvTask.SelectedRows.Count > 0 Then
                Return dgvTask.SelectedRows(0).Index
            Else
                Return -1
            End If
        End Get
    End Property

#End Region

#Region "METHODS"

    Private Sub EnableControls(ByVal enable As Boolean)
        For Each c As Control In Me.Controls
            If TypeOf c Is Button OrElse TypeOf c Is DataGridView Then
                c.Enabled = enable
            End If
        Next
    End Sub

    Private Sub FormatDataGridView()

        With dgvTask
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .AllowUserToResizeRows = False
            .MultiSelect = False
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .RowHeadersVisible = False
            .ReadOnly = True
            .Columns.Add("dgvc0", "Id")
            .Columns.Add("dgvc1", "Source Directory")
            .Columns.Add("dgvc2", "Target Directory")
            '.Columns.Add("dgvc3", "Options")
            '.Columns.Add("dgvc4", "Exemptions")
            .Columns(0).Visible = False
            .Columns(1).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            .Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            '.Columns(3).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            '.Columns(4).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            .Columns(1).FillWeight = 12
            .Columns(2).FillWeight = 12
            '.Columns(3).FillWeight = 15
            '.Columns(4).FillWeight = 15
            .Columns(1).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(2).SortMode = DataGridViewColumnSortMode.NotSortable
            '.Columns(3).SortMode = DataGridViewColumnSortMode.NotSortable
            '.Columns(4).SortMode = DataGridViewColumnSortMode.NotSortable
        End With

    End Sub

    Private Sub PopulateDataGridView(ByVal selectedRow As SelectionPosition)

        Dim selectedIndex, rowIndex As Integer

        If selectedRow = SelectionPosition.Current OrElse _
            selectedRow = SelectionPosition.Previous OrElse _
            selectedRow = SelectionPosition.Next Then
            If dgvTask.SelectedRows.Count > 0 Then
                selectedIndex = dgvTask.SelectedRows(0).Index
            End If
        End If

        dgvTask.Rows.Clear()

        If Me.Tasks IsNot Nothing Then
            For Each task As SyncTask In Me.Tasks
                rowIndex = dgvTask.Rows.Add()
                With dgvTask.Rows(rowIndex)
                    .Cells(1).Value = task.SourceDirectory
                    .Cells(2).Value = task.TargetDirectory
                    '.Cells(3).Value = String.Join(", ", (From o In FormattedSyncTaskOptions
                    '                                     Where task.Options.HasFlag(o.Key)
                    '                                     Order By o.Key
                    '                                     Select o.Value))
                    '.Cells(4).Value = String.Join(", ", (From e In task.Exemptions
                    '                                     Select FormattedExemptionEntities.Item(e.Entity) & Space(1) & _
                    '                                            FormattedExemptionOperators.Item(e.Operator) & Space(1) & _
                    '                                            e.Value))
                End With
            Next
        End If

        With dgvTask
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

    Private Sub ShowForm(ByVal entryMode As FormEntryMode, Optional ByVal task As SyncTask = Nothing)
        Using newForm As New frmTask(entryMode, task)
            With newForm
                If .ShowDialog() = Windows.Forms.DialogResult.OK Then
                    If entryMode = FormEntryMode.Add Then
                        Me.Tasks.Add(.Task)
                        PopulateDataGridView(SelectionPosition.Last)
                    Else
                        Me.Tasks(Me.SelectedTaskIndex) = .Task
                        PopulateDataGridView(SelectionPosition.Current)
                    End If
                    Me.HasPendingChanges = True
                End If
            End With
        End Using
    End Sub

#End Region

#Region "EVENTS"

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Not Directory.Exists(SaveFilePath) Then
            Directory.CreateDirectory(SaveFilePath)
        End If

        FormatDataGridView()
        dgvTask_SelectionChanged(sender, e)

        tsmiClose.Enabled = False
        tsmiSave.Enabled = False
        tsmiSaveAs.Enabled = False

    End Sub

    Private Sub tsmiNew_Click(sender As Object, e As EventArgs) Handles tsmiNew.Click

        If Not Me.HasPendingChanges OrElse ShowWarning("There are pending changes on the current file. Discard changes?", "New File") = Windows.Forms.DialogResult.Yes Then
            Me.Tasks = New List(Of SyncTask)
            Me.Text = "New File - " & Common.APP_NAME
            Me.HasPendingChanges = True
            PopulateDataGridView(SelectionPosition.None)
            dgvTask_SelectionChanged(sender, e)
            tsmiClose.Enabled = True
            tsmiSaveAs.Enabled = True
        End If

    End Sub

    Private Sub tsmiOpen_Click(sender As Object, e As EventArgs) Handles tsmiOpen.Click

        Dim newForm As New OpenFileDialog
        Dim appFileFilter As String = "*." & Common.APP_FILE_EXTENSION
        Dim formatter As New BinaryFormatter

        With newForm
            .Filter = APP_NAME & " files (" & appFileFilter & ")|" & appFileFilter & "|All files (*.*)|*.*"
            .InitialDirectory = Common.SaveFilePath
            .Multiselect = False
            .RestoreDirectory = True
            .Title = "Open File"
        End With

        If Not Me.HasPendingChanges OrElse ShowWarning("There are pending changes on the current file. Discard changes?", "Open File") = Windows.Forms.DialogResult.Yes Then
            Try
                If newForm.ShowDialog() = Windows.Forms.DialogResult.OK Then
                    Using fileStream = newForm.OpenFile()
                        Me.Tasks = CType(formatter.Deserialize(fileStream), List(Of SyncTask))
                        Me.OpenFilePath = newForm.FileName
                        Me.HasPendingChanges = False
                        PopulateDataGridView(SelectionPosition.First)
                        dgvTask_SelectionChanged(sender, e)
                        tsmiClose.Enabled = True
                        tsmiSaveAs.Enabled = True
                    End Using
                End If
            Catch ex As Exception
                MessageBox.Show("Could not open file.", "Open File", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If

    End Sub

    Private Sub tsmiClose_Click(sender As Object, e As EventArgs) Handles tsmiClose.Click

        If Not Me.HasPendingChanges OrElse ShowWarning("There are pending changes on the current file. Discard changes?", "Close File") = Windows.Forms.DialogResult.Yes Then
            Me.Tasks = Nothing
            Me.OpenFilePath = Nothing
            Me.HasPendingChanges = False
            PopulateDataGridView(SelectionPosition.None)
            dgvTask_SelectionChanged(sender, e)
            tsmiClose.Enabled = False
            tsmiSaveAs.Enabled = False
        End If

    End Sub

    Private Sub tsmiSave_Click(sender As Object, e As EventArgs) Handles tsmiSave.Click

        Dim bf As BinaryFormatter

        If Me.OpenFilePath IsNot Nothing Then
            Using fs As New FileStream(Me.OpenFilePath, FileMode.Open, FileAccess.Write, FileShare.None)
                bf = New BinaryFormatter()
                bf.Serialize(fs, Me.Tasks)
            End Using
            Me.HasPendingChanges = False
        Else
            tsmiSaveAs_Click(sender, e)
        End If

    End Sub

    Private Sub tsmiSaveAs_Click(sender As Object, e As EventArgs) Handles tsmiSaveAs.Click

        Dim newForm As New SaveFileDialog
        Dim appFileFilter As String = "*." & Common.APP_FILE_EXTENSION
        Dim formatter As New BinaryFormatter

        With newForm
            .AddExtension = True
            .CreatePrompt = True
            .DefaultExt = Common.APP_FILE_EXTENSION
            .Filter = APP_NAME & " files (" & appFileFilter & ")|" & appFileFilter & "|All files (*.*)|*.*"
            .InitialDirectory = Common.SaveFilePath
            .OverwritePrompt = True
            .RestoreDirectory = True
            .Title = "Save File As"
        End With

        Try
            If newForm.ShowDialog() = Windows.Forms.DialogResult.OK Then
                Using fs = newForm.OpenFile()
                    formatter.Serialize(fs, Me.Tasks)
                End Using
                Me.OpenFilePath = newForm.FileName
                Me.HasPendingChanges = False
                tsmiClose.Enabled = True
                tsmiSave.Enabled = True
                tsmiSaveAs.Enabled = True
            End If
        Catch ex As SerializationException
            MessageBox.Show("Could not save file.", "Save File As", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub tsmiExit_Click(sender As Object, e As EventArgs) Handles tsmiExit.Click
        Me.Close()
    End Sub

    Private Sub btnAdd_EnabledChanged(sender As Object, e As EventArgs) Handles btnAdd.EnabledChanged
        tsmiAdd.Enabled = btnAdd.Enabled
    End Sub

    Private Sub btnEdit_EnabledChanged(sender As Object, e As EventArgs) Handles btnEdit.EnabledChanged
        tsmiEdit.Enabled = btnEdit.Enabled
    End Sub

    Private Sub btnRemove_EnabledChanged(sender As Object, e As EventArgs) Handles btnRemove.EnabledChanged
        tsmiRemove.Enabled = btnRemove.Enabled
    End Sub

    Private Sub btnCopy_EnabledChanged(sender As Object, e As EventArgs) Handles btnCopy.EnabledChanged
        tsmiCopy.Enabled = btnCopy.Enabled
    End Sub

    Private Sub btnPromote_EnabledChanged(sender As Object, e As EventArgs) Handles btnPromote.EnabledChanged
        tsmiPromote.Enabled = btnPromote.Enabled
    End Sub

    Private Sub btnDemote_EnabledChanged(sender As Object, e As EventArgs) Handles btnDemote.EnabledChanged
        tsmiDemote.Enabled = btnDemote.Enabled
    End Sub

    Private Sub dgvTask_DoubleClick(sender As Object, e As EventArgs) Handles dgvTask.DoubleClick
        If dgvTask.SelectedRows.Count > 0 Then
            btnEdit.PerformClick()
        End If
    End Sub

    Private Sub dgvTask_SelectionChanged(sender As Object, e As EventArgs) Handles dgvTask.SelectionChanged

        Dim enable As Boolean = dgvTask.SelectedRows.Count > 0

        btnAdd.Enabled = Me.Tasks IsNot Nothing
        btnEdit.Enabled = enable
        btnRemove.Enabled = enable
        btnCopy.Enabled = enable
        btnPromote.Enabled = enable AndAlso Me.SelectedTaskIndex > 0
        btnDemote.Enabled = enable AndAlso Me.SelectedTaskIndex < dgvTask.Rows.Count - 1
        btnExecute.Enabled = dgvTask.Rows.Count > 0

    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click, tsmiAdd.Click
        ShowForm(FormEntryMode.Add)
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click, tsmiEdit.Click
        If Me.SelectedTaskIndex > -1 Then
            ShowForm(FormEntryMode.Edit, Me.Tasks(Me.SelectedTaskIndex).Clone())
        End If
    End Sub

    Private Sub btnRemove_Click(sender As Object, e As EventArgs) Handles btnRemove.Click, tsmiRemove.Click
        If Me.SelectedTaskIndex > -1 Then
            If ShowWarning("Are you sure you want to remove this task?", "Remove Task") = Windows.Forms.DialogResult.Yes Then
                Me.Tasks.RemoveAt(Me.SelectedTaskIndex)
                Me.HasPendingChanges = True
                PopulateDataGridView(SelectionPosition.None)
                dgvTask_SelectionChanged(sender, e)
            End If
        End If
    End Sub

    Private Sub btnCopy_Click(sender As Object, e As EventArgs) Handles btnCopy.Click, tsmiCopy.Click
        If Me.SelectedTaskIndex > -1 Then
            ShowForm(FormEntryMode.Add, Me.Tasks(Me.SelectedTaskIndex).Clone())
        End If
    End Sub

    Private Sub btnPromote_Click(sender As Object, e As EventArgs) Handles btnPromote.Click, tsmiPromote.Click
        If Me.SelectedTaskIndex > -1 AndAlso Me.SelectedTaskIndex > 0 Then
            Me.Tasks.Promote(Me.SelectedTaskIndex)
            Me.HasPendingChanges = True
            PopulateDataGridView(SelectionPosition.Previous)
            dgvTask_SelectionChanged(sender, e)
        End If
    End Sub

    Private Sub btnDemote_Click(sender As Object, e As EventArgs) Handles btnDemote.Click, tsmiDemote.Click
        If Me.SelectedTaskIndex > -1 AndAlso Me.SelectedTaskIndex < dgvTask.Rows.Count - 1 Then
            Me.Tasks.Demote(Me.SelectedTaskIndex)
            Me.HasPendingChanges = True
            PopulateDataGridView(SelectionPosition.Next)
            dgvTask_SelectionChanged(sender, e)
        End If
    End Sub

    Private Sub btnExecute_Click(sender As Object, e As EventArgs) Handles btnExecute.Click
        Using newForm As New frmSync
            With newForm
                .PreviousForm = Me
                .ShowDialog()
            End With
        End Using
    End Sub

#End Region

End Class
