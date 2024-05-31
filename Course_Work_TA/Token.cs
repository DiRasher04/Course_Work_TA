using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course_Work_TA
{
    internal class Token
    {
        public TokenType Type;
        public string Value;
        public Token(string value, TokenType type)
        {
            Value = value;
            Type = type;
        }
        public override string ToString()
        {
            if (Type == TokenType.ID || Type == TokenType.LIT)
            {
                return string.Format("{0} : {1}", Type, Value);
            }
            else
            {
                return string.Format("{0}", Type);
            }
            
        }
    }
}
