Public Class frmSync

    Public Property PreviousForm As frmTasks

    Private Sub frmSync_Load(sender As Object, e As EventArgs) Handles Me.Load
        For Each task In Me.PreviousForm.Tasks
            For Each operation In task.GetOperations()
                rtbConsole.AppendText(operation.ToString())
            Next
            rtbConsole.AppendText(vbCrLf & vbCrLf & vbCrLf)
        Next
    End Sub

End Class