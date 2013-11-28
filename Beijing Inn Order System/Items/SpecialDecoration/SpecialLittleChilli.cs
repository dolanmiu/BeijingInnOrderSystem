using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beijing_Inn_Order_System.Items.SpecialDecoration
{
    [Serializable]
    class SpecialLittleChilli : SpecialDecorator
    {
        public SpecialLittleChilli(SpecialComponent specialComponent)
            : base(specialComponent)
        {
            this.englishDecoration = "Little Chilli";
            this.chineseDecoration = "不是很辣";
            this.type = SpecialButton.SpecialType.LittleChilli;
        }
    }
}
