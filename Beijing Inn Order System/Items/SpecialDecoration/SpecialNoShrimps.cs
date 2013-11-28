using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beijing_Inn_Order_System.Items.SpecialDecoration
{
    [Serializable]
    class SpecialNoShrimps : SpecialDecorator
    {
        public SpecialNoShrimps(SpecialComponent specialComponent)
            : base(specialComponent)
        {
            this.englishDecoration = "No Shrimps";
            this.chineseDecoration = "没有虾";
            this.type = SpecialButton.SpecialType.NoShrimps;
        }
    }
}
