lexer grammar ClangLexer;

import UniCase, CrLf, Symbols;

tokens { GT }

fragment COMMENT_START: '/*';
fragment COMMENT_END: '*/';
fragment LINE_COMMENT: '//';

SINGLE_COMMENT: LINE_COMMENT ~[\n\r]*; 
MULTI_LINE_COMMENT: COMMENT_START .*? COMMENT_END;
HASH: S_HASH -> pushMode(preCompiller);
SPACE: S_SPACE+;

EOL: CR? LF;
ANY: .;

/* modes  */
mode include;
I_LT: S_LT -> mode(lg_string), type(LT);
I_DQ: S_DQUOTE -> mode(dq_string), type(DQUOTE);

mode preCompiller;
DEFINE: D E F I N E SPACE;
UNDEF: U N D E F SPACE;
INCLUDE: I N C L U D E SPACE -> pushMode(include);
IF: I F SPACE;
IFNDEF: I F N D E F SPACE;
IFDEF: I F D E F SPACE;
ELSE: E L S E;
ENDIF: E N D I F;
ELIF: E L I F SPACE;
ELSEIF: E L S E I F SPACE;
PRAGMA: P R A G M A SPACE;

L_PAREN: S_L_PAREN;
R_PAREN: S_R_PAREN;
L_BRACE: S_L_BRACE;
R_BRACE: S_R_BRACE;
COMMA: S_COMMA;
NOT: S_EXCLAM;
OR: S_PIPE S_PIPE;
AND: S_AMPER S_AMPER;
LT: S_LT;
GT: S_GT;
PLUS: S_PLUS;
MINUS: S_DASH;
LE: LT S_EQUAL;
GE: GT S_EQUAL;
EQUAL: S_EQUAL S_EQUAL;
ASSIGN: S_EQUAL;
DIV: S_SLASH;
SQUOTE: S_SQUOTE -> pushMode(sq_string);
DQUOTE: S_DQUOTE -> pushMode(dq_string);
MUL: S_ASTERISK;

fragment IDENTIFIER_SYMB: [a-zA-Z_];
IDENTIFIER : IDENTIFIER_SYMB (IDENTIFIER_SYMB | DIGIT)*;
HEX: '0' X [0-9a-fA-F]+;
NUMBER: S_DASH? DIGIT+ ('.' DIGIT*)?;

P_SPACE: S_SPACE+ -> type(SPACE);
P_SINGLE_COMMENT: LINE_COMMENT ~[\n\r]* -> popMode, type(SINGLE_COMMENT); 
P_MULTI_LINE_COMMENT: COMMENT_START .*? COMMENT_END -> popMode, type(MULTI_LINE_COMMENT);
PRE_end: EOL -> popMode, type(EOL);

mode sq_string;
SQ_BS: S_BSLASH S_SQUOTE -> type(ANY);
SQ_SQUOTE: S_SQUOTE -> popMode, type(SQUOTE);
SQ_ANY: . -> type(ANY);

mode dq_string;
DQ_BS: S_BSLASH S_DQUOTE -> type(ANY);
DQ_DQUOTE: S_DQUOTE -> popMode, type(DQUOTE);
DQ_ANY: . -> type(ANY);

mode lg_string;
LG_GT: S_GT -> popMode, type(GT);
LG_ANY: . -> type(ANY);
