using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace CDL;

public class VisGlobalVars(EnvManager em) : CDLBaseVisitor<object>
{
    private readonly ILogger<VisGlobalVars> _logger = LoggerFactory.Create(builder => builder.AddNLog()).CreateLogger<VisGlobalVars>(); 
    private void AddSymbolToTable(string typeName,ParserRuleContext varNameContext){
        var type = em.Ts[typeName];
        var symbol = new Symbol(varNameContext.GetText(), type);
        em.AddVariableToScope(varNameContext, symbol);
    }
    private readonly List<CDLType> localProps = [];

    public override object VisitProgram([NotNull] CDLParser.ProgramContext context)
    {
        em.Env = new Env();
        em.Ts = new TypeSystem();
        var result = base.VisitProgram(context);
        return result;
    }
    public override object VisitConfigBlock([NotNull] CDLParser.ConfigBlockContext context)
    {
        _logger.LogInformation("Config block \"{name}\" visited, Env:\n{env}", context.GetChild(0).GetType(), em.Env.ToString());
        return base.VisitConfigBlock(context);
    }
    public override object VisitVariableDeclaration([NotNull] CDLParser.VariableDeclarationContext context)
    {
        var varName = context.varName().GetText();

        var type = em.Ts[context.typeName().GetText()];
        if (type == null)
        {
            _logger.LogError("Unknown type \"{type}\" at {pos}", context.typeName().GetText(), EnvManager.GetPos(context));
        }
        else
        {
            if (context.literalExpression() == null)
            {
                _logger.LogError("Undefined variable, this should not happen");
            }
            else
            {
                var expressionType = em.GetType(context.literalExpression());
                if (!expressionType.InheritsFrom(type))
                {
                    _logger.LogError("Error at {pos}, type {type} of expression is not compatible with the type {typeName} of the variable", EnvManager.GetPos(context.literalExpression()), expressionType.Name, type.Name);
                }
                else
                {
                    var symbol = new Symbol(context.varName().GetText(), type);
                    em.AddVariableToScope(context.varName(), symbol);
                }
            }
        }

        _logger.LogInformation("Var declaration visited, enviroment:\n{env}", em.Env.ToString());
        return base.VisitVariableDeclaration(context);
    }
    public override object VisitParamsDef([NotNull] CDLParser.ParamsDefContext context)
    {
        //string propsString = "";
        //List<CDLType> typesInProps = new List<CDLType>();
        foreach (var item in context.typeName())
        {
            if (item != null)
            {
                //propsString += item.GetText();
                //typesInProps.Add(em.Ts[item.GetText()]);
                CDLType sym = em.Ts[item.GetText()];
                System.Console.WriteLine("\n\tHali"+sym);
                localProps.Add(sym);
            }
        }
        return base.VisitParamsDef(context);
    }
    public override object VisitEffectDefinition([NotNull] CDLParser.EffectDefinitionContext context)
    {
        var result = base.VisitEffectDefinition(context);

        var type = em.Ts[context.GetChild(0).GetText()];
        string symbolText = context.varName().GetText();
        //List<CDLType> props = new List<CDLType>();
/*         if (context.paramsDef() != null)
        {
            // TODO 
            // Kétszer lesz bejárva
            // Nem lehetne ezt az egészet átteni?
            props = VisitParamsDef(context.paramsDef());
        } */
        //props = localProps;
        var symbol = new Symbol(symbolText, type, localProps);
        em.AddVariableToScope(context.varName(), symbol);

        localProps.Clear();
        _logger.LogInformation("Effect definition visited, enviroment:\n{env}", em.Env.ToString());
        return result;
    }
    public override object VisitCardDefinition([NotNull] CDLParser.CardDefinitionContext context)
    {
        AddSymbolToTable(context.GetChild(0).GetText(), context.varName());
        return base.VisitCardDefinition(context);
    }

    // Rarity names are parsed as strings, later nodes can refer to cards by it
    public override object VisitRarityName([NotNull] CDLParser.RarityNameContext context)
    {
        var type = em.Ts["string"];
        var symbol = new Symbol(context.GetText(), type);
        em.AddVariableToScope(context, symbol);
        return base.VisitRarityName(context);
    }
    public override object VisitStageDefinition([NotNull] CDLParser.StageDefinitionContext context)
    {
        AddSymbolToTable(context.GetChild(0).GetText(), context.varName());
        return base.VisitStageDefinition(context);
    }
    public override object VisitNodeDefinition([NotNull] CDLParser.NodeDefinitionContext context)
    {
        AddSymbolToTable(context.GetChild(0).GetText(), context.varName());
        return base.VisitNodeDefinition(context);
    }
    public override object VisitCharSetup([NotNull] CDLParser.CharSetupContext context)
    {
        AddSymbolToTable(context.GetChild(0).GetText(), context.varName());
        return base.VisitCharSetup(context);
    }
    public override object VisitEnemyDefinition([NotNull] CDLParser.EnemyDefinitionContext context)
    {
        AddSymbolToTable(context.GetChild(0).GetText(), context.varName());
        return base.VisitEnemyDefinition(context);
    }
}