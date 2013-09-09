using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Beijing_Inn_Order_System
{
    public static class Pages
    {
        private static OrderPage orderPage;
        //private MainWindow _window = Application.Current.MainWindow;

        public static UserControl First
        {
            get
            {
                if (orderPage == null)
                    orderPage = new OrderPage();
                return orderPage;
            }
        }
        // ...
    }
}
