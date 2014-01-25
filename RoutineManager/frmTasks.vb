Imports System.IO
Imports System.Runtime.Serialization
Imports System.Runtime.Serialization.Formatters.Binary

Public Class frmTasks

    Private _openFilePath As String

#Region "PROPERTIES"

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
            _openFilePath = value
        End Set
    End Property

    Public Property Tasks As Collection(Of SyncTask)

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
            .MultiSelect = False
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .RowHeadersVisible = False
            .ReadOnly = True
            .Columns.Add("dgvc0", "Id")
            .Columns.Add("dgvc1", "Source Directory")
            .Columns.Add("dgvc2", "Target Directory")
            .Columns.Add("dgvc3", "Has Exemptions")
            .Columns.Add("dgvc4", "Options")
            .Columns(0).Visible = False
            .Columns(1).Width = 260
            .Columns(1).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(2).Width = 260
            .Columns(2).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(3).Width = 110
            .Columns(3).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(4).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            .Columns(4).SortMode = DataGridViewColumnSortMode.NotSortable
        End With

    End Sub

    Private Sub PopulateDataGridView(ByVal mode As SelectionPosition)

        Dim selectedIndex, rowIndex As Integer

        If mode = SelectionPosition.Current OrElse _
            mode = SelectionPosition.Previous OrElse _
            mode = SelectionPosition.Next Then
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
                    .Cells(3).Value = IIf(task.Exemptions.Count > 0, "Yes", "No")
                    .Cells(4).Value = String.Join(", ", (From o In Core.Common.TASK_OPTIONS
                                                         Where task.Options.HasFlag(o.Key)
                                                         Order By o.Key
                                                         Select o.Value))
                End With
            Next
        End If

        If dgvTask.Rows.Count > 0 Then
            Select Case mode
                Case SelectionPosition.None
                    dgvTask.SelectedRows(0).Selected = False
                Case SelectionPosition.First
                    dgvTask.Rows(0).Selected = True
                Case SelectionPosition.Previous
                    dgvTask.Rows(selectedIndex - 1).Selected = True
                Case SelectionPosition.Current
                    dgvTask.Rows(selectedIndex).Selected = True
                Case SelectionPosition.Next
                    dgvTask.Rows(selectedIndex + 1).Selected = True
                Case SelectionPosition.Last
                    dgvTask.Rows(dgvTask.Rows.Count - 1).Selected = True
            End Select
        End If

    End Sub

    Private Sub ShowForm(ByVal mode As FormEntryMode, Optional ByVal populateWithSelectedTask As Boolean = False)
        Using newForm As New frmTask()
            With newForm
                .EntryMode = mode
                If mode = FormEntryMode.Add AndAlso Not populateWithSelectedTask Then
                    .Task = New SyncTask()
                Else
                    .Task = Me.Tasks(Me.SelectedTaskIndex).Clone()
                    .Populate()
                End If
                If .ShowDialog() = Windows.Forms.DialogResult.OK Then
                    If mode = FormEntryMode.Add Then
                        Me.Tasks.Add(.Task)
                        PopulateDataGridView(SelectionPosition.Last)
                    Else
                        Me.Tasks(Me.SelectedTaskIndex) = .Task
                        PopulateDataGridView(SelectionPosition.Current)
                    End If
                End If
            End With
        End Using
    End Sub

#End Region

