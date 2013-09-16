using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beijing_Inn_Order_System.Items.SpecialDecoration
{
    class SpecialSauceSeperate : SpecialDecorator
    {
        public SpecialSauceSeperate(SpecialComponent specialComponent)
            : base(specialComponent)
        {
            this.englishDecoration = "Sauce Seperate";
            this.chineseDecoration = "酱分开";
        }
    }
}
