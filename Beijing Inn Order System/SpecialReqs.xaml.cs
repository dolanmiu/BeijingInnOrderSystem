using Beijing_Inn_Order_System.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Beijing_Inn_Order_System
{
    /// <summary>
    /// Interaction logic for SpecialReqs.xaml
    /// </summary>
    public partial class SpecialReqs : Elysium.Controls.Window
    {
        private Item item;

        public SpecialReqs(Item item)
        {
            InitializeComponent();
            this.item = item;
            NoPeasCheckBox.IsChecked = item.Properties.NoPeas;
            NoOnionsCheckBox.IsChecked = item.Properties.NoOnions;
            NoChilliCheckBox.IsChecked = item.Properties.NoChilli;
        }

        private void NoPeasCheckBox_Click(object sender, RoutedEventArgs e)
        {
            item.Properties.NoPeas = (bool)NoPeasCheckBox.IsChecked;
        }

        private void NoOnionsCheckBox_Click(object sender, RoutedEventArgs e)
        {
            item.Properties.NoOnions = (bool)NoOnionsCheckBox.IsChecked;
        }

        private void NoChilliCheckBox_Click(object sender, RoutedEventArgs e)
        {
            item.Properties.NoChilli = (bool)NoChilliCheckBox.IsChecked;
        }

        private void DoneButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void NoPorkCheckBox_Click(object sender, RoutedEventArgs e)
        {

        }

        private void NoShrimpsCheckBox_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SpicyCheckBox_Click(object sender, RoutedEventArgs e)
        {

        }

        private void NoVegetablesCheckBox_Click(object sender, RoutedEventArgs e)
        {

        }

        private void NoBeansproutsCheckBox_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ExtraHotCheckBox_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LittleOilCheckBox_Click(object sender, RoutedEventArgs e)
        {

        }

        private void NoPeanutsCheckBox_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SauceSeperateCheckBox_Click(object sender, RoutedEventArgs e)
        {

        }

        private void NoMSGCheckBox_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LittleSaltCheckBox_Click(object sender, RoutedEventArgs e)
        {

        }

        private void NotTooHotCheckBox_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
