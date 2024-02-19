parser grammar MarlinConfigParser;

options { tokenVocab=MarlinConfigLexer; }

@members {
  bool areSame(string a, string b) => string.CompareOrdinal(a, b) == 0;
}

commented_block
    : simple_directive;

single_comment
    : LINE_COMMENT (commented_block | ~(CR | LF))*;

multi_comment
    : COMMENT_START (commented_block | ~COMMENT_END)* COMMENT_END;

sp : SP+; 

eol
    : single_comment | ((CR? LF) | CR);

dq_string
    : DQUOTE (~DQUOTE | (BSLASH DQUOTE))* DQUOTE;

sq_string
    : SQUOTE (~SQUOTE | (BSLASH SQUOTE))* SQUOTE;

ltgt_string
    : LT (~GT)* GT;

identifier
    : LETTER (LETTER | DIGIT)*;

include_filename: ltgt_string | dq_string;

directive_include
    : HASH_INCLUDE sp+ include_filename;

directive_undef:
    HASH_UNDEF sp identifier;

directive_pragma
    : HASH_PRAGMA sp LETTER+;

define_value
    : ((BSLASH (CR? LF)) | ~(CR | LF | LINE_COMMENT))+;

define_symbol
    : identifier | macro_call;

directive_define returns [string Symbol, string Value]
    : HASH_DEFINE sp+ s=define_symbol (sp+ v=define_value)? { $Symbol = $s.text; $Value=$v.text; } ;

integer
    : DIGIT+;

float
    : DIGIT* DOT DIGIT+;

numeric
    : integer | float;
    
constant
    : numeric | sq_string | dq_string;

macro_call_arg
    : constant | identifier;
    
macro_call_args
    : macro_call_arg (sp? COMMA sp? macro_call_arg)*;

macro_call
    : identifier sp? L_PAREN sp? macro_call_args? sp? R_PAREN;
    
operator
    : GT
    | LT
    | EQUAL EQUAL
    | GT EQUAL 
    | LT EQUAL 
    | EXCLAM EQUAL 
    | AMP AMP
    | PIPE PIPE
    ;

expression
    : unary sp? operator sp? unary;

unary
    : (EXCLAM unary) | identifier | macro_call | constant;

condition
    : unary | expression;

directive_if
    : HASH_IF sp condition;

directive_endif
    : HASH_ENDIF;

directive_ifdef
    : HASH_IFDEF sp+ identifier;

directive_ifndef
    : HASH_IFNDEF sp+ identifier;

directive_else
    : HASH_ELSE;

simple_directive
    : directive_define
    | directive_include
    | directive_pragma
    | directive_undef;

block:
    L_BRACE text* R_BRACE;

conditional_start
    : directive_ifndef
    | directive_ifdef
    | directive_if;

conditional_elif:
    HASH_ELIF sp condition;    

conditional_body
    : conditional_elif
    | directive_else
    | text+;

conditional_end
    : HASH_ENDIF;

conditional
    : conditional_start conditional_body* conditional_end;

text
    : simple_directive
    | single_comment
    | multi_comment
    | conditional
    | block
    | sp
    | eol;

file
    : text+ | EOF;
/*
#if
#endif
#ifndef
#ifdef
#else
ENABLED(BLTOUCH)
*/

