using System;

namespace GitViewer.Domain.Logging
{
    public interface ILogger
    {
        void Information(string message, params object[] data);

        void Information(Exception exception, params object[] data);

        void Warning(string message, params object[] data);

        void Warning(Exception exception, params object[] data);

        void Error(string message, params object[] data);

        void Error(Exception exception, params object[] data);
    }
}