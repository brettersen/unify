Public Class frmTask

    Private _entryMode As FormEntryMode
    Private _task As SyncTask

    Public Sub New(ByVal entryMode As FormEntryMode, Optional ByVal task As SyncTask = Nothing)
        InitializeComponent()
        Me.EntryMode = entryMode
        Me.Task = task
        FormatDataGridView()
        If Me.Task IsNot Nothing Then
            Populate()
        Else
            Me.Task = New SyncTask()
        End If
    End Sub

#Region "PROPERTIES"

    Public Property EntryMode As FormEntryMode
        Get
            Return _entryMode
        End Get
        Set(value As FormEntryMode)
            Select Case value
                Case FormEntryMode.Add
                    Me.Text = "Add Task"
                Case FormEntryMode.Edit
                    Me.Text = "Edit Task"
            End Select
            _entryMode = value
        End Set
    End Property

    Public Property Task As SyncTask
        Get
            Return _task
        End Get
        Set(value As SyncTask)
            _task = value
        End Set
    End Property

#End Region

#Region "METHODS"

    Private Sub FormatDataGridView()
        With dgvExemption
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .AllowUserToResizeRows = False
            .AllowUserToResizeColumns = False
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

    Public Sub Populate()
        With Me.Task
            txtSourceDirectory.Text = .SourceDirectory
            txtTargetDirectory.Text = .TargetDirectory
            chkAddFiles.Checked = .Options.HasFlag(SyncTaskOptions.AddFiles)
            chkReplaceFiles.Checked = .Options.HasFlag(SyncTaskOptions.ReplaceFiles)
            chkRemoveFiles.Checked = .Options.HasFlag(SyncTaskOptions.RemoveFiles)
            chkIncludeSubdirectories.Checked = .Options.HasFlag(SyncTaskOptions.IncludeSubdirectories)
            chkExcludeHiddenFiles.Checked = .Options.HasFlag(SyncTaskOptions.ExcludeHiddenFiles)
        End With
        PopulateDataGridView()
    End Sub

    Private Sub PopulateDataGridView()

        Dim rowIndex As Integer

        dgvExemption.Rows.Clear()

        For Each exemption As SyncTaskExemption In Me.Task.Exemptions
            rowIndex = dgvExemption.Rows.Add()
            With dgvExemption.Rows(rowIndex)
                .Cells(1).Value = EXEMPTION_ENTITIES(exemption.Entity) & Space(1) & EXEMPTION_OPERATORS(exemption.Operator) & Space(1) & exemption.Value
            End With
        Next

    End Sub

    Private Function Scrape() As Boolean
        With Me.Task
            If Not String.IsNullOrWhiteSpace(txtSourceDirectory.Text) Then
                .SourceDirectory = txtSourceDirectory.Text.Trim()
            Else
                ShowError("A source directory must be specified.", Me.Text)
                Return False
            End If
            If Not String.IsNullOrWhiteSpace(txtTargetDirectory.Text) Then
                .TargetDirectory = txtTargetDirectory.Text.Trim()
            Else
                ShowError("A target directory must be specified.", Me.Text)
                Return False
            End If
            If chkAddFiles.Checked AndAlso Not .Options.HasFlag(SyncTaskOptions.AddFiles) Then
                .Options += SyncTaskOptions.AddFiles
            ElseIf Not chkAddFiles.Checked AndAlso .Options.HasFlag(SyncTaskOptions.AddFiles) Then
                .Options -= SyncTaskOptions.AddFiles
            End If
            If chkReplaceFiles.Checked AndAlso Not .Options.HasFlag(SyncTaskOptions.ReplaceFiles) Then
                .Options += SyncTaskOptions.ReplaceFiles
            ElseIf Not chkReplaceFiles.Checked AndAlso .Options.HasFlag(SyncTaskOptions.ReplaceFiles) Then
                .Options -= SyncTaskOptions.ReplaceFiles
            End If
            If chkRemoveFiles.Checked AndAlso Not .Options.HasFlag(SyncTaskOptions.RemoveFiles) Then
                .Options += SyncTaskOptions.RemoveFiles
            ElseIf Not chkRemoveFiles.Checked AndAlso .Options.HasFlag(SyncTaskOptions.RemoveFiles) Then
                .Options -= SyncTaskOptions.RemoveFiles
            End If
            If chkIncludeSubdirectories.Checked AndAlso Not .Options.HasFlag(SyncTaskOptions.IncludeSubdirectories) Then
                .Options += SyncTaskOptions.IncludeSubdirectories
            ElseIf Not chkIncludeSubdirectories.Checked AndAlso .Options.HasFlag(SyncTaskOptions.IncludeSubdirectories) Then
                .Options -= SyncTaskOptions.IncludeSubdirectories
            End If
            If chkExcludeHiddenFiles.Checked AndAlso Not .Options.HasFlag(SyncTaskOptions.ExcludeHiddenFiles) Then
                .Options += SyncTaskOptions.ExcludeHiddenFiles
            ElseIf Not chkExcludeHiddenFiles.Checked AndAlso .Options.HasFlag(SyncTaskOptions.ExcludeHiddenFiles) Then
                .Options -= SyncTaskOptions.ExcludeHiddenFiles
            End If
        End With
        Return True
    End Function

#End Region

#Region "EVENTS"

    Private Sub frmTask_Load(sender As Object, e As EventArgs) Handles Me.Load

    End Sub

    Private Sub btnSourceDirectory_Click(sender As Object, e As EventArgs) Handles btnSourceDirectory.Click
        Using newForm As New FolderBrowserDialog()
            newForm.RootFolder = Environment.SpecialFolder.Desktop
            newForm.SelectedPath = IIf(txtSourceDirectory.TextLength > 0, txtSourceDirectory.Text, Nothing)
            If newForm.ShowDialog() = DialogResult.OK Then
                txtSourceDirectory.Text = newForm.SelectedPath
            End If
        End Using
    End Sub

    Private Sub btnDestinationDirectory_Click(sender As Object, e As EventArgs) Handles btnTargetDirectory.Click
        Using newForm As New FolderBrowserDialog()
            newForm.RootFolder = Environment.SpecialFolder.Desktop
            newForm.SelectedPath = IIf(txtTargetDirectory.TextLength > 0, txtTargetDirectory.Text, Nothing)
            If newForm.ShowDialog() = DialogResult.OK Then
                txtTargetDirectory.Text = newForm.SelectedPath
            End If
        End Using
    End Sub

    Private Sub btnManage_Click(sender As Object, e As EventArgs) Handles btnManage.Click
        Using newForm As New frmExemptions()
            With newForm
                .Exemptions = Me.Task.Exemptions
                If .ShowDialog() = Windows.Forms.DialogResult.OK Then
                    Me.Task.Exemptions = .Exemptions
                    PopulateDataGridView()
                End If
            End With
        End Using
    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        If Scrape() Then
            Me.DialogResult = Windows.Forms.DialogResult.OK
        End If
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
    End Sub

    Private Sub dgvExemption_SelectionChanged(sender As Object, e As EventArgs) Handles dgvExemption.SelectionChanged
        dgvExemption.ClearSelection()
    End Sub

#End Region

End Class