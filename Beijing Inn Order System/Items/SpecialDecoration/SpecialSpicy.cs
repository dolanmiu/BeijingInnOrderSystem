﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beijing_Inn_Order_System.Items.SpecialDecoration
{
    class SpecialSpicy : SpecialDecorator
    {
        public SpecialSpicy(SpecialComponent specialComponent)
            : base(specialComponent)
        {
            this.englishDecoration = "Spicy";
            this.chineseDecoration = "辣";
        }
    }
}
