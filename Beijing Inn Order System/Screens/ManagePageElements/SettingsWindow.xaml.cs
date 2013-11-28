using Beijing_Inn_Order_System.Printing;
using Beijing_Inn_Order_System.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Beijing_Inn_Order_System.Screens.ManagePageElements
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : UserControl
    {
        private ReceiptPrinter printer;
        public static readonly DependencyProperty PrinterProperty = DependencyProperty.Register("Printer", typeof(ReceiptPrinter), typeof(SettingsWindow), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));


        public SettingsWindow()
        {
            InitializeComponent();
        }

        private void UserControl_LostFocus(object sender, RoutedEventArgs e)
        {
            UserSettings.WriteSettingsFile();
        }

        #region Properties
        public string OrganisationName
        {
            get
            {
                return UserSettings.OrganisationName;
            }
            set
            {
                UserSettings.OrganisationName = value;
            }
        }
        public string ChargeAmount
        {
            get
            {
                return UserSettings.DeliveryCharge.ToString();
            }
            set
            {
                float price;
                float.TryParse(value, out price);
                UserSettings.DeliveryCharge = price;
            }
        }

        public string ThresholdAmount
        {
            get
            {
                return UserSettings.DeliveryChargeThreshold.ToString();
            }
            set
            {
                float threshold;
                float.TryParse(value, out threshold);
                UserSettings.DeliveryChargeThreshold = threshold;
            }
        }

        public string RadiusCharge
        {
            get
            {
                return UserSettings.DeliveryRadiusCharge.ToString();
            }
            set
            {
                float charge;
                float.TryParse(value, out charge);
                UserSettings.DeliveryRadiusCharge = charge;
            }
        }

        public string ThresholdRadius
        {
            get
            {
                return UserSettings.DeliveryRadiusThreshold.ToString();
            }
            set 
            {
                float threshold;
                float.TryParse(value, out threshold);
                UserSettings.DeliveryRadiusThreshold = threshold;
            }
        }

        public ReceiptPrinter Printer
        {
            get
            {
                return (ReceiptPrinter)GetValue(PrinterProperty);
            }
            set
            {
                SetValue(PrinterProperty, value);
            }
        }
        #endregion

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //PrintersComboBox.ItemsSource = ReceiptPrinter.PrinterDevices;
        }
    }
}
