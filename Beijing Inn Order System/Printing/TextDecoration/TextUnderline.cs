﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beijing_Inn_Order_System.Printing.TextDecoration
{
    class TextUnderline : TextDecorator
    {
        public TextUnderline(TextComponent textComponent)
            : base(textComponent)
        {
            this.textDecoration = "\x1B|uC";
        }
    }
}