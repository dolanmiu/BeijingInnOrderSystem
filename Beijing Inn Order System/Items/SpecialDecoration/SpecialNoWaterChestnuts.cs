using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beijing_Inn_Order_System.Items.SpecialDecoration
{
    class SpecialNoWaterChestnuts : SpecialDecorator
    {
        public SpecialNoWaterChestnuts(SpecialComponent specialComponent)
            : base(specialComponent)
        {
            this.englishDecoration = "No Water Chestnuts";
            this.chineseDecoration = "没有荸荠";
        }
    }
}
