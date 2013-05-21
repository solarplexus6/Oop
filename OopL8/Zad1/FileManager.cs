using System.IO;

namespace Zad1
{
    public class FileManager
    {
        #region Public methods

        public void CopyFile(string srcFileName, string destFileName)
        {
            File.Copy(srcFileName, destFileName);
        }

        public void CreateFile(string fileName, string content)
        {
            using (var fStream = File.CreateText(fileName))
            {
                fStream.Write(content);
            }
        }

        #endregion
    }
}