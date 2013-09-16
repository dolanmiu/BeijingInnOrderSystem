using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beijing_Inn_Order_System.Items.SpecialDecoration
{
    class SpecialBase : SpecialComponent
    {
        private string englishText;
        private string chineseText;

        public SpecialBase(string englishText, string chineseText)
        {
            this.englishText = englishText;
            this.chineseText = chineseText;
        }

        public override string GetEnglishValue()
        {
            return englishText;
        }

        public override string GetChineseValue()
        {
            return chineseText;
        }
    }
}
