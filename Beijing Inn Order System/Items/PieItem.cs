using System;

namespace Beijing_Inn_Order_System.Items
{
    [Serializable]
    public class PieItem : BaseItem, IItem
    {
        private float wholePrice;
        private float halfPrice;
        private float quarterPrice;
        public enum PieSize { Whole, Half, Quarter }
        private PieSize pieSize;

        public PieItem(String englishName, String chineseName, int number, float wholePrice, float halfPrice, float quarterPrice)
            : base(englishName, chineseName, number)
        {
            this.wholePrice = wholePrice;
            this.halfPrice = halfPrice;
            this.quarterPrice = quarterPrice;
            this.pieSize = PieSize.Whole;
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
        public float Price
        {
            get
            {
                switch (pieSize)
                {
                    case PieSize.Whole:
                        return wholePrice;
                    case PieSize.Half:
                        return halfPrice;
                    case PieSize.Quarter:
                        return quarterPrice;
                    default:
                        return wholePrice;
                }
            }
        }

        public string SizeString
        {
            get
            {
                return pieSize.ToString();
            }
        }

        public PieSize Size
        {
            get
            {
                return pieSize;
            }

            set
            {
                pieSize = value;
                NotifyPropertyChanged("Price");
            }
        }

        public string EnglishSizeString
        {
            get
            {
                switch (Size)
                {
                    case PieSize.Whole:
                        return "Whole";
                    case PieItem.PieSize.Half:
                        return "Half";
                    case PieItem.PieSize.Quarter:
                        return "Quarter";
                }
                return "";
            }
        }

        public string ChineseSizeString
        {
            get
            {
                switch (Size)
                {
                    case PieSize.Whole:
                        return "全";
                    case PieItem.PieSize.Half:
                        return "半";
                    case PieItem.PieSize.Quarter:
                        return "季度";
                }
                return "";
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
                return true;
            }
        }
        #endregion
    }
}
