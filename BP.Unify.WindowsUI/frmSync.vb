Imports System.ComponentModel
Imports System.Text
Imports System.Threading

Public Class frmSync

    Private currentTask, currentOperation As Long
    Private totalTasks, totalOperations As Long
    Private totalSucceeded, totalFailed, totalIgnored As Long
    Private WithEvents worker As BackgroundWorker

    Delegate Sub AppendToRichTextBoxCallback(ByVal text As String)
    Delegate Sub UpdateCounterLabelCallback(ByRef lbl As Label, ByVal value As Integer)
    Delegate Sub UpdatePercentageLabelCallback(ByRef lbl As Label, ByVal value As Decimal)

#Region "PROPERTIES"

    Public Property PreviousForm As frmMain

#End Region

#Region "METHODS"

    Private Sub AppendToRichTextBox(ByVal text As String)
        Dim callback As AppendToRichTextBoxCallback
        If rtbConsole.InvokeRequired Then
            callback = New AppendToRichTextBoxCallback(AddressOf AppendToRichTextBox)
            Me.Invoke(callback, New Object() {text})
        Else
            rtbConsole.AppendText(text)
        End If
    End Sub

    Private Sub UpdateCounterLabel(ByRef lbl As Label, ByVal value As Integer)
        Dim callback As UpdateCounterLabelCallback
        If lbl.InvokeRequired Then
            callback = New UpdateCounterLabelCallback(AddressOf UpdateCounterLabel)
            Me.Invoke(callback, New Object() {lbl, value})
        Else
            lbl.Text = value.ToString("N0")
        End If
    End Sub

    Private Sub UpdatePercentageLabel(ByRef lbl As Label, ByVal value As Decimal)
        Dim callback As UpdatePercentageLabelCallback
        If lbl.InvokeRequired Then
            callback = New UpdatePercentageLabelCallback(AddressOf UpdatePercentageLabel)
            Me.Invoke(callback, New Object() {lbl, value})
        Else
            lbl.Text = (value * 100).ToString("N0") & "%"
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
                    AppendToRichTextBox("Started task at " & Now.ToString("HH:mm:ss:fff") & vbCrLf)
                    AddHandler task.SyncStatusChanged, AddressOf worker_SyncStatusChanged
                    operations = task.GetOperations(.CancellationPending)
                    totalOperations = operations.Count
                    currentOperation = 0
                    For Each operation In operations
                        currentOperation += 1
                        UpdatePercentageLabel(lblFileProgress, 0)
                        If Not .CancellationPending Then
                            AddHandler operation.SyncOperationStarted, AddressOf worker_SyncOperationStarted
                            AddHandler operation.SyncOperationProgressed, AddressOf worker_SyncOperationProgressed
                            AddHandler operation.SyncOperationFinished, AddressOf worker_SyncOperationFinished
                            operation.Execute(.CancellationPending)
                            RemoveHandler operation.SyncOperationStarted, AddressOf worker_SyncOperationStarted
                            RemoveHandler operation.SyncOperationProgressed, AddressOf worker_SyncOperationProgressed
                            RemoveHandler operation.SyncOperationFinished, AddressOf worker_SyncOperationFinished
                        Else
                            e.Cancel = True
                            Exit For
                        End If
                        UpdatePercentageLabel(lblTaskProgress, currentOperation / totalOperations)
                    Next
                Else
                    e.Cancel = True
                    Exit For
                End If
                UpdatePercentageLabel(lblRoutineProgress, currentTask / totalTasks)
                RemoveHandler task.SyncStatusChanged, AddressOf worker_SyncStatusChanged
                If Not e.Cancel Then
                    AppendToRichTextBox("Finished task at " & Now.ToString("HH:mm:ss:fff") & vbCrLf)
                End If
            Next
        End With
    End Sub

    Private Sub worker_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles worker.RunWorkerCompleted
        Me.Text = IIf(Not e.Cancelled, "Syncing Finished", "Syncing Canceled")
        btnCancel.Text = "Close"
        btnCancel.Enabled = True
    End Sub

    Private Sub worker_SyncStatusChanged(ByVal status As String)
        AppendToRichTextBox(status & vbCrLf)
    End Sub

    Private Sub worker_SyncOperationStarted(operation As SyncOperation)
        Select Case operation.Operation
            Case FileOperation.Add
                AppendToRichTextBox("Adding " & operation.RelativeFilePath)
            Case FileOperation.Replace
                AppendToRichTextBox("Replacing " & operation.RelativeFilePath)
            Case FileOperation.Remove
                AppendToRichTextBox("Removing " & operation.RelativeFilePath)
            Case FileOperation.None
                AppendToRichTextBox("Ignoring " & operation.RelativeFilePath)
        End Select
    End Sub

    Private Sub worker_SyncOperationProgressed(bytesTotal As Long, bytesTransferred As Long)
        If bytesTotal > 0 AndAlso bytesTransferred > 0 Then
            UpdatePercentageLabel(lblFileProgress, bytesTransferred / bytesTotal)
        End If
    End Sub

    Private Sub worker_SyncOperationFinished(operation As SyncOperation, e As SyncOperationFinishedEventArgs)
        If operation.Exemption Is Nothing Then
            If Not e.Canceled AndAlso Not e.Failed Then
                AppendToRichTextBox(" succeeded" & vbCrLf)
                totalSucceeded += 1
                UpdateCounterLabel(lblTotalSucceeded, totalSucceeded)
            ElseIf e.Failed Then
                AppendToRichTextBox(" failed" & " -- " & "Failure reason: " & e.FailureReason.Message.Replace(vbCr, Space(1)).Replace(vbLf, Space(1)) & vbCrLf)
                totalFailed += 1
                UpdateCounterLabel(lblTotalFailed, totalFailed)
            ElseIf e.Canceled Then
                AppendToRichTextBox(" cancelled" & vbCrLf)
            End If
        Else
            AppendToRichTextBox(" -- " & "Exemption reason: " & operation.Exemption.ToString() & vbCrLf)
            totalIgnored += 1
            UpdateCounterLabel(lblTotalIgnored, totalIgnored)
        End If
        UpdatePercentageLabel(lblFileProgress, 1)
    End Sub

#End Region

End Class