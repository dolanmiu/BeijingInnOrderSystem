using Beijing_Inn_Order_System.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beijing_Inn_Order_System
{
    class Basket
    {
        private List<Item> items = new List<Item>();
        private float totalPrice;

        public Basket()
        {

        }

        public float CalculatePrice()
        {
            float total = 0;
            foreach (Item item in items)
            {
                total += item.Price;
            }
            return total;
        }

        #region Properties
        public List<Item> Items
        {
            get
            {
                return items;
            }
        }

        public float TotalPrice
        {
            get
            {
                return totalPrice;
            }
        }
        #endregion
    }
}
