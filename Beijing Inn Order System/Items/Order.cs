using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beijing_Inn_Order_System.Items
{
    class Order
    {
        private List<Item> items;

        public Order()
        {
            items = new List<Item>();
        }

        public void AddItem(Item item)
        {
            items.Add(item);
        }

        public void RemoveItem(int index)
        {
            items.RemoveAt(index);
        }

        public void RemoveItem(Item item)
        {
            items.Remove(item);
        }

        public List<Item> Items
        {
            get
            {
                return items;
            }
        }
    }
}
