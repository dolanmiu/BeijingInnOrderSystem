using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beijing_Inn_Order_System.Items.SpecialDecoration
{
    [Serializable]
    public abstract class SpecialComponent
    {
        public abstract string GetEnglishValue();
        public abstract string GetChineseValue();
        public abstract SpecialComponent BaseComponent();
        public abstract Beijing_Inn_Order_System.Items.SpecialButton.SpecialType GetSpecialType();
    }
}
