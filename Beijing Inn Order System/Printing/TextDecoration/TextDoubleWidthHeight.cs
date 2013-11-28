using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Beijing_Inn_Order_System.Printing.TextDecoration
{
    class TextDoubleWidthHeight : TextDecorator
    {
        public TextDoubleWidthHeight(TextComponent textComponent) 
            : base(textComponent)
        {
            this.textDecoration = "\x1B|4C";
        }
    }
}
