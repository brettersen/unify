Public Class frmTask

#Region "PROPERTIES"

    Public Property FormMode As FormMode
    Public Property PreviousForm As frmMain

    Public ReadOnly Property Task As SyncTask
        Get
            Return Me.PreviousForm.SelectedTask
        End Get
    End Property

#End Region

#Region "CONSTRUCTORS"

    Public Sub New()

        InitializeComponent()

    End Sub

#End Region

#Region "EVENTS"

    Private Sub frmTask_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing

        Me.PreviousForm.Enabled = True

    End Sub

    Private Sub frmTask_Load(sender As Object, e As EventArgs) Handles Me.Load

        Select Case Me.FormMode
            Case Common.FormMode.Adding
                Me.Text = "Add Task"
            Case Common.FormMode.Editing
                Me.Text = "Edit Task"
        End Select

    End Sub

    Private Sub btnSourceDirectory_Click(sender As Object, e As EventArgs) Handles btnSourceDirectory.Click

        Using newForm As New FolderBrowserDialog()
            If newForm.ShowDialog() = DialogResult.OK Then
                txtSourceDirectory.Text = newForm.SelectedPath
            End If
        End Using

    End Sub

    Private Sub btnDestinationDirectory_Click(sender As Object, e As EventArgs) Handles btnTargetDirectory.Click

        Using newForm As New FolderBrowserDialog()
            If newForm.ShowDialog() = DialogResult.OK Then
                txtTargetDirectory.Text = newForm.SelectedPath
            End If
        End Using

    End Sub

    Private Sub btnManage_Click(sender As Object, e As EventArgs) Handles btnManage.Click

        ShowForm(Common.FormMode.Adding)

    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click

        Scrape()
        Me.Close()

    End Sub

#End Region

#Region "METHODS"

    Private Sub Populate()

    End Sub

    Private Sub Scrape()

        With Me.Task
            .SourceDirectory = txtSourceDirectory.Text.Trim()
            .TargetDirectory = txtTargetDirectory.Text.Trim()
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

        Me.PreviousForm.Routine.Tasks.Add(Me.Task)

    End Sub

    Private Sub ShowForm(ByVal mode As FormMode)

        Dim newForm As New frmExemptions()

        Me.Enabled = False

        With newForm
            .PreviousForm = Me
            .ShowDialog()
        End With

    End Sub

#End Region

End Class