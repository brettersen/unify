Imports System.ComponentModel
Imports System.Text

Public Class frmSync

    Private _previousForm As frmTasks

    Private WithEvents worker As BackgroundWorker
    Private currentTask, currentOperation As Long
    Private totalTasks, totalOperations As Long

    Delegate Sub SyncOperationProgressedCallback(ByVal bytesTotal As Long, ByVal bytesTransferred As Long)
    Delegate Sub AppendToRichTextBoxCallback(ByVal text As String)

#Region "PROPERTIES"

    Public Property PreviousForm As frmTasks
        Get
            Return _previousForm
        End Get
        Set(value As frmTasks)
            _previousForm = value
        End Set
    End Property

#End Region

#Region "METHODS"

    Private Sub AppendToRichTextBox(ByVal text As String)
        Dim callback As AppendToRichTextBoxCallback
        If rtbConsole.InvokeRequired Then
            callback = New AppendToRichTextBoxCallback(AddressOf AppendToRichTextBox)
            Me.Invoke(callback, New Object() {text})
        Else
            With rtbConsole
                .AppendText(text)
            End With
        End If
    End Sub

#End Region

#Region "EVENTS"

    Private Sub frmSync_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.Text = "Syncing..."
        worker = New BackgroundWorker()
        worker.WorkerReportsProgress = True
        worker.WorkerSupportsCancellation = True
        worker.RunWorkerAsync()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Select Case btnCancel.Text
            Case "Cancel"
                Me.Text = "Canceling..."
                btnCancel.Enabled = False
                worker.CancelAsync()
            Case "Close"
                Me.Close()
        End Select
    End Sub

    Private Sub worker_DoWork(sender As Object, e As DoWorkEventArgs) Handles worker.DoWork
        Dim operations As List(Of SyncOperation)
        With CType(sender, BackgroundWorker)
            totalTasks = Me.PreviousForm.Tasks.Count
            For Each task In Me.PreviousForm.Tasks
                currentTask += 1
                If Not .CancellationPending Then
                    operations = task.GetOperations(.CancellationPending)
                    totalOperations = operations.Count
                    For Each operation In operations
                        currentOperation += 1
                        If Not .CancellationPending Then
                            AddHandler operation.SyncOperationStarted, AddressOf worker_SyncOperationStarted
                            AddHandler operation.SyncOperationProgressed, AddressOf worker_SyncOperationProgressed
                            AddHandler operation.SyncOperationFinished, AddressOf worker_SyncOperationFinished
                            operation.Execute(.CancellationPending)
                        Else
                            e.Cancel = True
                            Exit For
                        End If
                    Next
                Else
                    e.Cancel = True
                    Exit For
                End If
            Next
        End With
    End Sub

    Private Sub worker_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles worker.RunWorkerCompleted
        AppendToRichTextBox(IIf(Not e.Cancelled, "Syncing finished", "Syncing canceled"))
        Me.Text = IIf(Not e.Cancelled, "Syncing Finished", "Syncing Canceled")
        btnCancel.Text = "Close"
        btnCancel.Enabled = True
    End Sub

    Private Sub worker_SyncOperationStarted(ByVal operation As SyncOperation)
        Dim currentPostion As String = "[" & currentTask.ToString("D" & totalTasks.ToString().Length.ToString()) & "/" & totalTasks.ToString() & "]  " & _
                                       "[" & currentOperation.ToString("D" & totalOperations.ToString().Length.ToString()) & "/" & totalOperations.ToString() & "]  "
        Select Case operation.Operation
            Case FileOperation.Add
                AppendToRichTextBox(currentPostion & "Adding " & operation.RelativeFilePath)
            Case FileOperation.Replace
                AppendToRichTextBox(currentPostion & "Replacing " & operation.RelativeFilePath)
            Case FileOperation.Remove
                AppendToRichTextBox(currentPostion & "Removing " & operation.RelativeFilePath)
            Case FileOperation.None
                AppendToRichTextBox(currentPostion & "Ignoring " & operation.RelativeFilePath)
        End Select
    End Sub

    Private Sub worker_SyncOperationProgressed(ByVal bytesTotal As Long, ByVal bytesTransferred As Long)
        Dim callback As SyncOperationProgressedCallback
        If pbrOperation.InvokeRequired Then
            callback = New SyncOperationProgressedCallback(AddressOf worker_SyncOperationProgressed)
            Me.Invoke(callback, New Object() {bytesTotal, bytesTransferred})
        Else
            If pbrOperation.Maximum <> bytesTotal Then
                pbrOperation.Maximum = bytesTotal
            End If
            pbrOperation.Value = bytesTransferred
        End If
    End Sub

    Private Sub worker_SyncOperationFinished(ByVal operation As SyncOperation, ByVal e As SyncOperationFinishedEventArgs)
        If operation.Exemption IsNot Nothing Then
            AppendToRichTextBox(" exempted: " & operation.Exemption.ToString() & vbCrLf)
        Else
            If Not e.Canceled AndAlso Not e.Failed Then
                AppendToRichTextBox(" succeeded" & vbCrLf)
            ElseIf e.Failed Then
                AppendToRichTextBox(" failed: " & e.FailureReason.Message.Replace(vbCr, Space(1)).Replace(vbLf, Space(1)) & vbCrLf)
            ElseIf e.Canceled Then
                AppendToRichTextBox(" cancelled" & vbCrLf)
            End If
        End If
    End Sub

#End Region

End Class