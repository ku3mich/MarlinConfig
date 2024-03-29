lexer grammar Symbols;

fragment S_DIGIT: [0-9];
fragment S_NON_DIGIT : ~[0-9];
fragment S_SQUOTE: '\'';
fragment S_DQUOTE: '"';
fragment S_GT: '>';
fragment S_LT: '<';
fragment S_HASH: '#';
fragment S_SPACE: [ \t];
fragment S_SLASH: '/';
fragment S_BSLASH: '\\';
fragment S_L_PAREN: '(';
fragment S_R_PAREN: ')';
fragment S_L_BRACE: '{';
fragment S_R_BRACE: '}';
fragment S_COMMA: ',';
fragment S_EXCLAM: '!';
fragment S_PIPE: '|';
fragment S_AMP: '&';
fragment S_DASH: '-';
fragment S_PLUS: '+';
fragment S_EQUAL: '=';
fragment S_ASTERISK: '*';

