using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course_Work_TA
{
    internal class Parser_LR
    {
        private List<Token> tokens;
        private int index = 0;
        private TokenType currentLexem;
        public Parser_LR(List<Token> tokens)
        {
            this.tokens = tokens;
            currentLexem = tokens[index++].Type;
        }
        private void Next()
        {
            if (index < tokens.Count)
            {
                currentLexem = tokens[index].Type;
                index++;
            }
        }
    }
}
