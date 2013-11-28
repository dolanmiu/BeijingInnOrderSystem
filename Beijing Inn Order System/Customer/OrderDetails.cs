using Beijing_Inn_Order_System.Helper_Classes;
using Beijing_Inn_Order_System.Items;
using Beijing_Inn_Order_System.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Beijing_Inn_Order_System.Customer
{
    public class OrderDetails : INotifyPropertyChanged
    {
        private Basket itemBasket;
        private Address currentAddress;

        public OrderDetails()
        {
            this.itemBasket = new Basket();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private float? GetDistance()
        {
            if (currentAddress == null)
            {
                return 0;
            }
            else
            {
                if (currentAddress.Latitude == 0 || currentAddress.Longitude == 0)
                {
                    return null;
                }
                return (float)DistanceCalculator.getDistanceFromLatLonInMiles(DistanceCalculator.BeijingInnCoords[0], DistanceCalculator.BeijingInnCoords[1], currentAddress.Latitude, currentAddress.Longitude);
            }
        }

        public float CalculateDeliveryCharge()
        {
            if (currentAddress == null) return 0;
            float deliveryCharge = 0;
            if (itemBasket.CalculatePrice() < UserSettings.DeliveryChargeThreshold)
            {
                deliveryCharge += UserSettings.DeliveryCharge;
            }

            if (GetDistance() > UserSettings.DeliveryRadiusThreshold)
            {
                deliveryCharge += UserSettings.DeliveryRadiusCharge;
            }
            return deliveryCharge;
        }


        private float CalculateTotalPrice()
        {
            float total = itemBasket.CalculatePrice();
            float deliveryCharge = 0;
            deliveryCharge = CalculateDeliveryCharge();
            total += deliveryCharge;
            return total;
        }

        #region Properties
        public Basket ItemBasket
        {
            get
            {
                return itemBasket;
            }
        }

        public Address CurrentAddress
        {
            get
            {
                return currentAddress;
            }
            set
            {
                currentAddress = value;
                NotifyPropertyChanged("RoadText");
                NotifyPropertyChanged("TownText");
                NotifyPropertyChanged("PostCodeText");
                NotifyPropertyChanged("DistanceText");
            }
        }

        public string RoadText
        {
            get
            {
                if (currentAddress == null)
                {
                    return "Road Name";
                }
                else
                {
                    return currentAddress.Number + " " + currentAddress.Road;
                }
            }
        }

        public string TownText
        {
            get
            {
                if (currentAddress == null)
                {
                    return "Town Name";
                }
                else
                {
                    return currentAddress.Town;
                }
            }
        }

        public string PostCodeText
        {
            get
            {
                if (currentAddress == null)
                {
                    return "Post Code";
                }
                else
                {
                    return currentAddress.PostCode;
                }
            }
        }

        public string DistanceText
        {
            get
            {
                float? distance = GetDistance();
                if (distance == null)
                {
                    return "Distance: N/A";
                }
                else
                {
                    return "Distance: " + distance + " Miles";
                }
            }
        }

        public string PhoneNumber
        {
            get
            {
                if (currentAddress == null)
                {
                    return "";
                }
                else
                {
                    return currentAddress.PhoneNumber;
                }
            }
            set
            {
                if (currentAddress != null)
                {
                    currentAddress.PhoneNumber = value;
                }
            }
        }

        public string HouseNumber
        {
            get
            {
                if (currentAddress == null)
                {
                    return "";
                }
                else
                {
                    return currentAddress.Number;
                }
            }
            set
            {
                if (currentAddress != null)
                {
                    currentAddress.Number = value;
                    NotifyPropertyChanged("HouseNumber");
                    NotifyPropertyChanged("RoadText");
                }
            }
        }

        public string PriceText
        {
            get
            {
                if (itemBasket == null)
                {
                    return "£0.00";
                }
                else
                {
                    return "£" + CalculateTotalPrice().ToString("0.00");
                }
            }
        }

        public string DeliveryChargeText
        {
            get
            {
                float deliveryCharge = CalculateDeliveryCharge();
                if (deliveryCharge > 0)
                {
                    return "Inc. £" + deliveryCharge.ToString("0.00") + " delivery charge";
                }
                else
                {
                    return "";
                }
            }

            set
            {
            }
        }
        #endregion
    }
}
