using Beijing_Inn_Order_System.Items;
using Beijing_Inn_Order_System.MenuDesigner;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Beijing_Inn_Order_System.Screens.ManagePageElements
{
    /// <summary>
    /// Interaction logic for MenuDesignerWindow.xaml
    /// </summary>
    public partial class MenuDesignerWindow : UserControl, INotifyPropertyChanged
    {
        private MenuCategory currentCategory;

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public MenuDesignerWindow()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            /*Style itemContainerStyle = new Style(typeof(ListBoxItem));
            itemContainerStyle.Setters.Add(new Setter(ListBoxItem.AllowDropProperty, true));
            itemContainerStyle.Setters.Add(new EventSetter(ListBoxItem.PreviewMouseLeftButtonDownEvent, new MouseButtonEventHandler(s_PreviewMouseLeftButtonDown)));
            itemContainerStyle.Setters.Add(new EventSetter(ListBoxItem.DropEvent, new DragEventHandler(MenuSectionsListBox_Drop)));
            MenuSectionsListBox.ItemContainerStyle = itemContainerStyle;*/
        }

        private void MenuSectionsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox lb = (ListBox)sender;
            if (lb.SelectedItem == null) return;
            currentCategory = (MenuCategory)lb.SelectedItem;
            NotifyPropertyChanged("CategoryItems");
        }

        private void TotalItemsListBox_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ListBox lb = (ListBox)sender;
            if (lb.SelectedItem == null) return;
            IItem item = (IItem)lb.SelectedItem;
            if (currentCategory == null) return;
            currentCategory.Items.Add(item);
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            MenuManager.WriteMenuFile();
        }

        private void AddCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            MenuCategory menuCategory = new MenuCategory("");
            NewMenuCategoryWindow window = new NewMenuCategoryWindow(menuCategory);
            window.ShowDialog();
            if (string.IsNullOrEmpty(menuCategory.EnglishName)) return;
            MenuManager.MenuCategories.Add(menuCategory);
            currentCategory = menuCategory;
            NotifyPropertyChanged("CategoryItems");
        }

        private void DeleteCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            MenuCategory category = (MenuCategory)MenuSectionsListBox.SelectedItem;
            if (category != null)
            {
                MenuManager.MenuCategories.Remove(category);
            }
            currentCategory = null;
            NotifyPropertyChanged("CategoryItems");
        }

        private void CategoryItemsListBox_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ListBox lb = (ListBox)sender;
            if (lb.SelectedItem == null) return;
            IItem item = (IItem)lb.SelectedItem;
            currentCategory.Items.Remove(item);
        }

        private void MenuSectionsListBox_Drop(object sender, DragEventArgs e)
        {
            MenuCategory droppedData = e.Data.GetData(typeof(MenuCategory)) as MenuCategory;
            MenuCategory target = ((ListBoxItem)(sender)).DataContext as MenuCategory;

            int removedIdx = MenuSectionsListBox.Items.IndexOf(droppedData);
            int targetIdx = MenuSectionsListBox.Items.IndexOf(target);

            if (removedIdx < targetIdx)
            {
                MenuManager.MenuCategories.Insert(targetIdx + 1, droppedData);
                MenuManager.MenuCategories.RemoveAt(removedIdx);
            }
            else
            {
                int remIdx = removedIdx + 1;
                if (MenuManager.MenuCategories.Count + 1 > remIdx)
                {
                    MenuManager.MenuCategories.Insert(targetIdx, droppedData);
                    MenuManager.MenuCategories.RemoveAt(remIdx);
                }
            }
        }

        void s_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            if (sender is ListBoxItem)
            {
                ListBoxItem draggedItem = sender as ListBoxItem;
                DragDrop.DoDragDrop(draggedItem, draggedItem.DataContext, DragDropEffects.Move);
                draggedItem.IsSelected = true;
            }
        }

        private void MenuSectionsListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListBox lb = (ListBox)sender;
            MenuCategory category = (MenuCategory)lb.SelectedItem;
            NewMenuCategoryWindow window = new NewMenuCategoryWindow(category);
            window.ShowDialog();
        }

        private void MenuSectionsListBox_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Pressed) return;
        }

        #region Properties
        public List<IItem> TotalItems
        {
            get
            {
                return ItemManager.TotalItems;
            }
        }

        public ObservableCollection<MenuCategory> MenuCategories
        {
            get
            {
                return MenuManager.MenuCategories;
            }
        }

        public ObservableCollection<IItem> CategoryItems
        {
            get
            {
                return currentCategory.Items;
            }
        }
        #endregion

        private void MoveCategoryUpButton_Click(object sender, RoutedEventArgs e)
        {
            if (MenuSectionsListBox.SelectedItem == null) return;
            int selectedValue = MenuManager.MoveCategoryUp((MenuCategory)MenuSectionsListBox.SelectedItem);
            if (selectedValue == -1) return;
            MenuSectionsListBox.SelectedIndex = selectedValue;
        }

        private void MoveCategoryDownButton_Click(object sender, RoutedEventArgs e)
        {
            if (MenuSectionsListBox.SelectedItem == null) return;
            int selectedValue = MenuManager.MoveCategoryDown((MenuCategory)MenuSectionsListBox.SelectedItem);
            if (selectedValue == -1) return;
            MenuSectionsListBox.SelectedIndex = selectedValue;
        }
    }
}
