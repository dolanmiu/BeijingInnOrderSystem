using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beijing_Inn_Order_System.Items.SpecialDecoration
{
    [Serializable]
    class SpecialNoPeanuts : SpecialDecorator
    {
        public SpecialNoPeanuts(SpecialComponent specialComponent)
            : base(specialComponent)
        {
            this.englishDecoration = "No Peanuts";
            this.chineseDecoration = "没有花生";
            this.type = SpecialButton.SpecialType.NoPeanuts;
        }
    }
}
