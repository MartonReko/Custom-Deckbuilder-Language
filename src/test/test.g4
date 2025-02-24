grammar test;

//Parser

program
    :   (gameSetup
    |   stageDefinition
    |   nodeDefinition
    |   charSetup
    |   effectDefinition
    |   cardDefinition)+
    ;

varName: ID;
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
lengthDef : LENGTH CLN INT EOS;
minWidthDef:MINWIDTH CLN INT EOS;
maxWidthDef:MAXWIDTH CLN INT EOS;
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
    :   (HEALTH CLN INT EOS
    |   EFFECTEVERYTURN CLN list EOS)+
    ;
effectDefinition
    :   (passiveEffect
    |   activeEffect)+
    ;
passiveEffect
    :   (OUTGOING|INCOMING) DAMAGE IS expression;
activeEffect
    :   ((DEAL) expression DAMAGE (INSTANTLY | FOR INT TURNS TO) (ENEMIES | TARGET | PLAYER)
    |   APPLY list FOR INT TURNS TO (ENEMIES | TARGET | PLAYER))+
    ;
expression
    :   primary
        |   expression (PLUS | MINUS | MUL | DIV) expression
    ;
primary
    :   parenthesizedExpression
    |   literalExpression
    ;
parenthesizedExpression
    :   LPAREN expression RPAREN
    ;
literalExpression
    :   doubleLiteral
    |   varRef
    ;

varRef: ID;
rarityName: ID;
doubleLiteral: MINUS? DOUBLE;
cardDefinition
    :   (RARITY CLN rarityName
    |   VALIDTARGETS CLN list
    |   APPLY list  FOR INT TURNS)+
    ;
list
    :   LBRACKET
    (listItem ((OR | COMMA) listItem)*)?
    RBRACKET
    ;

listItem
    :   varRef
    |   INT X varRef
    |   INT X varRef WITHCHANCE INT PERCENT
    ;

//Lexer

// map
CHARACTER: 'Character';
HEALTH: 'Health';
EFFECTEVERYTURN: 'EffectEveryTurn';
//ENEMIES: 'enemies';
WITHCHANCE: 'with chance';
OR: 'or';
ACTIONS: 'Actions';

// effects
EFFECT: 'Effect';
EFFECTS: 'effects';
APPLY: 'Apply';
DAMAGE: 'damage';
INSTANTLY: 'instantly';
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
//EOS: ('\n' | '\r');
COMMENT: '//' ~[\r\n]* -> skip;