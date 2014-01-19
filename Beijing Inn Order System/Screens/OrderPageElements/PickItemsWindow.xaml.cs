using Beijing_Inn_Order_System.Customer;
using Beijing_Inn_Order_System.Helper_Classes;
using Beijing_Inn_Order_System.Items;
using Beijing_Inn_Order_System.MenuDesigner;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace Beijing_Inn_Order_System.Screens.OrderPageElements
{
    /// <summary>
    /// Interaction logic for PickItemsWindow.xaml
    /// </summary>
    public partial class PickItemsWindow : UserControl, INotifyPropertyChanged
    {
        private OrderDetails orderDetails;
        private FoodMenu menu;

        public delegate ObservableCollection<IItem> SearchMenuItemsDelegate(string query); 

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public PickItemsWindow(OrderDetails orderDetails, ObservableCollection<MenuCategory> menuCategories)
        {
            this.orderDetails = orderDetails;
            menu = new FoodMenu(menuCategories);
            InitializeComponent();
            MainGrid.Children.Add(menu.Grid);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
        }

        #region MenuSeparation
        private void FilterAppetisersButton_Click(object sender, RoutedEventArgs e)
        {
            TotalItems.ItemsSource = ItemManager.GetAppetisers();
        }

        private void FilterSoupButton_Click(object sender, RoutedEventArgs e)
        {
            TotalItems.ItemsSource = ItemManager.GetSoup();
        }

        private void FIlterDuckButton_Click(object sender, RoutedEventArgs e)
        {
            TotalItems.ItemsSource = ItemManager.GetDuck();
        }

        private void FilterSeafoodButton_Click(object sender, RoutedEventArgs e)
        {
            TotalItems.ItemsSource = ItemManager.GetSeafood();
        }

        private void FilterChickenButton_Click(object sender, RoutedEventArgs e)
        {
            TotalItems.ItemsSource = ItemManager.GetChicken();
        }

        private void FilterPorkBeefLambButton_Click(object sender, RoutedEventArgs e)
        {
            TotalItems.ItemsSource = ItemManager.GetPorkBeefLamb();
        }

        private void FilterCurryButton_Click(object sender, RoutedEventArgs e)
        {
            TotalItems.ItemsSource = ItemManager.GetCurry();
        }

        private void FilterVegetableButton_Click(object sender, RoutedEventArgs e)
        {
            TotalItems.ItemsSource = ItemManager.GetVegetable();
        }

        private void FilterChopSueyButton_Click(object sender, RoutedEventArgs e)
        {
            TotalItems.ItemsSource = ItemManager.GetChopSuey();
        }

        private void FilterChowMeinButton_Click(object sender, RoutedEventArgs e)
        {
            TotalItems.ItemsSource = ItemManager.GetChowMein();
        }

        private void FilterVermicelliButton_Click(object sender, RoutedEventArgs e)
        {
            TotalItems.ItemsSource = ItemManager.GetVermicelli();
        }

        private void FilterRiceButton_Click(object sender, RoutedEventArgs e)
        {
            TotalItems.ItemsSource = ItemManager.GetRice();
        }

        private void FilterEnglishButton_Click(object sender, RoutedEventArgs e)
        {
            TotalItems.ItemsSource = ItemManager.GetEnglish();
        }

        private void FilterDessertsButton_Click(object sender, RoutedEventArgs e)
        {
            TotalItems.ItemsSource = ItemManager.GetDesserts();
        }

        private void FilterSetMealsButton_Click(object sender, RoutedEventArgs e)
        {
            TotalItems.ItemsSource = ItemManager.GetSetMeals();
        }
        #endregion

        private void ListBox_Drop(object sender, DragEventArgs e)
        {
            ListBox parent = (ListBox)sender;
            IItem data = GetDragEventObject(e);
            IItem item = Helper.DeepClone<IItem>(data);
            orderDetails.ItemBasket.Items.Add(item);
            SearchItemTextBox.Text = "";
        }

        private IItem GetDragEventObject(DragEventArgs e)
        {
            IItem data;
            data = (IItem)e.Data.GetData(typeof(Item));
            if (data != null)
            {
                return data;
            }
            data = (IItem)e.Data.GetData(typeof(SizeItem));
            if (data != null)
            {
                return data;
            }
            data = (IItem)e.Data.GetData(typeof(PieItem));
            if (data != null)
            {
                return data;
            }
            return null;
        }

        private void ClearBasket_Click(object sender, RoutedEventArgs e)
        {
            orderDetails.ItemBasket.Items.Clear();
            orderDetails.CurrentAddress = null;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            IItem local = ((sender as Button).Tag as IItem);
            orderDetails.ItemBasket.Items.Remove((IItem)local);
        }

        private void AddSpecialButton_Click(object sender, RoutedEventArgs e)
        {
            IItem local = ((sender as Button).Tag as IItem);
            SpecialReqs special = new SpecialReqs(local);
            special.ShowDialog();
        }

        private void AddItemToBasket(object sender, EventArgs e)
        {
            ListBox lb = (ListBox)sender;
            if (lb.SelectedItem == null) return;
            IItem item = Helper.DeepClone<IItem>((IItem)lb.SelectedItem);
            orderDetails.ItemBasket.Items.Add(item);
            //orderDetails.ItemBasket.NotifyPropertyChanged("ConcatItems");
            SearchItemTextBox.Clear();
        }

        private void ChangeMenuLanguageButton_Click(object sender, RoutedEventArgs e)
        {
            string path = TotalItems.DisplayMemberPath;
            if (path == "NumberedChineseName")
            {
                TotalItems.DisplayMemberPath = "NumberedEnglishName";
                BasketItemName.DisplayMemberBinding = new Binding("EnglishName");
            }
            else
            {
                TotalItems.DisplayMemberPath = "NumberedChineseName";
                BasketItemName.DisplayMemberBinding = new Binding("ChineseName");
            }
        }

        private void SearchItemTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox t = (sender as TextBox);
            string text = t.Text.Trim();
            if (TotalItems == null || text == "Search item name or number...") return; //orderDetails.ItemBasketListView == null 
            SearchMenuItemsDelegate searchItemsDelegate = new SearchMenuItemsDelegate(SearchItems);
            searchItemsDelegate.BeginInvoke(text, new AsyncCallback(SearchCallBack), searchItemsDelegate);
        }

        private ObservableCollection<IItem> SearchItems(string query)
        {
            return ItemManager.Search(query);
        }

        private void SearchCallBack(IAsyncResult result)
        {
            SearchMenuItemsDelegate searchDelegate = (SearchMenuItemsDelegate)result.AsyncState;
            ObservableCollection<IItem> currentAddresses = searchDelegate.EndInvoke(result);
            Menu.Items = currentAddresses;
        }

        #region Properties
        public OrderDetails OrderDetails
        {
            get
            {
                return orderDetails;
            }
        }

        public FoodMenu Menu
        {
            get
            {
                return menu;
            }
        }
        #endregion
    }
}
