using System.Collections.Concurrent;

namespace APICatalogo.Logging;

public class CustomLoggerProvider : ILoggerProvider {
    readonly CustomLoggerProviderConfiguration loggerConfig;

    readonly ConcurrentDictionary<string, CostumerLogger> loggers =
               new ConcurrentDictionary<string, CostumerLogger>();

    public CustomLoggerProvider(CustomLoggerProviderConfiguration config) {
        loggerConfig = config;
    }
    public ILogger CreateLogger(string categoryName) {
        return loggers.GetOrAdd(categoryName, name => new CostumerLogger(name, loggerConfig));
    }
    public void Dispose() {
        loggers.Clear();
    }
}
