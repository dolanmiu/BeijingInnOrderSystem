using Beijing_Inn_Order_System.Items;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace Beijing_Inn_Order_System.Customer
{
    [Serializable]
    public class Address
    {
        private String number;
        private String postCode;
        private String road;
        private String town;
        private double latitude;
        private double longitude;
        private string phoneNumber = "";

        public Address()
        {

        }

        public Address(String postCode, String road, String town, double latitude, double longitude)
        {
            this.postCode = postCode;
            this.road = road;
            this.town = town;
            this.latitude = latitude;
            this.longitude = longitude;
        }

        public Address(string number, String postCode, String road, String town)
        {
            this.number = number;
            this.postCode = postCode;
            this.road = road;
            this.town = town;
        }

        #region Properties
        public string Concat
        {
            get
            {
                return number + " " + road + ", " + postCode + ", " + town;
            }
        }

        public string Number
        {
            set
            {
                number = value;
            }
            get
            {
                return number;
            }
        }

        public string PostCode
        {
            get
            {
                return postCode;
            }

            set
            {
                postCode = value;
            }
        }

        public string Road
        {
            get
            {
                return road;
            }
            set
            {
                road = value;
            }
        }

        public string Town
        {
            get
            {
                return town;
            }
            set
            {
                town = value;
            }
        }

        public double Latitude
        {
            get
            {
                return latitude;
            }
        }

        public double Longitude
        {
            get
            {
                return longitude;
            }
        }

        public string PhoneNumber
        {
            get
            {
                return phoneNumber;
            }

            set
            {
                phoneNumber = value;
            }
        }
        #endregion
    }
}
