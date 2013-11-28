using Beijing_Inn_Order_System.Customer;
using System;
using System.Collections.Generic;
using System.Globalization;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Beijing_Inn_Order_System.Screens
{
    /// <summary>
    /// Interaction logic for NewAddressControl.xaml
    /// </summary>
    public partial class NewAddressControl : Elysium.Controls.Window
    {
        private Address address;

        public NewAddressControl(Address address)
        {
            InitializeComponent();
            this.address = address;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void AddAddressButton_Click(object sender, RoutedEventArgs e)
        {
            address.Road = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(RoadNameTextBox.Text.ToLower());
            address.PostCode = TextBoxInputMaskBehavior.RemoveSpecialCharacters(PostCodeTextBox.Text.ToUpper().Trim());
            address.Town = (string)TownComboBox.SelectedValue;
            this.Close();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        #region Properties
        public List<string> Towns
        {
            get
            {
                return AddressManager.Towns;
            }
        }
        #endregion
    }
}
