using GrupoBIOS_PEDWEB.PWA.Helpers.Interfaces;
using System.Reflection;

namespace GrupoBIOS_PEDWEB.PWA.Helpers
{
    public class CustomLoggerProvider : ILoggerProvider
    {
        private readonly ILogModel LogModel;

        public CustomLoggerProvider(ILogModel LogModel)
        {
            this.LogModel = LogModel;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new CustomLogger(LogModel);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }

    public class CustomLogger : ILogger
    {
        private readonly ILogModel LogModel;

        public CustomLogger(ILogModel LogModel)
        {
            this.LogModel = LogModel;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            string BuildInfo;
            Assembly curAssembly = typeof(Program).Assembly;
            BuildInfo = $"{curAssembly.GetCustomAttributes(false).OfType<AssemblyTitleAttribute>().FirstOrDefault().Title}";
            LogModel.RegistrarLog($"{formatter(state, exception)}, Fecha = {DateTime.Now} UTC, " +
                $"Fecha ultima actualizacion= {BuildInfo}, Gravedad = {logLevel}");
        }
    }
}
