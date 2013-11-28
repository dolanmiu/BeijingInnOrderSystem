using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Beijing_Inn_Order_System.Items.SpecialDecoration
{
    [Serializable]
    class SpecialNoSalt : SpecialDecorator
    {
        public SpecialNoSalt(SpecialComponent specialComponent)
            : base(specialComponent)
        {
            this.englishDecoration = "No Salt";
            this.chineseDecoration = "无盐";
            this.type = SpecialButton.SpecialType.NoSalt;
        }
    }
}
