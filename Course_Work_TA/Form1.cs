using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Course_Work_TA
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void ShowRPM(List<string> strings)
        {
            foreach(string s in strings)
            {
                richTextBox3.AppendText(s + "\n");
            }
        }
        private List<string> InitMatrix(List<string> rpn)
        {
            List<string> matrix = new List<string>();
            Stack<string> operands = new Stack<string>();
            string buffer = string.Empty;
            int mark;

            foreach (string item in rpn)
            {
                matrix.Add("\n" + item);
                mark = 0;
                if (item.Length == 2)
                {
                    matrix.Add($"M{mark}   {item}");
                    continue;
                }
                for (int i = 0; i < item.Length; i++)
                {
                    if (item[i] != ' ' && i < item.Length - 1)
                    {
                        buffer += item[i];
                        continue;
                    }

                    if ((buffer.Length == 1 && !Lexer.IsSpecialSymbol(buffer[0].ToString())) || (buffer.Length > 1 && !Lexer.IsSpecialWord(buffer)))
                    {
                        operands.Push(buffer);
                        buffer = string.Empty;
                    }
                    else
                    {
                        matrix.Add($"M{mark}   {buffer} {operands.Pop()} {operands.Pop()}");
                        operands.Push($"M{mark}");
                        mark++;
                        buffer = string.Empty;
                    }
                }
            }
            return matrix;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string S = richTextBox1.Text.ToLower();
            try
            {
                Lexer lexer = new Lexer();
                List<Token> tokens = lexer.LexAnaliz(S);
                richTextBox2.Clear();
                richTextBox3.Clear();
                richTextBox4.Clear();
                for (int i = 0; i < tokens.Count; i++)
                {
                    richTextBox2.Text += tokens[i].ToString() + "\n";
                }
                Parser_LL parser = new Parser_LL(tokens, tokens);
                parser.programm();
                //ShowRPM(parser.ReversePolishNotation);
                richTextBox4.Text = "Разбор успешно выполнен";
                ShowRPM(InitMatrix(parser.ReversePolishNotation));
            }
            catch (Exception exception)
            {
                richTextBox4.Text = exception.Message;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TextReader textReader = new TextReader();
            string S = textReader.TextRead();
            if (S != null)
            {
                richTextBox1.Text = S;
            }
        }
    }
}
