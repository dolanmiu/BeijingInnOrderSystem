using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Elysium;
using Beijing_Inn_Order_System.Items;
using Beijing_Inn_Order_System.Customer;

namespace Beijing_Inn_Order_System
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        void StartupHandler(object sender, System.Windows.StartupEventArgs e)
        {
            Item.LoadItems();
            Address.LoadAllAddresses();
            //Address.LoadAllPostCodes();
        }
    }
}
