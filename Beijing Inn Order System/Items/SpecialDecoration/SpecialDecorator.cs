using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beijing_Inn_Order_System.Items.SpecialDecoration
{
    [Serializable]
    class SpecialDecorator : SpecialComponent
    {
        private SpecialComponent baseComponent = null;
        protected string englishDecoration = "Unspecified Special";
        protected string chineseDecoration = "Unspecified Special";
        protected Beijing_Inn_Order_System.Items.SpecialButton.SpecialType type;

        public SpecialDecorator(SpecialComponent baseComponent)
        {
            this.baseComponent = baseComponent;
        }

        public override string GetEnglishValue()
        {
            return string.Format("{0}, {1}", englishDecoration, baseComponent.GetEnglishValue());
        }

        public override string GetChineseValue()
        {
            return string.Format("{0}, {1}", chineseDecoration, baseComponent.GetChineseValue());
        }

        public override SpecialComponent BaseComponent()
        {
            return baseComponent;
        }

        public override SpecialButton.SpecialType GetSpecialType()
        {
            return type;
        }
    }
}
