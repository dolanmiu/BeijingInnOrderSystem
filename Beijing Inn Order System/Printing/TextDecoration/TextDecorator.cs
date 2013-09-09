using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beijing_Inn_Order_System.Printing.TextDecoration
{
    public abstract class TextDecorator : TextComponent
    {
        private TextComponent baseComponent = null;
        protected string textDecoration = "Unspecified Decoration";

        public TextDecorator(TextComponent baseComponent)
        {
            this.baseComponent = baseComponent;
        }

        public override string GetDec()
        {
            return string.Format("{0}{1}", textDecoration, baseComponent.GetDec());
        }



    }
}
