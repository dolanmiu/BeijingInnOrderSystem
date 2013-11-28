using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Beijing_Inn_Order_System.MenuDesigner
{
    /// <summary>
    /// Interaction logic for NewMenuCategoryWindow.xaml
    /// </summary>
    public partial class NewMenuCategoryWindow : Elysium.Controls.Window
    {
        private MenuCategory menuCategory;
        public NewMenuCategoryWindow(MenuCategory menuCategory)
        {
            this.menuCategory = menuCategory;
            InitializeComponent();
            if (!string.IsNullOrEmpty(menuCategory.EnglishName))
            {
                EnglishNameTextBox.Text = menuCategory.EnglishName;
            }

            if (!string.IsNullOrEmpty(menuCategory.ChineseName))
            {
                ChineseNameTextBox.Text = menuCategory.ChineseName;
            }
        }

        private void CreateCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(EnglishNameTextBox.ContentText)) this.Close();

            menuCategory.EnglishName = EnglishNameTextBox.ContentText.Trim();
            if (!string.IsNullOrEmpty(ChineseNameTextBox.ContentText))
            {
                menuCategory.ChineseName = ChineseNameTextBox.ContentText.Trim();
            }
            this.Close();
        }
    }
}
