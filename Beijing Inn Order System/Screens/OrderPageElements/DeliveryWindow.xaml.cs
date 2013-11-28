using Beijing_Inn_Order_System.CustomControls;
using Beijing_Inn_Order_System.Customer;
using Beijing_Inn_Order_System.Helper_Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Beijing_Inn_Order_System.Screens.OrderPageElements
{
    /// <summary>
    /// Interaction logic for DeliveryWindow.xaml
    /// </summary>
    public partial class DeliveryWindow : UserControl, INotifyPropertyChanged
    {
        private OrderDetails orderDetails;
        private List<Address> searchedAddresses;

        public delegate List<Address> AddressSearchDelegate(string query);

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public DeliveryWindow(OrderDetails orderDetails)
        {
            this.orderDetails = orderDetails;
            searchedAddresses = new List<Address>();
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (orderDetails.CurrentAddress == null)
            {
                SearchTextBox.Text = "";
                PhoneNumberTextBox.Clear();
                HouseNumberTextBox.Clear();
            }
        }

        private void SearchResultsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((Address)((ListBox)sender).SelectedItem == null) return;
            orderDetails.CurrentAddress = Helper.DeepClone<Address>((Address)((ListBox)sender).SelectedItem);
        }

        private void NewAddressButton_Click(object sender, RoutedEventArgs e)
        {
            Address address = new Address();
            NewAddressControl nac = new NewAddressControl(address);
            nac.ShowDialog();
            if (address.PostCode == null || address.Road == null || address.Town == null) return;
            AddressManager.AddForeignAddress(address);
            orderDetails.CurrentAddress = Helper.DeepClone<Address>(address);
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            AddressSearchDelegate asd = new AddressSearchDelegate(SearchAddresses);
            TextBox t = (sender as TextBox);
            asd.BeginInvoke(t.Text, new AsyncCallback(SearchFinishCallBack), asd);
        }

        private List<Address> SearchAddresses(string query)
        {
            string text = query.Trim();
            List<Address> currentAddresses = AddressManager.Search(text);
            return currentAddresses;
        }

        private void SearchFinishCallBack(IAsyncResult result)
        {
            AddressSearchDelegate asd = (AddressSearchDelegate)result.AsyncState;
            List<Address> currentAddresses = asd.EndInvoke(result);

            this.Dispatcher.Invoke((Action)(() => {
                //if (SearchResultsListBox == null) return;
                SearchedAddresses = currentAddresses;

                if (currentAddresses.Count == 1)
                {
                    orderDetails.CurrentAddress = currentAddresses[0];
                }
                }));
        }

        #region Properties
        public OrderDetails OrderDetails
        {
            get
            {
                return orderDetails;
            }
        }

        public string PhoneNumberText
        {
            get
            {
                if (orderDetails.CurrentAddress == null)
                {
                    return "";
                }
                else
                {
                    return orderDetails.CurrentAddress.PhoneNumber;
                }
            }
            set
            {
                if (orderDetails.CurrentAddress != null)
                {
                    orderDetails.CurrentAddress.PhoneNumber = value;
                }
            }
        }

        public List<Address> SearchedAddresses
        {
            get
            {
                return searchedAddresses;
            }
            set
            {
                searchedAddresses = value;
                NotifyPropertyChanged("SearchedAddresses");
            }
        }
        #endregion
    }
}
