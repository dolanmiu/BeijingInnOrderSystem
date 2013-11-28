using System;
using System.Windows.Data;
using System.Windows.Media;
using System.Globalization;
using System.Windows.Controls;


namespace Beijing_Inn_Order_System
{
    public sealed class BackgroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
            CultureInfo culture)
        {
            ListViewItem item = (ListViewItem)value;
            ListView listView =
                ItemsControl.ItemsControlFromItemContainer(item) as ListView;
            // Get the index of a ListViewItem
            int index =
                listView.ItemContainerGenerator.IndexFromContainer(item);

            if (index % 2 == 0)
            {
                return Brushes.Silver;
            }
            else
            {
                return Brushes.White;
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }
}
