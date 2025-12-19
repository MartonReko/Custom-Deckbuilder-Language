grammar CDL;

//Parser

program: (configBlock | variableDeclaration)+ EOF;

configBlock:
	gameSetup
	| stageDefinition
	| nodeDefinition
	| charSetup
	| enemyDefinition
	| enemyActionDefinition
	| effectDefinition
	| cardDefinition;

boolean: TRUE | FALSE;
varName: ID;
number: INT | DOUBLE;
varRef: varName | varName params;
rarityName: ID;
typeName: INTTYPE | DOUBLETYPE | STRINGTYPE | BOOLEANTYPE;
variableDeclaration:
	typeName varName EQUALS literalExpression EOS;
typeNameVarName: typeName varName;

expression:
	primary									# primaryExpression
	| expression expressionOp expression	# opExpression;
// | expression addSubOp expression # addSubExpression | expression mulDivOp expression #
// mulDivExpression;
expressionContainer: expression;
expressionOp: PLUS | MINUS | MUL | DIV;
primary: parenthesizedExpression | literalExpression;
parenthesizedExpression: LPAREN expression RPAREN;
literalExpression: INT | DOUBLE | STRING | boolean | varName;
//| DAMAGE;

list: LBRACKET (listItem (COMMA listItem)*)? RBRACKET;
listItem:
	varRef									# singleListItem
	| INT X varRef							# numberedListItem
	| INT X varRef WITHCHANCE INT PERCENT	# chanceListItem
	| (INT X)? varRef enemyTarget			# attackListItem
	| targetItem							# targetListItem;

targetItem: ENEMIES | ENEMYLC | PLAYER;
enemyTarget: PLAYER | SELF;

paramsDef:
	LPAREN typeNameVarName (COMMA typeNameVarName)? RPAREN;
params: LPAREN (varRef | literalExpression)? RPAREN;

gameSetup: GAME LCURLY gameProperties+ RCURLY;
gameProperties:
	PLAYERSELECT CLN varName EOS	# gamePlayerSelect
	| NAME CLN varName EOS			# gameName
	| STAGES CLN list EOS			# gameStages;

stageDefinition: STAGE varName LCURLY stageProperties+ RCURLY;
stageProperties:
	LENGTH CLN INT EOS			# stageLength
	| MINWIDTH CLN INT EOS		# stageWidthMin
	| MAXWIDTH CLN INT EOS		# stageWidthMax
	| FILLWITH CLN list EOS		# stageFillWith
	| MUSTCONTAIN CLN list EOS	# stageMustContain
	| ENDSWITH CLN varName EOS	# stageEndsWith;

nodeDefinition: NODE varName LCURLY nodeProperties+ RCURLY;
nodeProperties:
	NODEENEMIES CLN list EOS	# nodeEnemies
	| REWARDS CLN list EOS		# nodeRewards;

charSetup: CHARACTER varName LCURLY charProperties+ RCURLY;
charProperties:
	HEALTH CLN INT EOS			# charHealth
	| EFFECTEVERYTURN CLN list EOS	# charEffects
	| DECK CLN list EOS				# charDeck;

enemyDefinition: ENEMY varName LCURLY enemyProperties+ RCURLY;
enemyProperties:
	HEALTH CLN number EOS	# enemyHealth
	| ACTIONS CLN list EOS	# enemyActions;

enemyActionDefinition: ENEMYACTION varName LCURLY enemyActionProperties+ RCURLY;
enemyActionProperties: 
	APPLY CLN list EOS	# enemyActionEffects
	| DESC CLN STRING EOS # enemyActionDesc;

effectDefinition:
	EFFECT varName paramsDef? LCURLY effectType+ RCURLY;
effectType: passiveEffect | activeEffect;
passiveEffect:
	INCOMING DAMAGE IS expressionContainer X EOS # damageInModEffect
	| OUTGOING DAMAGE IS expressionContainer X EOS # damageOutModEffect;
activeEffect:
	(DEAL) expressionContainer DAMAGE effectActivationOpt EOS	# damageDealEffect;
effectActivationOpt: INSTANTLY | ENDOFTURN;
effectTarget: ENEMIES | TARGET | PLAYER;
cardDefinition: CARD varName LCURLY cardProperty+ RCURLY;
cardProperty:
	RARITY CLN rarityName EOS	# cardRarity
	| COST CLN INT EOS          # cardCost
	| VALIDTARGETS CLN list EOS	# cardTargets
	| APPLY CLN list EOS		# cardEffects
	;

//Lexer

CHARACTER: 'Character';
HEALTH: 'Health';
EFFECTEVERYTURN: 'EffectEveryTurn';
DECK: 'Deck';
EOS: ';';

WITHCHANCE: 'with chance';
OR: 'or';
ACTIONS: 'Actions';
EFFECT: 'Effect';
EFFECTS: 'effects';
APPLY: 'Apply';
DAMAGE: 'damage';
INSTANTLY: 'instantly';
ENDOFTURN: 'end of turn';
FOR: 'for';
TURNS: 'turns';
TO: 'to';
ENEMIES: 'enemies';
PLAYER: 'player';
OUTGOING: 'Outgoing';
INCOMING: 'Incoming';
IS: 'is';
SET: 'set';
TARGET: 'target';
DEAL: 'Deal';
CARD: 'Card';
RARITY: 'Rarity';
VALIDTARGETS: 'ValidTargets';
SELF: 'self';
DESC: 'Desc';
GAME: 'Game';
STAGES: 'Stages';
LENGTH: 'Length';
MAXWIDTH: 'Max-width';
MINWIDTH: 'Min-width';
FILLWITH: 'FillWith';
MUSTCONTAIN: 'MustContain';
ENDSWITH: 'EndsWith';
NODE: 'Node';
NODEENEMIES: 'Enemies';
STAGE: 'Stage';
REWARDS: 'Rewards';
NAME: 'Name';
PLAYERSELECT: 'Player';
ENEMY: 'Enemy';
ENEMYLC: 'enemy';
COST: 'Cost';



ENEMYACTION: 'EnemyAction';

X: 'x';
MUL: '*';
DIV: '/';
PLUS: '+';
MINUS: '-';
CARET: '^';
CLN: ':';
LBRACKET: '[';
RBRACKET: ']';
LCURLY: '{';
RCURLY: '}';
LPAREN: '(';
RPAREN: ')';
COMMA: ',';
PERCENT: '%';
EQUALS: '=';

INTTYPE: 'int';
DOUBLETYPE: 'double';
STRINGTYPE: 'string';
BOOLEANTYPE: 'bool';

TRUE: [Tt] [Rr] [Uu] [Ee];
FALSE: [Ff] [Aa] [Ll] [Ss] [Ee];

STRING: '"' (~[\r\n"])* '"';
INT: MINUS? [0-9]+;
DOUBLE: MINUS? [0-9]+ (.[0-9]+)?;
ID: [a-zA-Z][a-zA-Z0-9_]*;
WS: (' ' | '\t' | '\n' | '\r') -> skip;

COMMENT: '//' ~[\r\n]* -> skip;
