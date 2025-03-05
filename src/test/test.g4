grammar test;

//Parser

program
    :   (gameSetup
    |   stageDefinition
    |   nodeDefinition
    |   charSetup
    |   enemyDefinition
    |   effectDefinition
    |   cardDefinition)+
    ;

varName: ID;
numberValue: MINUS? (INT | DOUBLE);
varRef: ID;
rarityName: ID;

expression
    :   primary                                                     #primaryExpression
        |   expression addSubOp expression                          #addSubExpression
        |   expression mulDivOp expression                          #mulDivExpression
    ;
addSubOp
    :   PLUS | MINUS
    ;
mulDivOp
    :   MUL | DIV
    ;
primary
    :   parenthesizedExpression
    |   literalExpression
    ;
parenthesizedExpression
    :   LPAREN expression RPAREN
    ;
literalExpression
    :   numberValue
    |   varRef
    |   DAMAGE
    ;

list
    :   LBRACKET (listItem ((OR | COMMA) listItem)*)? RBRACKET
    ;
listItem
    :   varRef
    |   INT X varRef
    |   INT X varRef WITHCHANCE INT PERCENT
    //  Enemy attacks target with varRef
    |   (INT X)? varRef (PLAYER)
    |   targetItem
    ;
targetItem
    :   ENEMIES
    |   ENEMYLC
    |   PLAYER
    ;

gameSetup: GAME LCURLY gameProperties RCURLY;
gameProperties
    :   (PLAYERSELECT CLN varName EOS
    |   STAGES CLN list EOS
    |   NAME CLN varName EOS)+
    ;

stageDefinition: STAGE varName LCURLY stageProperties RCURLY;
stageProperties
    :   (lengthDef
    |   maxWidthDef
    |   minWidthDef
    |   fillWithDef
    |   mustContainDef
    |   endsWithDef)+
    ;
lengthDef : LENGTH CLN numberValue EOS;
minWidthDef:MINWIDTH CLN numberValue EOS;
maxWidthDef:MAXWIDTH CLN numberValue EOS;
fillWithDef:FILLWITH CLN list EOS;
mustContainDef:MUSTCONTAIN CLN list EOS;
endsWithDef:ENDSWITH CLN varName EOS;

nodeDefinition: NODE varName LCURLY nodeProperties RCURLY;
nodeProperties
    :   (NODEENEMIES CLN list EOS
    |   REWARDS CLN list EOS)+
    ;

charSetup: CHARACTER varName LCURLY charProperties RCURLY;
charProperties
    :   (HEALTH CLN numberValue EOS
    |   EFFECTEVERYTURN CLN list EOS)+
    ;

enemyDefinition
    :   ENEMY varName LCURLY enemyProperties RCURLY
    ;
enemyProperties
    :   (HEALTH CLN numberValue EOS
    |   ACTIONS CLN list EOS)+
    ;

effectDefinition
    :   EFFECT varName LCURLY effectType+ RCURLY
    ;
effectType
    : passiveEffect
    | activeEffect
    ;
passiveEffect
    :   (OUTGOING|INCOMING) DAMAGE IS expression EOS;
activeEffect
    :   ((DEAL) expression DAMAGE effectActivationOpt EOS
    |   APPLY list FOR numberValue TURNS TO effectTarget EOS)+
    ;
effectActivationOpt
    :   INSTANTLY
    |   ENDOFTURN
    ;
effectTarget
    :   ENEMIES
    |   TARGET
    |   PLAYER
    ;
cardDefinition
    :   CARD varName LCURLY cardProperties RCURLY
    ;
cardProperties
    :   (RARITY CLN rarityName EOS
    |   VALIDTARGETS CLN list EOS
    |   APPLY CLN list EOS)+
    ;

//Lexer

CHARACTER: 'Character';
HEALTH: 'Health';
EFFECTEVERYTURN: 'EffectEveryTurn';
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
DESCRIPTION: 'Desc';
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

STRING: '"' (~[\r\n"])* '"';
INT: [0-9]+;
DOUBLE: [0-9]+(.[0-9]+)?;
ID: [a-zA-Z][a-zA-Z0-9_]*;
WS: (' ' | '\t' | '\n' | '\r' )-> skip;
EOS: ';';
COMMENT: '//' ~[\r\n]* -> skip;