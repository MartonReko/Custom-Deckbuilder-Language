using Antlr4.Runtime.Misc;
using Microsoft.Extensions.Logging;

namespace CDL;

public class VisGlobalVars(ILoggerFactory loggerFactory, EnvManager em) : CDLBaseVisitor<object>
{
    private readonly ILogger<VisGlobalVars> _logger = loggerFactory.CreateLogger<VisGlobalVars>();

    public override object VisitProgram([NotNull] CDLParser.ProgramContext context)
    {
        em.env = new Env();
        em.ts = new TypeSystem();
        var result = base.VisitProgram(context);
        return result;
    }
    public override object VisitConfigBlock([NotNull] CDLParser.ConfigBlockContext context)
    {
        _logger.LogInformation("Config block \"{name}\" visited, Env:\n{env}", context.GetChild(0).GetType(), em.env.ToString());
        return base.VisitConfigBlock(context);
    }
    public override object VisitVariableDeclaration([NotNull] CDLParser.VariableDeclarationContext context)
    {
        var varName = context.varName().GetText();

        var type = em.ts[context.typeName().GetText()];
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

        _logger.LogInformation("Var declaration visited, enviroment:\n{env}", em.env.ToString());
        return base.VisitVariableDeclaration(context);
    }
    public override List<CDLType> VisitParamsDef([NotNull] CDLParser.ParamsDefContext context)
    {
        string propsString = "";
        List<CDLType> typesInProps = new List<CDLType>();
        foreach(var item in context.typeName()){
            if(item != null){
                propsString += item.GetText();
                typesInProps.Add(em.ts[item.GetText()]);
            }
        }
        return typesInProps;
    }
    public override object VisitEffectDefinition([NotNull] CDLParser.EffectDefinitionContext context)
    {
        var type = em.ts[context.GetChild(0).GetText()];
        string symbolText = context.varName().GetText();
        List<CDLType> props = new List<CDLType>();
        if(context.paramsDef() != null){
            props = VisitParamsDef(context.paramsDef());
        }
        var symbol = new Symbol(symbolText, type,props);
        em.AddVariableToScope(context.varName(), symbol);

        _logger.LogInformation("Effect definition visited, enviroment:\n{env}", em.env.ToString());
        return base.VisitEffectDefinition(context);
    }
    public override object VisitCardDefinition([NotNull] CDLParser.CardDefinitionContext context)
    {
        var type = em.ts[context.GetChild(0).GetText()];
        var symbol = new Symbol(context.varName().GetText(), type);
        em.AddVariableToScope(context.varName(), symbol);
        return base.VisitCardDefinition(context);
    }
    public override object VisitStageDefinition([NotNull] CDLParser.StageDefinitionContext context)
    {
        var type = em.ts[context.GetChild(0).GetText()];
        var symbol = new Symbol(context.varName().GetText(), type);
        em.AddVariableToScope(context.varName(), symbol);
        return base.VisitStageDefinition(context);
    }
    public override object VisitNodeDefinition([NotNull] CDLParser.NodeDefinitionContext context)
    {
        var type = em.ts[context.GetChild(0).GetText()];
        var symbol = new Symbol(context.varName().GetText(), type);
        em.AddVariableToScope(context.varName(), symbol);
        return base.VisitNodeDefinition(context);
    }
    public override object VisitCharSetup([NotNull] CDLParser.CharSetupContext context)
    {
        var type = em.ts[context.GetChild(0).GetText()];
        var symbol = new Symbol(context.varName().GetText(), type);
        em.AddVariableToScope(context.varName(), symbol);
        return base.VisitCharSetup(context);
    }
    public override object VisitEnemyDefinition([NotNull] CDLParser.EnemyDefinitionContext context)
    {
        var type = em.ts[context.GetChild(0).GetText()];
        var symbol = new Symbol(context.varName().GetText(), type);
        em.AddVariableToScope(context.varName(), symbol);
        return base.VisitEnemyDefinition(context);
    }
}