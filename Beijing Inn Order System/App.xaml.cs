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
using Beijing_Inn_Order_System.Settings;
using Beijing_Inn_Order_System.MenuDesigner;

namespace Beijing_Inn_Order_System
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application, ISingleInstanceApp
    {
        private const string Unique = "Restaurant_Order_System";

        [STAThread]
        public static void Main()
        {
            if (SingleInstance<App>.InitializeAsFirstInstance(Unique))
            {
                var application = new App();
                application.InitializeComponent();
                application.Run();
                // Allow single instance code to perform cleanup operations
                SingleInstance<App>.Cleanup();
            }
        }

        #region ISingleInstanceApp Members
        public bool SignalExternalCommandLineArgs(IList<string> args)
        {
            // handle command line arguments of second instance
            // ...
            return true;
        }
        #endregion

        void StartupHandler(object sender, System.Windows.StartupEventArgs e)
        {
            Task loadAddressesTask = new Task(() =>
            {
                AddressManager.LoadAllAddresses();
            });
            loadAddressesTask.Start();
            //Task initialLoad = new Task(() =>
            //{
            ItemManager.LoadItems();
                
                ItemManager.LoadTotalOrders();
                MenuManager.ReadMenuFile();
                try
                {
                    UserSettings.ReadSettingsFile();
                }
                catch
                {
                    UserSettings.WriteSettingsFile();
                }
            //});
            //initialLoad.Start();
        }
    }
}
