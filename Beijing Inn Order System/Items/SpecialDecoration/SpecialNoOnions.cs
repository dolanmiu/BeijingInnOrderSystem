using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beijing_Inn_Order_System.Items.SpecialDecoration
{
    [Serializable]
    class SpecialNoOnions : SpecialDecorator
    {
        public SpecialNoOnions(SpecialComponent specialComponent)
            : base(specialComponent)
        {
            this.englishDecoration = "No Onions";
            this.chineseDecoration = "没有洋葱";
            this.type = SpecialButton.SpecialType.NoOnions;
        }
    }
}
