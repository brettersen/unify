Public Class frmMain

#Region "PROPERTIES"

    Public Property Routine As Routine

    Public ReadOnly Property SelectedTask As SyncTask
        Get
            Return Me.Routine.Tasks(dgvTask.SelectedRows(0).Index)
        End Get
    End Property

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
            .Columns.Add("dgvc2", "Target Directory")
            .Columns.Add("dgvc3", "Options")
            .Columns.Add("dgvc4", "Exemptions")
            .Columns(0).Visible = False
            .Columns(1).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            .Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            .Columns(3).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            .Columns(1).FillWeight = 50
            .Columns(2).FillWeight = 50
            .Columns(4).Width = 68
        End With

    End Sub

    Private Sub PopulateDataGridView()

        Dim newRowIndex As Integer

        dgvTask.Rows.Clear()

        For Each t As SyncTask In Me.Routine.Tasks
            newRowIndex = dgvTask.Rows.Add()
            With dgvTask.Rows(newRowIndex)
                .Cells(1).Value = t.SourceDirectory
                .Cells(2).Value = t.TargetDirectory
                .Cells(3).Value = t.Options.HasFlag(SyncTaskOptions.AddFiles).ToString()
                .Cells(4).Value = t.Exemptions.Count.ToString()
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

#Region "EVENTS"

    Private Sub frmTasks_EnabledChanged(sender As Object, e As EventArgs) Handles Me.EnabledChanged

        'If Me.Enabled Then
        '    PopulateDataGridView()
        'End If

    End Sub

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Not System.IO.Directory.Exists(SaveFilePath) Then
            System.IO.Directory.CreateDirectory(SaveFilePath)
        End If

        FormatDataGridView()

    End Sub

    Private Sub tsmiNew_Click(sender As Object, e As EventArgs) Handles tsmiNew.Click

        Me.Routine = New Routine()
        Me.Text = "New Routine - " & APP_NAME

    End Sub

    Private Sub tsmiOpen_Click(sender As Object, e As EventArgs) Handles tsmiOpen.Click

        Dim openForm As New frmOpen()

        openForm.ShowDialog()

    End Sub

    Private Sub tsmiClose_Click(sender As Object, e As EventArgs) Handles tsmiClose.Click

    End Sub

    Private Sub tsmiSave_Click(sender As Object, e As EventArgs) Handles tsmiSave.Click

    End Sub

    Private Sub tsmiSaveAs_Click(sender As Object, e As EventArgs) Handles tsmiSaveAs.Click

    End Sub

    Private Sub tsmiExit_Click(sender As Object, e As EventArgs) Handles tsmiExit.Click

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
        pbrTest.Maximum = totalFileSize
        pbrTest.Minimum = 0
        pbrTest.Value = totalBytesTransferred
        Return Win32API.CopyProgressResult.PROGRESS_CONTINUE
    End Function

#End Region

End Class
