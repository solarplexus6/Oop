namespace Zad1
{
    public class CopyFileCommand : ICommand
    {
        private readonly FileManager _fileManager;
        private readonly string _srcFileName;
        private readonly string _destFileName;

        public CopyFileCommand(FileManager fileManager, string srcFileName, string destFileName)
        {
            _fileManager = fileManager;
            _srcFileName = srcFileName;
            _destFileName = destFileName;
        }


        public void Execute()
        {
            _fileManager.CopyFile(_srcFileName, _destFileName);
        }
    }
}