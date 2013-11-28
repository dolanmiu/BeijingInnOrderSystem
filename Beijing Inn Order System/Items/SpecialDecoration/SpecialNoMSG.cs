using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beijing_Inn_Order_System.Items.SpecialDecoration
{
    [Serializable]
    class SpecialNoMSG : SpecialDecorator
    {
        public SpecialNoMSG(SpecialComponent specialComponent)
            : base(specialComponent)
        {
            this.englishDecoration = "No MSG";
            this.chineseDecoration = "这味精";
            this.type = SpecialButton.SpecialType.NoMSG;
        }
    }
}
