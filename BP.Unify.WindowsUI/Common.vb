Imports System.IO
Imports System.Reflection

Public Module Common

    Public Const APP_NAME As String = "Unify"
    Public Const APP_FILE_EXTENSION As String = "uni"

    Public ExecutionPath As String = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
    Public ApplicationDataPath As String = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
    Public SaveFilePath As String = ApplicationDataPath & Path.DirectorySeparatorChar & APP_NAME

    Public Enum FormEntryMode
        Add
        Edit
    End Enum

    Public Enum SelectionPosition
        None
        First
        Previous
        Current
        [Next]
        Last
    End Enum

    Public Function ShowError(ByVal text As String, ByVal caption As String) As DialogResult
        Return MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Function

    Public Function ShowWarning(ByVal text As String, ByVal caption As String) As DialogResult
        Return MessageBox.Show(text, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
    End Function

End Module
