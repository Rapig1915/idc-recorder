using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ScreenRecorderLib;

namespace IDCRecorder
{
    public static class Helper
    {
        // Application global variables
        public const string AppName = "IDCRecorder";
        public static string HostName = System.Environment.MachineName;

        public static readonly string LocalPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), AppName);       

        #region "Util functions for helper"
        public static void ShowMsg(this string message, string title = "", MessageBoxIcon icon = MessageBoxIcon.None)
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, icon);
        }

        /// <summary>
        /// Create initial directories for the program
        /// </summary>
        public static void CreateDirectory()
        {
            if (!Directory.Exists(LocalPath))
                Directory.CreateDirectory(LocalPath);
        }        
        
        public static string RandomString(int length = 12)
        {
            const string pool = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var chars = Enumerable.Range(0, length)
                .Select(x => pool[Random.Next(0, pool.Length)]);
            return new string(chars.ToArray());
        }

        public static string GetNextFileName(String prefix, String extension = "")
        {
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
            return Path.Combine(LocalPath, prefix + timestamp + RandomString(3) + extension);
        }

        public static void ClearFolder(string FolderName)
        {
            DirectoryInfo dir = new DirectoryInfo(FolderName);

            foreach (FileInfo fi in dir.GetFiles())
            {
                fi.Delete();
            }

            foreach (DirectoryInfo di in dir.GetDirectories())
            {
                ClearFolder(di.FullName);
                di.Delete();
            }
        }

        public static void DeleteTempFiles()
        {
            ClearFolder(LocalPath);
        }        

        static Random Random = new Random();

        public static bool LoadD3DGear()
        {
            return GameRecorder.LoadD3DEngine() == 0;
        }
    }
    #endregion
}
