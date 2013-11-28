using Beijing_Inn_Order_System.Helper_Classes;
using Beijing_Inn_Order_System.Items;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace Beijing_Inn_Order_System.Screens.ManagePageElements
{
    /// <summary>
    /// Interaction logic for ItemSalesWindow.xaml
    /// </summary>
    public partial class ItemSalesWindow : UserControl
    {
        public ItemSalesWindow()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ListViewSorter.SortDescending(TotalBoughtHeader, ItemListView);
        }

        private void SortClickItems(object sender, RoutedEventArgs e)
        {
            ListViewSorter.SortAlternate(sender, ItemListView);
        }

        #region Properties
        public DataView TotalBoughtItems
        {
            get
            {
                if (ItemManager.TotalBoughtItems == null) return null;
                return ItemManager.TotalBoughtItems.DefaultView;
            }
        }
        #endregion
    }
}
