using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beijing_Inn_Order_System.Items.SpecialDecoration
{
    class SpecialLittleSalt : SpecialDecorator
    {
        public SpecialLittleSalt(SpecialComponent specialComponent)
            : base(specialComponent)
        {
            this.englishDecoration = "Little Salt";
            this.chineseDecoration = "小盐";
        }
    }
}
