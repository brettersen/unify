Imports System.Runtime.InteropServices

Public Class Win32API

    <Flags()> _
    Public Enum CopyFileFlags As UInteger
        COPY_FILE_ALLOW_DECRYPTED_DESTINATION = &H8
        COPY_FILE_COPY_SYMLINK = &H800
        COPY_FILE_FAIL_IF_EXISTS = &H1
        COPY_FILE_NO_BUFFERING = &H1000
        COPY_FILE_OPEN_SOURCE_FOR_WRITE = &H4
        COPY_FILE_RESTARTABLE = &H2
    End Enum

    Public Enum CopyProgressCallbackReason As UInteger
        CALLBACK_CHUNK_FINISHED = &H0
        CALLBACK_STREAM_SWITCH = &H1
    End Enum

    Public Enum CopyProgressResult As UInteger
        PROGRESS_CANCEL = 1
        PROGRESS_CONTINUE = 0
        PROGRESS_STOP = 2
        PROGRESS_QUIET = 3
    End Enum

    Public Declare Auto Function CopyFileEx Lib "kernel32.dll" (ByVal existingFileName As String, _
                                                                 ByVal newFileName As String, _
                                                                 ByVal progressRoutine As CopyProgressRoutine, _
                                                                 ByVal data As IntPtr, _
                                                                 ByRef cancel As Boolean,
                                                                 ByVal copyFlags As CopyFileFlags) As <MarshalAs(UnmanagedType.Bool)> Boolean

    Public Delegate Function CopyProgressRoutine(ByVal totalFileSize As Long, _
                                                  ByVal totalBytesTransferred As Long, _
                                                  ByVal streamSize As Long, _
                                                  ByVal streamBytesTransferred As Long, _
                                                  ByVal streamNumber As UInteger,
                                                  ByVal callbackReason As CopyProgressCallbackReason, _
                                                  ByVal sourceFile As IntPtr,
                                                  ByVal destinationFile As IntPtr,
                                                  ByVal data As IntPtr) As CopyProgressResult

End Class
