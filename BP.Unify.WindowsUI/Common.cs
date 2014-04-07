using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BP.Unify.WindowsUI
{
    static class Common
    {
        public static const string APP_NAME = "Unify";
        public static const string APP_FILE_EXTENSION = "uni";

        public static string ExecutionPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        public static string ApplicationDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        public static string SaveFilePath = ApplicationDataPath + Path.DirectorySeparatorChar + APP_NAME;

        public static enum FormEntryMode { Add, Edit }
        public static enum SelectionPosition { None, First, Previous, Current, Next, Last }

        public static DialogResult ShowError(string text, string caption)
        {
            return MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static DialogResult ShowWarning(string text, string caption)
        {
            return MessageBox.Show(text, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        }
    }
}
