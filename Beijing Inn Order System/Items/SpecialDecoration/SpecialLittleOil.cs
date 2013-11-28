using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beijing_Inn_Order_System.Items.SpecialDecoration
{
    [Serializable]
    class SpecialLittleOil : SpecialDecorator
    {
        public SpecialLittleOil(SpecialComponent specialComponent)
            : base(specialComponent)
        {
            this.englishDecoration = "Little Oil";
            this.chineseDecoration = "少油";
            this.type = SpecialButton.SpecialType.LittleOil;
        }
    }
}
