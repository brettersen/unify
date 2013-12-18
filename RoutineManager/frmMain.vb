Public Class frmMain

    Public Property Routine As Routine

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles Me.Load

        btnManageTasks.Enabled = False

    End Sub

    Private Sub tsmiNew_Click(sender As Object, e As EventArgs) Handles tsmiNew.Click

        Me.Routine = New Routine()
        Me.Text = "New Routine - Unify"

        btnManageTasks.Enabled = True

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

    

    Private Sub PopulateDataGridView()

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        Dim exemptionForm As New frmExemptions()

        exemptionForm.ShowDialog()

    End Sub

    
    Private Sub btnManageTasks_Click(sender As Object, e As EventArgs) Handles btnManageTasks.Click

        Dim f As New frmTasks(Me)

        f.ShowDialog()

    End Sub
End Class
