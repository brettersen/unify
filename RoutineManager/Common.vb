Imports System.IO
Imports System.Reflection

Public Module Common

    Public Const APP_NAME As String = "Unify"
    Public Const APP_FILE_EXTENSION As String = "uni"

    Public ExecutionPath As String = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
    Public ApplicationDataPath As String = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
    Public SaveFilePath As String = ApplicationDataPath & Path.DirectorySeparatorChar & APP_NAME

    Public Enum FormMode
        Adding
        Editing
        Viewing
    End Enum

End Module
