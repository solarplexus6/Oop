using System;
using System.Collections.Generic;
using System.Threading;

namespace Zad1
{
    public class Invoker
    {
        private readonly bool _threaded;
        #region Private fields

        private readonly Queue<ICommand> _commandQueue = new Queue<ICommand>();
        private readonly Thread _primaryWorkThread;
        private readonly Thread _secondaryWorkThread;

        #endregion
        #region Ctors

        public Invoker(bool threaded = false)
        {
            _threaded = threaded;
            if (!threaded)
            {                
                return;
            }
            _primaryWorkThread = new Thread(ThreadWork);
            _secondaryWorkThread = new Thread(ThreadWork);

            _primaryWorkThread.Start();
            _secondaryWorkThread.Start();
        }

        #endregion
        #region Public methods

        public void Execute(ICommand command)
        {
            if (_threaded)
            {
                _commandQueue.Enqueue(command);
                return;                
            }
            
            command.Execute();
        }

        #endregion
        #region Private methods

        private void ThreadWork()
        {
            while (true)
            {
                if (_commandQueue.Count <= 0)
                {
                    Thread.Sleep(10);
                    continue;                    
                }
                ICommand command;
                lock (_commandQueue)
                {
                    try
                    {
                        command = _commandQueue.Dequeue();
                    }
                    catch (Exception)
                    {
                        Thread.Sleep(10);
                        continue;
                    }
                }
                command.Execute();
            }
        }

        #endregion
    }
}