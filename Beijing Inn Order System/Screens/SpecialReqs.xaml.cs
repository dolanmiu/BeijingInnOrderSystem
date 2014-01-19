using Beijing_Inn_Order_System.CustomControls;
using Beijing_Inn_Order_System.Items;
using Beijing_Inn_Order_System.Items.SpecialDecoration;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Linq;
using System.Text.RegularExpressions;

namespace Beijing_Inn_Order_System
{
    /// <summary>
    /// Interaction logic for SpecialReqs.xaml
    /// </summary>
    public partial class SpecialReqs : Elysium.Controls.Window
    {
        private IItem item;
        private List<SpecialButton> buttons = new List<SpecialButton>();

        public SpecialReqs(IItem item)
        {
            InitializeComponent();
            this.item = item;
            SetUpWrapPanels();
            List<SpecialComponent> props = item.GetPropertyList();
            PositionButtons(props);
            if (item.ExtraDetails != "")
            {
                CustomRequirementTextBox.Text = item.ExtraDetails;
            }
        }

        private void PositionButtons(List<SpecialComponent> properties)
        {
            if (properties == null) return;
            foreach (SpecialComponent component in properties)
            {
                SpecialButton b = FindButton(component);
                if (b == null) continue;
                ToggleButton(b);
            }
        }

        private SpecialButton FindButton(SpecialComponent component)
        {            
            foreach(SpecialButton button in buttons) {
                if (button.IsEqualTo(component.GetSpecialType()))
                {
                    return button;
                }
            }
            return null;
        }

        private void SetUpWrapPanels()
        {
            //List<SpecialButton.SpecialType> sorted = (from e in Enum.GetValues(typeof(SpecialButton.SpecialType)).Cast<SpecialButton.SpecialType>() 
                                                      //orderby e.ToString() select e).ToList();
            List<SpecialButton.SpecialType> sorted = Enum.GetValues(typeof(SpecialButton.SpecialType)).Cast<SpecialButton.SpecialType>().OrderBy(e => SelectSortingString(e.ToString())).Select(e => e).ToList();

            foreach (SpecialButton.SpecialType type in sorted)
            {
                if (type == SpecialButton.SpecialType.NoType) continue;
                SpecialButton sb = new SpecialButton(type.ToString(), this, type);
                buttons.Add(sb);
                SpecialSource.Children.Add(sb);
            }
            //buttons.Add(new SpecialButton("Extra Hot", this, SpecialButton.SpecialType.ExtraHot));
        }

        private string SelectSortingString(string reqString)
        {
            string[] results = Regex.Split(reqString.ToString(), "(\\B[A-Z])");
            if (results.Length > 1)
            {
                return results[1];
            }
            else
            {
                return results[0];
            }
        }

        #region Depreciated
        /*
        private void NoPeasCheckBox_Click(object sender, RoutedEventArgs e)
        {
            item.Properties.NoPeas = (bool)((CheckBox)sender).IsChecked;
        }

        private void NoOnionsCheckBox_Click(object sender, RoutedEventArgs e)
        {
            item.Properties.NoOnions = (bool)((CheckBox)sender).IsChecked;
        }

        private void NoChilliCheckBox_Click(object sender, RoutedEventArgs e)
        {
            item.Properties.NoChilli = (bool)((CheckBox)sender).IsChecked;
        }

        private void NoPorkCheckBox_Click(object sender, RoutedEventArgs e)
        {
            item.Properties.NoPork = (bool)((CheckBox)sender).IsChecked;
        }

        private void NoShrimpsCheckBox_Click(object sender, RoutedEventArgs e)
        {
            item.Properties.NoShrimps = (bool)((CheckBox)sender).IsChecked;
        }

        private void SpicyCheckBox_Click(object sender, RoutedEventArgs e)
        {
            item.Properties.Spicy = (bool)((CheckBox)sender).IsChecked;
        }

        private void NoVegetablesCheckBox_Click(object sender, RoutedEventArgs e)
        {
            item.Properties.NoVegetables = (bool)((CheckBox)sender).IsChecked;
        }

        private void NoBeansproutsCheckBox_Click(object sender, RoutedEventArgs e)
        {
            item.Properties.NoBeansprouts = (bool)((CheckBox)sender).IsChecked;
        }

        private void ExtraHotCheckBox_Click(object sender, RoutedEventArgs e)
        {
            item.Properties.ExtraHot = (bool)((CheckBox)sender).IsChecked;
        }

        private void LittleOilCheckBox_Click(object sender, RoutedEventArgs e)
        {
            item.Properties.LittleOil = (bool)((CheckBox)sender).IsChecked;

        }

        private void NoPeanutsCheckBox_Click(object sender, RoutedEventArgs e)
        {
            item.Properties.NoChilli = (bool)((CheckBox)sender).IsChecked;
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

        private void CustomRequirementsTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (((TextBox)sender).Text == "Custom Requirements...")
            {
                ((TextBox)sender).Text = "";
                return;
            }

            if (string.IsNullOrEmpty(((TextBox)sender).Text))
            {
                ((TextBox)sender).Text = "Custom Requirements...";
                return;
            }
        }

        private void CustomRequirementsTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(((TextBox)sender).Text))
            {
                ((TextBox)sender).Text = "Custom Requirements...";
                return;
            }
        }        */
        #endregion

        private void DoneButton_Click(object sender, RoutedEventArgs e)
        {
            SpecialComponent special = new SpecialBase("", "");
            foreach (SpecialButton button in SpecialSink.Children) {
                special = button.Decorate(special);
            }
            item.Properties = special;
            this.Close();
        }

        public void ToggleButton(SpecialButton button)
        {
            if (SpecialSource.Children.Contains(button))
            {
                SpecialSource.Children.Remove(button);
                SpecialSink.Children.Add(button);
            }
            else
            {
                SpecialSink.Children.Remove(button);
                SpecialSource.Children.Add(button);
            }
        }

        private void CustomRequirementTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (item == null) return;
            PromptTB tb = (PromptTB)sender;
            item.ExtraDetails = tb.ContentText;
        }
    }
}
