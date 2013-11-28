using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beijing_Inn_Order_System.Items.SpecialDecoration
{
    [Serializable]
    class SpecialNoBeansprouts : SpecialDecorator
    {
        public SpecialNoBeansprouts(SpecialComponent specialComponent)
            : base(specialComponent)
        {
            this.englishDecoration = "No Beansprouts";
            this.chineseDecoration = "没有豆芽";
            this.type = SpecialButton.SpecialType.NoBeanSprouts;
        }
    }
}
