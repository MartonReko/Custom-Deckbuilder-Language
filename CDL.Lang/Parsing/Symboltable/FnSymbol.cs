using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace CDL.Lang.Parsing.Symboltable;

// Currently has no use since props have no parameters
public class FnSymbol : Symbol
{
    public List<CDLType> Properties { get; set; } = [];
    public Dictionary<string, Symbol> parameters = [];
    private readonly ILogger<FnSymbol> _logger = LoggerFactory.Create(builder => builder.AddNLog().SetMinimumLevel(LogLevel.Trace)).CreateLogger<FnSymbol>();
    public FnSymbol(string name, CDLType type) : base(name, type)
    {
    }
    public void AddParam(string name, Symbol varSymbol)
    {
        if (!parameters.TryAdd(name, varSymbol))
        {
            _logger.LogError("Parameter {name} is already in scope", name);
        }
    }
    public override string ToString()
    {
        if (Properties == null)
        {
            return $"{Name}() : {Type.Name}";
        }
        else
        {
            return $"{Name}({PropsToString()}) : {Type.Name}"; ;
        }
    }
    private string PropsToString()
    {
        string p = "";
        for (int i = 0; i < parameters.Count; i++)
        {
            if (i > 0)
            {

            p += $", {parameters.ElementAt(i).Key} : {parameters.ElementAt(i).Value.Type.Name}";
            }
            else
            {
            p += $"{parameters.ElementAt(i).Key} : {parameters.ElementAt(i).Value.Type.Name}";
            }
        }
        return p;
    }
}