parser grammar ClangParser;

options { tokenVocab=ClangLexer; }

end: EOL | EOF;

char: SQUOTE ANY SQUOTE;
string: DQUOTE ANY* DQUOTE;
array: L_BRACE SPACE? expression* SPACE? (COMMA SPACE? expression)* SPACE? R_BRACE;
identifier: IDENTIFIER (L_PAREN R_PAREN)?;

number: NUMBER | HEX;
value: identifier | number | char | string | array;

not:
    NOT expression;

negate:
    MINUS expression;

unary
    : not
    | negate
    | call
    | value
    ;

op:
    OR | AND | GT | LT | PLUS | MINUS | GE | LE | ASSIGN | EQUAL | DIV | MUL;
 
expression
    : unary 
    | L_PAREN SPACE? expression SPACE? R_PAREN
    | expression SPACE? op SPACE? expression
    ;

call locals[string Macro, string Identifier]
    : m=identifier SPACE? L_PAREN SPACE? i=identifier SPACE? R_PAREN { $Macro = $m.text; $Identifier=$i.text; };

define returns [string Identifier, string Value]
    : HASH DEFINE i=identifier (SPACE v=expression)* { $Identifier=$i.text; $Value=$v.text; } ;

undef
    : HASH UNDEF identifier;

pragma: HASH PRAGMA .*? end;

elif: HASH (ELIF | ELSEIF) expression condbody;
condbody: (block | elif | else)*;
    
else: HASH ELSE SPACE? EOL block;
if: HASH IF expression EOL condbody HASH ENDIF;
ifdef: HASH IFDEF identifier EOL condbody HASH ENDIF;
ifndef: HASH IFNDEF identifier EOL condbody HASH ENDIF;

filePath: ANY*;

localFile returns [string result]
    : DQUOTE r=filePath DQUOTE { $result = $r.text; };

pathFile returns [string result]
    :  LT r=filePath GT { $result = $r.text; };

fileName returns [bool isLocal, string fname]
    : tl=localFile { $isLocal = true; $fname=$tl.result;}
    | ta=pathFile { $isLocal = false; $fname=$ta.result;};

include locals [bool isLocal, string fname]
    : HASH INCLUDE t=fileName { $isLocal = $t.isLocal; $fname=$t.fname; } ;

stmt
    : define 
    | undef
    | include 
    | comment
    | pragma
    | if
    | ifdef
    | ifndef
    | SPACE;

comment locals [string body]
    : single=single_line_comment+ { $body=$single.result; } 
    | multi=multiline_comment { $body=$multi.result; }
    ;

single_line_comment returns [string result]
    :  t=SINGLE_COMMENT { $result = $t.text.Substring(2, $t.text.Length - 2); };

multiline_comment returns [string result]
    : t=MULTI_LINE_COMMENT { $result += $t.text.Substring(2, $t.text.Length - 4); } ;

block: stmt | empty;

file 
    : block* EOF;    

empty : (SPACE | ANY)* EOL;

    