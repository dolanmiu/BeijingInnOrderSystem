using Beijing_Inn_Order_System.Items;
using Beijing_Inn_Order_System.Screens.OrderPageElements;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Beijing_Inn_Order_System.MenuDesigner
{
    public class FoodMenu : INotifyPropertyChanged
    {
        private ObservableCollection<MenuCategory> menuCategories;
        private ObservableCollection<IItem> currentListItems;

        private Grid grid;

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public FoodMenu(ObservableCollection<MenuCategory> menuCategories)
        {
            this.menuCategories = menuCategories;
            this.grid = GenerateMenu();
            currentListItems = new ObservableCollection<IItem>();
        }

        private Grid GenerateMenu()
        {
            Grid grid = new Grid();
            grid.VerticalAlignment = VerticalAlignment.Stretch;
            grid.HorizontalAlignment = HorizontalAlignment.Left;
            grid.Margin = new Thickness(5, 0, 0, 10);
            grid.Width = 120;

            if (menuCategories.Count == 0)
            {
                TextBlock noCategoriesText = new TextBlock();
                noCategoriesText.Text = "Sorry :( No menu found. Please create one in the Manage Section\n\n\n抱歉 :( 发现没有菜单，请在“管理”一节中创建一个";
                noCategoriesText.TextWrapping = TextWrapping.Wrap;
                noCategoriesText.TextAlignment = TextAlignment.Center;
                noCategoriesText.VerticalAlignment = VerticalAlignment.Center;
                grid.Children.Add(noCategoriesText);
                return grid;
            }

            for (int i = 0; i < menuCategories.Count; i++)
            {
                RowDefinition buttonRowDefinition = new RowDefinition();
                RowDefinition spacerRowDefinition = new RowDefinition();
                buttonRowDefinition.Height = new GridLength(5, GridUnitType.Star);
                spacerRowDefinition.Height = new GridLength(5);

                grid.RowDefinitions.Add(buttonRowDefinition);
                if (i < menuCategories.Count - 1)
                {
                    grid.RowDefinitions.Add(spacerRowDefinition);
                }

                CategoryButton button = new CategoryButton(this, menuCategories[i]);
                string buttonContent = menuCategories[i].EnglishName;
                if (!string.IsNullOrEmpty(menuCategories[i].ChineseName))
                {
                    buttonContent += "\n" + menuCategories[i].ChineseName;
                }
                button.Content = buttonContent;

                grid.Children.Add(button);
                Grid.SetRow(button, i*2);
            }
            return grid;
        }

        #region Properties
        public Grid Grid
        {
            get
            {
                return grid;
            }
        }

        public ObservableCollection<IItem> Items
        {
            get
            {
                return currentListItems;
            }
            set
            {
                currentListItems = value;
                NotifyPropertyChanged("Items");
            }
        }
        #endregion
    }
}
