using Antlr4.Runtime;
using Microsoft.Extensions.Logging;

namespace CDL;

public class EnvManager(ILoggerFactory loggerFactory){

    private readonly ILogger<VisGlobalVars> _logger = loggerFactory.CreateLogger<VisGlobalVars>();
    public Env env {get;set;}
    public TypeSystem ts {get;set;}
    public static string GetPos(ParserRuleContext context)
    {
        return $"line #{context.Start.Line}, column #{context.Start.Column}";
    }
    public void AddVariableToScope(ParserRuleContext ctx, Symbol symbol)
    {
        try
        {
            env[symbol.Name] = symbol;
        }
        catch
        {
            _logger.LogError("Error at {pos}: variable {symName} is already in scope", GetPos(ctx), symbol.Name);
        }
    }
    public Symbol getVariableFromScope(ParserRuleContext context, string varName)
    {
        var symbol = env[varName];
        if (symbol != null) return symbol;
        _logger.LogError("Error at {pos}: name {varName} does not exist", GetPos(context), varName);
        return null;
    }
    public CDLType GetType(CDLParser.LiteralExpressionContext context)
    {
        if (context.varName() != null)
        {
            Symbol symbol = null;
            var varName = context.varName();
            symbol = getVariableFromScope(varName, varName.GetText());
            return symbol?.Type ?? ts.ERROR;
        }
        if (context.INT() != null)
            return ts.INT;
        if (context.boolean() != null)
            return ts.BOOLEAN;
        if (context.STRING() != null)
            return ts.STRING;
        if (context.DOUBLE() != null)
            return ts.DOUBLE;

        return null;
    }
}