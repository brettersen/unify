Imports System.IO

Public Enum FileOperation
    None
    Add
    Replace
    Remove
End Enum

Public Class SyncOperation

    Private _sourceFilePath As String
    Private _targetFilePath As String
    Private _relativeFilePath As String
    Private _operation As FileOperation
    Private _exemption As SyncTaskExemption

    Public Event SyncOperationStarted(ByVal operation As SyncOperation)
    Public Event SyncOperationProgressed(ByVal bytesTotal As Long, ByVal bytesTransferred As Long)
    Public Event SyncOperationFinished(ByVal operation As SyncOperation, ByVal e As SyncOperationFinishedEventArgs)

    Friend Sub New()

    End Sub

#Region "PROPERTIES"

    Public Property SourceFilePath As String
        Get
            Return _sourceFilePath
        End Get
        Friend Set(value As String)
            _sourceFilePath = value
        End Set
    End Property

    Public Property TargetFilePath As String
        Get
            Return _targetFilePath
        End Get
        Friend Set(value As String)
            _targetFilePath = value
        End Set
    End Property

    Public Property RelativeFilePath As String
        Get
            Return _relativeFilePath
        End Get
        Friend Set(value As String)
            _relativeFilePath = value
        End Set
    End Property

    Public Property Operation As FileOperation
        Get
            Return _operation
        End Get
        Friend Set(value As FileOperation)
            _operation = value
        End Set
    End Property

    Public Property Exemption As SyncTaskExemption
        Get
            Return _exemption
        End Get
        Friend Set(value As SyncTaskExemption)
            _exemption = value
        End Set
    End Property

#End Region

#Region "METHODS"

    Private Function CopyProgress(ByVal totalFileSize As Long, _
                                  ByVal totalBytesTransferred As Long, _
                                  ByVal streamSize As Long, _
                                  ByVal streamBytesTransferred As Long, _
                                  ByVal streamNumber As UInteger,
                                  ByVal callbackReason As Win32API.CopyProgressCallbackReason, _
                                  ByVal sourceFile As IntPtr,
                                  ByVal destinationFile As IntPtr,
                                  ByVal data As IntPtr) As Win32API.CopyProgressResult
        RaiseEvent SyncOperationProgressed(totalFileSize, totalBytesTransferred)
        Return Win32API.CopyProgressResult.PROGRESS_CONTINUE
    End Function

    Private Sub CreateParentDirectory()
        Dim sourceParentDirectory As String = Path.GetDirectoryName(Me.SourceFilePath)
        Dim targetParentDirectory As String = Path.GetDirectoryName(Me.TargetFilePath)
        If Not Directory.Exists(targetParentDirectory) Then
            Directory.CreateDirectory(targetParentDirectory)
            File.SetAttributes(targetParentDirectory, File.GetAttributes(sourceParentDirectory))
        End If
    End Sub

    Private Sub DeleteParentDirectory()
        Dim parentDirectory As New DirectoryInfo(Path.GetDirectoryName(Me.TargetFilePath))
        If (Not parentDirectory.EnumerateDirectories().Any()) AndAlso (Not parentDirectory.EnumerateFiles().Any()) Then
            parentDirectory.Delete(False)
        End If
    End Sub

    Public Sub Execute(ByRef stopRequested As Boolean)
        Try
            RaiseEvent SyncOperationStarted(Me)
            Select Case Me.Operation
                Case FileOperation.Add
                    CreateParentDirectory()
                    Win32API.CopyFileEx(Me.SourceFilePath, Me.TargetFilePath, AddressOf CopyProgress, Nothing, stopRequested, Nothing)
                Case FileOperation.Replace
                    Win32API.CopyFileEx(Me.SourceFilePath, Me.TargetFilePath, AddressOf CopyProgress, Nothing, stopRequested, Nothing)
                Case FileOperation.Remove
                    File.SetAttributes(Me.TargetFilePath, FileAttributes.Normal)
                    File.Delete(Me.TargetFilePath)
                    If Path.GetDirectoryName(Me.TargetFilePath) <> Me.TargetFilePath.Replace(Path.DirectorySeparatorChar & Me.RelativeFilePath, Space(0)) Then
                        DeleteParentDirectory()
                    End If
            End Select
            If Not stopRequested Then
                RaiseEvent SyncOperationFinished(Me, New SyncOperationFinishedEventArgs())
            Else
                RaiseEvent SyncOperationFinished(Me, New SyncOperationFinishedEventArgs(True))
            End If
        Catch ex As Exception
            RaiseEvent SyncOperationFinished(Me, New SyncOperationFinishedEventArgs(False, True, ex))
        End Try
    End Sub

#End Region

End Class

Public Class SyncOperationFinishedEventArgs

    Friend Sub New(Optional ByVal canceled As Boolean = False, _
                   Optional ByVal failed As Boolean = False, _
                   Optional ByVal failureReason As Exception = Nothing)
        Me.Canceled = canceled
        Me.Failed = failed
        Me.FailureReason = failureReason
    End Sub

    Public Property Canceled As Boolean
    Public Property Failed As Boolean
    Public Property FailureReason As Exception

End Class
