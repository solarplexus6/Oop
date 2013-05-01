using System;
using System.IO;

namespace Zad1
{
    public class FileLogger : ILogger
    {
        #region Private fields

        private readonly StreamWriter _writer;

        #endregion
        #region Ctors

        public FileLogger(string filePath)
        {
            _writer = new StreamWriter(filePath, true) {AutoFlush = true};
            AppDomain.CurrentDomain.ProcessExit += OnProcessExit;
        }

        #endregion
        #region Event triggers

        private void OnProcessExit(object sender, EventArgs eventArgs)
        {
            if (_writer != null)
            {
                _writer.Dispose();
            }
        }

        #endregion
        #region ILogger Members

        public void Log(string message)
        {
            _writer.WriteLine(message);            
        }

        #endregion
    }
}