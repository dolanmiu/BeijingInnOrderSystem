using Beijing_Inn_Order_System.Items;
using Beijing_Inn_Order_System.Settings;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Beijing_Inn_Order_System.Customer
{
    public class Basket
    {
        private ObservableCollection<IItem> items = new ObservableCollection<IItem>();

        public Basket()
        {

        }

        public float CalculatePrice()
        {
            float total = 0;
            foreach (IItem item in items)
            {
                total += item.Price;
            }
            return total;
        }

        private int CountItemInBasket(IItem _item)
        {
            //int itemCount = items.GroupBy(n => _item).Any(c => c.Count() > 1);
            int itemCount = 0;
            foreach (IItem item in items)
            {
                if (item.IsEqualTo(_item))
                {
                    itemCount++;
                }
            }
            return itemCount;
        }

        #region Properties
        public ObservableCollection<IItem> Items
        {
            get
            {
                return items;
            }
        }

        public List<Tuple<IItem, int>> ConcatItems
        {
            get
            {
                List<Tuple<IItem, int>> result = new List<Tuple<IItem, int>>();
                List<IItem> tempItemCache = new List<IItem>();
                foreach (IItem item in items)
                {
                    if (!IsItemInList(tempItemCache, item))
                    {
                        Tuple<IItem, int> newItemTuple = new Tuple<IItem, int>(item, CountItemInBasket(item));
                        tempItemCache.Add(item);
                        result.Add(newItemTuple);
                    }
                }
                return result;
            }
        }

        private bool IsItemInList(List<IItem> items, IItem _item)
        {
            foreach (IItem item in items)
            {
                if (item.IsEqualTo(_item)) 
                {
                    return true;
                } 
            }
            return false;
        }
        #endregion
    }
}
