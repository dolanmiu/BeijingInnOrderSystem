using Beijing_Inn_Order_System.Items.SpecialDecoration;
using System.Collections.Generic;

namespace Beijing_Inn_Order_System.Items
{
    public interface IItem
    {
        bool IsEqualTo(IItem item);
        List<SpecialComponent> GetPropertyList();
        string EnglishName
        {
            get;
            set;
        }

        string ChineseName
        {
            get;
        }

        float Price
        {
            get;
        }

        int Number
        {
            get;
            set;
        }

        string NumberedEnglishName
        {
            get;
        }

        SpecialComponent Properties
        {
            get;
            set;
        }

        string ConcatProperties
        {
            get;
        }

        bool IsSizeDish
        {
            get;
        }

        bool IsPieDish
        {
            get;
        }

        string ExtraDetails
        {
            get;
            set;
        }
    }
}
