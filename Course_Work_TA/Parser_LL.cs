using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Course_Work_TA
{
    internal class Parser_LL
    {
        private List<Token> tokens;
        private int index = 0;
        private TokenType currentLexem;
        public List<string> ReversePolishNotation = new List<string>();
        public List<Token> lexResult;
        private Dictionary<TokenType, int> OpPriority;
        int lparCount = 0, rparCount = 0;
        public Parser_LL(List<Token> tokens, List<Token> lexResult)
        {
            this.lexResult = lexResult;
            this.tokens = tokens;
            currentLexem = tokens[index++].Type;
        }
        private void Next()
        {
            if (index < tokens.Count)
            {
                currentLexem = tokens[index].Type;
                index++;
                if(currentLexem == TokenType.LPAR)
                {
                    lparCount++;
                }
                if (currentLexem == TokenType.RPAR)
                {
                    rparCount++;
                }
            }
        }
        private TokenType ShowNext()
        {
            if (index < tokens.Count)
            {
                return tokens[index + 1].Type;
            }
            else { return currentLexem; }
        }
        public void programm()
        {
            
            if (currentLexem != TokenType.MAIN)
            {
                throw new Exception($"Ожидался main а встретился {currentLexem}");
            }
            Next();
            if (currentLexem != TokenType.LBRACE)
            {
                throw new Exception($"Ожидался ( а встретился {currentLexem}");
            }
            Next();
            if (currentLexem != TokenType.RBRACE)
            {
                throw new Exception($"Ожидался ) а встретился {currentLexem}");
            }
            Next();
            if (currentLexem != TokenType.LPAR)
            {
                throw new Exception("Ожидался { а встретился " + currentLexem);
            }
            Next();
            list_oper();
            if (currentLexem != TokenType.RPAR)
            {
                throw new Exception("Ожидался } а встретился " + currentLexem);
            }
            if (lparCount != rparCount)
            {
                throw new Exception("Ожидался } а встретился конец программы" + currentLexem);
            }
            if (tokens.Count > index)
            {
                throw new Exception("После завершённой программы осстались лишние символы");
            }
        }
        private void list_oper()
        {
            if (currentLexem == TokenType.RPAR)
            {
                return;
            }
            oper();
            Y();
        }
        private void Y()
        {
            if (currentLexem == TokenType.RPAR)
            {
                return;
            }
            else if (currentLexem == TokenType.INT || currentLexem == TokenType.FLOAT || currentLexem == TokenType.DOUBLE || currentLexem == TokenType.ID || currentLexem == TokenType.WHILE)
            {
                list_oper();
            }
            else
            {
                throw new Exception("Ожидался тип данных, цикл, идентификатор или } а встретился " + currentLexem);
            }
        }
        private void oper()
        {
            if (currentLexem == TokenType.INT || currentLexem == TokenType.FLOAT || currentLexem == TokenType.DOUBLE || currentLexem == TokenType.ID)
            {
                description();
                if (currentLexem != TokenType.SEMICOLON)
                {
                    throw new Exception("Ожидался ; а встретился " + currentLexem + " " + index);
                }
            }
            else if (currentLexem == TokenType.WHILE)
            {
                loop();
            }
            else
            {
                throw new Exception("Ожидалось присваивание или инициализация цикла а встретился " + currentLexem);
            }
            if (currentLexem != TokenType.WHILE)
            {
                Next();
            }
        }
        private void loop()
        {
            if (currentLexem != TokenType.WHILE)
            {
                throw new Exception("Ожидался while а встретился " + currentLexem);
            }
            Next();
            if (currentLexem != TokenType.LBRACE)
            {
                throw new Exception("Ожидался ( а встретился " + currentLexem);
            }
            Next();
            log_oper();
            if (currentLexem != TokenType.RBRACE)
            {
                throw new Exception("Ожидался ) а встретился " + currentLexem);
            }
            Next();
            if (currentLexem != TokenType.LPAR)
            {
                throw new Exception("Ожидался { а встретился " + currentLexem);
            }
            Next();
            list_oper();
            if (currentLexem != TokenType.RPAR)
            {
                throw new Exception("Ожидался } а встретился " + currentLexem);
            }
            Next();
        }
        private void log_oper()
        {
            operand();
            Next();
            sign();
            Next();
            operand();
            Next();
        }
        private void operand()
        {
            if (currentLexem != TokenType.ID && currentLexem != TokenType.LIT)
            {
                throw new Exception("Ожидался идентификатор или литерал а встретился " + currentLexem);
            }
        }
        private void sign()
        {
            if (currentLexem != TokenType.LESS &&
                currentLexem != TokenType.MORE &&
                currentLexem != TokenType.LESSEQUAL &&
                currentLexem != TokenType.MOREEQUAL &&
                currentLexem != TokenType.EQUALITY &&
                currentLexem != TokenType.NOTEQUAL)
            {
                throw new Exception("Ожидался логический знак а встретился " + currentLexem);
            }
        }
        private void description()
        {
            type();
            if (currentLexem != TokenType.ID)
            {
                Next();
            }
            list_vars();
        }
        private void type()
        {
            if (currentLexem != TokenType.INT &&
                currentLexem != TokenType.FLOAT &&
                currentLexem != TokenType.DOUBLE &&
                currentLexem != TokenType.ID)
            {
                throw new Exception("Ожидался идентификатор или тип данных а встретился " + currentLexem);
            }
        }
        private void list_vars()
        {
            assigment();
            X();
        }
        private void X()
        {
            if (currentLexem == TokenType.SEMICOLON)
            {
                return;
            }
            else if (currentLexem == TokenType.COMMA)
            {
                U();
            }
        }
        private void U()
        {
            if (currentLexem != TokenType.COMMA)
            {
                throw new Exception("Ожидался , а встретился " + currentLexem);
            }
            Next();
            assigment();
            X();
        }
        private void assigment()
        {
            if (currentLexem != TokenType.ID)
            {
                throw new Exception("Ожидался идентификатор а встретился " + currentLexem);
            }
            Next();
            F();
        }
        private void F()
        {
            if (currentLexem == TokenType.COMMA || currentLexem == TokenType.SEMICOLON)
            {

                return;
            }
            else if (currentLexem == TokenType.EQUAL)
            {
                Next();
                expr();
            }
        }
        private void expr()
        {
            
            if (currentLexem != TokenType.ID && currentLexem != TokenType.LIT && currentLexem != TokenType.LBRACE)
            {
                throw new Exception("Ошибка в выражении");
            }
            string buffer = string.Empty;
            Stack<TokenType> operations = new Stack<TokenType>();

            OpPriority = new Dictionary<TokenType, int>()
            {
                { TokenType.LBRACE, 0 },
                { TokenType.RBRACE, 1 },
                { TokenType.PLUS, 2 },
                { TokenType.MINUS, 2 },
                { TokenType.MULTIPLY, 3 },
                { TokenType.DIVISION, 3 },
            };
            int lparCount = 0, rparCount = 0;

            for (; currentLexem != TokenType.SEMICOLON && currentLexem != TokenType.COMMA; Next())
            {
                if (currentLexem == TokenType.ID || currentLexem == TokenType.LIT)
                {
                    buffer += ToValue(index - 1) + " ";
                }
                else
                {
                    if (operations.Count == 0 || currentLexem == TokenType.LBRACE)
                    {
                        operations.Push(currentLexem);
                        continue;
                    }
                    if (currentLexem == TokenType.RBRACE)
                    {
                        if (!operations.Contains(TokenType.LBRACE))
                        {
                            throw new Exception("Ошибка в выражении");
                        }
                        while (operations.Count() > 0 && operations.Peek() != TokenType.LBRACE)
                        {
                            buffer += OperationToString(operations.Pop()) + " ";
                        }
                        if (operations.Count() > 0)
                        {
                            operations.Pop();
                        }
                        continue;
                    }
                    int priority = GetOpPriority(OpPriority, currentLexem);
                    if (priority == 0 || priority > GetOpPriority(OpPriority, operations.Peek()))
                    {
                        operations.Push(currentLexem);
                    }
                    else
                    {
                        buffer += OperationToString(operations.Pop()) + " ";
                        while (operations.Count > 0)
                        {
                            int currPriority = GetOpPriority(OpPriority, operations.Peek());
                            if (currPriority >= priority)
                            {
                                buffer += OperationToString(operations.Pop()) + " ";
                                continue;
                            }
                            break;
                        }
                        operations.Push(currentLexem);
                    }
                }
            }
            if (lparCount != rparCount)
            {
                throw new Exception("Ошибка в выражении");
            }
            while (operations.Count() > 0)
            {
                buffer += OperationToString(operations.Pop()) + " ";
            }
            ReversePolishNotation.Add(buffer);
        }
        private string ToValue(int index)
        {
            return lexResult[index].Value;
        }
        private string OperationToString(TokenType operation)
        {
            switch (operation)
            {
                
                case TokenType.PLUS:
                    return "+";
                case TokenType.MINUS:
                    return "-";
                case TokenType.MULTIPLY:
                    return "*";
                case TokenType.DIVISION:
                    return "/";
                default:
                    return string.Empty;
            }
        }
        private int GetOpPriority(Dictionary<TokenType, int> opPriority, TokenType operation)
        {
            int priority;
            if (opPriority.ContainsKey(operation))
            {
                opPriority.TryGetValue(operation, out priority);
            }
            else
            {
                throw new Exception("Недопустимый символ в выражении!");
            }
            return priority;
        }
    }
}