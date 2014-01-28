Imports System.ComponentModel
Imports System.Text
Imports System.Threading

Public Class frmSync

    Private currentTask, currentOperation As Long
    Private totalTasks, totalOperations As Long
    Private totalSucceeded, totalFailed, totalIgnored As Long
    Private WithEvents worker As BackgroundWorker

    Delegate Sub AppendToRichTextBoxCallback(ByVal text As String)
    Delegate Sub SetProgressBarPropertyCallback(ByRef pbr As ProgressBar, ByVal value As Long)

#Region "PROPERTIES"

    Public Property PreviousForm As frmTasks

    Private ReadOnly Property TaskPosition As String
        Get
            Return "[" & currentTask.ToString("D" & totalTasks.ToString().Length.ToString()) & "/" & totalTasks.ToString() & "] "
        End Get
    End Property

    Private ReadOnly Property OperationPosition As String
        Get
            Return "[" & currentOperation.ToString("D" & totalOperations.ToString().Length.ToString()) & "/" & totalOperations.ToString() & "] "
        End Get
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

    Private Sub SetProgressBarMaximum(ByRef pbr As ProgressBar, ByVal value As Integer)
        Dim callback As SetProgressBarPropertyCallback
        If pbr.InvokeRequired Then
            callback = New SetProgressBarPropertyCallback(AddressOf SetProgressBarMaximum)
            Me.Invoke(callback, New Object() {pbr, value})
        Else
            pbr.Maximum = value
        End If
    End Sub

    Private Sub SetProgressBarValue(ByRef pbr As ProgressBar, ByVal value As Integer)
        Dim callback As SetProgressBarPropertyCallback
        If pbr.InvokeRequired Then
            callback = New SetProgressBarPropertyCallback(AddressOf SetProgressBarValue)
            Me.Invoke(callback, New Object() {pbr, value})
        Else
            pbr.Value = value
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
            SetProgressBarMaximum(pbrRoutine, totalTasks)
            For Each task In Me.PreviousForm.Tasks
                currentTask += 1
                If Not .CancellationPending Then
                    AppendToRichTextBox(Me.TaskPosition & "Started task at " & Now.ToLongTimeString & vbCrLf)
                    AddHandler task.SyncTaskMilestoneReached, AddressOf worker_SyncTaskMilestoneReached
                    operations = task.GetOperations(.CancellationPending)
                    totalOperations = operations.Count
                    SetProgressBarMaximum(pbrTask, totalOperations)
                    SetProgressBarValue(pbrTask, 0)
                    currentOperation = 0
                    For Each operation In operations
                        currentOperation += 1
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
                        SetProgressBarValue(pbrTask, pbrTask.Value + 1)
                        Thread.Sleep(6)
                    Next
                Else
                    e.Cancel = True
                    Exit For
                End If
                SetProgressBarValue(pbrRoutine, pbrRoutine.Value + 1)
                RemoveHandler task.SyncTaskMilestoneReached, AddressOf worker_SyncTaskMilestoneReached
                AppendToRichTextBox(Me.TaskPosition & "Finished task at " & Now.ToLongTimeString & vbCrLf)
            Next
        End With
    End Sub

    Private Sub worker_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles worker.RunWorkerCompleted
        If pbrFile.Value <> pbrFile.Maximum Then
            SetProgressBarValue(pbrFile, pbrFile.Maximum)
        End If
        Me.Text = IIf(Not e.Cancelled, "Syncing Finished", "Syncing Canceled")
        lblTotalSucceeded.Text = totalSucceeded.ToString("N0")
        lblTotalFailed.Text = totalFailed.ToString("N0")
        lblTotalIgnored.Text = totalIgnored.ToString("N0")
        btnCancel.Text = "Close"
        btnCancel.Enabled = True
    End Sub

    Private Sub worker_SyncTaskMilestoneReached(milestone As SyncTaskMilestone)
        Select Case milestone
            Case SyncTaskMilestone.DeterminingSourceFiles
                AppendToRichTextBox(Me.TaskPosition & "Determining source files..." & vbCrLf)
            Case SyncTaskMilestone.DeterminingTargetFiles
                AppendToRichTextBox(Me.TaskPosition & "Determining target files..." & vbCrLf)
            Case SyncTaskMilestone.DeterminingFilesToAdd
                AppendToRichTextBox(Me.TaskPosition & "Determining files to add..." & vbCrLf)
            Case SyncTaskMilestone.DeterminingFilesToReplace
                AppendToRichTextBox(Me.TaskPosition & "Determining files to replace..." & vbCrLf)
            Case SyncTaskMilestone.DeterminingFilesToRemove
                AppendToRichTextBox(Me.TaskPosition & "Determining files to remove..." & vbCrLf)
        End Select
    End Sub

    Private Sub worker_SyncOperationStarted(operation As SyncOperation)
        Select Case operation.Operation
            Case FileOperation.Add
                AppendToRichTextBox(Me.TaskPosition & Me.OperationPosition & "Adding " & operation.RelativeFilePath)
            Case FileOperation.Replace
                AppendToRichTextBox(Me.TaskPosition & Me.OperationPosition & "Replacing " & operation.RelativeFilePath)
            Case FileOperation.Remove
                AppendToRichTextBox(Me.TaskPosition & Me.OperationPosition & "Removing " & operation.RelativeFilePath)
            Case FileOperation.None
                AppendToRichTextBox(Me.TaskPosition & Me.OperationPosition & "Ignoring " & operation.RelativeFilePath)
        End Select
    End Sub

    Private Sub worker_SyncOperationProgressed(bytesTotal As Long, bytesTransferred As Long)
        If pbrFile.Maximum <> bytesTotal Then
            SetProgressBarMaximum(pbrFile, bytesTotal)
        End If
        SetProgressBarValue(pbrFile, bytesTransferred)
    End Sub

    Private Sub worker_SyncOperationFinished(operation As SyncOperation, e As SyncOperationFinishedEventArgs)
        If operation.Exemption Is Nothing Then
            If Not e.Canceled AndAlso Not e.Failed Then
                AppendToRichTextBox(" succeeded" & vbCrLf)
                totalSucceeded += 1
            ElseIf e.Failed Then
                AppendToRichTextBox(" failed" & " -- " & "Failure reason: " & e.FailureReason.Message.Replace(vbCr, Space(1)).Replace(vbLf, Space(1) & vbCrLf))
                totalFailed += 1
            ElseIf e.Canceled Then
                AppendToRichTextBox(" cancelled" & vbCrLf)
            End If
        Else
            AppendToRichTextBox(" -- " & "Exemption reason: " & operation.Exemption.ToString() & vbCrLf)
            totalIgnored += 1
        End If
    End Sub

#End Region

End Class