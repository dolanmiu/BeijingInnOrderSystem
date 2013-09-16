using Beijing_Inn_Order_System.Customer;
using Beijing_Inn_Order_System.Items;
using Beijing_Inn_Order_System.Printing;
using Beijing_Inn_Order_System.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using WpfUIPickerLib;

namespace Beijing_Inn_Order_System
{
    /// <summary>
    /// Interaction logic for OrderPage.xaml
    /// </summary>
    public partial class OrderPage : UserControl
    {
        ListBox dragSource = null;
        Basket itemBasket;
        List<Address> currentAddresses = new List<Address>();
        Address currentAddress;

        public OrderPage()
        {
            itemBasket = new Basket();
            InitializeComponent();
        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            TotalItems.ItemsSource = Item.TotalItems;
            BasketList.ItemsSource = itemBasket.Items;
            ItemBasketListView.ItemsSource = itemBasket.Items;

            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(UpdatePrinterStatusTick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 2);
            dispatcherTimer.Start();
        }
        
        private void RefreshControls()
        {
            ItemBasketListView.Items.Refresh();
            BasketList.Items.Refresh();
            PriceTextBlock.Text = "£" + itemBasket.CalculatePrice().ToString("0.00");
        }

        public List<TumblerData> ApparelTumblers
        {
            get
            {
                List<TumblerData> retVal = new List<TumblerData>();
                retVal.Add(new TumblerData(new string[] { "White", "Black", "Blue", "Orange", "Green", "Red", "Yellow", "Pink", "VanDyke Brown" }.ToList<object>(), 0, "--"));
                retVal.Add(new TumblerData(new string[] { "XS", "S", "M", "L", "XL", "XXL" }.ToList<object>(), 0, ""));
                return retVal;
            }
        }

        private void SetAddressFields()
        {
            if (currentAddress == null) return;
            if (HouseNumberTextBox.Text == "")
            {
                RoadTextBlock_Print.Text = currentAddress.Road;
                RoadTextBlock.Text = currentAddress.Road;
            }
            else
            {
                RoadTextBlock_Print.Text = HouseNumberTextBox.Text + " " + currentAddress.Road;
                RoadTextBlock.Text = HouseNumberTextBox.Text + " " + currentAddress.Road;
            }

            TownTextBlock_Print.Text = currentAddress.Town;
            PostCodeTextBlock_Print.Text = currentAddress.PostCode;
            TownTextBlock.Text = currentAddress.Town;
            PostCodeTextBlock.Text = currentAddress.PostCode;
        }

        private void SetDistanceField()
        {
            if (currentAddress != null)
            {
                DistanceTextBlock.Text = "Distance: " + DistanceCalculator.getDistanceFromLatLonInKm(DistanceCalculator.BeijingInnCoords[0], DistanceCalculator.BeijingInnCoords[1], currentAddress.Latitude, currentAddress.Longitude) + "Km";
            }
        }

        #region GetDataFromListBox(ListBox,Point)
        private static object GetDataFromListBox(ListBox source, Point point)
        {
            UIElement element = source.InputHitTest(point) as UIElement;
            if (element != null)
            {
                object data = DependencyProperty.UnsetValue;
                while (data == DependencyProperty.UnsetValue)
                {
                    data = source.ItemContainerGenerator.ItemFromContainer(element);
                    if (data == DependencyProperty.UnsetValue)
                    {
                        element = VisualTreeHelper.GetParent(element) as UIElement;
                    }
                    if (element == source)
                    {
                        return null;
                    }
                }
                if (data != DependencyProperty.UnsetValue)
                {
                    return data;
                }
            }
            return null;
        }
        #endregion

        #region Control Methods
        private void ListBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ListBox parent = (ListBox)sender;
            dragSource = parent;
            object data = GetDataFromListBox(dragSource, e.GetPosition(parent));

            if (data != null)
            {
                DragDrop.DoDragDrop(parent, data, DragDropEffects.Move);
            }
        }

        private void ListBox_Drop(object sender, DragEventArgs e)
        {
            ListBox parent = (ListBox)sender;
            Item data = (Item)e.Data.GetData(typeof(Item));
            Item item = Item.DeepClone<Item>(data);
            itemBasket.Items.Add(item);
            RefreshControls();
        }
        
        private void ClearBasket_Click(object sender, RoutedEventArgs e)
        {
            itemBasket.Items.Clear();
            RefreshControls();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Item local = ((sender as Button).Tag as Item);
            itemBasket.Items.Remove((Item)local);
            RefreshControls();
        }

