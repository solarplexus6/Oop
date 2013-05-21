using System;
using System.Linq;

namespace Zad1
{
    public class CreateRandomFileCommand : ICommand
    {
        private const int FILE_LENGTH = 1024;

        private readonly string _fileName;
        private readonly FileManager _fileManager;

        public CreateRandomFileCommand(FileManager fileManager, string fileName)
        {
            _fileManager = fileManager;
            _fileName = fileName;
        }

        public void Execute()
        {
            var rand = new Random((int) DateTime.Now.Ticks);
            var randomContent = new string(Enumerable.Repeat(' ', FILE_LENGTH).Select(c => (char)rand.Next(' ', '~')).ToArray());
            _fileManager.CreateFile(_fileName, randomContent);
        }
    }
}