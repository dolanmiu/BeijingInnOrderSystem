using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace Beijing_Inn_Order_System.Helper_Classes
{
    public static class ListViewSorter
    {
        private static GridViewColumnHeader _CurSortColItem = null;
        private static SortAdorner _CurAdornerItem = null;

        private static void Sort(object sender, ListView listView, ListSortDirection newDir)
        {
            GridViewColumnHeader column = sender as GridViewColumnHeader;
            String field = column.Tag as String;

            if (_CurSortColItem != null)
            {
                var adornerLayer = AdornerLayer.GetAdornerLayer(_CurSortColItem);
                if (adornerLayer != null) {
                    adornerLayer.Remove(_CurAdornerItem);
                    listView.Items.SortDescriptions.Clear();
                }

                _CurSortColItem = column;
                _CurAdornerItem = new SortAdorner(_CurSortColItem, newDir);
                AdornerLayer.GetAdornerLayer(_CurSortColItem).Add(_CurAdornerItem);
                listView.Items.SortDescriptions.Add(new SortDescription(field, newDir));
            }

            //ListSortDirection newDir = ListSortDirection.Descending;
            //if (_CurSortColItem == column && _CurAdornerItem.Direction == newDir)
            //{
            //    newDir = ListSortDirection.Ascending;
            //}
        }

        public static void SortAlternate(object sender, ListView listView)
        {
            GridViewColumnHeader column = sender as GridViewColumnHeader;
            ListSortDirection newDir = ListSortDirection.Descending;
            if (_CurSortColItem == column && _CurAdornerItem.Direction == newDir)
            {
                Sort(sender, listView, ListSortDirection.Ascending);
            }
            else
            {
                Sort(sender, listView, ListSortDirection.Descending);
            }
        }

        public static void SortAccending(object sender, ListView listView)
        {
            Sort(sender, listView, ListSortDirection.Ascending);
        }

        public static void SortDescending(object sender, ListView listView)
        {
            Sort(sender, listView, ListSortDirection.Descending);
        }
    }


    class SortAdorner : Adorner
    {
        private readonly static Geometry _AscGeometry = Geometry.Parse("M 0,0 L 10,0 L 5,5 Z");
        private readonly static Geometry _DescGeometry = Geometry.Parse("M 0,5 L 10,5 L 5,0 Z");

        public ListSortDirection Direction { get; private set; }

        public SortAdorner(UIElement element, ListSortDirection dir)
            : base(element)
        {
            Direction = dir;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            if (AdornedElement.RenderSize.Width < 20)
            {
                return;
            }

            drawingContext.PushTransform(new TranslateTransform(AdornedElement.RenderSize.Width - 15, (AdornedElement.RenderSize.Height - 5) / 2));

            drawingContext.DrawGeometry(Brushes.Black, null, Direction == ListSortDirection.Ascending ? _AscGeometry : _DescGeometry);
            drawingContext.Pop();
        }
    }
}
