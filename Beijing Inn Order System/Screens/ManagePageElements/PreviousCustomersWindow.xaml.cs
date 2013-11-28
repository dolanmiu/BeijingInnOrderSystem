using Beijing_Inn_Order_System.Customer;
using Beijing_Inn_Order_System.Helper_Classes;
using System;
using System.ComponentModel;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace Beijing_Inn_Order_System.Screens.ManagePageElements
{
    /// <summary>
    /// Interaction logic for PreviousCustomersWindow.xaml
    /// </summary>
    public partial class PreviousCustomersWindow : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public PreviousCustomersWindow()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //SortClickCustomers(TotalCustomersHeader, null);
            NotifyPropertyChanged("TotalCustomers");
        }

        private void SortClickCustomers(object sender, RoutedEventArgs e)
        {
            ListViewSorter.SortAlternate(sender, CustomerListView);
        }

        #region Properties 
        public DataTable TotalCustomers
        {
            get
            {
                //if (Address.TotalCustomers == null) return null;
                return AddressManager.TotalCustomers;
            }
        }
        #endregion

        private void UserControl_GotFocus(object sender, RoutedEventArgs e)
        {
            //ListViewSorter.SortDescending(TotalCustomersHeader, CustomerListView);
        }
    }
}
