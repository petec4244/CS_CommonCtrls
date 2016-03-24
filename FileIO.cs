using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace CommonCtrls
{
    class File_IO
    {

        public string fileName_pub = "";
        ///<summary>
        ///Get the current working path
        ///</summary>
        public string GetAppWorkDir()
        {
            return Directory.GetCurrentDirectory();
        }

        public bool FileExists(string fileName)
        {
            return File.Exists(fileName);
        }

        public bool DeleteFile(string fileName)
        {
            try
            {
                File.Delete(fileName);
                return true;
            }
            catch (IOException)
            {
                return false;
            }
        }

        public bool WriteTxtAryFile(string[] texttowrite, string fileFullPath)
        {
            try
            {
                TextWriter Tw = new StreamWriter(fileFullPath);
                foreach (string S in texttowrite)
                {
                    Tw.WriteLine(S);
                }
                Tw.Close();
                return true;
            }
            catch (IOException)
            {
                return false;
            }
        }

        #region FileInformation
        public string FilePath(string filename)
        {
            return Path.GetDirectoryName(filename);
        }

        public FileInfo GetFileInfo(string filename)
        {
            return new FileInfo(filename);
        }

        //get the file extension
        public string GetFileExt(string filename)
        {
            FileInfo F = GetFileInfo(filename);
            return F.Extension;
        }

        //get the full file path
        public string GetFullNamePath(string filename)
        {
            FileInfo F = GetFileInfo(filename);
            return F.FullName;
        }

        //get the file size
        public string GetFileSize(string filename)
        {
            FileInfo F = GetFileInfo(filename);
            return F.Length.ToString();
        }

        public bool GetReadOnly(string filename)
        {
            FileInfo F = GetFileInfo(filename);
            return F.IsReadOnly;
        }

        #endregion

        public DialogResult openFolderDialog()
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();
            return result;
        }

        public bool saveFile(string fileType)
        {
            DialogResult DR = save_FileDialog(fileType);
            if (DR == DialogResult.OK) return true; //chosen file will be fileName_p
            else return false;
        }

        private DialogResult save_FileDialog(string fileType)
        {
            var saveFile = new SaveFileDialog();
            saveFile.Filter = fileType;
            DialogResult result = saveFile.ShowDialog();
            if (result == DialogResult.OK)
            {
                fileName_pub = saveFile.FileName;
            }
            return result;
        }

        //Open a file using the default program for it's extension
        public void OpenFileExternal(string filename)
        {
            System.Diagnostics.Process.Start(filename);
        }

        public List<string> ReadFileLineByLine(string filename)
        {
            List<string> LinesRead = new List<string>();
            foreach (string line in File.ReadLines(filename))
            {
                LinesRead.Add(line);
            }
            return LinesRead;
        }
    }
}
