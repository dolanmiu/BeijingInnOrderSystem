using Beijing_Inn_Order_System.Helper_Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Beijing_Inn_Order_System.MenuDesigner
{
    public static class MenuManager
    {
        private static ObservableCollection<MenuCategory> menuCategories = new ObservableCollection<MenuCategory>();

        static MenuManager()
        {
        }

        public static void WriteMenuFile()
        {
            XDocument doc = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                new XElement("Menu",
                    from menuCategory in menuCategories
                    select new XElement("Category",
                        new XAttribute("EnglishName", menuCategory.EnglishName),
                        new XAttribute("ChineseName", menuCategory.ChineseName),
                        from item in menuCategory.Items
                        select new XElement("ID", item.Number - 1))));

            string fileLocation = Helper.GetAppDataFile("menu.xml");
            doc.Save(fileLocation);
        }

        public static void ReadMenuFile()
        {
            string fileLocation = Helper.GetAppDataFile("menu.xml");

            XDocument xDoc = new XDocument();
            try
            {
                xDoc = XDocument.Load(fileLocation);
            }
            catch (Exception e)
            {
                Debug.Write(e);
                return;
                //WriteMenuFile();
            }

            IEnumerable<XElement> menus = from row in xDoc.Descendants("Menu") select row;

            foreach (XElement menu in menus)
            {
                IEnumerable<XElement> categoies = from att in menu.Descendants("Category") select att;
                foreach (XElement category in categoies)
                {
                    string categoryEnglishName = category.Attribute("EnglishName").Value;
                    string categoryChineseName = category.Attribute("ChineseName").Value;

                    MenuCategory menuCategory = new MenuCategory(categoryEnglishName);
                    menuCategory.ChineseName = categoryChineseName;
                    menuCategories.Add(menuCategory);

                    IEnumerable<XElement> itemIDs = from att in category.Descendants("ID") select att;
                    foreach (XElement itemID in itemIDs)
                    {
                        Debug.Print(itemID.Name + " " + itemID.Value);
                        int itemNum;
                        if (int.TryParse(itemID.Value, out itemNum)) 
                        {
                            menuCategory.AddItemID(itemNum);
                        }
                    }
                }
            }
        }

        public static int MoveCategoryUp(MenuCategory menuCat)
        {
            int catIndex = menuCategories.IndexOf(menuCat);
            if (catIndex <= 0) return -1;
            menuCategories.RemoveAt(catIndex);
            menuCategories.Insert(catIndex - 1, menuCat);
            return catIndex - 1;
        }

        public static int MoveCategoryDown(MenuCategory menuCat)
        {
            int catIndex = menuCategories.IndexOf(menuCat);
            if (catIndex >= menuCategories.Count - 1) return -1;
            menuCategories.RemoveAt(catIndex);
            menuCategories.Insert(catIndex + 1, menuCat);
            return catIndex + 1;
        }

        #region Properties
        public static ObservableCollection<MenuCategory> MenuCategories
        {
            get
            {
                return menuCategories;
            }
        }
        #endregion
    }
}
