using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Elysium;
using Beijing_Inn_Order_System.Items;

namespace Beijing_Inn_Order_System
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        void StartupHandler(object sender, System.Windows.StartupEventArgs e)
        {
            //Elysium.Manager.Apply(this, Elysium.Theme.Dark, Elysium.AccentBrushes.Blue, Elysium.AccentBrushes.Sky);
            //ExcelReader.ReadXLS("test.xlsx");
            ExcelReader_NPOI.ReadXLSX("text.xls");
        }
    }
}
