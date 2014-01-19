using Beijing_Inn_Order_System.Customer;
using Beijing_Inn_Order_System.Helper_Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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

namespace Beijing_Inn_Order_System.Screens.StatisticsPageElements
{
    /// <summary>
    /// Interaction logic for OrderLogWindow.xaml
    /// </summary>
    public partial class OrderLogWindow : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public OrderLogWindow()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            NotifyPropertyChanged("OrderLog");
        }

        private void SortClickCustomers(object sender, RoutedEventArgs e)
        {
            ListViewSorter.SortAlternate(sender, CustomerListView);
        }

        #region Properties
        public DataTable OrderLog
        {
            get
            {
                return AddressManager.LoadOrderLog();
            }
        }
        #endregion

        private void UserControl_GotFocus(object sender, RoutedEventArgs e)
        {
            //ListViewSorter.SortDescending(DateGridViewColumn, CustomerListView);
        }
    }
}
