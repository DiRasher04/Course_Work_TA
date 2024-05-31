using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Course_Work_TA
{
    internal class TextReader
    {
        public string TextRead()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                ofd.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                StreamReader reader = new StreamReader(ofd.FileName);
                string text = reader.ReadToEnd();
                return text;
            }
            else
            {
                return null;
            }
        }
    }
}
