using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beijing_Inn_Order_System.Items
{
    public class Item
    {
        private static List<Item> totalItems = new List<Item>();

        private String englishName;
        private String chineseName;
        private float price;

        public Item(String englishName, String chineseName, float price)
        {
            this.englishName = englishName;
            this.chineseName = chineseName;
            this.price = price;
        }

        public String EnglishName
        {
            get
            {
                return englishName;
            }

            set
            {
                englishName = value;
            }
        }

        public float Price
        {
            get
            {
                return price;
            }

            set
            {
                price = value;
            }
        }

        public static List<Item> TotalItems
        {
            get
            {
                return totalItems;
            }

            set
            {
                totalItems = value;
            }
        }
    }
}
