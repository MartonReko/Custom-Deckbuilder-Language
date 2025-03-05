// Generated from /home/tuna/Documents/Onlab/Test/CDL/CDL.g4 by ANTLR 4.13.1
import org.antlr.v4.runtime.tree.ParseTreeVisitor;

/**
 * This interface defines a complete generic visitor for a parse tree produced
 * by {@link CDLParser}.
 *
 * @param <T> The return type of the visit operation. Use {@link Void} for
 * operations with no return type.
 */
public interface CDLVisitor<T> extends ParseTreeVisitor<T> {
	/**
	 * Visit a parse tree produced by {@link CDLParser#program}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitProgram(CDLParser.ProgramContext ctx);
	/**
	 * Visit a parse tree produced by {@link CDLParser#varName}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitVarName(CDLParser.VarNameContext ctx);
	/**
	 * Visit a parse tree produced by {@link CDLParser#numberValue}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitNumberValue(CDLParser.NumberValueContext ctx);
	/**
	 * Visit a parse tree produced by {@link CDLParser#varRef}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitVarRef(CDLParser.VarRefContext ctx);
	/**
	 * Visit a parse tree produced by {@link CDLParser#rarityName}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitRarityName(CDLParser.RarityNameContext ctx);
	/**
	 * Visit a parse tree produced by the {@code primaryExpression}
	 * labeled alternative in {@link CDLParser#expression}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitPrimaryExpression(CDLParser.PrimaryExpressionContext ctx);
	/**
	 * Visit a parse tree produced by the {@code mulDivExpression}
	 * labeled alternative in {@link CDLParser#expression}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitMulDivExpression(CDLParser.MulDivExpressionContext ctx);
	/**
	 * Visit a parse tree produced by the {@code addSubExpression}
	 * labeled alternative in {@link CDLParser#expression}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitAddSubExpression(CDLParser.AddSubExpressionContext ctx);
	/**
	 * Visit a parse tree produced by {@link CDLParser#addSubOp}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitAddSubOp(CDLParser.AddSubOpContext ctx);
	/**
	 * Visit a parse tree produced by {@link CDLParser#mulDivOp}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitMulDivOp(CDLParser.MulDivOpContext ctx);
	/**
	 * Visit a parse tree produced by {@link CDLParser#primary}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitPrimary(CDLParser.PrimaryContext ctx);
	/**
	 * Visit a parse tree produced by {@link CDLParser#parenthesizedExpression}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitParenthesizedExpression(CDLParser.ParenthesizedExpressionContext ctx);
	/**
	 * Visit a parse tree produced by {@link CDLParser#literalExpression}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitLiteralExpression(CDLParser.LiteralExpressionContext ctx);
	/**
	 * Visit a parse tree produced by {@link CDLParser#list}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitList(CDLParser.ListContext ctx);
	/**
	 * Visit a parse tree produced by {@link CDLParser#listItem}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitListItem(CDLParser.ListItemContext ctx);
	/**
	 * Visit a parse tree produced by {@link CDLParser#targetItem}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitTargetItem(CDLParser.TargetItemContext ctx);
	/**
	 * Visit a parse tree produced by {@link CDLParser#gameSetup}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitGameSetup(CDLParser.GameSetupContext ctx);
	/**
	 * Visit a parse tree produced by {@link CDLParser#gameProperties}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitGameProperties(CDLParser.GamePropertiesContext ctx);
	/**
	 * Visit a parse tree produced by {@link CDLParser#stageDefinition}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitStageDefinition(CDLParser.StageDefinitionContext ctx);
	/**
	 * Visit a parse tree produced by {@link CDLParser#stageProperties}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitStageProperties(CDLParser.StagePropertiesContext ctx);
	/**
	 * Visit a parse tree produced by {@link CDLParser#lengthDef}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitLengthDef(CDLParser.LengthDefContext ctx);
	/**
	 * Visit a parse tree produced by {@link CDLParser#minWidthDef}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitMinWidthDef(CDLParser.MinWidthDefContext ctx);
	/**
	 * Visit a parse tree produced by {@link CDLParser#maxWidthDef}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitMaxWidthDef(CDLParser.MaxWidthDefContext ctx);
	/**
	 * Visit a parse tree produced by {@link CDLParser#fillWithDef}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitFillWithDef(CDLParser.FillWithDefContext ctx);
	/**
	 * Visit a parse tree produced by {@link CDLParser#mustContainDef}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitMustContainDef(CDLParser.MustContainDefContext ctx);
	/**
	 * Visit a parse tree produced by {@link CDLParser#endsWithDef}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitEndsWithDef(CDLParser.EndsWithDefContext ctx);
	/**
	 * Visit a parse tree produced by {@link CDLParser#nodeDefinition}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitNodeDefinition(CDLParser.NodeDefinitionContext ctx);
	/**
	 * Visit a parse tree produced by {@link CDLParser#nodeProperties}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitNodeProperties(CDLParser.NodePropertiesContext ctx);
	/**
	 * Visit a parse tree produced by {@link CDLParser#charSetup}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitCharSetup(CDLParser.CharSetupContext ctx);
	/**
	 * Visit a parse tree produced by {@link CDLParser#charProperties}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitCharProperties(CDLParser.CharPropertiesContext ctx);
	/**
	 * Visit a parse tree produced by {@link CDLParser#enemyDefinition}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitEnemyDefinition(CDLParser.EnemyDefinitionContext ctx);
	/**
	 * Visit a parse tree produced by {@link CDLParser#enemyProperties}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitEnemyProperties(CDLParser.EnemyPropertiesContext ctx);
	/**
	 * Visit a parse tree produced by {@link CDLParser#effectDefinition}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitEffectDefinition(CDLParser.EffectDefinitionContext ctx);
	/**
	 * Visit a parse tree produced by {@link CDLParser#effectType}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitEffectType(CDLParser.EffectTypeContext ctx);
	/**
	 * Visit a parse tree produced by {@link CDLParser#passiveEffect}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitPassiveEffect(CDLParser.PassiveEffectContext ctx);
	/**
	 * Visit a parse tree produced by {@link CDLParser#activeEffect}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitActiveEffect(CDLParser.ActiveEffectContext ctx);
	/**
	 * Visit a parse tree produced by {@link CDLParser#effectActivationOpt}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitEffectActivationOpt(CDLParser.EffectActivationOptContext ctx);
	/**
	 * Visit a parse tree produced by {@link CDLParser#effectTarget}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitEffectTarget(CDLParser.EffectTargetContext ctx);
	/**
	 * Visit a parse tree produced by {@link CDLParser#cardDefinition}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitCardDefinition(CDLParser.CardDefinitionContext ctx);
	/**
	 * Visit a parse tree produced by {@link CDLParser#cardProperties}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitCardProperties(CDLParser.CardPropertiesContext ctx);
}