        private void HouseNumberTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (currentAddress != null)
            {
                currentAddress.Number = HouseNumberTextBox.Text;
                SetAddressFields();
            }
        }

        private void FilterAppetisersButton_Click(object sender, RoutedEventArgs e)
        {
            TotalItems.ItemsSource = Item.GetAppetisers();
            RefreshControls();
        }

        private void FilterSoupButton_Click(object sender, RoutedEventArgs e)
        {
            TotalItems.ItemsSource = Item.GetSoup();
            RefreshControls();
        }

        private void FIlterDuckButton_Click(object sender, RoutedEventArgs e)
        {
            TotalItems.ItemsSource = Item.GetDuck();
            RefreshControls();
        }

        private void FilterSeafoodButton_Click(object sender, RoutedEventArgs e)
        {
            TotalItems.ItemsSource = Item.GetSeafood();
            RefreshControls();
        }

        private void FilterChickenButton_Click(object sender, RoutedEventArgs e)
        {
            TotalItems.ItemsSource = Item.GetChicken();
            RefreshControls();
        }

        private void FilterPorkBeefLambButton_Click(object sender, RoutedEventArgs e)
        {
            TotalItems.ItemsSource = Item.GetPorkBeefLamb();
            RefreshControls();
        }

        private void FilterCurryButton_Click(object sender, RoutedEventArgs e)
        {
            TotalItems.ItemsSource = Item.GetCurry();
            RefreshControls();
        }

        private void FilterVegetableButton_Click(object sender, RoutedEventArgs e)
        {
            TotalItems.ItemsSource = Item.GetVegetable();
            RefreshControls();
        }

        private void FilterChopSueyButton_Click(object sender, RoutedEventArgs e)
        {
            TotalItems.ItemsSource = Item.GetChopSuey();
            RefreshControls();
        }

        private void FilterChowMeinButton_Click(object sender, RoutedEventArgs e)
        {
            TotalItems.ItemsSource = Item.GetChowMein();
            RefreshControls();
        }

        private void FilterVermicelliButton_Click(object sender, RoutedEventArgs e)
        {
            TotalItems.ItemsSource = Item.GetVermicelli();
            RefreshControls();
        }

        private void FilterRiceButton_Click(object sender, RoutedEventArgs e)
        {
            TotalItems.ItemsSource = Item.GetRice();
            RefreshControls();
        }

        private void FilterEnglishButton_Click(object sender, RoutedEventArgs e)
        {
            TotalItems.ItemsSource = Item.GetEnglish();
            RefreshControls();
        }

        private void FilterDessertsButton_Click(object sender, RoutedEventArgs e)
        {
            TotalItems.ItemsSource = Item.GetDesserts();
            RefreshControls();
        }

        private void FilterSetMealsButton_Click(object sender, RoutedEventArgs e)
        {
            TotalItems.ItemsSource = Item.GetSetMeals();
            RefreshControls();
        }
        #endregion

        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            ReceiptPrinter.Print(itemBasket, currentAddress);
            if (currentAddress != null)
            {
                if (currentAddress.Number != null && currentAddress.Road != null && itemBasket != null)
                {
                    Item.AddOrderToCount(itemBasket.Items);
                    Address.AddCustomerToCount(currentAddress);
                    //print it
                }
            }
        }

        private void AddSpecialButton_Click(object sender, RoutedEventArgs e)
        {
            Item local = ((sender as Button).Tag as Item);
            SpecialReqs special = new SpecialReqs(local);
            special.ShowDialog();
            RefreshControls();
        }

        private void PortionSizeToggleButton_Click(object sender, RoutedEventArgs e)
        {
            Item local = ((sender as Button).Tag as Item);
            if (local.IsLarge)
            {
                local.IsLarge = false;
            }
            else
            {
                local.IsLarge = true;
            }
            RefreshControls();
        }

        private void NewAddressButton_Click(object sender, RoutedEventArgs e)
        {
            Address address = new Address();
            NewAddressControl nac = new NewAddressControl(address);
            nac.ShowDialog();
            if (address.PostCode == null || address.Road == null || address.Town == null) return;
            Address.AddForeignAddress(address);
            currentAddress = address;
        }

        private void SearchResultsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((Address)((ListBox)sender).SelectedItem == null) return;
            currentAddress = (Address)((ListBox)sender).SelectedItem;
            SetAddressFields();
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox t = (sender as TextBox);
            string text = t.Text.Trim();
            currentAddresses = Address.Search(text);
            if (SearchResultsListBox == null) return;
            SearchResultsListBox.ItemsSource = currentAddresses;
            SearchResultsListBox.Items.Refresh();
        }

        
        private void SearchTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(((TextBox)sender).Text))
            {
                ((TextBox)sender).Text = "Search postcode or road...";
                return;
            }
        }

        private void SearchTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (((TextBox)sender).Text == "Search postcode or road...")
            {
                ((TextBox)sender).Text = "";
                return;
            }

            if (string.IsNullOrEmpty(((TextBox)sender).Text))
            {
                ((TextBox)sender).Text = "Search postcode or road...";
                return;
            }
        }

        private void SetPrinterStatusText(TextBlock tb)
        {
            tb.Text = "Health: " + ReceiptPrinter.Health + "\nPower: " + ReceiptPrinter.PowerState + "\nCover: ";
            if (ReceiptPrinter.CoverOpen == true)
            {
                tb.Text += "Open";
            }
            if (ReceiptPrinter.CoverOpen == false)
            {
                tb.Text += "Closed";
            }
            if (ReceiptPrinter.CoverOpen == null)
            {
                tb.Text += "Not Connected";
            }
        }

        private void UpdatePrinterStatusTick(object sender, EventArgs e)
        {
            SetPrinterStatusText(PrinterStatusTextBlock);
        }
    }
}
