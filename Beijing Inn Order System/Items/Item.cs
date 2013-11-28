using System;

namespace Beijing_Inn_Order_System.Items
{
    [Serializable]
    public class Item : BaseItem, IItem
    {

        private float price;
        private bool isLarge;

        public Item(String englishName, String chineseName, int number, float price)
            : base(englishName, chineseName, number)
        {
            this.price = price;
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
                return price;
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
            }
        }

        public bool IsSizeDish
        {
            get
            {
                return false;
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
