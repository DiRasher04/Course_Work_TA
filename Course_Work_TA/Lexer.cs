using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course_Work_TA
{
    internal class Lexer
    {
        string tempWord = null, tempType = null;
        public List<Token> LexAnaliz(string S) 
        {
            S += " ";
            List<Token> tokens = new List<Token>();
            int i = 0;
            while (i < S.Length)
            {
                if (check(S[i]) == 'l')
                {
                    tempType = "l";
                    while (check(S[i]) != 'n' && check(S[i]) != 's')
                    {
                        tempWord += S[i];
                        if (tempWord.Length > 8)
                        {
                            throw new Exception($"Слишком много символов в идентификаторе: {tempWord}");
                        }
                        i++;
                    }
                    tokens.Add(GetToken(tempWord, tempType));
                    tempWord = null; tempType = null;
                    if (i < S.Length - 1 && check(S[i]) != 's')
                        i++;
                }
                if (check(S[i]) == 'd')
                {
                    tempType = "d";
                    while (check(S[i]) != 'n' && check(S[i]) != 's')
                    {
                        if (check(S[i]) == 'l')
                        {
                            throw new Exception($"Недопустимые символ '{S[i]}' в литерале: {tempWord}");
                        }
                        tempWord += S[i];
                        if(tempWord.Length > 8)
                        {
                            throw new Exception($"Слишком много символов в литерале: {tempWord}");
                        }
                        if (i < S.Length - 1 && check(S[i]) != 's')
                        i++;
                    }
                    tokens.Add(GetToken(tempWord, tempType));
                    tempWord = null; tempType = null;
                    if (i < S.Length - 1 && check(S[i]) != 's')
                        i++;
                }
                if (check(S[i]) == 's')
                {
                    tempWord += S[i];
                    tempType = "s";
                    if (S[i].ToString() + S[i + 1].ToString() == ">=" ||
                        S[i].ToString() + S[i + 1].ToString() == "<=" ||
                        S[i].ToString() + S[i + 1].ToString() == "!=" ||
                        S[i].ToString() + S[i + 1].ToString() == "==")
                    {
                        tempWord += S[i + 1];
                        i++;
                    }
                    tokens.Add(GetToken(tempWord, tempWord));
                    tempWord = null; tempType = null;
                    i++;
                }
                if (check(S[i]) == 'n')
                {
                    tempWord = null; tempType = null;
                    i++;
                }
            }
            return tokens;
        }

        private char check(char c)
        {
            if (Char. IsLetter(c))
                return 'l';
            if (Char.IsDigit(c))
                return 'd';
            if (c == ';' || c == ':' || c == ',' || c == '.' || c == '{' || c == '}' || c == '(' || c == ')' ||
                c == '+' || c == '-' || c == '*' || c == '/' || c == '=' || c == '<' || c == '>' || c == '!')
                return 's';
            if (c == ' ' || c == '\n' )
                return 'n';
            else
                throw new Exception("Недопустимый символ: " + c);
        }
        public Token GetToken(string word, string type)
        {
            if (IsSpecialWord(word))
            {
                return new Token(word, SpecialWords[word]);
            }
            if (IsSpecialSymbol(word))
            {
                return new Token(word, SpecialSymbols[word]);
            }
            if (word == "!")
            {
                throw new Exception("Недопустимое слово: !");
            }
            if (type == "d")
            {
                return new Token(word, TokenType.LIT);
            }
            if (type == "l")
            {
                return new Token(word, TokenType.ID);
            }
            else
            {
                return null;
            }
        }
        public static bool IsSpecialSymbol(string ch)
        {
            return SpecialSymbols.ContainsKey(ch);
        }
        public static bool IsSpecialWord(string word)
        {
            return SpecialWords.ContainsKey(word);
        }
        static Dictionary<string, TokenType> SpecialWords =
        new Dictionary<string, TokenType>() {
             { "main", TokenType.MAIN },
             { "int", TokenType.INT },
             { "float", TokenType.FLOAT },
             { "double", TokenType.DOUBLE },
             { "while", TokenType.WHILE }
        };
        static Dictionary<string, TokenType> SpecialSymbols =
        new Dictionary<string, TokenType>() {
             { "(", TokenType.LBRACE },
             { ")", TokenType.RBRACE },
             { "{", TokenType.LPAR },
             { "}", TokenType.RPAR },
             { ">", TokenType.MORE },
             { "<", TokenType.LESS },
             { ">=", TokenType.MOREEQUAL },
             { "<=", TokenType.LESSEQUAL },
             { "==", TokenType.EQUALITY },
             { "!=", TokenType.NOTEQUAL },
             { "=", TokenType.EQUAL },
             { "+", TokenType.PLUS },
             { "-", TokenType.MINUS },
             { "*", TokenType.MULTIPLY },
             { "/", TokenType.DIVISION },
             { ";", TokenType.SEMICOLON },
             { ",", TokenType.COMMA },
             { ".", TokenType.POINT }
        };
    }
}   
