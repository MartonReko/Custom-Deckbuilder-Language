// Generated from /home/tuna/Documents/Onlab/Test/CDL/CDL.g4 by ANTLR 4.13.1
import org.antlr.v4.runtime.atn.*;
import org.antlr.v4.runtime.dfa.DFA;
import org.antlr.v4.runtime.*;
import org.antlr.v4.runtime.misc.*;
import org.antlr.v4.runtime.tree.*;
import java.util.List;
import java.util.Iterator;
import java.util.ArrayList;

@SuppressWarnings({"all", "warnings", "unchecked", "unused", "cast", "CheckReturnValue"})
public class CDLParser extends Parser {
	static { RuntimeMetaData.checkVersion("4.13.1", RuntimeMetaData.VERSION); }

	protected static final DFA[] _decisionToDFA;
	protected static final PredictionContextCache _sharedContextCache =
		new PredictionContextCache();
	public static final int
		CHARACTER=1, HEALTH=2, EFFECTEVERYTURN=3, WITHCHANCE=4, OR=5, ACTIONS=6, 
		EFFECT=7, EFFECTS=8, APPLY=9, DAMAGE=10, INSTANTLY=11, ENDOFTURN=12, FOR=13, 
		TURNS=14, TO=15, ENEMIES=16, PLAYER=17, OUTGOING=18, INCOMING=19, IS=20, 
		SET=21, TARGET=22, DEAL=23, CARD=24, RARITY=25, VALIDTARGETS=26, SELF=27, 
		DESCRIPTION=28, GAME=29, STAGES=30, LENGTH=31, MAXWIDTH=32, MINWIDTH=33, 
		FILLWITH=34, MUSTCONTAIN=35, ENDSWITH=36, NODE=37, NODEENEMIES=38, STAGE=39, 
		REWARDS=40, NAME=41, PLAYERSELECT=42, ENEMY=43, ENEMYLC=44, X=45, MUL=46, 
		DIV=47, PLUS=48, MINUS=49, CARET=50, CLN=51, LBRACKET=52, RBRACKET=53, 
		LCURLY=54, RCURLY=55, LPAREN=56, RPAREN=57, COMMA=58, PERCENT=59, STRING=60, 
		INT=61, DOUBLE=62, ID=63, WS=64, EOS=65, COMMENT=66;
	public static final int
		RULE_program = 0, RULE_varName = 1, RULE_numberValue = 2, RULE_varRef = 3, 
		RULE_rarityName = 4, RULE_expression = 5, RULE_addSubOp = 6, RULE_mulDivOp = 7, 
		RULE_primary = 8, RULE_parenthesizedExpression = 9, RULE_literalExpression = 10, 
		RULE_list = 11, RULE_listItem = 12, RULE_targetItem = 13, RULE_gameSetup = 14, 
		RULE_gameProperties = 15, RULE_stageDefinition = 16, RULE_stageProperties = 17, 
		RULE_lengthDef = 18, RULE_minWidthDef = 19, RULE_maxWidthDef = 20, RULE_fillWithDef = 21, 
		RULE_mustContainDef = 22, RULE_endsWithDef = 23, RULE_nodeDefinition = 24, 
		RULE_nodeProperties = 25, RULE_charSetup = 26, RULE_charProperties = 27, 
		RULE_enemyDefinition = 28, RULE_enemyProperties = 29, RULE_effectDefinition = 30, 
		RULE_effectType = 31, RULE_passiveEffect = 32, RULE_activeEffect = 33, 
		RULE_effectActivationOpt = 34, RULE_effectTarget = 35, RULE_cardDefinition = 36, 
		RULE_cardProperties = 37;
	private static String[] makeRuleNames() {
		return new String[] {
			"program", "varName", "numberValue", "varRef", "rarityName", "expression", 
			"addSubOp", "mulDivOp", "primary", "parenthesizedExpression", "literalExpression", 
			"list", "listItem", "targetItem", "gameSetup", "gameProperties", "stageDefinition", 
			"stageProperties", "lengthDef", "minWidthDef", "maxWidthDef", "fillWithDef", 
			"mustContainDef", "endsWithDef", "nodeDefinition", "nodeProperties", 
			"charSetup", "charProperties", "enemyDefinition", "enemyProperties", 
			"effectDefinition", "effectType", "passiveEffect", "activeEffect", "effectActivationOpt", 
			"effectTarget", "cardDefinition", "cardProperties"
		};
	}
	public static final String[] ruleNames = makeRuleNames();

	private static String[] makeLiteralNames() {
		return new String[] {
			null, "'Character'", "'Health'", "'EffectEveryTurn'", "'with chance'", 
			"'or'", "'Actions'", "'Effect'", "'effects'", "'Apply'", "'damage'", 
			"'instantly'", "'end of turn'", "'for'", "'turns'", "'to'", "'enemies'", 
			"'player'", "'Outgoing'", "'Incoming'", "'is'", "'set'", "'target'", 
			"'Deal'", "'Card'", "'Rarity'", "'ValidTargets'", "'self'", "'Desc'", 
			"'Game'", "'Stages'", "'Length'", "'Max-width'", "'Min-width'", "'FillWith'", 
			"'MustContain'", "'EndsWith'", "'Node'", "'Enemies'", "'Stage'", "'Rewards'", 
			"'Name'", "'Player'", "'Enemy'", "'enemy'", "'x'", "'*'", "'/'", "'+'", 
			"'-'", "'^'", "':'", "'['", "']'", "'{'", "'}'", "'('", "')'", "','", 
			"'%'", null, null, null, null, null, "';'"
		};
	}
	private static final String[] _LITERAL_NAMES = makeLiteralNames();
	private static String[] makeSymbolicNames() {
		return new String[] {
			null, "CHARACTER", "HEALTH", "EFFECTEVERYTURN", "WITHCHANCE", "OR", "ACTIONS", 
			"EFFECT", "EFFECTS", "APPLY", "DAMAGE", "INSTANTLY", "ENDOFTURN", "FOR", 
			"TURNS", "TO", "ENEMIES", "PLAYER", "OUTGOING", "INCOMING", "IS", "SET", 
			"TARGET", "DEAL", "CARD", "RARITY", "VALIDTARGETS", "SELF", "DESCRIPTION", 
			"GAME", "STAGES", "LENGTH", "MAXWIDTH", "MINWIDTH", "FILLWITH", "MUSTCONTAIN", 
			"ENDSWITH", "NODE", "NODEENEMIES", "STAGE", "REWARDS", "NAME", "PLAYERSELECT", 
			"ENEMY", "ENEMYLC", "X", "MUL", "DIV", "PLUS", "MINUS", "CARET", "CLN", 
			"LBRACKET", "RBRACKET", "LCURLY", "RCURLY", "LPAREN", "RPAREN", "COMMA", 
			"PERCENT", "STRING", "INT", "DOUBLE", "ID", "WS", "EOS", "COMMENT"
		};
	}
	private static final String[] _SYMBOLIC_NAMES = makeSymbolicNames();
	public static final Vocabulary VOCABULARY = new VocabularyImpl(_LITERAL_NAMES, _SYMBOLIC_NAMES);

	/**
	 * @deprecated Use {@link #VOCABULARY} instead.
	 */
	@Deprecated
	public static final String[] tokenNames;
	static {
		tokenNames = new String[_SYMBOLIC_NAMES.length];
		for (int i = 0; i < tokenNames.length; i++) {
			tokenNames[i] = VOCABULARY.getLiteralName(i);
			if (tokenNames[i] == null) {
				tokenNames[i] = VOCABULARY.getSymbolicName(i);
			}

			if (tokenNames[i] == null) {
				tokenNames[i] = "<INVALID>";
			}
		}
	}

	@Override
	@Deprecated
	public String[] getTokenNames() {
		return tokenNames;
	}

	@Override

	public Vocabulary getVocabulary() {
		return VOCABULARY;
	}

	@Override
	public String getGrammarFileName() { return "CDL.g4"; }

	@Override
	public String[] getRuleNames() { return ruleNames; }

	@Override
	public String getSerializedATN() { return _serializedATN; }

	@Override
	public ATN getATN() { return _ATN; }

	public CDLParser(TokenStream input) {
		super(input);
		_interp = new ParserATNSimulator(this,_ATN,_decisionToDFA,_sharedContextCache);
	}

