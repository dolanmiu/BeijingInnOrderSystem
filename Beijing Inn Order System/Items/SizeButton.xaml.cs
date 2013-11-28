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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Beijing_Inn_Order_System.Items
{
    /// <summary>
    /// Interaction logic for SizeButton.xaml
    /// </summary>
    public partial class SizeButton : Button
    {
        public SizeButton()
        {
            InitializeComponent();
        }

        private void SizeButt_Loaded(object sender, RoutedEventArgs e)
        {
            IItem local = (Tag as IItem);
            if (local.IsPieDish)
            {
                Content = ((PieItem)local).SizeString;
            }
            else if (local.IsSizeDish)
            {
                Content = ((SizeItem)local).EnglishSizeString;
            }
            else
            {
                Visibility = System.Windows.Visibility.Hidden;
            }
        }

        private void SizeButt_Click(object sender, RoutedEventArgs e)
        {
            IItem local = (Tag as IItem);

            if (local.IsPieDish)
            {
                PieItem p = ((PieItem)local);
                switch (p.Size)
                {
                    case PieItem.PieSize.Whole:
                        p.Size = PieItem.PieSize.Half;
                        break;
                    case PieItem.PieSize.Half:
                        p.Size = PieItem.PieSize.Quarter;
                        break;
                    case PieItem.PieSize.Quarter:
                        p.Size = PieItem.PieSize.Whole;
                        break;
                }
                Content = p.SizeString;
            }

            if (local.IsSizeDish)
            {
                SizeItem item = ((SizeItem)local);
                if (item.IsLarge)
                {
                    item.IsLarge = false;
                }
                else
                {
                    item.IsLarge = true;
                }
                Content = item.EnglishSizeString;
            }
        }
    }
}
