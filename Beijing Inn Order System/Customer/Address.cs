using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beijing_Inn_Order_System.Customer
{
    class Address
    {
        private int number;
        private String postCode;
        private String road;
        private String town;

        public Address(int number, String postCode)
        {
            this.number = number;
            this.postCode = postCode;
        }
    }
}
