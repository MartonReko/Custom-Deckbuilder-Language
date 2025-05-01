using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace CDL.Exceptions;

public class CDLException
{
    private readonly ILogger<CDLException> _logger = LoggerFactory.Create(builder => builder.AddNLog().SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace)).CreateLogger<CDLException>();
    public int Line { get; }
    public int Column { get; }
    public string Message { get; }
    public CDLException(int line, int column, string message)
    {
        Line = line;
        Column = column;
        Message = message;
        _logger.LogError("{line}:{column} : {exception}",line,column,message);
    }
    public CDLException(string message)
    {
        Line = -1;
        Column = -1;
        Message = message;
        _logger.LogError("{exception}",message);
    }
    public override string ToString()
    {
        if (Line == -1 || Column == -1)
        {

            return $"{Message}";
        }
        else
        {
            return $"Line {Line}, column {Column}: {Message}";
        }
    }
}