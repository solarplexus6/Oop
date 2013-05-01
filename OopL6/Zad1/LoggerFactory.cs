using System;

namespace Zad1
{
    public class LoggerFactory
    {
        #region Private fields

        private static LoggerFactory _instance;

        #endregion
        #region Properties

        public static LoggerFactory Instance
        {
            get { return _instance ?? (_instance = new LoggerFactory()); }
        }

        #endregion
        #region Ctors

        private LoggerFactory()
        {
        }

        #endregion
        #region Public methods

        public ILogger GetLogger(LogType logType, string parameters = null)
        {
            ILogger logger = new NoneLogger();

            switch (logType)
            {
                case LogType.Console:
                    logger = new ConsoleLogger();
                    break;
                case LogType.File:
                    if (string.IsNullOrEmpty(parameters))
                    {
                        throw new ArgumentNullException();
                    }
                    logger = new FileLogger(parameters);
                    break;
            }

            return logger;
        }

        #endregion
    }
}