using Beijing_Inn_Order_System.Customer;
using Beijing_Inn_Order_System.Items;
using Beijing_Inn_Order_System.Printing;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Beijing_Inn_Order_System.Screens.OrderPageElements
{
    /// <summary>
    /// Interaction logic for PrintWindow.xaml
    /// </summary>
    public partial class PrintWindow : UserControl, INotifyPropertyChanged
    {
        private OrderDetails orderDetails;
        private ReceiptPrinter printer;

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public PrintWindow(OrderDetails orderDetails, ReceiptPrinter printer)
        {
            this.orderDetails = orderDetails;
            this.printer = printer;
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            NotifyPropertyChanged("PriceText"); //got to change this sometime
            NotifyPropertyChanged("DeliveryChargeText");
        }

        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            if (orderDetails.CurrentAddress == null)
            {
                printer.Print(orderDetails);
            }

            ItemManager.AddOrderToCount(orderDetails.ItemBasket.Items);
            if (orderDetails.CurrentAddress != null)
            {
                if (!string.IsNullOrEmpty(orderDetails.CurrentAddress.Number) && !string.IsNullOrEmpty(orderDetails.CurrentAddress.Road) && !string.IsNullOrEmpty(orderDetails.CurrentAddress.PhoneNumber))
                {
                    printer.Print(orderDetails);
                    AddressManager.AddCustomerToCount(orderDetails.CurrentAddress);
                }
                NotifyPropertyChanged("PrintDiagnosticMessage");
            }
            AddressManager.AddToRecentOrder(orderDetails);
        }

        #region Properties
        public OrderDetails OrderDetails
        {
            get
            {
                return orderDetails;
            }
        }

        public ReceiptPrinter Printer
        {
            get
            {
                return printer;
            }
        }

        public string PriceText
        {
            get
            {
                return orderDetails.PriceText;
            }
        }

        public string DeliveryChargeText
        {
            get
            {
                return orderDetails.DeliveryChargeText;
            }
        }

        public string PrintDiagnosticMessage
        {
            get
            {
                string message = "";
                if (string.IsNullOrEmpty(orderDetails.CurrentAddress.Number)) 
                {
                    message += "House Number not specified! ";
                }
                if (string.IsNullOrEmpty(orderDetails.CurrentAddress.Road)) 
                {
                    message += "Road not not specified! ";
                }

                if (string.IsNullOrEmpty(orderDetails.CurrentAddress.PhoneNumber))
                {
                    message += "Phone Number not specified! ";
                }
                return message;
            }
        }
        #endregion
    }
}
