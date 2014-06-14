Imports System.Runtime.InteropServices

Public Module Win32

    <Flags()> _
    Public Enum CopyFileFlags As UInteger
        AllowDecryptedDestination = &H8
        CopySymlink = &H800
        FailIfExists = &H1
        NoBuffering = &H1000
        OpenSourceForWrite = &H4
        Restartable = &H2
    End Enum

    Public Enum CopyProgressCallbackReason As UInteger
        ChunkFinished = &H0
        StreamSwitch = &H1
    End Enum

    Public Enum CopyProgressResult As UInteger
        [Continue] = 0
        Cancel = 1
        [Stop] = 2
        Quiet = 3
    End Enum

    Public Enum FindExInfoLevels
        Standard = 0
        Basic = 1
    End Enum

    Public Enum FindExSearchOps
        NameMatch = 0
        LimitToDirectories = 1
        LimitToDevices = 2
    End Enum

    Public Enum FindExAdditionalFlags
        CaseSensitive = 1
        LargeFetch = 2
    End Enum

    <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Auto)> _
    Structure WIN32_FIND_DATA
        Public FileAttributes As System.IO.FileAttributes
        Public CreationTime As System.Runtime.InteropServices.ComTypes.FILETIME
        Public LastAccessTime As System.Runtime.InteropServices.ComTypes.FILETIME
        Public LastWriteTime As System.Runtime.InteropServices.ComTypes.FILETIME
        Public FileSizeHigh As UInteger
        Public FileSizeLow As UInteger
        Public Reserved0 As UInteger
        Public Reserved1 As UInteger
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=260)> Public FileName As String
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=14)> Public AlternateFileName As String
    End Structure

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

    <DllImport("kernel32.dll", CharSet:=CharSet.Auto)> _
    Public Function FindFirstFileEx(ByVal fileName As String, _
                                    ByVal infoLevelId As FindExInfoLevels, _
                                    ByRef findFileData As WIN32_FIND_DATA, _
                                    ByVal searchOp As FindExSearchOps, _
                                    ByVal searchFilter As Int32, _
                                    ByVal additionalFlags As FindExAdditionalFlags) As Int32
    End Function

    <DllImport("kernel32.dll", CharSet:=CharSet.Auto)> _
    Public Function FindNextFile(ByVal findFile As IntPtr, _
                                 ByRef findFileData As WIN32_FIND_DATA) As Boolean
    End Function

    <DllImport("kernel32.dll")> _
    Public Function FindClose(ByVal findFile As IntPtr) As Boolean
    End Function

End Module
