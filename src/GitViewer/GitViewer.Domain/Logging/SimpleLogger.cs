using System;
using System.Diagnostics;

namespace GitViewer.Domain.Logging
{
    public class SimpleLogger : ILogger
    {
        public void Error(string message, params object[] data)
        {
            Log(message);
        }

        public void Error(Exception exception, params object[] data)
        {
            Log(exception.Message);
        }

        public void Information(string message, params object[] data)
        {
            Log(message);
        }

        public void Information(Exception exception, params object[] data)
        {
            Log(exception.Message);
        }

        public void Warning(string message, params object[] data)
        {
            Log(message);
        }

        public void Warning(Exception exception, params object[] data)
        {
            Log(exception.Message);
        }

        /// <summary>
        /// This is just to get some logging in the application that I can test. I dont think
        /// </summary>
        /// <param name="message"></param>
        private void Log(string message)
        {
            Debug.WriteLine(message);
        }
    }
}
