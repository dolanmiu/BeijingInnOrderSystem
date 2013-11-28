using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beijing_Inn_Order_System.Items.SpecialDecoration
{
    [Serializable]
    class SpecialExtraHot : SpecialDecorator
    {
        public SpecialExtraHot(SpecialComponent specialComponent)
            : base(specialComponent)
        {
            this.englishDecoration = "Extra Chilli";
            this.chineseDecoration = "额外的辣椒";
            this.type = SpecialButton.SpecialType.ExtraHot;
        }
    }
}
