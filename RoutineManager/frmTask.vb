Public Class frmTask

#Region "PROPERTIES"

    Public Property FormMode As FormMode
    Public Property PreviousForm As frmTasks

    Public ReadOnly Property Task As Task
        Get
            Return Me.PreviousForm.SelectedTask
        End Get
    End Property

#End Region

#Region "CONSTRUCTORS"

    Public Sub New(ByRef previousForm As frmTasks)

        InitializeComponent()

        Me.PreviousForm = previousForm

    End Sub

#End Region

#Region "EVENTS"

    Private Sub frmTask_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing

        Me.PreviousForm.Enabled = True

    End Sub

    Private Sub frmTask_Load(sender As Object, e As EventArgs) Handles Me.Load



    End Sub

    Private Sub btnSourceDirectory_Click(sender As Object, e As EventArgs) Handles btnSourceDirectory.Click

        Using newForm As New FolderBrowserDialog()
            If newForm.ShowDialog() = DialogResult.OK Then
                txtSourceDirectory.Text = newForm.SelectedPath
            End If
        End Using

    End Sub

    Private Sub btnDestinationDirectory_Click(sender As Object, e As EventArgs) Handles btnDestinationDirectory.Click

        Using newForm As New FolderBrowserDialog()
            If newForm.ShowDialog() = DialogResult.OK Then
                txtDestinationDirectory.Text = newForm.SelectedPath
            End If
        End Using

    End Sub

    Private Sub btnManage_Click(sender As Object, e As EventArgs) Handles btnManage.Click



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
            .DestinationDirectory = txtDestinationDirectory.Text.Trim()
            .AddFiles = chkAddFiles.Checked
            .ReplaceFiles = chkReplaceFiles.Checked
            .RemoveFiles = chkRemoveFiles.Checked
            .SearchRecursively = chkSearchRecursively.Checked
            .ExcludeHiddenFiles = chkExcludeHiddenFiles.Checked
            .PendingAction = Core.Action.Insert
        End With

        Me.PreviousForm.PreviousForm.Routine.Tasks.Add(Me.Task)

    End Sub

#End Region

End Class