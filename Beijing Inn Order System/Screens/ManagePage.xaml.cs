using Beijing_Inn_Order_System.Customer;
using Beijing_Inn_Order_System.Items;
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

namespace Beijing_Inn_Order_System
{
    /// <summary>
    /// Interaction logic for ManagePage.xaml
    /// </summary>
    public partial class ManagePage : UserControl
    {
        //private ListViewColumnSorter lvwColumnSorter;
        private GridViewColumnHeader _CurSortColItem = null;
        private SortAdorner _CurAdornerItem = null;
        private GridViewColumnHeader _CurSortColCustomer = null;
        private SortAdorner _CurAdornerCustomer = null;

        public ManagePage()
        {
            InitializeComponent();
        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            Item.LoadTotalOrders();
            Address.LoadTotalCustomers();
            ItemListView.ItemsSource = Item.TotalBoughtItems.DefaultView;
            CustomerListView.ItemsSource = Address.TotalCustomers.DefaultView;

            SortClickItems(TotalBoughtHeader, null);
            SortClickCustomers(TotalCustomersHeader, null);
        }

        private void SortClickCustomers(object sender, RoutedEventArgs e)
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

        private void SortClickItems(object sender, RoutedEventArgs e)
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
        }
    }

    public class SortAdorner : Adorner
    {
        private readonly static Geometry _AscGeometry = Geometry.Parse("M 0,0 L 10,0 L 5,5 Z");
        private readonly static Geometry _DescGeometry = Geometry.Parse("M 0,5 L 10,5 L 5,0 Z");

        public ListSortDirection Direction { get; private set; }

        public SortAdorner(UIElement element, ListSortDirection dir) : base(element)
        { 
            Direction = dir; 
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            if (AdornedElement.RenderSize.Width < 20)
            {
                return;
            }

            drawingContext.PushTransform(new TranslateTransform(AdornedElement.RenderSize.Width - 15,(AdornedElement.RenderSize.Height - 5) / 2));

            drawingContext.DrawGeometry(Brushes.Black, null, Direction == ListSortDirection.Ascending ? _AscGeometry : _DescGeometry);
            drawingContext.Pop();
        }
    }
}

