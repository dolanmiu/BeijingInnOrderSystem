using Beijing_Inn_Order_System.Customer;
using Beijing_Inn_Order_System.Items;
using Beijing_Inn_Order_System.MenuDesigner;
using Beijing_Inn_Order_System.Printing;
using Beijing_Inn_Order_System.Screens.ManagePageElements;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Beijing_Inn_Order_System.Screens
{
    /// <summary>
    /// Interaction logic for ManagePage.xaml
    /// </summary>
    public partial class ManagePage : UserControl
    {
        private ReceiptPrinter printer;

        public ManagePage(ReceiptPrinter printer)
        {
            this.printer = printer;
            InitializeComponent();
        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
        }

        public ReceiptPrinter ReceiptPrinter
        {
            get
            {
                return printer;
            }
            set
            {
                printer = value;
            }
        }

        /*private void SortClickCustomers(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader column = sender as GridViewColumnHeader;
            String field = column.Tag as String;

            if (_CurSortColCustomer != null)
            {
                AdornerLayer.GetAdornerLayer(_CurSortColCustomer).Remove(_CurAdornerCustomer);
                CustomerListView.Items.SortDescriptions.Clear();
            }

            ListSortDirection newDir = ListSortDirection.Descending;
            if (_CurSortColCustomer == column && _CurAdornerCustomer.Direction == newDir)
            {
                newDir = ListSortDirection.Ascending;
            }

            _CurSortColCustomer = column;
            _CurAdornerCustomer = new SortAdorner(_CurSortColCustomer, newDir);
            //AdornerLayer.GetAdornerLayer(_CurSortColCustomer).Add(_CurAdornerCustomer);
            CustomerListView.Items.SortDescriptions.Add(new SortDescription(field, newDir));
        }

        /*private void SortClickItems(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader column = sender as GridViewColumnHeader;
            String field = column.Tag as String;

            if (_CurSortColItem != null)
            {
                AdornerLayer.GetAdornerLayer(_CurSortColItem).Remove(_CurAdornerItem);
                ItemListView.Items.SortDescriptions.Clear();
            }

            ListSortDirection newDir = ListSortDirection.Descending;
            if (_CurSortColItem == column && _CurAdornerItem.Direction == newDir)
            {
                newDir = ListSortDirection.Ascending;
            }

            _CurSortColItem = column;
            _CurAdornerItem = new SortAdorner(_CurSortColItem, newDir);
            AdornerLayer.GetAdornerLayer(_CurSortColItem).Add(_CurAdornerItem);
            ItemListView.Items.SortDescriptions.Add(new SortDescription(field, newDir));
        }*/
    }
}