#Region "EVENTS"

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Not System.IO.Directory.Exists(SaveFilePath) Then
            System.IO.Directory.CreateDirectory(SaveFilePath)
        End If

        FormatDataGridView()
        dgvTask_SelectionChanged(sender, e)

        tsmiClose.Enabled = False
        tsmiSave.Enabled = False
        tsmiSaveAs.Enabled = False

    End Sub

    Private Sub tsmiNew_Click(sender As Object, e As EventArgs) Handles tsmiNew.Click

        Me.Tasks = New Collection(Of SyncTask)
        Me.Text = "New File - " & Common.APP_NAME
        PopulateDataGridView(SelectionPosition.None)
        dgvTask_SelectionChanged(sender, e)

        tsmiClose.Enabled = True
        tsmiSave.Enabled = False
        tsmiSaveAs.Enabled = True

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

        Try
            If newForm.ShowDialog() = Windows.Forms.DialogResult.OK Then
                Using fileStream = newForm.OpenFile()
                    Me.Tasks = CType(formatter.Deserialize(fileStream), Collection(Of SyncTask))
                    Me.OpenFilePath = newForm.FileName
                    PopulateDataGridView(SelectionPosition.First)
                    dgvTask_SelectionChanged(sender, e)
                    tsmiClose.Enabled = True
                    tsmiSave.Enabled = False
                    tsmiSaveAs.Enabled = True
                End Using
            End If
        Catch ex As SerializationException
            MessageBox.Show("Could not open file.", "Open File", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub tsmiClose_Click(sender As Object, e As EventArgs) Handles tsmiClose.Click

        Me.Tasks = Nothing
        Me.OpenFilePath = Nothing
        PopulateDataGridView(SelectionPosition.None)
        dgvTask_SelectionChanged(sender, e)

        tsmiClose.Enabled = False
        tsmiSave.Enabled = False
        tsmiSaveAs.Enabled = False

    End Sub

    Private Sub tsmiSave_Click(sender As Object, e As EventArgs) Handles tsmiSave.Click



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
                Using fileStream = newForm.OpenFile()
                    formatter.Serialize(fileStream, Me.Tasks)
                    Me.OpenFilePath = newForm.FileName
                    tsmiClose.Enabled = True
                    tsmiSave.Enabled = False
                    tsmiSaveAs.Enabled = True
                End Using
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
            ShowForm(FormEntryMode.Edit)
        End If
    End Sub

    Private Sub btnRemove_Click(sender As Object, e As EventArgs) Handles btnRemove.Click, tsmiRemove.Click
        If Me.SelectedTaskIndex > -1 Then
            If ShowWarning("Are you sure you want to remove this task?", "Remove Task?") = Windows.Forms.DialogResult.Yes Then
                Me.Tasks.RemoveAt(Me.SelectedTaskIndex)
                PopulateDataGridView(SelectionPosition.None)
                dgvTask_SelectionChanged(sender, e)
            End If
        End If
    End Sub

    Private Sub btnCopy_Click(sender As Object, e As EventArgs) Handles btnCopy.Click, tsmiCopy.Click
        If Me.SelectedTaskIndex > -1 Then
            ShowForm(FormEntryMode.Add, True)
        End If
    End Sub

    Private Sub btnPromote_Click(sender As Object, e As EventArgs) Handles btnPromote.Click, tsmiPromote.Click
        If Me.SelectedTaskIndex > -1 AndAlso Me.SelectedTaskIndex > 0 Then
            Me.Tasks.Promote(Me.SelectedTaskIndex)
            PopulateDataGridView(SelectionPosition.Previous)
            dgvTask_SelectionChanged(sender, e)
        End If
    End Sub

    Private Sub btnDemote_Click(sender As Object, e As EventArgs) Handles btnDemote.Click, tsmiDemote.Click
        If Me.SelectedTaskIndex > -1 AndAlso Me.SelectedTaskIndex < dgvTask.Rows.Count - 1 Then
            Me.Tasks.Demote(Me.SelectedTaskIndex)
            PopulateDataGridView(SelectionPosition.Next)
            dgvTask_SelectionChanged(sender, e)
        End If
    End Sub

    Private Sub btnExecute_Click(sender As Object, e As EventArgs) Handles btnExecute.Click

        Win32API.CopyFileEx("C:\Program Files (x86)\WinSCP\winscp517setup.exe", _
                            "C:\Users\Brett Petersen\Desktop\winscp517setup.exe", _
                            AddressOf UpdateProgressBar, _
                            Nothing, _
                            False, _
                            Win32API.CopyFileFlags.COPY_FILE_ALLOW_DECRYPTED_DESTINATION)


    End Sub

    Private Function UpdateProgressBar(ByVal totalFileSize As Long, _
                                       ByVal totalBytesTransferred As Long, _
                                       ByVal streamSize As Long, _
                                       ByVal streamBytesTransferred As Long, _
                                       ByVal streamNumber As UInteger,
                                       ByVal callbackReason As Win32API.CopyProgressCallbackReason, _
                                       ByVal sourceFile As IntPtr,
                                       ByVal destinationFile As IntPtr,
                                       ByVal data As IntPtr) As Win32API.CopyProgressResult
        'pbrTest.Maximum = totalFileSize
        'pbrTest.Minimum = 0
        'pbrTest.Value = totalBytesTransferred
        Return Win32API.CopyProgressResult.PROGRESS_CONTINUE
    End Function

#End Region

End Class
