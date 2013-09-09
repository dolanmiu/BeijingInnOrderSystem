using Beijing_Inn_Order_System.Customer;
using Beijing_Inn_Order_System.Items;
using Beijing_Inn_Order_System.Printing;
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
        //List<Item> itemBasket;
        Basket itemBasket;
        Address currentAddresses;

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
        }

        private string calculateBasketPrice()
        {
            float totalPrice = 0;
            for (int i = 0; i < itemBasket.Items.Count; i++)
            {
                totalPrice += itemBasket.Items[i].Price;
            }
            return totalPrice.ToString("0.00");
        }

        private void RefreshControls()
        {
            ItemBasketListView.Items.Refresh();
            BasketList.Items.Refresh();
            PriceTextBlock.Text = "£" + calculateBasketPrice();
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
            if (HouseNumberTextBox.Text == "")
            {
                RoadTextBlock.Text = currentAddresses.Road;
                RoadTextBlock_Print.Text = currentAddresses.Road;
            }
            else
            {
                RoadTextBlock.Text = HouseNumberTextBox.Text + " " + currentAddresses.Road;
                RoadTextBlock_Print.Text = HouseNumberTextBox.Text + " " + currentAddresses.Road;
            }

            TownTextBlock.Text = currentAddresses.Town;
            TownTextBlock_Print.Text = currentAddresses.Town;
            PostCodeTextBlock.Text = currentAddresses.PostCode;
            PostCodeTextBlock_Print.Text = currentAddresses.PostCode;
        }

        private void SetDistanceField()
        {
            DistanceTextBlock.Text = DistanceCalculator.getDistanceFromLatLonInKm(DistanceCalculator.BeijingInnCoords[0], DistanceCalculator.BeijingInnCoords[1], currentAddresses.Latitude, currentAddresses.Longitude) + "Km";
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
            //((IList)dragSource.ItemsSource).Remove(data);
            Item item = Item.DeepClone<Item>(data); 
            itemBasket.Items.Add(item);
            RefreshControls();
            //parent.Items.Add(data.EnglishName);
        }

        private void PostCodeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text.Length > 6)
            {
                for (int i = 0; i < Address.TotalAddresses.Count; i++)
                {
                    if (Address.TotalAddresses[i].PostCode != null)
                    {
                        if (Address.TotalAddresses[i].PostCode.Contains(tb.Text))
                        {
                            currentAddresses = Address.TotalAddresses[i];
                            SetAddressFields();
                            SetDistanceField();
                            break;
                        }
                    }
                }
            }
        }

        private void RoadNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text.Length > 6)
            {
                for (int i = 0; i < Address.TotalAddresses.Count; i++)
                {
                    if (Address.TotalAddresses[i].Road != null)
                    {
                        string road = Address.TotalAddresses[i].Road.ToUpper();
                        if (road.Contains(tb.Text.ToUpper()))
                        {
                            currentAddresses = Address.TotalAddresses[i];
                            SetAddressFields();
                            SetDistanceField();
                            break;
                        }
                    }
                }
            }
        }

        private void ClearBasket_Click(object sender, RoutedEventArgs e)
        {
            itemBasket.Items.Clear();
            RefreshControls();
        }

        private void BasketList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Console.WriteLine("selection changed");
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
            if (currentAddresses != null)
            {
                currentAddresses.Number = HouseNumberTextBox.Text;
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
            PrintReceipt.Print(itemBasket);
            if (currentAddresses != null)
            {
                if (currentAddresses.Number != null && currentAddresses.Road != null && itemBasket != null)
                {
                    Item.AddOrderToCount(itemBasket.Items);
                    Address.AddCustomerToCount(currentAddresses);
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
    }
}
