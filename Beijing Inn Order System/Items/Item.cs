using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Beijing_Inn_Order_System.Items
{
    [Serializable]
    public class Item
    {
        private static List<Item> totalItems;
        private static DataTable totalBoughtItems;

        private String englishName;
        private String chineseName;
        private float price;
        private float smallPrice;
        private float largePrice;
        private bool isLarge;
        private bool isSizeDish;
        private ItemProperties itemProperties;

        public Item(String englishName, String chineseName, float price)
        {
            this.englishName = englishName;
            this.chineseName = chineseName;
            this.price = price;
            this.isSizeDish = false;
            itemProperties = new ItemProperties();
        }

        public Item(String englishName, String chineseName, float fakePrice, float smallPrice, float largePrice)
        {
            this.englishName = englishName;
            this.chineseName = chineseName;
            this.price = fakePrice;
            this.smallPrice = smallPrice;
            this.largePrice = largePrice;
            this.isLarge = false;
            this.isSizeDish = true;
            itemProperties = new ItemProperties();
        }

        public void _Reset() //Depreciated
        {
            if (isSizeDish)
            {
                isLarge = false;
            }
        }

        public static void LoadItems()
        {
            totalItems = new List<Item>();
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
                    Item item = new Item(output[i, 0], output[i, 1], price, smallPrice, largePrice);
                    totalItems.Add(item);
                }
                //else if (price == -2)
                //{
                //    Console.WriteLine("seabass");
                //}
                else
                {
                    Item item = new Item(output[i, 0], output[i, 1], price);
                    totalItems.Add(item);
                }
            }
        }

        public static void AddOrderToCount(List<Item> basket)
        {
            XSSFWorkbook hssfwb = ExcelReader_NPOI.OpenXLSX("OrderStats.xlsx", false);
            ISheet sheet = hssfwb.GetSheet("Sheet1");

            foreach (Item item in basket)
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
            tempBoughtItems.Columns.Add("Item", typeof(Item));
            tempBoughtItems.Columns.Add("Count", typeof(int));

            int[] columnsString = new int[1] { 0 };
            int[] columnNumber = new int[1] { 1 };
            string[,] output = ExcelReader_NPOI.ReadAllXLSX("OrderStats.xlsx", columnsString, columnNumber, false);
            for (int i = 0; i < output.GetLength(0); i++)
            {
                if (output[i, 0] != null)
                {
                    float totalBought = float.Parse(output[i, 1]);
                    Item item = null;
                    for (int j = 0; j < totalItems.Count; j++)
                    {
                        if (totalItems[j].englishName == output[i, 0])
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

        public static List<Item> GetAppetisers()
        {
            return totalItems.GetRange(0, 28);
        }

        public static List<Item> GetSoup()
        {
            return totalItems.GetRange(28, 9);
        }

        public static List<Item> GetDuck()
        {
            return totalItems.GetRange(37, 4);
        }

        public static List<Item> GetSeafood()
        {
            return totalItems.GetRange(41, 23);
        }

        public static List<Item> GetChicken()
        {
            return totalItems.GetRange(64, 13);
        }

        public static List<Item> GetPorkBeefLamb()
        {
            return totalItems.GetRange(77, 20);
        }

        public static List<Item> GetCurry()
        {
            return totalItems.GetRange(97, 8);
        }

        public static List<Item> GetVegetable()
        {
            return totalItems.GetRange(105, 8);
        }

        public static List<Item> GetChopSuey()
        {
            return totalItems.GetRange(113, 5);
        }

        public static List<Item> GetChowMein()
        {
            return totalItems.GetRange(118, 10);
        }

        public static List<Item> GetVermicelli()
        {
            return totalItems.GetRange(128, 3);
        }

        public static List<Item> GetRice()
        {
            return totalItems.GetRange(131, 11);
        }

        public static List<Item> GetEnglish()
        {
            return totalItems.GetRange(142, 5);
        }

        public static List<Item> GetDesserts()
        {
            return totalItems.GetRange(147, 6);
        }

        public static List<Item> GetSetMeals()
        {
            return totalItems.GetRange(153, 4);
        }

        public static T DeepClone<T>(T obj)
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                ms.Position = 0;

                return (T)formatter.Deserialize(ms);
            }
        }

        public bool IsEqualTo(Item item)
        {
            if (item.EnglishName == englishName && item.ChineseName == chineseName && item.IsLarge == isLarge)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #region Properties
        public String EnglishName
        {
            get
            {
                return englishName;
            }

            set
            {
                englishName = value;
            }
        }

        public String ChineseName
        {
            get
            {
                return chineseName;
            }
        }

        public virtual float Price
        {
            get
            {
                if (price == -1)
                {
                    if (isLarge)
                    {
                        return largePrice;
                    }
                    else
                    {
                        return smallPrice;
                    }
                }
                return price;
            }
        }

        public bool IsLarge
        {
            get
            {
                return isLarge;
            }
            set
            {
                isLarge = value;
            }
        }

        public bool IsSizeDish
        {
            get
            {
                return isSizeDish;
            }
        }

        public ItemProperties Properties
        {
            get
            {
                return itemProperties;
            }
        }
        
        public string ConcatProperties
        {
            get
            {
                if (itemProperties.Special == null) return "";
                return itemProperties.Special.GetEnglishValue();
            }
        }

        public static List<Item> TotalItems
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
