Imports System.IO

Public Enum OperationType
    Create
    Update
    Delete
End Enum

Public Class SyncOperation

    Public Sub New()

    End Sub

    Public Sub New(sourcePath As String, _
                   destinationPath As String, _
                   attributes As FileAttributes, _
                   operation As OperationType)

        Me.SourcePath = sourcePath
        Me.DestinationPath = destinationPath
        Me.Attributes = attributes
        Me.Operation = operation

    End Sub

    Public Property SourcePath As String
    Public Property DestinationPath As String
    Public Property Attributes As FileAttributes
    Public Property Operation As OperationType

    Public Function Execute() As Boolean

        Try
            Select Case Me.Operation
                Case OperationType.Create
                    CreateParentDirectory(Me.DestinationPath)
                    File.Copy(Me.SourcePath, Me.DestinationPath)
                    File.SetAttributes(Me.DestinationPath, Me.Attributes)
                Case OperationType.Update
                    File.Copy(Me.SourcePath, Me.DestinationPath, True)
                    File.SetAttributes(Me.DestinationPath, Me.Attributes)
                Case OperationType.Delete
                    File.Delete(Me.DestinationPath)
            End Select
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function

    Private Sub CreateParentDirectory(ByVal filePath As String)

        Dim parentDirectory As String = Path.GetDirectoryName(filePath)

        If Not Directory.Exists(parentDirectory) Then
            Directory.CreateDirectory(parentDirectory)
        End If

    End Sub

End Class
