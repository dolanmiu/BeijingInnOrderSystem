using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beijing_Inn_Order_System.Items.SpecialDecoration
{
    class SpecialNoPork : SpecialDecorator
    {
        public SpecialNoPork(SpecialComponent specialComponent)
            : base(specialComponent)
        {
            this.englishDecoration = "No Pork";
            this.chineseDecoration = "没有猪肉";
        }
    }
}
