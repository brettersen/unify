Imports System.IO

Public Enum FileOperation
    Add
    Replace
    Remove
End Enum

Public Class SyncOperation

    Private _sourceFilePath As String
    Private _targetFilePath As String
    Private _attributes As FileAttributes
    Private _operation As FileOperation
    Private _isExempt As Boolean
    Private _exemptionReason As String

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

    Public Property Attributes As FileAttributes
        Get
            Return _attributes
        End Get
        Friend Set(value As FileAttributes)
            _attributes = value
        End Set
    End Property

    Public Property Operation As FileOperation
        Get
            Return _operation
        End Get
        Friend Set(value As FileOperation)

        End Set
    End Property

    Public Property IsExempt As Boolean
        Get
            Return _isExempt
        End Get
        Friend Set(value As Boolean)
            _isExempt = value
        End Set
    End Property

    Public Property ExemptionReason As String
        Get
            Return _exemptionReason
        End Get
        Friend Set(value As String)
            _exemptionReason = value
        End Set
    End Property

#End Region

#Region "METHODS"

    Private Sub CreateParentDirectory(ByVal filePath As String)

        Dim parentDirectory As String = Path.GetDirectoryName(filePath)

        If Not Directory.Exists(parentDirectory) Then
            Directory.CreateDirectory(parentDirectory)
        End If

    End Sub

    Public Function Execute() As Boolean

        Try
            Select Case Me.Operation
                Case FileOperation.Add
                    CreateParentDirectory(Me.TargetFilePath)
                    File.Copy(Me.SourceFilePath, Me.TargetFilePath)
                    File.SetAttributes(Me.TargetFilePath, Me.Attributes)
                Case FileOperation.Replace
                    File.Copy(Me.SourceFilePath, Me.TargetFilePath, True)
                    File.SetAttributes(Me.TargetFilePath, Me.Attributes)
                Case FileOperation.Remove
                    File.Delete(Me.TargetFilePath)
            End Select
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Overrides Function ToString() As String

        Return (Me.SourceFilePath & vbCrLf & _
                Me.TargetFilePath & vbCrLf & _
                Me.Operation.ToString() & vbCrLf & vbCrLf)

    End Function

#End Region

End Class
