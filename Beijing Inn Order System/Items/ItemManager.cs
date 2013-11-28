using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;

namespace Beijing_Inn_Order_System.Items
{
    public static class ItemManager
    {
        private static List<IItem> totalItems;
        private static DataTable totalBoughtItems;

        public static void LoadItems()
        {
            totalItems = new List<IItem>();
            int[] columnsString = new int[2] { 0, 1 };
            int[] columnNumber = new int[1] { 2 };
            string[,] output = ExcelReader_NPOI.ReadAllXLSX("FoodItems.xlsx", columnsString, columnNumber, true);
            XSSFWorkbook footItemsBook = ExcelReader_NPOI.OpenXLSX("FoodItems.xlsx", true);
            for (int i = 0; i < output.GetLength(0); i++)
            {
                float price = float.Parse(output[i, 2]);
                if (price == -1)
                {
                    float smallPrice = float.Parse(ExcelReader_NPOI.ReadSingleXLSXFromWorkBook(footItemsBook, i, 3, true));
                    float largePrice = float.Parse(ExcelReader_NPOI.ReadSingleXLSXFromWorkBook(footItemsBook, i, 4, true));
                    SizeItem item = new SizeItem(output[i, 0], output[i, 1], i + 1, smallPrice, largePrice);
                    totalItems.Add(item);
                }
                else if (price == -2)
                {
                    float wholePrice = float.Parse(ExcelReader_NPOI.ReadSingleXLSXFromWorkBook(footItemsBook, i, 3, true));
                    float halfPrice = float.Parse(ExcelReader_NPOI.ReadSingleXLSXFromWorkBook(footItemsBook, i, 4, true));
                    float quarterPrice = float.Parse(ExcelReader_NPOI.ReadSingleXLSXFromWorkBook(footItemsBook, i, 5, true));
                    PieItem item = new PieItem(output[i, 0], output[i, 1], i + 1, wholePrice, halfPrice, quarterPrice);
                    //Console.WriteLine("seabass");
                    totalItems.Add(item);
                }
                else
                {
                    Item item = new Item(output[i, 0], output[i, 1], i + 1, price);
                    totalItems.Add(item);
                }
            }
        }

        public static void AddOrderToCount(ObservableCollection<IItem> basket)
        {
            XSSFWorkbook hssfwb = ExcelReader_NPOI.OpenXLSX("OrderStats.xlsx", false);
            ISheet sheet = hssfwb.GetSheet("Sheet1");

            foreach (IItem item in basket)
            {
                int numberOfRows = sheet.LastRowNum;
                for (int i = 0; i <= sheet.LastRowNum; i++)
                {
                    if (sheet.GetRow(i) != null)
                    {
                        if (sheet.GetRow(i).GetCell(0).StringCellValue == item.EnglishName)
                        {
                            int count = (int)sheet.GetRow(i).GetCell(1).NumericCellValue;
                            sheet.GetRow(i).GetCell(1).SetCellValue(count + 1);
                            break;
                        }
                    }

                    if (i == sheet.LastRowNum)
                    {
                        sheet.CreateRow(i + 1); //Creating Row Overwrites whole row
                        sheet.GetRow(i + 1).CreateCell(0).SetCellValue(item.EnglishName);
                        sheet.GetRow(i + 1).CreateCell(1).SetCellValue(0);
                    }
                }
            }
            ExcelReader_NPOI.WriteXLSX(hssfwb, "OrderStats.xlsx");
        }

        public static void LoadTotalOrders()
        {
            DataTable tempBoughtItems = new DataTable();
            tempBoughtItems = new DataTable();
            tempBoughtItems.Columns.Add("Item", typeof(IItem));
            tempBoughtItems.Columns.Add("Count", typeof(int));

            int[] columnsString = new int[1] { 0 };
            int[] columnNumber = new int[1] { 1 };
            string[,] output = ExcelReader_NPOI.ReadAllXLSX("OrderStats.xlsx", columnsString, columnNumber, false);
            for (int i = 0; i < output.GetLength(0); i++)
            {
                if (output[i, 0] != null)
                {
                    float totalBought = float.Parse(output[i, 1]);
                    IItem item = null;
                    for (int j = 0; j < totalItems.Count; j++)
                    {
                        if (totalItems[j].EnglishName == output[i, 0])
                        {
                            item = totalItems[j];
                            break;
                        }
                    }
                    tempBoughtItems.Rows.Add(item, totalBought);
                }
            }
            totalBoughtItems = tempBoughtItems;
        }

        #region ListFilter
        public static List<IItem> GetAppetisers()
        {
            return totalItems.GetRange(0, 28);
        }

        public static List<IItem> GetSoup()
        {
            return totalItems.GetRange(28, 9);
        }

        public static List<IItem> GetDuck()
        {
            return totalItems.GetRange(37, 4);
        }

        public static List<IItem> GetSeafood()
        {
            return totalItems.GetRange(41, 23);
        }

        public static List<IItem> GetChicken()
        {
            return totalItems.GetRange(64, 13);
        }

        public static List<IItem> GetPorkBeefLamb()
        {
            return totalItems.GetRange(77, 20);
        }

        public static List<IItem> GetCurry()
        {
            return totalItems.GetRange(97, 8);
        }

        public static List<IItem> GetVegetable()
        {
            return totalItems.GetRange(105, 8);
        }

        public static List<IItem> GetChopSuey()
        {
            return totalItems.GetRange(113, 5);
        }

        public static List<IItem> GetChowMein()
        {
            return totalItems.GetRange(118, 10);
        }

        public static List<IItem> GetVermicelli()
        {
            return totalItems.GetRange(128, 3);
        }

        public static List<IItem> GetRice()
        {
            return totalItems.GetRange(131, 11);
        }

        public static List<IItem> GetEnglish()
        {
            return totalItems.GetRange(142, 5);
        }

        public static List<IItem> GetDesserts()
        {
            return totalItems.GetRange(147, 6);
        }

        public static List<IItem> GetSetMeals()
        {
            return totalItems.GetRange(153, 4);
        }
        #endregion

        public static ObservableCollection<IItem> Search(string text)
        {
            ObservableCollection<IItem> items = new ObservableCollection<IItem>();
            int foodNumber;
            bool isNum = int.TryParse(text, out foodNumber);

            if (isNum)
            {
                foodNumber = int.Parse(text);
                foreach (IItem item in totalItems)
                {
                    if (item.Number == foodNumber)
                    {
                        items.Add(item);
                        //break;
                    }
                }
            }
            else
            {
                string[] queries = text.Split(' ');
                foreach (IItem item in totalItems)
                {
                    if (StringContainsStrings(item.EnglishName, queries))
                    {
                        items.Add(item);
                    }
                }
            }
            return items;
        }

        private static bool StringContainsStrings(string theString, string[] queries)
        {
            bool output = false;
            foreach (string query in queries)
            {
                string formattedQuery = query.Trim().ToLower();
                if (theString.ToLower().Contains(formattedQuery))
                {
                    output = true;
                }
                else
                {
                    return false;
                }
            }
            return output;
        }

        #region Properties
        public static List<IItem> TotalItems
        {
            get
            {
                return totalItems;
            }

            set
            {
                totalItems = value;
            }
        }

        public static DataTable TotalBoughtItems
        {
            get
            {
                return totalBoughtItems;
            }

            set
            {
                totalBoughtItems = value;
            }
        }
        #endregion
    }
}
