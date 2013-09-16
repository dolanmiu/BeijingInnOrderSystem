using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beijing_Inn_Order_System.Items.SpecialDecoration
{
    class SpecialNotTooHot : SpecialDecorator
    {
        public SpecialNotTooHot(SpecialComponent specialComponent)
            : base(specialComponent)
        {
            this.englishDecoration = "Not too Hot";
            this.chineseDecoration = "不是很辣";
        }
    }
}
