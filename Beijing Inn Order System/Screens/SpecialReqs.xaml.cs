﻿using Beijing_Inn_Order_System.Items;
using Beijing_Inn_Order_System.Items.SpecialDecoration;
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
        private List<SpecialButton> buttons = new List<SpecialButton>();

        public SpecialReqs(Item item)
        {
            InitializeComponent();
            this.item = item;
            //NoPeasCheckBox.IsChecked = item.Properties.NoPeas;
            //NoOnionsCheckBox.IsChecked = item.Properties.NoOnions;
            //NoChilliCheckBox.IsChecked = item.Properties.NoChilli;
            SetUpWrapPanels();
        }

        private void SetUpWrapPanels()
        {
            foreach (SpecialButton.SpecialType type in Enum.GetValues(typeof(SpecialButton.SpecialType)))
            {
                SpecialButton sb = new SpecialButton(type.ToString(), this, type);
                buttons.Add(sb);
                SpecialSource.Children.Add(sb);
            }
            //buttons.Add(new SpecialButton("Extra Hot", this, SpecialButton.SpecialType.ExtraHot));
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
            item.Properties.Special = special;
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
    }
}
