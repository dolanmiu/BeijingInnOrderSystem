using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beijing_Inn_Order_System.Items.SpecialDecoration
{
    [Serializable]
    class SpecialNoPeas : SpecialDecorator
    {
        public SpecialNoPeas(SpecialComponent specialComponent)
            : base(specialComponent)
        {
            this.englishDecoration = "No Peas";
            this.chineseDecoration = "豌豆";
            this.type = SpecialButton.SpecialType.NoPeas;
        }
    }
}
