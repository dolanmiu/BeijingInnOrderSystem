using Beijing_Inn_Order_System.Items;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Beijing_Inn_Order_System.Customer
{
    public class Address
    {
        private static List<Address> totalAddresses = new List<Address>();
        private static List<string> totalPostCodes = new List<string>();
        private static DataTable totalCustomers;
        private static List<string> towns = new List<string>(new string[] { "Gillingham", "Chatham", "Rochester", "Lordswood", "Walderslade" });
        private static Dictionary<string, Address> postcodeDictionary;
        private String number;
        private String postCode;
        private String road;
        private String town;
        private double latitude;
        private double longitude;

        public Address()
        {

        }

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

        public static void LoadForeignAddresses()
        {
            int[] columns = new int[3] { 0, 1, 2, };
            int[] latLong = new int[2] { 3, 4 };
            string[,] output = ExcelReader_NPOI.ReadAllXLSX("ForeignAddressList.xlsx", columns, latLong, false);
            for (int i = 1; i < output.GetLength(0); i++)
            {
                Address address = new Address(output[i, 0], output[i, 1], output[i, 2], double.Parse(output[i, 3]), double.Parse(output[i, 4]));
                totalAddresses.Add(address);
            }
        }

        public static void AddForeignAddress(Address address)
        {
            XSSFWorkbook hssfwb = ExcelReader_NPOI.OpenXLSX("ForeignAddressList.xlsx", false);
            ISheet sheet = hssfwb.GetSheet("Sheet1");

            int numberOfRows = sheet.LastRowNum;
            for (int i = 0; i <= sheet.LastRowNum; i++)
            {
                if (sheet.GetRow(i) != null)
                {
                    if (sheet.GetRow(i).GetCell(0).StringCellValue == address.PostCode)
                    {
                        break;
                    }
                }

                if (i == sheet.LastRowNum)
                {
                    sheet.CreateRow(i + 1); //Creating Row Overwrites whole row
                    sheet.GetRow(i + 1).CreateCell(0).SetCellValue(address.PostCode);
                    sheet.GetRow(i + 1).CreateCell(1).SetCellValue(address.Road);
                    sheet.GetRow(i + 1).CreateCell(2).SetCellValue(address.Town);
                    sheet.GetRow(i + 1).CreateCell(3).SetCellValue(address.Latitude);
                    sheet.GetRow(i + 1).CreateCell(4).SetCellValue(address.Longitude);
                }
            }
            ExcelReader_NPOI.WriteXLSX(hssfwb, "ForeignAddressList.xlsx");
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
            postcodeDictionary = GeneratePostCodeDictionary(totalAddresses);
        }

        /*public static void LoadAllPostCodes() //Depreciated
        {
            int[] columns = new int[1] { 0 };
            string[,] output = ExcelReader_NPOI.ReadAllXLSX("AddressList.xlsx", columns, null, true);
            for (int i = 0; i < output.GetLength(0); i++)
            {
                string postCode = output[i, 0];
                totalPostCodes.Add(postCode);
            }
        }*/

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

        private static Dictionary<string, Address> GeneratePostCodeDictionary(List<Address> addresses)
        {
            /*Dictionary<string, Address> dictionary = new Dictionary<string, Address>();
            foreach (Address address in addresses)
            {
                if (!dictionary.Keys.Contains(address.PostCode))
                {
                    dictionary.Add(address.PostCode, address);
                }
                else
                {
                    Console.WriteLine(address.PostCode);
                }
            }*/
            Dictionary<string, Address> dictionary = addresses.ToDictionary(x => x.PostCode, x => x);
            return dictionary;
        }

        public static Address SearchWithPostCode(string postcode)
        {
            postcode = postcode.ToUpper();
            Match m = Regex.Match(postcode, "(ME[0-9][0-9][A-z][A-z])");
            if (m.Success)
            {
                postcode = postcode.Insert(2, " ");
            }
            Address address = new Address();
            if (postcodeDictionary.TryGetValue(postcode, out address))
            {
                address = postcodeDictionary[postcode];
            }
            return address;
            /*IEnumerable<KeyValuePair<string, Address>> res = postcodeDictionary.Where(r => r.Key.StartsWith(postcode));
            Dictionary<string, Address> dic = res.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            return dic.Values.ToList();*/
        }

        public static List<Address> SearchWithRoad(string roadText)
        {
            /*List<Address> addresses = new List<Address>();
            for (int i = 0; i < Address.TotalAddresses.Count; i++)
            {
                if (Address.TotalAddresses[i].Road != null)
                {
                    string road = Address.TotalAddresses[i].Road.ToUpper();
                    if (road.Contains(roadText.ToUpper()))
                    {
                        addresses.Add(Address.TotalAddresses[i]);
                    }
                }
            }
            return addresses;*/
            string postCodeTitleCase = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(roadText.ToLower());
            IEnumerable<Address> addresses = totalAddresses.Where<Address>(r => r.Road.StartsWith(postCodeTitleCase));
            return addresses.ToList<Address>();
            /*IEnumerable<KeyValuePair<string, Address>> res = roadDictionary.Where(r => r.Key.StartsWith(postCodeTitleCase));
            Dictionary<string, Address> dic = res.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            return dic.Values.ToList();*/
        }

        public static List<Address> Search(string query)
        {
            List<Address> addresses = new List<Address>();
            Address postCodeAddress = new Address();
            List<Address> roadAddresses = new List<Address>();
            Match m = Regex.Match(query.ToUpper(), "(ME[0-9][ ]?[0-9][A-z][A-z])");
            string postCode = "";
            if (m.Success)
            {
                postCode = m.Groups[1].Value;
                query = query.Replace(postCode, "");
            }

            string searchTerm = query.Trim();
            if (postCode != "")
            {
                postCodeAddress = SearchWithPostCode(postCode);
            }
            else
            {
                roadAddresses = SearchWithRoad(searchTerm);
            }

            if (postCodeAddress.PostCode != null)
            {
                addresses.Add(postCodeAddress);
            }
            else
            {
                addresses.AddRange(roadAddresses);
            }
            return addresses;
        }

        #region Properties
        public string Concat
        {
            get
            {
                return road + ", " + postCode + ", " + town;
            }
        }

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
            set
            {
                road = value;
            }
        }

        public string Town
        {
            get
            {
                return town;
            }
            set
            {
                town = value;
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

        public static List<string> Towns
        {
            get
            {
                return towns;
            }
        }
        #endregion
    }
}
