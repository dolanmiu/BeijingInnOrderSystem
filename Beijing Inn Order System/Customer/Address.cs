using Beijing_Inn_Order_System.Items;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beijing_Inn_Order_System.Customer
{
    public class Address
    {
        private static List<Address> totalAddresses = new List<Address>();
        private static List<string> totalPostCodes = new List<string>();
        private static DataTable totalCustomers;
        private String number;
        private String postCode;
        private String road;
        private String town;
        private double latitude;
        private double longitude;

        public Address(String postCode, String road, String town, double latitude, double longitude)
        {
            this.postCode = postCode;
            this.road = road;
            this.town = town;
            this.latitude = latitude;
            this.longitude = longitude;
        }

        public Address(string number, String postCode, String road, String town)
        {
            this.number = number;
            this.postCode = postCode;
            this.road = road;
            this.town = town;
        }

        public static void LoadAllAddresses()
        {
            int[] columns = new int[3] { 0, 1, 2, };
            int[] latLong = new int[2] { 3, 4 };
            string[,] output = ExcelReader_NPOI.ReadAllXLSX("AddressList.xlsx", columns, latLong, true);
            for (int i = 1; i < output.GetLength(0); i++)
            {
                Address address = new Address(output[i, 0], output[i, 1], output[i, 2], double.Parse(output[i, 3]), double.Parse(output[i, 4]));
                totalAddresses.Add(address);
            }
        }

        public static void LoadAllPostCodes()
        {
            int[] columns = new int[1] { 0 };
            string[,] output = ExcelReader_NPOI.ReadAllXLSX("AddressList.xlsx", columns, null, true);
            for (int i = 0; i < output.GetLength(0); i++)
            {
                string postCode = output[i, 0];
                totalPostCodes.Add(postCode);
            }
        }

        public static void AddCustomerToCount(Address address)
        {
            XSSFWorkbook hssfwb = ExcelReader_NPOI.OpenXLSX("Customers.xlsx", false);
            ISheet sheet = hssfwb.GetSheet("Sheet1");

            int numberOfRows = sheet.LastRowNum;
            for (int i = 0; i <= sheet.LastRowNum; i++)
            {
                if (sheet.GetRow(i) != null)
                {
                    if (sheet.GetRow(i).GetCell(0).StringCellValue == address.Number && sheet.GetRow(i).GetCell(1).StringCellValue == address.Road)
                    {
                        int count = (int)sheet.GetRow(i).GetCell(2).NumericCellValue;
                        sheet.GetRow(i).GetCell(2).SetCellValue(count + 1);
                        break;
                    }
                }

                if (i == sheet.LastRowNum)
                {
                    sheet.CreateRow(i + 1); //Creating Row Overwrites whole row
                    sheet.GetRow(i + 1).CreateCell(0).SetCellValue(address.Number);
                    sheet.GetRow(i + 1).CreateCell(1).SetCellValue(address.Road);
                    sheet.GetRow(i + 1).CreateCell(2).SetCellValue(0);
                }
            }
            ExcelReader_NPOI.WriteXLSX(hssfwb, "Customers.xlsx");
        }

        public static void LoadTotalCustomers()
        {
            DataTable tempCustomers = new DataTable();
            tempCustomers = new DataTable();
            tempCustomers.Columns.Add("Number", typeof(int));
            tempCustomers.Columns.Add("Address", typeof(Address));
            tempCustomers.Columns.Add("Count", typeof(int));

            int[] columnsString = new int[2] { 0, 1 };
            int[] columnNumber = new int[1] { 2 };
            string[,] output = ExcelReader_NPOI.ReadAllXLSX("Customers.xlsx", columnsString, columnNumber, false);
            for (int i = 0; i < output.GetLength(0); i++)
            {
                if (output[i, 0] != null)
                {
                    float houseNumber = float.Parse(output[i, 0]);
                    float totalBought = float.Parse(output[i, 2]);
                    Address address = null;
                    for (int j = 0; j < totalAddresses.Count; j++)
                    {
                        if (totalAddresses[j].Road == output[i, 1])
                        {
                            address = totalAddresses[j];
                            break;
                        }
                    }
                    tempCustomers.Rows.Add(houseNumber, address, totalBought);
                }
            }
            totalCustomers = tempCustomers;
        }

        #region Properties

        public string Number
        {
            set
            {
                number = value;
            }
            get
            {
                return number;
            }
        }

        public string PostCode
        {
            get
            {
                return postCode;
            }

            set
            {
                postCode = value;
            }
        }

        public string Road
        {
            get
            {
                return road;
            }
        }

        public string Town
        {
            get
            {
                return town;
            }
        }

        public double Latitude
        {
            get
            {
                return latitude;
            }
        }

        public double Longitude
        {
            get
            {
                return longitude;
            }
        }

        public static List<Address> TotalAddresses
        {
            get
            {
                return totalAddresses;
            }
        }

        public static List<string> TotalPostCodes
        {
            get
            {
                return totalPostCodes;
            }
        }

        public static DataTable TotalCustomers
        {
            get
            {
                return totalCustomers;
            }
        }
        #endregion
    }
}
