using Beijing_Inn_Order_System.Items.SpecialDecoration;
using System;

namespace Beijing_Inn_Order_System.Items
{
    [Serializable]
    public class SizeItem : BaseItem, IItem
    {
        private float smallPrice;
        private float largePrice;
        private bool isLarge = false;

        public SizeItem(String englishName, String chineseName, int number, float smallPrice, float largePrice) : base(englishName, chineseName, number)
        {
            this.smallPrice = smallPrice;
            this.largePrice = largePrice;
        }

        public bool IsEqualTo(IItem item)
        {
            if (item.EnglishName == englishName && item.ChineseName == chineseName && item.Price == Price && IsPropertiesTheSameAs(item))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #region Properties
        public virtual float Price
        {
            get
            {
                if (isLarge)
                {
                    return largePrice;
                }
                else
                {
                    return smallPrice;
                }
            }
        }

        public string EnglishSizeString
        {
            get
            {
                if (IsLarge)
                {
                    return "Large";
                }
                else
                {
                    return "Small";
                }
            }
        }

        public string ChineseSizeString
        {
            get
            {
                if (IsLarge)
                {
                    return "大";
                }
                else
                {
                    return "小";
                }
            }
        }

        public bool IsLarge
        {
            get
            {
                return isLarge;
            }
            set
            {
                isLarge = value;
                NotifyPropertyChanged("Price");
            }
        }

        public bool IsSizeDish
        {
            get
            {
                return true;
            }
        }

        public bool IsPieDish
        {
            get
            {
                return false;
            }
        }
        #endregion
    }
}
