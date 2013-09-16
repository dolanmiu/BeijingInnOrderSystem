using Beijing_Inn_Order_System.Printing;
using System.Windows;

namespace Beijing_Inn_Order_System
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Elysium.Controls.Window
    {
        OrderPage orderPage = new OrderPage();
        ManagePage managePage = new ManagePage();

        public MainWindow()
        {
        }

        void OnLoad(object sender, RoutedEventArgs e)
        {
            ReceiptPrinter.LoadPrinter();
            this.Content = orderPage;
        }


        private void ManageButton_Click(object sender, RoutedEventArgs e)
        {
            managePage = new ManagePage();
            this.Content = managePage;
        }

        private void OrderButton_Click(object sender, RoutedEventArgs e)
        {
            orderPage = new OrderPage();
            this.Content = orderPage;
        }

        private void MainScreen_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ReceiptPrinter.UnloadPrinter();
        }
    }
}
