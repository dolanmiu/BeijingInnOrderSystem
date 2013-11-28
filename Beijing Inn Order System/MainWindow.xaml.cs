using Beijing_Inn_Order_System.Printing;
using System.Windows;
using Beijing_Inn_Order_System.Settings;
using Beijing_Inn_Order_System.Items;
using Beijing_Inn_Order_System.Customer;
using Beijing_Inn_Order_System.MenuDesigner;

namespace Beijing_Inn_Order_System
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Elysium.Controls.Window
    {
        private OrderPage orderPage;
        private ManagePage managePage;
        private ReceiptPrinter printer;
        private MenuXMLReaderWriter menuReaderWriter;

        public MainWindow()
        {
            UserSettings.ReadSettingsFile();
            menuReaderWriter = new MenuXMLReaderWriter();
            menuReaderWriter.ReadMenuFile();
            printer = new ReceiptPrinter();
            orderPage = new OrderPage(printer, menuReaderWriter.MenuCategories);
            managePage = new ManagePage(printer, menuReaderWriter);
        }

        void OnLoad(object sender, RoutedEventArgs e)
        {
            ItemManager.LoadTotalOrders();
            this.Content = orderPage;
            printer.LoadPrinter();
        }

        private void ManageButton_Click(object sender, RoutedEventArgs e)
        {
            managePage = new ManagePage(printer, menuReaderWriter);
            this.Content = managePage;
        }

        private void OrderButton_Click(object sender, RoutedEventArgs e)
        {
            orderPage = new OrderPage(printer, menuReaderWriter.MenuCategories);
            this.Content = orderPage;
        }

        private void MainScreen_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            printer.UnloadPrinter();
        }

        #region Properties
        public string WindowTitle
        {
            get
            {
                return UserSettings.OrganisationName + " Order System";
            }
        }
        #endregion
    }
}
