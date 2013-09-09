using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beijing_Inn_Order_System.Printing.TextDecoration
{
    public class TextBase : TextComponent
    {
        private string stringText;
        //private bool nextLine;

        public TextBase(string text)
        {
            this.stringText = text;
        }

        public override string GetDec()
        {
            /*if (nextLine)
            {
                return stringText + "\r\n";

            }
            else
            {*/
                return stringText;
            //}
        }

    }
}
