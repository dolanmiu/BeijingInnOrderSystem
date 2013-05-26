using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beijing_Inn_Order_System.Customer
{
    class Customer
    {
        private String name;
        private Address address;

        public Customer(String name, Address address)
        {
            this.name = name;
            this.address = address;
        }

        public String Name
        {
            get
            {
                return name;
            }
        }

        public Address Address
        {
            get
            {
                return address;
            }
        }
    }
}
