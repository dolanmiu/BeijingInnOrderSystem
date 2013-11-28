using Beijing_Inn_Order_System.Customer;
using Beijing_Inn_Order_System.MenuDesigner;
using Beijing_Inn_Order_System.Printing;
using Beijing_Inn_Order_System.Screens.OrderPageElements;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Beijing_Inn_Order_System
{
    /// <summary>
    /// Interaction logic for OrderPage.xaml
    /// </summary>
    public partial class OrderPage : UserControl
    {
        //ListBox dragSource = null;
        OrderDetails orderDetails;

        PickItemsWindow pickItemsWindow { get; set; }
        DeliveryWindow deliveryWindow;
        PrintWindow printWindow;
        ReceiptPrinter printer;

        public OrderPage(ReceiptPrinter printer, ObservableCollection<MenuCategory> menuCategories)
        {
            this.printer = printer;
            orderDetails = new OrderDetails();
            InitializeComponent();
            pickItemsWindow = new PickItemsWindow(orderDetails, menuCategories);
            PickItemsTab.Content = pickItemsWindow;
            deliveryWindow = new DeliveryWindow(orderDetails);
            DeliveryTab.Content = deliveryWindow;
            printWindow = new PrintWindow(orderDetails, printer);
            PrintTab.Content = printWindow;
        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            
        }

        #region GetDataFromListBox(ListBox,Point)
        private static object GetDataFromListBox(ListBox source, Point point)
        {
            UIElement element = source.InputHitTest(point) as UIElement;
            if (element != null)
            {
                object data = DependencyProperty.UnsetValue;
                while (data == DependencyProperty.UnsetValue)
                {
                    data = source.ItemContainerGenerator.ItemFromContainer(element);
                    if (data == DependencyProperty.UnsetValue)
                    {
                        element = VisualTreeHelper.GetParent(element) as UIElement;
                    }
                    if (element == source)
                    {
                        return null;
                    }
                }
                if (data != DependencyProperty.UnsetValue)
                {
                    return data;
                }
            }
            return null;
        }
        #endregion

        #region Control Methods
        /*private void ListBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ListBox parent = (ListBox)sender;
            dragSource = parent;
            object data = GetDataFromListBox(dragSource, e.GetPosition(parent));

            if (data != null)
            {
                DragDrop.DoDragDrop(parent, data, DragDropEffects.Move);
            }
        }*/
        #endregion
    }
}
