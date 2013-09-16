using Beijing_Inn_Order_System.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beijing_Inn_Order_System.Items
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

        private int CountItemInBasket(Item _item)
        {
            //int itemCount = items.GroupBy(n => _item).Any(c => c.Count() > 1);
            int itemCount = 0;
            foreach (Item item in items) {
                if (item.IsEqualTo(_item))
                {
                    itemCount++;
                }
            }
            return itemCount;
        }

        #region Properties
        public List<Item> Items
        {
            get
            {
                return items;
            }
        }

        public List<Tuple<Item, int>> ConcatItems
        {
            get
            {
                List<Tuple<Item, int>> result = new List<Tuple<Item, int>>();
                List<Item> tempItemCache = new List<Item>();
                foreach (Item item in items)
                {
                    if (!IsItemInList(tempItemCache, item))
                    {
                        Tuple<Item, int> newItemTuple = new Tuple<Item, int>(item, CountItemInBasket(item));
                        tempItemCache.Add(item);
                        result.Add(newItemTuple);
                    }
                }
                return result;
            }
        }

        private bool IsItemInList(List<Item> items, Item _item)
        {
            foreach (Item item in items)
            {
                if (item.IsEqualTo(_item)) 
                {
                    return true;
                } 
            }
            return false;
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
