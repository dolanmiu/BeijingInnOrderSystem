using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beijing_Inn_Order_System.Items.SpecialDecoration
{
    class SpecialNoVegetables : SpecialDecorator
    {
        public SpecialNoVegetables(SpecialComponent specialComponent)
            : base(specialComponent)
        {
            this.englishDecoration = "No Vegetables";
            this.chineseDecoration = "没有蔬菜";
        }
    }
}
