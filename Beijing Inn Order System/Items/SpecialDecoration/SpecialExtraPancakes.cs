using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Beijing_Inn_Order_System.Items.SpecialDecoration
{
    [Serializable]
    class SpecialExtraPancakes : SpecialDecorator
    {
        public SpecialExtraPancakes(SpecialComponent specialComponent)
            : base(specialComponent)
        {
            this.englishDecoration = "Extra Pancakes";
            this.chineseDecoration = "加煎饼";
            this.type = SpecialButton.SpecialType.ExtraPancakes;
        }
    }
}
