using Beijing_Inn_Order_System.Items;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Beijing_Inn_Order_System.MenuDesigner
{
    public class MenuCategory : INotifyPropertyChanged
    {
        private ObservableCollection<IItem> items;
        private string englishName = "";
        private string chineseName = "";

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public MenuCategory(string englishName)
        {
            this.englishName = englishName;
            items = new ObservableCollection<IItem>();
        }

        public void AddItemID(int id)
        {
            items.Add(ItemManager.TotalItems[id]);
        }

        #region Properties
        public ObservableCollection<IItem> Items
        {
            get
            {
                return items;
            }
        }

        public string EnglishName
        {
            get
            {
                return englishName;
            }

            set
            {
                englishName = value;
                NotifyPropertyChanged("EnglishChineseName");
            }
        }

        public string ChineseName
        {
            get
            {
                return chineseName;
            }

            set
            {
                chineseName = value;
                NotifyPropertyChanged("EnglishChineseName");
            }
        }

        public string EnglishChineseName
        {
            get
            {
                return englishName + " - " + chineseName;
            }
        }
        #endregion
    }
}
