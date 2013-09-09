using System;
using System.Collections.Generic;
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
using GeoCoding;
using GeoCoding.Google;
using GeoCoding.Yahoo;
using GeoCoding.Microsoft;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;

namespace Reverse_Geocoder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            //IGeoCoder geoCoder = new GoogleGeoCoder();
            //Address[] addresses = geoCoder.ReverseGeocode(51.369479, 0.518109).ToArray();
            //Location latLang = geoCoder.GeoCode("ME1 2DQ").ToArray()[0].Coordinates;
            //Address[] addresses = geoCoder.ReverseGeocode(latLang.Latitude, latLang.Longitude).ToArray();

            XSSFWorkbook workbook = LoadXLSX("AddressList.xlsx");
            XSSFWorkbook postcodeWorkbook = ReadPostCodeXLSX();
            DecypherPostcode(postcodeWorkbook, workbook);
            //workbook = EditXLSX(workbook);
            //WriteXLSX(workbook, "AddressList.xlsx");
        }

        public XSSFWorkbook ReadPostCodeXLSX()
        {
            XSSFWorkbook hssfwb;
            string fileLocation = Directory.GetCurrentDirectory() + "\\MedwayPostcodes.xlsx";
            using (FileStream file = new FileStream(@fileLocation, FileMode.Open, FileAccess.Read))
            {
                hssfwb = new XSSFWorkbook(file);
            }

            return hssfwb;
        }

        public XSSFWorkbook LoadXLSX(string filename)
        {
            XSSFWorkbook hssfwb;
            string fileLocation = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\" + filename;
            using (FileStream file = new FileStream(@fileLocation, FileMode.Open, FileAccess.Read))
            {
                hssfwb = new XSSFWorkbook(file);
            }

            return hssfwb;
        }

        public void DecypherPostcode(XSSFWorkbook medwayPostcodes, XSSFWorkbook outputXLSX)
        {
            ISheet medwayPostcodeSheet = medwayPostcodes.GetSheet("Sheet1");
            ISheet outputSheet = outputXLSX.GetSheet("Sheet1");
            //for (int row = 1; row <= medwayPostcodeSheet.LastRowNum; row++)
            int count = 0;
            for (int row = 0; row <= 1025; row++)
            {
                if (medwayPostcodeSheet.GetRow(row) != null) //null is when the row only contains empty cells 
                {
                    string postCode = medwayPostcodeSheet.GetRow(row).GetCell(0).StringCellValue;
                    //if (outputSheet.GetRow(row) == null)
                    //{
                        System.Threading.Thread.Sleep(1000);
                        AddStreet(outputSheet, row, postCode);
                        count++;
                    //}
                }
                if (count % 3 == 0)
                {
                    //System.Threading.Thread.Sleep(4000);
                    WriteXLSX(outputXLSX, "AddressList.xlsx");
                }
            }
            System.Threading.Thread.Sleep(4000);
            WriteXLSX(outputXLSX, "AddressList.xlsx");
        }

        public void AddStreet(ISheet sheet, int row, string postCode)
        {
            //IGeoCoder geoCoder = new BingMapsGeoCoder("AoOO7N-PyP6n3dBlaVYM8_tKhG2Hslm9yL088fTftMQkV9H1LmQnMysWTedYqiSK");
            //IGeoCoder geoCoder = new GoogleGeoCoder("AIzaSyD4qJe0Memxi9X6YjK5kHutrfiAtSm41rM");
            IGeoCoder geoCoder = new GoogleGeoCoder();
            string streetNameNumber;
            Address[] addresses;
            while (true)
            {
                try
                {
                    Location latLang = geoCoder.GeoCode(postCode).ToArray()[0].Coordinates;
                    addresses = geoCoder.ReverseGeocode(latLang.Latitude, latLang.Longitude).ToArray();
                    streetNameNumber = addresses[0].FormattedAddress.Split(',')[0];
                    break;
                }
                catch (Exception)
                {
                    
                }
            }

            bool hasHouseNumber = System.Text.RegularExpressions.Regex.IsMatch(streetNameNumber, @"\d");
            int count = 0;
            if (hasHouseNumber)
            {
                count = 1;
            }

            string[] streetNameNumberArray = addresses[0].FormattedAddress.Split(',')[0].Split(' ');
            //string town = addresses[0].FormattedAddress.Split(',')[1].Split(' ')[0];
            string town = addresses[0].FormattedAddress.Split(',')[1].Trim();
            string streetName = "";

            while (count < streetNameNumberArray.Length)
            {
                streetName += streetNameNumberArray[count];
                if (count != streetNameNumberArray.Length - 1)
                {
                    streetName += " ";
                }
                count++;
            }
            sheet.CreateRow(row);
            sheet.GetRow(row).CreateCell(0).SetCellValue(postCode);
            sheet.GetRow(row).CreateCell(1).SetCellValue(streetName);
            sheet.GetRow(row).CreateCell(2).SetCellValue(town);
            sheet.GetRow(row).CreateCell(3).SetCellValue(addresses[0].Coordinates.Latitude);
            sheet.GetRow(row).CreateCell(4).SetCellValue(addresses[0].Coordinates.Longitude);
            Console.WriteLine(postCode + " " + streetName + " " + town + addresses[0].Coordinates.Latitude + addresses[0].Coordinates.Longitude);
        }

        public XSSFWorkbook EditXLSX(XSSFWorkbook workbook)
        {
            ISheet sheet1 = workbook.GetSheet("Sheet1");
            sheet1.CreateRow(0).CreateCell(0).SetCellValue("This is a Sample");
            return workbook;
        }

        public void WriteXLSX(XSSFWorkbook workbook, string filename)
        {
            string fileLocation = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\" + filename;
            FileStream file;
            try
            {
                file = new FileStream(@fileLocation, FileMode.Create);
                workbook.Write(file);
                file.Close();
            }
            catch (IOException)
            {

            }
        }
    }
}
