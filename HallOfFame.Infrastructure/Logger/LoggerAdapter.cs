using HallOfFame.Core.ExternalServices;
using Microsoft.Extensions.Logging;

namespace HallOfFame.Infrastructure.Logger;

public class LoggerAdapter<T> : IAppLogger<T>
{
    private readonly FileLogger _fileLogger;
    private readonly ILogger<T> _logger;

    public LoggerAdapter(ILogger<T> logger,
        FileLogger fileLogger)
    {
        _logger = logger;
        _fileLogger = fileLogger;
    }

    public void LogError(string messageError)
    {
        _logger.LogError(messageError);
        _fileLogger.LogError(messageError);
    }
}