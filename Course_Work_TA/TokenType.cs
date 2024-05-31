using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course_Work_TA
{
    public enum TokenType
    {
        MAIN, INT, FLOAT, DOUBLE, WHILE,
        LBRACE, RBRACE, LPAR, RPAR, MORE, LESS,
        MOREEQUAL, LESSEQUAL, EQUALITY, NOTEQUAL, EQUAL, PLUS, MINUS, MULTIPLY, DIVISION,
        SEMICOLON, COMMA, POINT, ID, LIT, EXPR
    }
}