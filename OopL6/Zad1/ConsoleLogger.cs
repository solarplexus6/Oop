using System;

namespace Zad1
{
    public class ConsoleLogger : ILogger
    {
        #region ILogger Members

        public void Log(string message)
        {
            Console.WriteLine(message);
        }

        #endregion
    }
}