using Beijing_Inn_Order_System.Items;
using Beijing_Inn_Order_System.Screens.OrderPageElements;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Beijing_Inn_Order_System.MenuDesigner
{
    /// <summary>
    /// Interaction logic for CategoryButton.xaml
    /// </summary>
    public partial class CategoryButton : Button
    {
        private FoodMenu menu;
        private MenuCategory category;

        public delegate void SetMenuItemsDelegate(); 

        public CategoryButton(FoodMenu menu, MenuCategory category)
        {
            this.menu = menu;
            this.category = category;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           //SetMenuItemsDelegate setMenuItems = new SetMenuItemsDelegate(SetMenuItems);
           //setMenuItems.BeginInvoke(null, null);

            Task setMenuTask = new Task(() =>
            {
                SetMenuItems();
            });
            setMenuTask.Start();
        }

        private void SetMenuItems() 
        {
             menu.Items = category.Items;
        }
    }
}