	@SuppressWarnings("CheckReturnValue")
	public static class ProgramContext extends ParserRuleContext {
		public List<GameSetupContext> gameSetup() {
			return getRuleContexts(GameSetupContext.class);
		}
		public GameSetupContext gameSetup(int i) {
			return getRuleContext(GameSetupContext.class,i);
		}
		public List<StageDefinitionContext> stageDefinition() {
			return getRuleContexts(StageDefinitionContext.class);
		}
		public StageDefinitionContext stageDefinition(int i) {
			return getRuleContext(StageDefinitionContext.class,i);
		}
		public List<NodeDefinitionContext> nodeDefinition() {
			return getRuleContexts(NodeDefinitionContext.class);
		}
		public NodeDefinitionContext nodeDefinition(int i) {
			return getRuleContext(NodeDefinitionContext.class,i);
		}
		public List<CharSetupContext> charSetup() {
			return getRuleContexts(CharSetupContext.class);
		}
		public CharSetupContext charSetup(int i) {
			return getRuleContext(CharSetupContext.class,i);
		}
		public List<EnemyDefinitionContext> enemyDefinition() {
			return getRuleContexts(EnemyDefinitionContext.class);
		}
		public EnemyDefinitionContext enemyDefinition(int i) {
			return getRuleContext(EnemyDefinitionContext.class,i);
		}
		public List<EffectDefinitionContext> effectDefinition() {
			return getRuleContexts(EffectDefinitionContext.class);
		}
		public EffectDefinitionContext effectDefinition(int i) {
			return getRuleContext(EffectDefinitionContext.class,i);
		}
		public List<CardDefinitionContext> cardDefinition() {
			return getRuleContexts(CardDefinitionContext.class);
		}
		public CardDefinitionContext cardDefinition(int i) {
			return getRuleContext(CardDefinitionContext.class,i);
		}
		public ProgramContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_program; }
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof CDLVisitor ) return ((CDLVisitor<? extends T>)visitor).visitProgram(this);
			else return visitor.visitChildren(this);
		}
	}

	public final ProgramContext program() throws RecognitionException {
		ProgramContext _localctx = new ProgramContext(_ctx, getState());
		enterRule(_localctx, 0, RULE_program);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(83); 
			_errHandler.sync(this);
			_la = _input.LA(1);
			do {
				{
				setState(83);
				_errHandler.sync(this);
				switch (_input.LA(1)) {
				case GAME:
					{
					setState(76);
					gameSetup();
					}
					break;
				case STAGE:
					{
					setState(77);
					stageDefinition();
					}
					break;
				case NODE:
					{
					setState(78);
					nodeDefinition();
					}
					break;
				case CHARACTER:
					{
					setState(79);
					charSetup();
					}
					break;
				case ENEMY:
					{
					setState(80);
					enemyDefinition();
					}
					break;
				case EFFECT:
					{
					setState(81);
					effectDefinition();
					}
					break;
				case CARD:
					{
					setState(82);
					cardDefinition();
					}
					break;
				default:
					throw new NoViableAltException(this);
				}
				}
				setState(85); 
				_errHandler.sync(this);
				_la = _input.LA(1);
			} while ( (((_la) & ~0x3f) == 0 && ((1L << _la) & 9483841437826L) != 0) );
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class VarNameContext extends ParserRuleContext {
		public TerminalNode ID() { return getToken(CDLParser.ID, 0); }
		public VarNameContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_varName; }
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof CDLVisitor ) return ((CDLVisitor<? extends T>)visitor).visitVarName(this);
			else return visitor.visitChildren(this);
		}
	}

	public final VarNameContext varName() throws RecognitionException {
		VarNameContext _localctx = new VarNameContext(_ctx, getState());
		enterRule(_localctx, 2, RULE_varName);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(87);
			match(ID);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class NumberValueContext extends ParserRuleContext {
		public TerminalNode INT() { return getToken(CDLParser.INT, 0); }
		public TerminalNode DOUBLE() { return getToken(CDLParser.DOUBLE, 0); }
		public TerminalNode MINUS() { return getToken(CDLParser.MINUS, 0); }
		public NumberValueContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_numberValue; }
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof CDLVisitor ) return ((CDLVisitor<? extends T>)visitor).visitNumberValue(this);
			else return visitor.visitChildren(this);
		}
	}

	public final NumberValueContext numberValue() throws RecognitionException {
		NumberValueContext _localctx = new NumberValueContext(_ctx, getState());
		enterRule(_localctx, 4, RULE_numberValue);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(90);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==MINUS) {
				{
				setState(89);
				match(MINUS);
				}
			}

			setState(92);
			_la = _input.LA(1);
			if ( !(_la==INT || _la==DOUBLE) ) {
			_errHandler.recoverInline(this);
			}
			else {
				if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
				_errHandler.reportMatch(this);
				consume();
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class VarRefContext extends ParserRuleContext {
		public TerminalNode ID() { return getToken(CDLParser.ID, 0); }
		public VarRefContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_varRef; }
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof CDLVisitor ) return ((CDLVisitor<? extends T>)visitor).visitVarRef(this);
			else return visitor.visitChildren(this);
		}
	}

	public final VarRefContext varRef() throws RecognitionException {
		VarRefContext _localctx = new VarRefContext(_ctx, getState());
		enterRule(_localctx, 6, RULE_varRef);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(94);
			match(ID);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class RarityNameContext extends ParserRuleContext {
		public TerminalNode ID() { return getToken(CDLParser.ID, 0); }
		public RarityNameContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_rarityName; }
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof CDLVisitor ) return ((CDLVisitor<? extends T>)visitor).visitRarityName(this);
			else return visitor.visitChildren(this);
		}
	}

	public final RarityNameContext rarityName() throws RecognitionException {
		RarityNameContext _localctx = new RarityNameContext(_ctx, getState());
		enterRule(_localctx, 8, RULE_rarityName);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(96);
			match(ID);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class ExpressionContext extends ParserRuleContext {
		public ExpressionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_expression; }
	 
		public ExpressionContext() { }
		public void copyFrom(ExpressionContext ctx) {
			super.copyFrom(ctx);
		}
	}
	@SuppressWarnings("CheckReturnValue")
	public static class PrimaryExpressionContext extends ExpressionContext {
		public PrimaryContext primary() {
			return getRuleContext(PrimaryContext.class,0);
		}
		public PrimaryExpressionContext(ExpressionContext ctx) { copyFrom(ctx); }
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof CDLVisitor ) return ((CDLVisitor<? extends T>)visitor).visitPrimaryExpression(this);
			else return visitor.visitChildren(this);
		}
	}
	@SuppressWarnings("CheckReturnValue")
	public static class MulDivExpressionContext extends ExpressionContext {
		public List<ExpressionContext> expression() {
			return getRuleContexts(ExpressionContext.class);
		}
		public ExpressionContext expression(int i) {
			return getRuleContext(ExpressionContext.class,i);
		}
		public MulDivOpContext mulDivOp() {
			return getRuleContext(MulDivOpContext.class,0);
		}
		public MulDivExpressionContext(ExpressionContext ctx) { copyFrom(ctx); }
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof CDLVisitor ) return ((CDLVisitor<? extends T>)visitor).visitMulDivExpression(this);
			else return visitor.visitChildren(this);
		}
	}
	@SuppressWarnings("CheckReturnValue")
	public static class AddSubExpressionContext extends ExpressionContext {
		public List<ExpressionContext> expression() {
			return getRuleContexts(ExpressionContext.class);
		}
		public ExpressionContext expression(int i) {
			return getRuleContext(ExpressionContext.class,i);
		}
		public AddSubOpContext addSubOp() {
			return getRuleContext(AddSubOpContext.class,0);
		}
		public AddSubExpressionContext(ExpressionContext ctx) { copyFrom(ctx); }
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof CDLVisitor ) return ((CDLVisitor<? extends T>)visitor).visitAddSubExpression(this);
			else return visitor.visitChildren(this);
		}
	}

	public final ExpressionContext expression() throws RecognitionException {
		return expression(0);
	}

	private ExpressionContext expression(int _p) throws RecognitionException {
		ParserRuleContext _parentctx = _ctx;
		int _parentState = getState();
		ExpressionContext _localctx = new ExpressionContext(_ctx, _parentState);
		ExpressionContext _prevctx = _localctx;
		int _startState = 10;
		enterRecursionRule(_localctx, 10, RULE_expression, _p);
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			{
			_localctx = new PrimaryExpressionContext(_localctx);
			_ctx = _localctx;
			_prevctx = _localctx;

			setState(99);
			primary();
			}
			_ctx.stop = _input.LT(-1);
			setState(111);
			_errHandler.sync(this);
			_alt = getInterpreter().adaptivePredict(_input,4,_ctx);
			while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					if ( _parseListeners!=null ) triggerExitRuleEvent();
					_prevctx = _localctx;
					{
					setState(109);
					_errHandler.sync(this);
					switch ( getInterpreter().adaptivePredict(_input,3,_ctx) ) {
					case 1:
						{
						_localctx = new AddSubExpressionContext(new ExpressionContext(_parentctx, _parentState));
						pushNewRecursionContext(_localctx, _startState, RULE_expression);
						setState(101);
						if (!(precpred(_ctx, 2))) throw new FailedPredicateException(this, "precpred(_ctx, 2)");
						setState(102);
						addSubOp();
						setState(103);
						expression(3);
						}
						break;
					case 2:
						{
						_localctx = new MulDivExpressionContext(new ExpressionContext(_parentctx, _parentState));
						pushNewRecursionContext(_localctx, _startState, RULE_expression);
						setState(105);
						if (!(precpred(_ctx, 1))) throw new FailedPredicateException(this, "precpred(_ctx, 1)");
						setState(106);
						mulDivOp();
						setState(107);
						expression(2);
						}
						break;
					}
					} 
				}
				setState(113);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,4,_ctx);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			unrollRecursionContexts(_parentctx);
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class AddSubOpContext extends ParserRuleContext {
		public TerminalNode PLUS() { return getToken(CDLParser.PLUS, 0); }
		public TerminalNode MINUS() { return getToken(CDLParser.MINUS, 0); }
		public AddSubOpContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_addSubOp; }
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof CDLVisitor ) return ((CDLVisitor<? extends T>)visitor).visitAddSubOp(this);
			else return visitor.visitChildren(this);
		}
	}

	public final AddSubOpContext addSubOp() throws RecognitionException {
		AddSubOpContext _localctx = new AddSubOpContext(_ctx, getState());
		enterRule(_localctx, 12, RULE_addSubOp);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(114);
			_la = _input.LA(1);
			if ( !(_la==PLUS || _la==MINUS) ) {
			_errHandler.recoverInline(this);
			}
			else {
				if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
				_errHandler.reportMatch(this);
				consume();
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class MulDivOpContext extends ParserRuleContext {
		public TerminalNode MUL() { return getToken(CDLParser.MUL, 0); }
		public TerminalNode DIV() { return getToken(CDLParser.DIV, 0); }
		public MulDivOpContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_mulDivOp; }
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof CDLVisitor ) return ((CDLVisitor<? extends T>)visitor).visitMulDivOp(this);
			else return visitor.visitChildren(this);
		}
	}

	public final MulDivOpContext mulDivOp() throws RecognitionException {
		MulDivOpContext _localctx = new MulDivOpContext(_ctx, getState());
		enterRule(_localctx, 14, RULE_mulDivOp);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(116);
			_la = _input.LA(1);
			if ( !(_la==MUL || _la==DIV) ) {
			_errHandler.recoverInline(this);
			}
			else {
				if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
				_errHandler.reportMatch(this);
				consume();
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class PrimaryContext extends ParserRuleContext {
		public ParenthesizedExpressionContext parenthesizedExpression() {
			return getRuleContext(ParenthesizedExpressionContext.class,0);
		}
		public LiteralExpressionContext literalExpression() {
			return getRuleContext(LiteralExpressionContext.class,0);
		}
		public PrimaryContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_primary; }
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof CDLVisitor ) return ((CDLVisitor<? extends T>)visitor).visitPrimary(this);
			else return visitor.visitChildren(this);
		}
	}

	public final PrimaryContext primary() throws RecognitionException {
		PrimaryContext _localctx = new PrimaryContext(_ctx, getState());
		enterRule(_localctx, 16, RULE_primary);
		try {
			setState(120);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case LPAREN:
				enterOuterAlt(_localctx, 1);
				{
				setState(118);
				parenthesizedExpression();
				}
				break;
			case DAMAGE:
			case MINUS:
			case INT:
			case DOUBLE:
			case ID:
				enterOuterAlt(_localctx, 2);
				{
				setState(119);
				literalExpression();
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class ParenthesizedExpressionContext extends ParserRuleContext {
		public TerminalNode LPAREN() { return getToken(CDLParser.LPAREN, 0); }
		public ExpressionContext expression() {
			return getRuleContext(ExpressionContext.class,0);
		}
		public TerminalNode RPAREN() { return getToken(CDLParser.RPAREN, 0); }
		public ParenthesizedExpressionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_parenthesizedExpression; }
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof CDLVisitor ) return ((CDLVisitor<? extends T>)visitor).visitParenthesizedExpression(this);
			else return visitor.visitChildren(this);
		}
	}

	public final ParenthesizedExpressionContext parenthesizedExpression() throws RecognitionException {
		ParenthesizedExpressionContext _localctx = new ParenthesizedExpressionContext(_ctx, getState());
		enterRule(_localctx, 18, RULE_parenthesizedExpression);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(122);
			match(LPAREN);
			setState(123);
			expression(0);
			setState(124);
			match(RPAREN);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class LiteralExpressionContext extends ParserRuleContext {
		public NumberValueContext numberValue() {
			return getRuleContext(NumberValueContext.class,0);
		}
		public VarRefContext varRef() {
			return getRuleContext(VarRefContext.class,0);
		}
		public TerminalNode DAMAGE() { return getToken(CDLParser.DAMAGE, 0); }
		public LiteralExpressionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_literalExpression; }
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof CDLVisitor ) return ((CDLVisitor<? extends T>)visitor).visitLiteralExpression(this);
			else return visitor.visitChildren(this);
		}
	}

	public final LiteralExpressionContext literalExpression() throws RecognitionException {
		LiteralExpressionContext _localctx = new LiteralExpressionContext(_ctx, getState());
		enterRule(_localctx, 20, RULE_literalExpression);
		try {
			setState(129);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case MINUS:
			case INT:
			case DOUBLE:
				enterOuterAlt(_localctx, 1);
				{
				setState(126);
				numberValue();
				}
				break;
			case ID:
				enterOuterAlt(_localctx, 2);
				{
				setState(127);
				varRef();
				}
				break;
			case DAMAGE:
				enterOuterAlt(_localctx, 3);
				{
				setState(128);
				match(DAMAGE);
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class ListContext extends ParserRuleContext {
		public TerminalNode LBRACKET() { return getToken(CDLParser.LBRACKET, 0); }
		public TerminalNode RBRACKET() { return getToken(CDLParser.RBRACKET, 0); }
		public List<ListItemContext> listItem() {
			return getRuleContexts(ListItemContext.class);
		}
		public ListItemContext listItem(int i) {
			return getRuleContext(ListItemContext.class,i);
		}
		public List<TerminalNode> OR() { return getTokens(CDLParser.OR); }
		public TerminalNode OR(int i) {
			return getToken(CDLParser.OR, i);
		}
		public List<TerminalNode> COMMA() { return getTokens(CDLParser.COMMA); }
		public TerminalNode COMMA(int i) {
			return getToken(CDLParser.COMMA, i);
		}
		public ListContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_list; }
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof CDLVisitor ) return ((CDLVisitor<? extends T>)visitor).visitList(this);
			else return visitor.visitChildren(this);
		}
	}

	public final ListContext list() throws RecognitionException {
		ListContext _localctx = new ListContext(_ctx, getState());
		enterRule(_localctx, 22, RULE_list);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(131);
			match(LBRACKET);
			setState(140);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if ((((_la) & ~0x3f) == 0 && ((1L << _la) & -6917511435454840832L) != 0)) {
				{
				setState(132);
				listItem();
				setState(137);
				_errHandler.sync(this);
				_la = _input.LA(1);
				while (_la==OR || _la==COMMA) {
					{
					{
					setState(133);
					_la = _input.LA(1);
					if ( !(_la==OR || _la==COMMA) ) {
					_errHandler.recoverInline(this);
					}
					else {
						if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
						_errHandler.reportMatch(this);
						consume();
					}
					setState(134);
					listItem();
					}
					}
					setState(139);
					_errHandler.sync(this);
					_la = _input.LA(1);
				}
				}
			}

			setState(142);
			match(RBRACKET);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class ListItemContext extends ParserRuleContext {
		public VarRefContext varRef() {
			return getRuleContext(VarRefContext.class,0);
		}
		public List<TerminalNode> INT() { return getTokens(CDLParser.INT); }
		public TerminalNode INT(int i) {
			return getToken(CDLParser.INT, i);
		}
		public TerminalNode X() { return getToken(CDLParser.X, 0); }
		public TerminalNode WITHCHANCE() { return getToken(CDLParser.WITHCHANCE, 0); }
		public TerminalNode PERCENT() { return getToken(CDLParser.PERCENT, 0); }
		public TerminalNode PLAYER() { return getToken(CDLParser.PLAYER, 0); }
		public TargetItemContext targetItem() {
			return getRuleContext(TargetItemContext.class,0);
		}
		public ListItemContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_listItem; }
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof CDLVisitor ) return ((CDLVisitor<? extends T>)visitor).visitListItem(this);
			else return visitor.visitChildren(this);
		}
	}

	public final ListItemContext listItem() throws RecognitionException {
		ListItemContext _localctx = new ListItemContext(_ctx, getState());
		enterRule(_localctx, 24, RULE_listItem);
		int _la;
		try {
			setState(163);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,10,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(144);
				varRef();
				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(145);
				match(INT);
				setState(146);
				match(X);
				setState(147);
				varRef();
				}
				break;
			case 3:
				enterOuterAlt(_localctx, 3);
				{
				setState(148);
				match(INT);
				setState(149);
				match(X);
				setState(150);
				varRef();
				setState(151);
				match(WITHCHANCE);
				setState(152);
				match(INT);
				setState(153);
				match(PERCENT);
				}
				break;
			case 4:
				enterOuterAlt(_localctx, 4);
				{
				setState(157);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==INT) {
					{
					setState(155);
					match(INT);
					setState(156);
					match(X);
					}
				}

				setState(159);
				varRef();
				{
				setState(160);
				match(PLAYER);
				}
				}
				break;
			case 5:
				enterOuterAlt(_localctx, 5);
				{
				setState(162);
				targetItem();
				}
				break;
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class TargetItemContext extends ParserRuleContext {
		public TerminalNode ENEMIES() { return getToken(CDLParser.ENEMIES, 0); }
		public TerminalNode ENEMYLC() { return getToken(CDLParser.ENEMYLC, 0); }
		public TerminalNode PLAYER() { return getToken(CDLParser.PLAYER, 0); }
		public TargetItemContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_targetItem; }
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof CDLVisitor ) return ((CDLVisitor<? extends T>)visitor).visitTargetItem(this);
			else return visitor.visitChildren(this);
		}
	}

	public final TargetItemContext targetItem() throws RecognitionException {
		TargetItemContext _localctx = new TargetItemContext(_ctx, getState());
		enterRule(_localctx, 26, RULE_targetItem);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(165);
			_la = _input.LA(1);
			if ( !((((_la) & ~0x3f) == 0 && ((1L << _la) & 17592186241024L) != 0)) ) {
			_errHandler.recoverInline(this);
			}
			else {
				if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
				_errHandler.reportMatch(this);
				consume();
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class GameSetupContext extends ParserRuleContext {
		public TerminalNode GAME() { return getToken(CDLParser.GAME, 0); }
		public TerminalNode LCURLY() { return getToken(CDLParser.LCURLY, 0); }
		public GamePropertiesContext gameProperties() {
			return getRuleContext(GamePropertiesContext.class,0);
		}
		public TerminalNode RCURLY() { return getToken(CDLParser.RCURLY, 0); }
		public GameSetupContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_gameSetup; }
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof CDLVisitor ) return ((CDLVisitor<? extends T>)visitor).visitGameSetup(this);
			else return visitor.visitChildren(this);
		}
	}

	public final GameSetupContext gameSetup() throws RecognitionException {
		GameSetupContext _localctx = new GameSetupContext(_ctx, getState());
		enterRule(_localctx, 28, RULE_gameSetup);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(167);
			match(GAME);
			setState(168);
			match(LCURLY);
			setState(169);
			gameProperties();
			setState(170);
			match(RCURLY);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class GamePropertiesContext extends ParserRuleContext {
		public List<TerminalNode> PLAYERSELECT() { return getTokens(CDLParser.PLAYERSELECT); }
		public TerminalNode PLAYERSELECT(int i) {
			return getToken(CDLParser.PLAYERSELECT, i);
		}
		public List<TerminalNode> CLN() { return getTokens(CDLParser.CLN); }
		public TerminalNode CLN(int i) {
			return getToken(CDLParser.CLN, i);
		}
		public List<VarNameContext> varName() {
			return getRuleContexts(VarNameContext.class);
		}
		public VarNameContext varName(int i) {
			return getRuleContext(VarNameContext.class,i);
		}
		public List<TerminalNode> EOS() { return getTokens(CDLParser.EOS); }
		public TerminalNode EOS(int i) {
			return getToken(CDLParser.EOS, i);
		}
		public List<TerminalNode> STAGES() { return getTokens(CDLParser.STAGES); }
		public TerminalNode STAGES(int i) {
			return getToken(CDLParser.STAGES, i);
		}
		public List<ListContext> list() {
			return getRuleContexts(ListContext.class);
		}
		public ListContext list(int i) {
			return getRuleContext(ListContext.class,i);
		}
		public List<TerminalNode> NAME() { return getTokens(CDLParser.NAME); }
		public TerminalNode NAME(int i) {
			return getToken(CDLParser.NAME, i);
		}
		public GamePropertiesContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_gameProperties; }
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof CDLVisitor ) return ((CDLVisitor<? extends T>)visitor).visitGameProperties(this);
			else return visitor.visitChildren(this);
		}
	}

	public final GamePropertiesContext gameProperties() throws RecognitionException {
		GamePropertiesContext _localctx = new GamePropertiesContext(_ctx, getState());
		enterRule(_localctx, 30, RULE_gameProperties);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(187); 
			_errHandler.sync(this);
			_la = _input.LA(1);
			do {
				{
				setState(187);
				_errHandler.sync(this);
				switch (_input.LA(1)) {
				case PLAYERSELECT:
					{
					setState(172);
					match(PLAYERSELECT);
					setState(173);
					match(CLN);
					setState(174);
					varName();
					setState(175);
					match(EOS);
					}
					break;
				case STAGES:
					{
					setState(177);
					match(STAGES);
					setState(178);
					match(CLN);
					setState(179);
					list();
					setState(180);
					match(EOS);
					}
					break;
				case NAME:
					{
					setState(182);
					match(NAME);
					setState(183);
					match(CLN);
					setState(184);
					varName();
					setState(185);
					match(EOS);
					}
					break;
				default:
					throw new NoViableAltException(this);
				}
				}
				setState(189); 
				_errHandler.sync(this);
				_la = _input.LA(1);
			} while ( (((_la) & ~0x3f) == 0 && ((1L << _la) & 6598143508480L) != 0) );
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class StageDefinitionContext extends ParserRuleContext {
		public TerminalNode STAGE() { return getToken(CDLParser.STAGE, 0); }
		public VarNameContext varName() {
			return getRuleContext(VarNameContext.class,0);
		}
		public TerminalNode LCURLY() { return getToken(CDLParser.LCURLY, 0); }
		public StagePropertiesContext stageProperties() {
			return getRuleContext(StagePropertiesContext.class,0);
		}
		public TerminalNode RCURLY() { return getToken(CDLParser.RCURLY, 0); }
		public StageDefinitionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_stageDefinition; }
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof CDLVisitor ) return ((CDLVisitor<? extends T>)visitor).visitStageDefinition(this);
			else return visitor.visitChildren(this);
		}
	}

	public final StageDefinitionContext stageDefinition() throws RecognitionException {
		StageDefinitionContext _localctx = new StageDefinitionContext(_ctx, getState());
		enterRule(_localctx, 32, RULE_stageDefinition);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(191);
			match(STAGE);
			setState(192);
			varName();
			setState(193);
			match(LCURLY);
			setState(194);
			stageProperties();
			setState(195);
			match(RCURLY);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class StagePropertiesContext extends ParserRuleContext {
		public List<LengthDefContext> lengthDef() {
			return getRuleContexts(LengthDefContext.class);
		}
		public LengthDefContext lengthDef(int i) {
			return getRuleContext(LengthDefContext.class,i);
		}
		public List<MaxWidthDefContext> maxWidthDef() {
			return getRuleContexts(MaxWidthDefContext.class);
		}
		public MaxWidthDefContext maxWidthDef(int i) {
			return getRuleContext(MaxWidthDefContext.class,i);
		}
		public List<MinWidthDefContext> minWidthDef() {
			return getRuleContexts(MinWidthDefContext.class);
		}
		public MinWidthDefContext minWidthDef(int i) {
			return getRuleContext(MinWidthDefContext.class,i);
		}
		public List<FillWithDefContext> fillWithDef() {
			return getRuleContexts(FillWithDefContext.class);
		}
		public FillWithDefContext fillWithDef(int i) {
			return getRuleContext(FillWithDefContext.class,i);
		}
		public List<MustContainDefContext> mustContainDef() {
			return getRuleContexts(MustContainDefContext.class);
		}
		public MustContainDefContext mustContainDef(int i) {
			return getRuleContext(MustContainDefContext.class,i);
		}
		public List<EndsWithDefContext> endsWithDef() {
			return getRuleContexts(EndsWithDefContext.class);
		}
		public EndsWithDefContext endsWithDef(int i) {
			return getRuleContext(EndsWithDefContext.class,i);
		}
		public StagePropertiesContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_stageProperties; }
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof CDLVisitor ) return ((CDLVisitor<? extends T>)visitor).visitStageProperties(this);
			else return visitor.visitChildren(this);
		}
	}

	public final StagePropertiesContext stageProperties() throws RecognitionException {
		StagePropertiesContext _localctx = new StagePropertiesContext(_ctx, getState());
		enterRule(_localctx, 34, RULE_stageProperties);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(203); 
			_errHandler.sync(this);
			_la = _input.LA(1);
			do {
				{
				setState(203);
				_errHandler.sync(this);
				switch (_input.LA(1)) {
				case LENGTH:
					{
					setState(197);
					lengthDef();
					}
					break;
				case MAXWIDTH:
					{
					setState(198);
					maxWidthDef();
					}
					break;
				case MINWIDTH:
					{
					setState(199);
					minWidthDef();
					}
					break;
				case FILLWITH:
					{
					setState(200);
					fillWithDef();
					}
					break;
				case MUSTCONTAIN:
					{
					setState(201);
					mustContainDef();
					}
					break;
				case ENDSWITH:
					{
					setState(202);
					endsWithDef();
					}
					break;
				default:
					throw new NoViableAltException(this);
				}
				}
				setState(205); 
				_errHandler.sync(this);
				_la = _input.LA(1);
			} while ( (((_la) & ~0x3f) == 0 && ((1L << _la) & 135291469824L) != 0) );
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class LengthDefContext extends ParserRuleContext {
		public TerminalNode LENGTH() { return getToken(CDLParser.LENGTH, 0); }
		public TerminalNode CLN() { return getToken(CDLParser.CLN, 0); }
		public NumberValueContext numberValue() {
			return getRuleContext(NumberValueContext.class,0);
		}
		public TerminalNode EOS() { return getToken(CDLParser.EOS, 0); }
		public LengthDefContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_lengthDef; }
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof CDLVisitor ) return ((CDLVisitor<? extends T>)visitor).visitLengthDef(this);
			else return visitor.visitChildren(this);
		}
	}

	public final LengthDefContext lengthDef() throws RecognitionException {
		LengthDefContext _localctx = new LengthDefContext(_ctx, getState());
		enterRule(_localctx, 36, RULE_lengthDef);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(207);
			match(LENGTH);
			setState(208);
			match(CLN);
			setState(209);
			numberValue();
			setState(210);
			match(EOS);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class MinWidthDefContext extends ParserRuleContext {
		public TerminalNode MINWIDTH() { return getToken(CDLParser.MINWIDTH, 0); }
		public TerminalNode CLN() { return getToken(CDLParser.CLN, 0); }
		public NumberValueContext numberValue() {
			return getRuleContext(NumberValueContext.class,0);
		}
		public TerminalNode EOS() { return getToken(CDLParser.EOS, 0); }
		public MinWidthDefContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_minWidthDef; }
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof CDLVisitor ) return ((CDLVisitor<? extends T>)visitor).visitMinWidthDef(this);
			else return visitor.visitChildren(this);
		}
	}

	public final MinWidthDefContext minWidthDef() throws RecognitionException {
		MinWidthDefContext _localctx = new MinWidthDefContext(_ctx, getState());
		enterRule(_localctx, 38, RULE_minWidthDef);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(212);
			match(MINWIDTH);
			setState(213);
			match(CLN);
			setState(214);
			numberValue();
			setState(215);
			match(EOS);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class MaxWidthDefContext extends ParserRuleContext {
		public TerminalNode MAXWIDTH() { return getToken(CDLParser.MAXWIDTH, 0); }
		public TerminalNode CLN() { return getToken(CDLParser.CLN, 0); }
		public NumberValueContext numberValue() {
			return getRuleContext(NumberValueContext.class,0);
		}
		public TerminalNode EOS() { return getToken(CDLParser.EOS, 0); }
		public MaxWidthDefContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_maxWidthDef; }
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof CDLVisitor ) return ((CDLVisitor<? extends T>)visitor).visitMaxWidthDef(this);
			else return visitor.visitChildren(this);
		}
	}

	public final MaxWidthDefContext maxWidthDef() throws RecognitionException {
		MaxWidthDefContext _localctx = new MaxWidthDefContext(_ctx, getState());
		enterRule(_localctx, 40, RULE_maxWidthDef);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(217);
			match(MAXWIDTH);
			setState(218);
			match(CLN);
			setState(219);
			numberValue();
			setState(220);
			match(EOS);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class FillWithDefContext extends ParserRuleContext {
		public TerminalNode FILLWITH() { return getToken(CDLParser.FILLWITH, 0); }
		public TerminalNode CLN() { return getToken(CDLParser.CLN, 0); }
		public ListContext list() {
			return getRuleContext(ListContext.class,0);
		}
		public TerminalNode EOS() { return getToken(CDLParser.EOS, 0); }
		public FillWithDefContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_fillWithDef; }
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof CDLVisitor ) return ((CDLVisitor<? extends T>)visitor).visitFillWithDef(this);
			else return visitor.visitChildren(this);
		}
	}

	public final FillWithDefContext fillWithDef() throws RecognitionException {
		FillWithDefContext _localctx = new FillWithDefContext(_ctx, getState());
		enterRule(_localctx, 42, RULE_fillWithDef);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(222);
			match(FILLWITH);
			setState(223);
			match(CLN);
			setState(224);
			list();
			setState(225);
			match(EOS);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class MustContainDefContext extends ParserRuleContext {
		public TerminalNode MUSTCONTAIN() { return getToken(CDLParser.MUSTCONTAIN, 0); }
		public TerminalNode CLN() { return getToken(CDLParser.CLN, 0); }
		public ListContext list() {
			return getRuleContext(ListContext.class,0);
		}
		public TerminalNode EOS() { return getToken(CDLParser.EOS, 0); }
		public MustContainDefContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_mustContainDef; }
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof CDLVisitor ) return ((CDLVisitor<? extends T>)visitor).visitMustContainDef(this);
			else return visitor.visitChildren(this);
		}
	}

	public final MustContainDefContext mustContainDef() throws RecognitionException {
		MustContainDefContext _localctx = new MustContainDefContext(_ctx, getState());
		enterRule(_localctx, 44, RULE_mustContainDef);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(227);
			match(MUSTCONTAIN);
			setState(228);
			match(CLN);
			setState(229);
			list();
			setState(230);
			match(EOS);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class EndsWithDefContext extends ParserRuleContext {
		public TerminalNode ENDSWITH() { return getToken(CDLParser.ENDSWITH, 0); }
		public TerminalNode CLN() { return getToken(CDLParser.CLN, 0); }
		public VarNameContext varName() {
			return getRuleContext(VarNameContext.class,0);
		}
		public TerminalNode EOS() { return getToken(CDLParser.EOS, 0); }
		public EndsWithDefContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_endsWithDef; }
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof CDLVisitor ) return ((CDLVisitor<? extends T>)visitor).visitEndsWithDef(this);
			else return visitor.visitChildren(this);
		}
	}

	public final EndsWithDefContext endsWithDef() throws RecognitionException {
		EndsWithDefContext _localctx = new EndsWithDefContext(_ctx, getState());
		enterRule(_localctx, 46, RULE_endsWithDef);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(232);
			match(ENDSWITH);
			setState(233);
			match(CLN);
			setState(234);
			varName();
			setState(235);
			match(EOS);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class NodeDefinitionContext extends ParserRuleContext {
		public TerminalNode NODE() { return getToken(CDLParser.NODE, 0); }
		public VarNameContext varName() {
			return getRuleContext(VarNameContext.class,0);
		}
		public TerminalNode LCURLY() { return getToken(CDLParser.LCURLY, 0); }
		public NodePropertiesContext nodeProperties() {
			return getRuleContext(NodePropertiesContext.class,0);
		}
		public TerminalNode RCURLY() { return getToken(CDLParser.RCURLY, 0); }
		public NodeDefinitionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_nodeDefinition; }
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof CDLVisitor ) return ((CDLVisitor<? extends T>)visitor).visitNodeDefinition(this);
			else return visitor.visitChildren(this);
		}
	}

	public final NodeDefinitionContext nodeDefinition() throws RecognitionException {
		NodeDefinitionContext _localctx = new NodeDefinitionContext(_ctx, getState());
		enterRule(_localctx, 48, RULE_nodeDefinition);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(237);
			match(NODE);
			setState(238);
			varName();
			setState(239);
			match(LCURLY);
			setState(240);
			nodeProperties();
			setState(241);
			match(RCURLY);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class NodePropertiesContext extends ParserRuleContext {
		public List<TerminalNode> NODEENEMIES() { return getTokens(CDLParser.NODEENEMIES); }
		public TerminalNode NODEENEMIES(int i) {
			return getToken(CDLParser.NODEENEMIES, i);
		}
		public List<TerminalNode> CLN() { return getTokens(CDLParser.CLN); }
		public TerminalNode CLN(int i) {
			return getToken(CDLParser.CLN, i);
		}
		public List<ListContext> list() {
			return getRuleContexts(ListContext.class);
		}
		public ListContext list(int i) {
			return getRuleContext(ListContext.class,i);
		}
		public List<TerminalNode> EOS() { return getTokens(CDLParser.EOS); }
		public TerminalNode EOS(int i) {
			return getToken(CDLParser.EOS, i);
		}
		public List<TerminalNode> REWARDS() { return getTokens(CDLParser.REWARDS); }
		public TerminalNode REWARDS(int i) {
			return getToken(CDLParser.REWARDS, i);
		}
		public NodePropertiesContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_nodeProperties; }
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof CDLVisitor ) return ((CDLVisitor<? extends T>)visitor).visitNodeProperties(this);
			else return visitor.visitChildren(this);
		}
	}

	public final NodePropertiesContext nodeProperties() throws RecognitionException {
		NodePropertiesContext _localctx = new NodePropertiesContext(_ctx, getState());
		enterRule(_localctx, 50, RULE_nodeProperties);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(253); 
			_errHandler.sync(this);
			_la = _input.LA(1);
			do {
				{
				setState(253);
				_errHandler.sync(this);
				switch (_input.LA(1)) {
				case NODEENEMIES:
					{
					setState(243);
					match(NODEENEMIES);
					setState(244);
					match(CLN);
					setState(245);
					list();
					setState(246);
					match(EOS);
					}
					break;
				case REWARDS:
					{
					setState(248);
					match(REWARDS);
					setState(249);
					match(CLN);
					setState(250);
					list();
					setState(251);
					match(EOS);
					}
					break;
				default:
					throw new NoViableAltException(this);
				}
				}
				setState(255); 
				_errHandler.sync(this);
				_la = _input.LA(1);
			} while ( _la==NODEENEMIES || _la==REWARDS );
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class CharSetupContext extends ParserRuleContext {
		public TerminalNode CHARACTER() { return getToken(CDLParser.CHARACTER, 0); }
		public VarNameContext varName() {
			return getRuleContext(VarNameContext.class,0);
		}
		public TerminalNode LCURLY() { return getToken(CDLParser.LCURLY, 0); }
		public CharPropertiesContext charProperties() {
			return getRuleContext(CharPropertiesContext.class,0);
		}
		public TerminalNode RCURLY() { return getToken(CDLParser.RCURLY, 0); }
		public CharSetupContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_charSetup; }
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof CDLVisitor ) return ((CDLVisitor<? extends T>)visitor).visitCharSetup(this);
			else return visitor.visitChildren(this);
		}
	}

	public final CharSetupContext charSetup() throws RecognitionException {
		CharSetupContext _localctx = new CharSetupContext(_ctx, getState());
		enterRule(_localctx, 52, RULE_charSetup);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(257);
			match(CHARACTER);
			setState(258);
			varName();
			setState(259);
			match(LCURLY);
			setState(260);
			charProperties();
			setState(261);
			match(RCURLY);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class CharPropertiesContext extends ParserRuleContext {
		public List<TerminalNode> HEALTH() { return getTokens(CDLParser.HEALTH); }
		public TerminalNode HEALTH(int i) {
			return getToken(CDLParser.HEALTH, i);
		}
		public List<TerminalNode> CLN() { return getTokens(CDLParser.CLN); }
		public TerminalNode CLN(int i) {
			return getToken(CDLParser.CLN, i);
		}
		public List<NumberValueContext> numberValue() {
			return getRuleContexts(NumberValueContext.class);
		}
		public NumberValueContext numberValue(int i) {
			return getRuleContext(NumberValueContext.class,i);
		}
		public List<TerminalNode> EOS() { return getTokens(CDLParser.EOS); }
		public TerminalNode EOS(int i) {
			return getToken(CDLParser.EOS, i);
		}
		public List<TerminalNode> EFFECTEVERYTURN() { return getTokens(CDLParser.EFFECTEVERYTURN); }
		public TerminalNode EFFECTEVERYTURN(int i) {
			return getToken(CDLParser.EFFECTEVERYTURN, i);
		}
		public List<ListContext> list() {
			return getRuleContexts(ListContext.class);
		}
		public ListContext list(int i) {
			return getRuleContext(ListContext.class,i);
		}
		public CharPropertiesContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_charProperties; }
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof CDLVisitor ) return ((CDLVisitor<? extends T>)visitor).visitCharProperties(this);
			else return visitor.visitChildren(this);
		}
	}

	public final CharPropertiesContext charProperties() throws RecognitionException {
		CharPropertiesContext _localctx = new CharPropertiesContext(_ctx, getState());
		enterRule(_localctx, 54, RULE_charProperties);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(273); 
			_errHandler.sync(this);
			_la = _input.LA(1);
			do {
				{
				setState(273);
				_errHandler.sync(this);
				switch (_input.LA(1)) {
				case HEALTH:
					{
					setState(263);
					match(HEALTH);
					setState(264);
					match(CLN);
					setState(265);
					numberValue();
					setState(266);
					match(EOS);
					}
					break;
				case EFFECTEVERYTURN:
					{
					setState(268);
					match(EFFECTEVERYTURN);
					setState(269);
					match(CLN);
					setState(270);
					list();
					setState(271);
					match(EOS);
					}
					break;
				default:
					throw new NoViableAltException(this);
				}
				}
				setState(275); 
				_errHandler.sync(this);
				_la = _input.LA(1);
			} while ( _la==HEALTH || _la==EFFECTEVERYTURN );
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class EnemyDefinitionContext extends ParserRuleContext {
		public TerminalNode ENEMY() { return getToken(CDLParser.ENEMY, 0); }
		public VarNameContext varName() {
			return getRuleContext(VarNameContext.class,0);
		}
		public TerminalNode LCURLY() { return getToken(CDLParser.LCURLY, 0); }
		public EnemyPropertiesContext enemyProperties() {
			return getRuleContext(EnemyPropertiesContext.class,0);
		}
		public TerminalNode RCURLY() { return getToken(CDLParser.RCURLY, 0); }
		public EnemyDefinitionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_enemyDefinition; }
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof CDLVisitor ) return ((CDLVisitor<? extends T>)visitor).visitEnemyDefinition(this);
			else return visitor.visitChildren(this);
		}
	}

	public final EnemyDefinitionContext enemyDefinition() throws RecognitionException {
		EnemyDefinitionContext _localctx = new EnemyDefinitionContext(_ctx, getState());
		enterRule(_localctx, 56, RULE_enemyDefinition);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(277);
			match(ENEMY);
			setState(278);
			varName();
			setState(279);
			match(LCURLY);
			setState(280);
			enemyProperties();
			setState(281);
			match(RCURLY);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class EnemyPropertiesContext extends ParserRuleContext {
		public List<TerminalNode> HEALTH() { return getTokens(CDLParser.HEALTH); }
		public TerminalNode HEALTH(int i) {
			return getToken(CDLParser.HEALTH, i);
		}
		public List<TerminalNode> CLN() { return getTokens(CDLParser.CLN); }
		public TerminalNode CLN(int i) {
			return getToken(CDLParser.CLN, i);
		}
		public List<NumberValueContext> numberValue() {
			return getRuleContexts(NumberValueContext.class);
		}
		public NumberValueContext numberValue(int i) {
			return getRuleContext(NumberValueContext.class,i);
		}
		public List<TerminalNode> EOS() { return getTokens(CDLParser.EOS); }
		public TerminalNode EOS(int i) {
			return getToken(CDLParser.EOS, i);
		}
		public List<TerminalNode> ACTIONS() { return getTokens(CDLParser.ACTIONS); }
		public TerminalNode ACTIONS(int i) {
			return getToken(CDLParser.ACTIONS, i);
		}
		public List<ListContext> list() {
			return getRuleContexts(ListContext.class);
		}
		public ListContext list(int i) {
			return getRuleContext(ListContext.class,i);
		}
		public EnemyPropertiesContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_enemyProperties; }
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof CDLVisitor ) return ((CDLVisitor<? extends T>)visitor).visitEnemyProperties(this);
			else return visitor.visitChildren(this);
		}
	}

	public final EnemyPropertiesContext enemyProperties() throws RecognitionException {
		EnemyPropertiesContext _localctx = new EnemyPropertiesContext(_ctx, getState());
		enterRule(_localctx, 58, RULE_enemyProperties);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(293); 
			_errHandler.sync(this);
			_la = _input.LA(1);
			do {
				{
				setState(293);
				_errHandler.sync(this);
				switch (_input.LA(1)) {
				case HEALTH:
					{
					setState(283);
					match(HEALTH);
					setState(284);
					match(CLN);
					setState(285);
					numberValue();
					setState(286);
					match(EOS);
					}
					break;
				case ACTIONS:
					{
					setState(288);
					match(ACTIONS);
					setState(289);
					match(CLN);
					setState(290);
					list();
					setState(291);
					match(EOS);
					}
					break;
				default:
					throw new NoViableAltException(this);
				}
				}
				setState(295); 
				_errHandler.sync(this);
				_la = _input.LA(1);
			} while ( _la==HEALTH || _la==ACTIONS );
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class EffectDefinitionContext extends ParserRuleContext {
		public TerminalNode EFFECT() { return getToken(CDLParser.EFFECT, 0); }
		public VarNameContext varName() {
			return getRuleContext(VarNameContext.class,0);
		}
		public TerminalNode LCURLY() { return getToken(CDLParser.LCURLY, 0); }
		public TerminalNode RCURLY() { return getToken(CDLParser.RCURLY, 0); }
		public List<EffectTypeContext> effectType() {
			return getRuleContexts(EffectTypeContext.class);
		}
		public EffectTypeContext effectType(int i) {
			return getRuleContext(EffectTypeContext.class,i);
		}
		public EffectDefinitionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_effectDefinition; }
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof CDLVisitor ) return ((CDLVisitor<? extends T>)visitor).visitEffectDefinition(this);
			else return visitor.visitChildren(this);
		}
	}

	public final EffectDefinitionContext effectDefinition() throws RecognitionException {
		EffectDefinitionContext _localctx = new EffectDefinitionContext(_ctx, getState());
		enterRule(_localctx, 60, RULE_effectDefinition);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(297);
			match(EFFECT);
			setState(298);
			varName();
			setState(299);
			match(LCURLY);
			setState(301); 
			_errHandler.sync(this);
			_la = _input.LA(1);
			do {
				{
				{
				setState(300);
				effectType();
				}
				}
				setState(303); 
				_errHandler.sync(this);
				_la = _input.LA(1);
			} while ( (((_la) & ~0x3f) == 0 && ((1L << _la) & 9175552L) != 0) );
			setState(305);
			match(RCURLY);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class EffectTypeContext extends ParserRuleContext {
		public PassiveEffectContext passiveEffect() {
			return getRuleContext(PassiveEffectContext.class,0);
		}
		public ActiveEffectContext activeEffect() {
			return getRuleContext(ActiveEffectContext.class,0);
		}
		public EffectTypeContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_effectType; }
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof CDLVisitor ) return ((CDLVisitor<? extends T>)visitor).visitEffectType(this);
			else return visitor.visitChildren(this);
		}
	}

	public final EffectTypeContext effectType() throws RecognitionException {
		EffectTypeContext _localctx = new EffectTypeContext(_ctx, getState());
		enterRule(_localctx, 62, RULE_effectType);
		try {
			setState(309);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case OUTGOING:
			case INCOMING:
				enterOuterAlt(_localctx, 1);
				{
				setState(307);
				passiveEffect();
				}
				break;
			case APPLY:
			case DEAL:
				enterOuterAlt(_localctx, 2);
				{
				setState(308);
				activeEffect();
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class PassiveEffectContext extends ParserRuleContext {
		public TerminalNode DAMAGE() { return getToken(CDLParser.DAMAGE, 0); }
		public TerminalNode IS() { return getToken(CDLParser.IS, 0); }
		public ExpressionContext expression() {
			return getRuleContext(ExpressionContext.class,0);
		}
		public TerminalNode EOS() { return getToken(CDLParser.EOS, 0); }
		public TerminalNode OUTGOING() { return getToken(CDLParser.OUTGOING, 0); }
		public TerminalNode INCOMING() { return getToken(CDLParser.INCOMING, 0); }
		public PassiveEffectContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_passiveEffect; }
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof CDLVisitor ) return ((CDLVisitor<? extends T>)visitor).visitPassiveEffect(this);
			else return visitor.visitChildren(this);
		}
	}

	public final PassiveEffectContext passiveEffect() throws RecognitionException {
		PassiveEffectContext _localctx = new PassiveEffectContext(_ctx, getState());
		enterRule(_localctx, 64, RULE_passiveEffect);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(311);
			_la = _input.LA(1);
			if ( !(_la==OUTGOING || _la==INCOMING) ) {
			_errHandler.recoverInline(this);
			}
			else {
				if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
				_errHandler.reportMatch(this);
				consume();
			}
			setState(312);
			match(DAMAGE);
			setState(313);
			match(IS);
			setState(314);
			expression(0);
			setState(315);
			match(EOS);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class ActiveEffectContext extends ParserRuleContext {
		public List<ExpressionContext> expression() {
			return getRuleContexts(ExpressionContext.class);
		}
		public ExpressionContext expression(int i) {
			return getRuleContext(ExpressionContext.class,i);
		}
		public List<TerminalNode> DAMAGE() { return getTokens(CDLParser.DAMAGE); }
		public TerminalNode DAMAGE(int i) {
			return getToken(CDLParser.DAMAGE, i);
		}
		public List<EffectActivationOptContext> effectActivationOpt() {
			return getRuleContexts(EffectActivationOptContext.class);
		}
		public EffectActivationOptContext effectActivationOpt(int i) {
			return getRuleContext(EffectActivationOptContext.class,i);
		}
		public List<TerminalNode> EOS() { return getTokens(CDLParser.EOS); }
		public TerminalNode EOS(int i) {
			return getToken(CDLParser.EOS, i);
		}
		public List<TerminalNode> APPLY() { return getTokens(CDLParser.APPLY); }
		public TerminalNode APPLY(int i) {
			return getToken(CDLParser.APPLY, i);
		}
		public List<ListContext> list() {
			return getRuleContexts(ListContext.class);
		}
		public ListContext list(int i) {
			return getRuleContext(ListContext.class,i);
		}
		public List<TerminalNode> FOR() { return getTokens(CDLParser.FOR); }
		public TerminalNode FOR(int i) {
			return getToken(CDLParser.FOR, i);
		}
		public List<NumberValueContext> numberValue() {
			return getRuleContexts(NumberValueContext.class);
		}
		public NumberValueContext numberValue(int i) {
			return getRuleContext(NumberValueContext.class,i);
		}
		public List<TerminalNode> TURNS() { return getTokens(CDLParser.TURNS); }
		public TerminalNode TURNS(int i) {
			return getToken(CDLParser.TURNS, i);
		}
		public List<TerminalNode> TO() { return getTokens(CDLParser.TO); }
		public TerminalNode TO(int i) {
			return getToken(CDLParser.TO, i);
		}
		public List<EffectTargetContext> effectTarget() {
			return getRuleContexts(EffectTargetContext.class);
		}
		public EffectTargetContext effectTarget(int i) {
			return getRuleContext(EffectTargetContext.class,i);
		}
		public List<TerminalNode> DEAL() { return getTokens(CDLParser.DEAL); }
		public TerminalNode DEAL(int i) {
			return getToken(CDLParser.DEAL, i);
		}
		public ActiveEffectContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_activeEffect; }
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof CDLVisitor ) return ((CDLVisitor<? extends T>)visitor).visitActiveEffect(this);
			else return visitor.visitChildren(this);
		}
	}

	public final ActiveEffectContext activeEffect() throws RecognitionException {
		ActiveEffectContext _localctx = new ActiveEffectContext(_ctx, getState());
		enterRule(_localctx, 66, RULE_activeEffect);
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			setState(332); 
			_errHandler.sync(this);
			_alt = 1;
			do {
				switch (_alt) {
				case 1:
					{
					setState(332);
					_errHandler.sync(this);
					switch (_input.LA(1)) {
					case DEAL:
						{
						{
						setState(317);
						match(DEAL);
						}
						setState(318);
						expression(0);
						setState(319);
						match(DAMAGE);
						setState(320);
						effectActivationOpt();
						setState(321);
						match(EOS);
						}
						break;
					case APPLY:
						{
						setState(323);
						match(APPLY);
						setState(324);
						list();
						setState(325);
						match(FOR);
						setState(326);
						numberValue();
						setState(327);
						match(TURNS);
						setState(328);
						match(TO);
						setState(329);
						effectTarget();
						setState(330);
						match(EOS);
						}
						break;
					default:
						throw new NoViableAltException(this);
					}
					}
					break;
				default:
					throw new NoViableAltException(this);
				}
				setState(334); 
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,24,_ctx);
			} while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER );
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class EffectActivationOptContext extends ParserRuleContext {
		public TerminalNode INSTANTLY() { return getToken(CDLParser.INSTANTLY, 0); }
		public TerminalNode ENDOFTURN() { return getToken(CDLParser.ENDOFTURN, 0); }
		public EffectActivationOptContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_effectActivationOpt; }
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof CDLVisitor ) return ((CDLVisitor<? extends T>)visitor).visitEffectActivationOpt(this);
			else return visitor.visitChildren(this);
		}
	}

	public final EffectActivationOptContext effectActivationOpt() throws RecognitionException {
		EffectActivationOptContext _localctx = new EffectActivationOptContext(_ctx, getState());
		enterRule(_localctx, 68, RULE_effectActivationOpt);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(336);
			_la = _input.LA(1);
			if ( !(_la==INSTANTLY || _la==ENDOFTURN) ) {
			_errHandler.recoverInline(this);
			}
			else {
				if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
				_errHandler.reportMatch(this);
				consume();
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class EffectTargetContext extends ParserRuleContext {
		public TerminalNode ENEMIES() { return getToken(CDLParser.ENEMIES, 0); }
		public TerminalNode TARGET() { return getToken(CDLParser.TARGET, 0); }
		public TerminalNode PLAYER() { return getToken(CDLParser.PLAYER, 0); }
		public EffectTargetContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_effectTarget; }
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof CDLVisitor ) return ((CDLVisitor<? extends T>)visitor).visitEffectTarget(this);
			else return visitor.visitChildren(this);
		}
	}

	public final EffectTargetContext effectTarget() throws RecognitionException {
		EffectTargetContext _localctx = new EffectTargetContext(_ctx, getState());
		enterRule(_localctx, 70, RULE_effectTarget);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(338);
			_la = _input.LA(1);
			if ( !((((_la) & ~0x3f) == 0 && ((1L << _la) & 4390912L) != 0)) ) {
			_errHandler.recoverInline(this);
			}
			else {
				if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
				_errHandler.reportMatch(this);
				consume();
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class CardDefinitionContext extends ParserRuleContext {
		public TerminalNode CARD() { return getToken(CDLParser.CARD, 0); }
		public VarNameContext varName() {
			return getRuleContext(VarNameContext.class,0);
		}
		public TerminalNode LCURLY() { return getToken(CDLParser.LCURLY, 0); }
		public CardPropertiesContext cardProperties() {
			return getRuleContext(CardPropertiesContext.class,0);
		}
		public TerminalNode RCURLY() { return getToken(CDLParser.RCURLY, 0); }
		public CardDefinitionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_cardDefinition; }
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof CDLVisitor ) return ((CDLVisitor<? extends T>)visitor).visitCardDefinition(this);
			else return visitor.visitChildren(this);
		}
	}

	public final CardDefinitionContext cardDefinition() throws RecognitionException {
		CardDefinitionContext _localctx = new CardDefinitionContext(_ctx, getState());
		enterRule(_localctx, 72, RULE_cardDefinition);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(340);
			match(CARD);
			setState(341);
			varName();
			setState(342);
			match(LCURLY);
			setState(343);
			cardProperties();
			setState(344);
			match(RCURLY);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class CardPropertiesContext extends ParserRuleContext {
		public List<TerminalNode> RARITY() { return getTokens(CDLParser.RARITY); }
		public TerminalNode RARITY(int i) {
			return getToken(CDLParser.RARITY, i);
		}
		public List<TerminalNode> CLN() { return getTokens(CDLParser.CLN); }
		public TerminalNode CLN(int i) {
			return getToken(CDLParser.CLN, i);
		}
		public List<RarityNameContext> rarityName() {
			return getRuleContexts(RarityNameContext.class);
		}
		public RarityNameContext rarityName(int i) {
			return getRuleContext(RarityNameContext.class,i);
		}
		public List<TerminalNode> EOS() { return getTokens(CDLParser.EOS); }
		public TerminalNode EOS(int i) {
			return getToken(CDLParser.EOS, i);
		}
		public List<TerminalNode> VALIDTARGETS() { return getTokens(CDLParser.VALIDTARGETS); }
		public TerminalNode VALIDTARGETS(int i) {
			return getToken(CDLParser.VALIDTARGETS, i);
		}
		public List<ListContext> list() {
			return getRuleContexts(ListContext.class);
		}
		public ListContext list(int i) {
			return getRuleContext(ListContext.class,i);
		}
		public List<TerminalNode> APPLY() { return getTokens(CDLParser.APPLY); }
		public TerminalNode APPLY(int i) {
			return getToken(CDLParser.APPLY, i);
		}
		public CardPropertiesContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_cardProperties; }
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof CDLVisitor ) return ((CDLVisitor<? extends T>)visitor).visitCardProperties(this);
			else return visitor.visitChildren(this);
		}
	}

	public final CardPropertiesContext cardProperties() throws RecognitionException {
		CardPropertiesContext _localctx = new CardPropertiesContext(_ctx, getState());
		enterRule(_localctx, 74, RULE_cardProperties);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(361); 
			_errHandler.sync(this);
			_la = _input.LA(1);
			do {
				{
				setState(361);
				_errHandler.sync(this);
				switch (_input.LA(1)) {
				case RARITY:
					{
					setState(346);
					match(RARITY);
					setState(347);
					match(CLN);
					setState(348);
					rarityName();
					setState(349);
					match(EOS);
					}
					break;
				case VALIDTARGETS:
					{
					setState(351);
					match(VALIDTARGETS);
					setState(352);
					match(CLN);
					setState(353);
					list();
					setState(354);
					match(EOS);
					}
					break;
				case APPLY:
					{
					setState(356);
					match(APPLY);
					setState(357);
					match(CLN);
					setState(358);
					list();
					setState(359);
					match(EOS);
					}
					break;
				default:
					throw new NoViableAltException(this);
				}
				}
				setState(363); 
				_errHandler.sync(this);
				_la = _input.LA(1);
			} while ( (((_la) & ~0x3f) == 0 && ((1L << _la) & 100663808L) != 0) );
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public boolean sempred(RuleContext _localctx, int ruleIndex, int predIndex) {
		switch (ruleIndex) {
		case 5:
			return expression_sempred((ExpressionContext)_localctx, predIndex);
		}
		return true;
	}
	private boolean expression_sempred(ExpressionContext _localctx, int predIndex) {
		switch (predIndex) {
		case 0:
			return precpred(_ctx, 2);
		case 1:
			return precpred(_ctx, 1);
		}
		return true;
	}

	public static final String _serializedATN =
		"\u0004\u0001B\u016e\u0002\u0000\u0007\u0000\u0002\u0001\u0007\u0001\u0002"+
		"\u0002\u0007\u0002\u0002\u0003\u0007\u0003\u0002\u0004\u0007\u0004\u0002"+
		"\u0005\u0007\u0005\u0002\u0006\u0007\u0006\u0002\u0007\u0007\u0007\u0002"+
		"\b\u0007\b\u0002\t\u0007\t\u0002\n\u0007\n\u0002\u000b\u0007\u000b\u0002"+
		"\f\u0007\f\u0002\r\u0007\r\u0002\u000e\u0007\u000e\u0002\u000f\u0007\u000f"+
		"\u0002\u0010\u0007\u0010\u0002\u0011\u0007\u0011\u0002\u0012\u0007\u0012"+
		"\u0002\u0013\u0007\u0013\u0002\u0014\u0007\u0014\u0002\u0015\u0007\u0015"+
		"\u0002\u0016\u0007\u0016\u0002\u0017\u0007\u0017\u0002\u0018\u0007\u0018"+
		"\u0002\u0019\u0007\u0019\u0002\u001a\u0007\u001a\u0002\u001b\u0007\u001b"+
		"\u0002\u001c\u0007\u001c\u0002\u001d\u0007\u001d\u0002\u001e\u0007\u001e"+
		"\u0002\u001f\u0007\u001f\u0002 \u0007 \u0002!\u0007!\u0002\"\u0007\"\u0002"+
		"#\u0007#\u0002$\u0007$\u0002%\u0007%\u0001\u0000\u0001\u0000\u0001\u0000"+
		"\u0001\u0000\u0001\u0000\u0001\u0000\u0001\u0000\u0004\u0000T\b\u0000"+
		"\u000b\u0000\f\u0000U\u0001\u0001\u0001\u0001\u0001\u0002\u0003\u0002"+
		"[\b\u0002\u0001\u0002\u0001\u0002\u0001\u0003\u0001\u0003\u0001\u0004"+
		"\u0001\u0004\u0001\u0005\u0001\u0005\u0001\u0005\u0001\u0005\u0001\u0005"+
		"\u0001\u0005\u0001\u0005\u0001\u0005\u0001\u0005\u0001\u0005\u0001\u0005"+
		"\u0005\u0005n\b\u0005\n\u0005\f\u0005q\t\u0005\u0001\u0006\u0001\u0006"+
		"\u0001\u0007\u0001\u0007\u0001\b\u0001\b\u0003\by\b\b\u0001\t\u0001\t"+
		"\u0001\t\u0001\t\u0001\n\u0001\n\u0001\n\u0003\n\u0082\b\n\u0001\u000b"+
		"\u0001\u000b\u0001\u000b\u0001\u000b\u0005\u000b\u0088\b\u000b\n\u000b"+
		"\f\u000b\u008b\t\u000b\u0003\u000b\u008d\b\u000b\u0001\u000b\u0001\u000b"+
		"\u0001\f\u0001\f\u0001\f\u0001\f\u0001\f\u0001\f\u0001\f\u0001\f\u0001"+
		"\f\u0001\f\u0001\f\u0001\f\u0001\f\u0003\f\u009e\b\f\u0001\f\u0001\f\u0001"+
		"\f\u0001\f\u0003\f\u00a4\b\f\u0001\r\u0001\r\u0001\u000e\u0001\u000e\u0001"+
		"\u000e\u0001\u000e\u0001\u000e\u0001\u000f\u0001\u000f\u0001\u000f\u0001"+
		"\u000f\u0001\u000f\u0001\u000f\u0001\u000f\u0001\u000f\u0001\u000f\u0001"+
		"\u000f\u0001\u000f\u0001\u000f\u0001\u000f\u0001\u000f\u0001\u000f\u0004"+
		"\u000f\u00bc\b\u000f\u000b\u000f\f\u000f\u00bd\u0001\u0010\u0001\u0010"+
		"\u0001\u0010\u0001\u0010\u0001\u0010\u0001\u0010\u0001\u0011\u0001\u0011"+
		"\u0001\u0011\u0001\u0011\u0001\u0011\u0001\u0011\u0004\u0011\u00cc\b\u0011"+
		"\u000b\u0011\f\u0011\u00cd\u0001\u0012\u0001\u0012\u0001\u0012\u0001\u0012"+
		"\u0001\u0012\u0001\u0013\u0001\u0013\u0001\u0013\u0001\u0013\u0001\u0013"+
		"\u0001\u0014\u0001\u0014\u0001\u0014\u0001\u0014\u0001\u0014\u0001\u0015"+
		"\u0001\u0015\u0001\u0015\u0001\u0015\u0001\u0015\u0001\u0016\u0001\u0016"+
		"\u0001\u0016\u0001\u0016\u0001\u0016\u0001\u0017\u0001\u0017\u0001\u0017"+
		"\u0001\u0017\u0001\u0017\u0001\u0018\u0001\u0018\u0001\u0018\u0001\u0018"+
		"\u0001\u0018\u0001\u0018\u0001\u0019\u0001\u0019\u0001\u0019\u0001\u0019"+
		"\u0001\u0019\u0001\u0019\u0001\u0019\u0001\u0019\u0001\u0019\u0001\u0019"+
		"\u0004\u0019\u00fe\b\u0019\u000b\u0019\f\u0019\u00ff\u0001\u001a\u0001"+
		"\u001a\u0001\u001a\u0001\u001a\u0001\u001a\u0001\u001a\u0001\u001b\u0001"+
		"\u001b\u0001\u001b\u0001\u001b\u0001\u001b\u0001\u001b\u0001\u001b\u0001"+
		"\u001b\u0001\u001b\u0001\u001b\u0004\u001b\u0112\b\u001b\u000b\u001b\f"+
		"\u001b\u0113\u0001\u001c\u0001\u001c\u0001\u001c\u0001\u001c\u0001\u001c"+
		"\u0001\u001c\u0001\u001d\u0001\u001d\u0001\u001d\u0001\u001d\u0001\u001d"+
		"\u0001\u001d\u0001\u001d\u0001\u001d\u0001\u001d\u0001\u001d\u0004\u001d"+
		"\u0126\b\u001d\u000b\u001d\f\u001d\u0127\u0001\u001e\u0001\u001e\u0001"+
		"\u001e\u0001\u001e\u0004\u001e\u012e\b\u001e\u000b\u001e\f\u001e\u012f"+
		"\u0001\u001e\u0001\u001e\u0001\u001f\u0001\u001f\u0003\u001f\u0136\b\u001f"+
		"\u0001 \u0001 \u0001 \u0001 \u0001 \u0001 \u0001!\u0001!\u0001!\u0001"+
		"!\u0001!\u0001!\u0001!\u0001!\u0001!\u0001!\u0001!\u0001!\u0001!\u0001"+
		"!\u0001!\u0004!\u014d\b!\u000b!\f!\u014e\u0001\"\u0001\"\u0001#\u0001"+
		"#\u0001$\u0001$\u0001$\u0001$\u0001$\u0001$\u0001%\u0001%\u0001%\u0001"+
		"%\u0001%\u0001%\u0001%\u0001%\u0001%\u0001%\u0001%\u0001%\u0001%\u0001"+
		"%\u0001%\u0004%\u016a\b%\u000b%\f%\u016b\u0001%\u0000\u0001\n&\u0000\u0002"+
		"\u0004\u0006\b\n\f\u000e\u0010\u0012\u0014\u0016\u0018\u001a\u001c\u001e"+
		" \"$&(*,.02468:<>@BDFHJ\u0000\b\u0001\u0000=>\u0001\u000001\u0001\u0000"+
		"./\u0002\u0000\u0005\u0005::\u0002\u0000\u0010\u0011,,\u0001\u0000\u0012"+
		"\u0013\u0001\u0000\u000b\f\u0002\u0000\u0010\u0011\u0016\u0016\u0171\u0000"+
		"S\u0001\u0000\u0000\u0000\u0002W\u0001\u0000\u0000\u0000\u0004Z\u0001"+
		"\u0000\u0000\u0000\u0006^\u0001\u0000\u0000\u0000\b`\u0001\u0000\u0000"+
		"\u0000\nb\u0001\u0000\u0000\u0000\fr\u0001\u0000\u0000\u0000\u000et\u0001"+
		"\u0000\u0000\u0000\u0010x\u0001\u0000\u0000\u0000\u0012z\u0001\u0000\u0000"+
		"\u0000\u0014\u0081\u0001\u0000\u0000\u0000\u0016\u0083\u0001\u0000\u0000"+
		"\u0000\u0018\u00a3\u0001\u0000\u0000\u0000\u001a\u00a5\u0001\u0000\u0000"+
		"\u0000\u001c\u00a7\u0001\u0000\u0000\u0000\u001e\u00bb\u0001\u0000\u0000"+
		"\u0000 \u00bf\u0001\u0000\u0000\u0000\"\u00cb\u0001\u0000\u0000\u0000"+
		"$\u00cf\u0001\u0000\u0000\u0000&\u00d4\u0001\u0000\u0000\u0000(\u00d9"+
		"\u0001\u0000\u0000\u0000*\u00de\u0001\u0000\u0000\u0000,\u00e3\u0001\u0000"+
		"\u0000\u0000.\u00e8\u0001\u0000\u0000\u00000\u00ed\u0001\u0000\u0000\u0000"+
		"2\u00fd\u0001\u0000\u0000\u00004\u0101\u0001\u0000\u0000\u00006\u0111"+
		"\u0001\u0000\u0000\u00008\u0115\u0001\u0000\u0000\u0000:\u0125\u0001\u0000"+
		"\u0000\u0000<\u0129\u0001\u0000\u0000\u0000>\u0135\u0001\u0000\u0000\u0000"+
		"@\u0137\u0001\u0000\u0000\u0000B\u014c\u0001\u0000\u0000\u0000D\u0150"+
		"\u0001\u0000\u0000\u0000F\u0152\u0001\u0000\u0000\u0000H\u0154\u0001\u0000"+
		"\u0000\u0000J\u0169\u0001\u0000\u0000\u0000LT\u0003\u001c\u000e\u0000"+
		"MT\u0003 \u0010\u0000NT\u00030\u0018\u0000OT\u00034\u001a\u0000PT\u0003"+
		"8\u001c\u0000QT\u0003<\u001e\u0000RT\u0003H$\u0000SL\u0001\u0000\u0000"+
		"\u0000SM\u0001\u0000\u0000\u0000SN\u0001\u0000\u0000\u0000SO\u0001\u0000"+
		"\u0000\u0000SP\u0001\u0000\u0000\u0000SQ\u0001\u0000\u0000\u0000SR\u0001"+
		"\u0000\u0000\u0000TU\u0001\u0000\u0000\u0000US\u0001\u0000\u0000\u0000"+
		"UV\u0001\u0000\u0000\u0000V\u0001\u0001\u0000\u0000\u0000WX\u0005?\u0000"+
		"\u0000X\u0003\u0001\u0000\u0000\u0000Y[\u00051\u0000\u0000ZY\u0001\u0000"+
		"\u0000\u0000Z[\u0001\u0000\u0000\u0000[\\\u0001\u0000\u0000\u0000\\]\u0007"+
		"\u0000\u0000\u0000]\u0005\u0001\u0000\u0000\u0000^_\u0005?\u0000\u0000"+
		"_\u0007\u0001\u0000\u0000\u0000`a\u0005?\u0000\u0000a\t\u0001\u0000\u0000"+
		"\u0000bc\u0006\u0005\uffff\uffff\u0000cd\u0003\u0010\b\u0000do\u0001\u0000"+
		"\u0000\u0000ef\n\u0002\u0000\u0000fg\u0003\f\u0006\u0000gh\u0003\n\u0005"+
		"\u0003hn\u0001\u0000\u0000\u0000ij\n\u0001\u0000\u0000jk\u0003\u000e\u0007"+
		"\u0000kl\u0003\n\u0005\u0002ln\u0001\u0000\u0000\u0000me\u0001\u0000\u0000"+
		"\u0000mi\u0001\u0000\u0000\u0000nq\u0001\u0000\u0000\u0000om\u0001\u0000"+
		"\u0000\u0000op\u0001\u0000\u0000\u0000p\u000b\u0001\u0000\u0000\u0000"+
		"qo\u0001\u0000\u0000\u0000rs\u0007\u0001\u0000\u0000s\r\u0001\u0000\u0000"+
		"\u0000tu\u0007\u0002\u0000\u0000u\u000f\u0001\u0000\u0000\u0000vy\u0003"+
		"\u0012\t\u0000wy\u0003\u0014\n\u0000xv\u0001\u0000\u0000\u0000xw\u0001"+
		"\u0000\u0000\u0000y\u0011\u0001\u0000\u0000\u0000z{\u00058\u0000\u0000"+
		"{|\u0003\n\u0005\u0000|}\u00059\u0000\u0000}\u0013\u0001\u0000\u0000\u0000"+
		"~\u0082\u0003\u0004\u0002\u0000\u007f\u0082\u0003\u0006\u0003\u0000\u0080"+
		"\u0082\u0005\n\u0000\u0000\u0081~\u0001\u0000\u0000\u0000\u0081\u007f"+
		"\u0001\u0000\u0000\u0000\u0081\u0080\u0001\u0000\u0000\u0000\u0082\u0015"+
		"\u0001\u0000\u0000\u0000\u0083\u008c\u00054\u0000\u0000\u0084\u0089\u0003"+
		"\u0018\f\u0000\u0085\u0086\u0007\u0003\u0000\u0000\u0086\u0088\u0003\u0018"+
		"\f\u0000\u0087\u0085\u0001\u0000\u0000\u0000\u0088\u008b\u0001\u0000\u0000"+
		"\u0000\u0089\u0087\u0001\u0000\u0000\u0000\u0089\u008a\u0001\u0000\u0000"+
		"\u0000\u008a\u008d\u0001\u0000\u0000\u0000\u008b\u0089\u0001\u0000\u0000"+
		"\u0000\u008c\u0084\u0001\u0000\u0000\u0000\u008c\u008d\u0001\u0000\u0000"+
		"\u0000\u008d\u008e\u0001\u0000\u0000\u0000\u008e\u008f\u00055\u0000\u0000"+
		"\u008f\u0017\u0001\u0000\u0000\u0000\u0090\u00a4\u0003\u0006\u0003\u0000"+
		"\u0091\u0092\u0005=\u0000\u0000\u0092\u0093\u0005-\u0000\u0000\u0093\u00a4"+
		"\u0003\u0006\u0003\u0000\u0094\u0095\u0005=\u0000\u0000\u0095\u0096\u0005"+
		"-\u0000\u0000\u0096\u0097\u0003\u0006\u0003\u0000\u0097\u0098\u0005\u0004"+
		"\u0000\u0000\u0098\u0099\u0005=\u0000\u0000\u0099\u009a\u0005;\u0000\u0000"+
		"\u009a\u00a4\u0001\u0000\u0000\u0000\u009b\u009c\u0005=\u0000\u0000\u009c"+
		"\u009e\u0005-\u0000\u0000\u009d\u009b\u0001\u0000\u0000\u0000\u009d\u009e"+
		"\u0001\u0000\u0000\u0000\u009e\u009f\u0001\u0000\u0000\u0000\u009f\u00a0"+
		"\u0003\u0006\u0003\u0000\u00a0\u00a1\u0005\u0011\u0000\u0000\u00a1\u00a4"+
		"\u0001\u0000\u0000\u0000\u00a2\u00a4\u0003\u001a\r\u0000\u00a3\u0090\u0001"+
		"\u0000\u0000\u0000\u00a3\u0091\u0001\u0000\u0000\u0000\u00a3\u0094\u0001"+
		"\u0000\u0000\u0000\u00a3\u009d\u0001\u0000\u0000\u0000\u00a3\u00a2\u0001"+
		"\u0000\u0000\u0000\u00a4\u0019\u0001\u0000\u0000\u0000\u00a5\u00a6\u0007"+
		"\u0004\u0000\u0000\u00a6\u001b\u0001\u0000\u0000\u0000\u00a7\u00a8\u0005"+
		"\u001d\u0000\u0000\u00a8\u00a9\u00056\u0000\u0000\u00a9\u00aa\u0003\u001e"+
		"\u000f\u0000\u00aa\u00ab\u00057\u0000\u0000\u00ab\u001d\u0001\u0000\u0000"+
		"\u0000\u00ac\u00ad\u0005*\u0000\u0000\u00ad\u00ae\u00053\u0000\u0000\u00ae"+
		"\u00af\u0003\u0002\u0001\u0000\u00af\u00b0\u0005A\u0000\u0000\u00b0\u00bc"+
		"\u0001\u0000\u0000\u0000\u00b1\u00b2\u0005\u001e\u0000\u0000\u00b2\u00b3"+
		"\u00053\u0000\u0000\u00b3\u00b4\u0003\u0016\u000b\u0000\u00b4\u00b5\u0005"+
		"A\u0000\u0000\u00b5\u00bc\u0001\u0000\u0000\u0000\u00b6\u00b7\u0005)\u0000"+
		"\u0000\u00b7\u00b8\u00053\u0000\u0000\u00b8\u00b9\u0003\u0002\u0001\u0000"+
		"\u00b9\u00ba\u0005A\u0000\u0000\u00ba\u00bc\u0001\u0000\u0000\u0000\u00bb"+
		"\u00ac\u0001\u0000\u0000\u0000\u00bb\u00b1\u0001\u0000\u0000\u0000\u00bb"+
		"\u00b6\u0001\u0000\u0000\u0000\u00bc\u00bd\u0001\u0000\u0000\u0000\u00bd"+
		"\u00bb\u0001\u0000\u0000\u0000\u00bd\u00be\u0001\u0000\u0000\u0000\u00be"+
		"\u001f\u0001\u0000\u0000\u0000\u00bf\u00c0\u0005\'\u0000\u0000\u00c0\u00c1"+
		"\u0003\u0002\u0001\u0000\u00c1\u00c2\u00056\u0000\u0000\u00c2\u00c3\u0003"+
		"\"\u0011\u0000\u00c3\u00c4\u00057\u0000\u0000\u00c4!\u0001\u0000\u0000"+
		"\u0000\u00c5\u00cc\u0003$\u0012\u0000\u00c6\u00cc\u0003(\u0014\u0000\u00c7"+
		"\u00cc\u0003&\u0013\u0000\u00c8\u00cc\u0003*\u0015\u0000\u00c9\u00cc\u0003"+
		",\u0016\u0000\u00ca\u00cc\u0003.\u0017\u0000\u00cb\u00c5\u0001\u0000\u0000"+
		"\u0000\u00cb\u00c6\u0001\u0000\u0000\u0000\u00cb\u00c7\u0001\u0000\u0000"+
		"\u0000\u00cb\u00c8\u0001\u0000\u0000\u0000\u00cb\u00c9\u0001\u0000\u0000"+
		"\u0000\u00cb\u00ca\u0001\u0000\u0000\u0000\u00cc\u00cd\u0001\u0000\u0000"+
		"\u0000\u00cd\u00cb\u0001\u0000\u0000\u0000\u00cd\u00ce\u0001\u0000\u0000"+
		"\u0000\u00ce#\u0001\u0000\u0000\u0000\u00cf\u00d0\u0005\u001f\u0000\u0000"+
		"\u00d0\u00d1\u00053\u0000\u0000\u00d1\u00d2\u0003\u0004\u0002\u0000\u00d2"+
		"\u00d3\u0005A\u0000\u0000\u00d3%\u0001\u0000\u0000\u0000\u00d4\u00d5\u0005"+
		"!\u0000\u0000\u00d5\u00d6\u00053\u0000\u0000\u00d6\u00d7\u0003\u0004\u0002"+
		"\u0000\u00d7\u00d8\u0005A\u0000\u0000\u00d8\'\u0001\u0000\u0000\u0000"+
		"\u00d9\u00da\u0005 \u0000\u0000\u00da\u00db\u00053\u0000\u0000\u00db\u00dc"+
		"\u0003\u0004\u0002\u0000\u00dc\u00dd\u0005A\u0000\u0000\u00dd)\u0001\u0000"+
		"\u0000\u0000\u00de\u00df\u0005\"\u0000\u0000\u00df\u00e0\u00053\u0000"+
		"\u0000\u00e0\u00e1\u0003\u0016\u000b\u0000\u00e1\u00e2\u0005A\u0000\u0000"+
		"\u00e2+\u0001\u0000\u0000\u0000\u00e3\u00e4\u0005#\u0000\u0000\u00e4\u00e5"+
		"\u00053\u0000\u0000\u00e5\u00e6\u0003\u0016\u000b\u0000\u00e6\u00e7\u0005"+
		"A\u0000\u0000\u00e7-\u0001\u0000\u0000\u0000\u00e8\u00e9\u0005$\u0000"+
		"\u0000\u00e9\u00ea\u00053\u0000\u0000\u00ea\u00eb\u0003\u0002\u0001\u0000"+
		"\u00eb\u00ec\u0005A\u0000\u0000\u00ec/\u0001\u0000\u0000\u0000\u00ed\u00ee"+
		"\u0005%\u0000\u0000\u00ee\u00ef\u0003\u0002\u0001\u0000\u00ef\u00f0\u0005"+
		"6\u0000\u0000\u00f0\u00f1\u00032\u0019\u0000\u00f1\u00f2\u00057\u0000"+
		"\u0000\u00f21\u0001\u0000\u0000\u0000\u00f3\u00f4\u0005&\u0000\u0000\u00f4"+
		"\u00f5\u00053\u0000\u0000\u00f5\u00f6\u0003\u0016\u000b\u0000\u00f6\u00f7"+
		"\u0005A\u0000\u0000\u00f7\u00fe\u0001\u0000\u0000\u0000\u00f8\u00f9\u0005"+
		"(\u0000\u0000\u00f9\u00fa\u00053\u0000\u0000\u00fa\u00fb\u0003\u0016\u000b"+
		"\u0000\u00fb\u00fc\u0005A\u0000\u0000\u00fc\u00fe\u0001\u0000\u0000\u0000"+
		"\u00fd\u00f3\u0001\u0000\u0000\u0000\u00fd\u00f8\u0001\u0000\u0000\u0000"+
		"\u00fe\u00ff\u0001\u0000\u0000\u0000\u00ff\u00fd\u0001\u0000\u0000\u0000"+
		"\u00ff\u0100\u0001\u0000\u0000\u0000\u01003\u0001\u0000\u0000\u0000\u0101"+
		"\u0102\u0005\u0001\u0000\u0000\u0102\u0103\u0003\u0002\u0001\u0000\u0103"+
		"\u0104\u00056\u0000\u0000\u0104\u0105\u00036\u001b\u0000\u0105\u0106\u0005"+
		"7\u0000\u0000\u01065\u0001\u0000\u0000\u0000\u0107\u0108\u0005\u0002\u0000"+
		"\u0000\u0108\u0109\u00053\u0000\u0000\u0109\u010a\u0003\u0004\u0002\u0000"+
		"\u010a\u010b\u0005A\u0000\u0000\u010b\u0112\u0001\u0000\u0000\u0000\u010c"+
		"\u010d\u0005\u0003\u0000\u0000\u010d\u010e\u00053\u0000\u0000\u010e\u010f"+
		"\u0003\u0016\u000b\u0000\u010f\u0110\u0005A\u0000\u0000\u0110\u0112\u0001"+
		"\u0000\u0000\u0000\u0111\u0107\u0001\u0000\u0000\u0000\u0111\u010c\u0001"+
		"\u0000\u0000\u0000\u0112\u0113\u0001\u0000\u0000\u0000\u0113\u0111\u0001"+
		"\u0000\u0000\u0000\u0113\u0114\u0001\u0000\u0000\u0000\u01147\u0001\u0000"+
		"\u0000\u0000\u0115\u0116\u0005+\u0000\u0000\u0116\u0117\u0003\u0002\u0001"+
		"\u0000\u0117\u0118\u00056\u0000\u0000\u0118\u0119\u0003:\u001d\u0000\u0119"+
		"\u011a\u00057\u0000\u0000\u011a9\u0001\u0000\u0000\u0000\u011b\u011c\u0005"+
		"\u0002\u0000\u0000\u011c\u011d\u00053\u0000\u0000\u011d\u011e\u0003\u0004"+
		"\u0002\u0000\u011e\u011f\u0005A\u0000\u0000\u011f\u0126\u0001\u0000\u0000"+
		"\u0000\u0120\u0121\u0005\u0006\u0000\u0000\u0121\u0122\u00053\u0000\u0000"+
		"\u0122\u0123\u0003\u0016\u000b\u0000\u0123\u0124\u0005A\u0000\u0000\u0124"+
		"\u0126\u0001\u0000\u0000\u0000\u0125\u011b\u0001\u0000\u0000\u0000\u0125"+
		"\u0120\u0001\u0000\u0000\u0000\u0126\u0127\u0001\u0000\u0000\u0000\u0127"+
		"\u0125\u0001\u0000\u0000\u0000\u0127\u0128\u0001\u0000\u0000\u0000\u0128"+
		";\u0001\u0000\u0000\u0000\u0129\u012a\u0005\u0007\u0000\u0000\u012a\u012b"+
		"\u0003\u0002\u0001\u0000\u012b\u012d\u00056\u0000\u0000\u012c\u012e\u0003"+
		">\u001f\u0000\u012d\u012c\u0001\u0000\u0000\u0000\u012e\u012f\u0001\u0000"+
		"\u0000\u0000\u012f\u012d\u0001\u0000\u0000\u0000\u012f\u0130\u0001\u0000"+
		"\u0000\u0000\u0130\u0131\u0001\u0000\u0000\u0000\u0131\u0132\u00057\u0000"+
		"\u0000\u0132=\u0001\u0000\u0000\u0000\u0133\u0136\u0003@ \u0000\u0134"+
		"\u0136\u0003B!\u0000\u0135\u0133\u0001\u0000\u0000\u0000\u0135\u0134\u0001"+
		"\u0000\u0000\u0000\u0136?\u0001\u0000\u0000\u0000\u0137\u0138\u0007\u0005"+
		"\u0000\u0000\u0138\u0139\u0005\n\u0000\u0000\u0139\u013a\u0005\u0014\u0000"+
		"\u0000\u013a\u013b\u0003\n\u0005\u0000\u013b\u013c\u0005A\u0000\u0000"+
		"\u013cA\u0001\u0000\u0000\u0000\u013d\u013e\u0005\u0017\u0000\u0000\u013e"+
		"\u013f\u0003\n\u0005\u0000\u013f\u0140\u0005\n\u0000\u0000\u0140\u0141"+
		"\u0003D\"\u0000\u0141\u0142\u0005A\u0000\u0000\u0142\u014d\u0001\u0000"+
		"\u0000\u0000\u0143\u0144\u0005\t\u0000\u0000\u0144\u0145\u0003\u0016\u000b"+
		"\u0000\u0145\u0146\u0005\r\u0000\u0000\u0146\u0147\u0003\u0004\u0002\u0000"+
		"\u0147\u0148\u0005\u000e\u0000\u0000\u0148\u0149\u0005\u000f\u0000\u0000"+
		"\u0149\u014a\u0003F#\u0000\u014a\u014b\u0005A\u0000\u0000\u014b\u014d"+
		"\u0001\u0000\u0000\u0000\u014c\u013d\u0001\u0000\u0000\u0000\u014c\u0143"+
		"\u0001\u0000\u0000\u0000\u014d\u014e\u0001\u0000\u0000\u0000\u014e\u014c"+
		"\u0001\u0000\u0000\u0000\u014e\u014f\u0001\u0000\u0000\u0000\u014fC\u0001"+
		"\u0000\u0000\u0000\u0150\u0151\u0007\u0006\u0000\u0000\u0151E\u0001\u0000"+
		"\u0000\u0000\u0152\u0153\u0007\u0007\u0000\u0000\u0153G\u0001\u0000\u0000"+
		"\u0000\u0154\u0155\u0005\u0018\u0000\u0000\u0155\u0156\u0003\u0002\u0001"+
		"\u0000\u0156\u0157\u00056\u0000\u0000\u0157\u0158\u0003J%\u0000\u0158"+
		"\u0159\u00057\u0000\u0000\u0159I\u0001\u0000\u0000\u0000\u015a\u015b\u0005"+
		"\u0019\u0000\u0000\u015b\u015c\u00053\u0000\u0000\u015c\u015d\u0003\b"+
		"\u0004\u0000\u015d\u015e\u0005A\u0000\u0000\u015e\u016a\u0001\u0000\u0000"+
		"\u0000\u015f\u0160\u0005\u001a\u0000\u0000\u0160\u0161\u00053\u0000\u0000"+
		"\u0161\u0162\u0003\u0016\u000b\u0000\u0162\u0163\u0005A\u0000\u0000\u0163"+
		"\u016a\u0001\u0000\u0000\u0000\u0164\u0165\u0005\t\u0000\u0000\u0165\u0166"+
		"\u00053\u0000\u0000\u0166\u0167\u0003\u0016\u000b\u0000\u0167\u0168\u0005"+
		"A\u0000\u0000\u0168\u016a\u0001\u0000\u0000\u0000\u0169\u015a\u0001\u0000"+
		"\u0000\u0000\u0169\u015f\u0001\u0000\u0000\u0000\u0169\u0164\u0001\u0000"+
		"\u0000\u0000\u016a\u016b\u0001\u0000\u0000\u0000\u016b\u0169\u0001\u0000"+
		"\u0000\u0000\u016b\u016c\u0001\u0000\u0000\u0000\u016cK\u0001\u0000\u0000"+
		"\u0000\u001bSUZmox\u0081\u0089\u008c\u009d\u00a3\u00bb\u00bd\u00cb\u00cd"+
		"\u00fd\u00ff\u0111\u0113\u0125\u0127\u012f\u0135\u014c\u014e\u0169\u016b";
	public static final ATN _ATN =
		new ATNDeserializer().deserialize(_serializedATN.toCharArray());
	static {
		_decisionToDFA = new DFA[_ATN.getNumberOfDecisions()];
		for (int i = 0; i < _ATN.getNumberOfDecisions(); i++) {
			_decisionToDFA[i] = new DFA(_ATN.getDecisionState(i), i);
		}
	}
}