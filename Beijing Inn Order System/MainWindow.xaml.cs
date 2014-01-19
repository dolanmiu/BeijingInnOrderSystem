using Beijing_Inn_Order_System.Printing;
using System.Windows;
using Beijing_Inn_Order_System.Settings;
using Beijing_Inn_Order_System.Screens;
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
        private StatisticsPage statsPage;
        private ReceiptPrinter printer;

        public MainWindow()
        {
            printer = new ReceiptPrinter();
            orderPage = new OrderPage(printer);
            managePage = new ManagePage(printer);
            statsPage = new StatisticsPage();
        }

        void OnLoad(object sender, RoutedEventArgs e)
        {
            this.Content = orderPage;
            printer.LoadPrinter(); //Comment this out to make things work
        }

        private void ManageButton_Click(object sender, RoutedEventArgs e)
        {
            managePage = new ManagePage(printer);
            this.Content = managePage;
        }

        private void OrderButton_Click(object sender, RoutedEventArgs e)
        {
            orderPage = new OrderPage(printer);
            this.Content = orderPage;
        }

        private void MainScreen_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            printer.UnloadPrinter();
        }

        private void StatsButton_Click(object sender, RoutedEventArgs e)
        {
            this.Content = statsPage;
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
