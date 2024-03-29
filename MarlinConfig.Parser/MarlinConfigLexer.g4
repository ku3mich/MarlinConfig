lexer grammar MarlinConfigLexer;

import CrLf, Symbols;

COMMENT_START: '/*';
COMMENT_END: '*/';
LINE_COMMENT: '//';

LF: S_LF;
CR: S_CR;
SQUOTE: S_SQUOTE;
DQUOTE: S_DQUOTE;
HASH: S_HASH;
SLASH: S_SLASH;
BSLASH: S_BSLASH;
L_BRACE: S_L_BRACE;
R_BRACE: S_R_BRACE;
L_PAREN: S_L_PAREN;
R_PAREN: S_R_PAREN;
EQUAL: S_EQUAL;
EXCLAM: S_EXCLAM;
COMMA: S_COMMA;
AMP: S_AMP;
PIPE: S_PIPE;
GT: S_GT;
LT: S_LT;
LETTER: [A-Za-z_];
DIGIT: [0-9];
DOT: '.';
SP: [ \t];

HASH_PRAGMA: S_HASH 'pragma';
HASH_INCLUDE: S_HASH 'include';
HASH_DEFINE: S_HASH 'define';
HASH_IF: S_HASH 'if';
HASH_ELSE: S_HASH 'else';
HASH_ENDIF: S_HASH 'endif';
HASH_IFDEF: S_HASH 'ifdef';
HASH_IFNDEF: S_HASH 'ifndef';
HASH_UNDEF: S_HASH 'undef';
HASH_ELIF: S_HASH 'elif';

ANY: .;


