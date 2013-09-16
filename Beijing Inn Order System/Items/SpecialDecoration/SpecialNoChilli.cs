using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beijing_Inn_Order_System.Items.SpecialDecoration
{
    class SpecialNoChilli : SpecialDecorator
    {
        public SpecialNoChilli(SpecialComponent specialComponent)
            : base(specialComponent)
        {
            this.englishDecoration = "No Chilli";
            this.chineseDecoration = "没有辣椒";
        }
    }
}
