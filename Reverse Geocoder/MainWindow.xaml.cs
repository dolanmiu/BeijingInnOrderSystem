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
            double[] beijingInnCoords = { 51.387155 , 0.548719 };
            
            double distance = getDistanceFromLatLonInKm(51.369479, 0.518109, beijingInnCoords[0], beijingInnCoords[1]);
        }

        private double getDistanceFromLatLonInKm(double lat1, double lon1, double lat2, double lon2) {
            double R = 6378.16; // Radius of the earth in km
            double dLat = deg2rad(lat2 - lat1);  // deg2rad below
            double dLon = deg2rad(lon2 - lon1);
            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) + Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) * Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            double d = R * c; // Distance in km
            return d;
        }

        private double deg2rad(double deg) {
            return deg * (Math.PI/180);
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

        public void DecypherPostcode(XSSFWorkbook medwayPostcodes, XSSFWorkbook outputXLSX)
        {
            ISheet medwayPostcodeSheet = medwayPostcodes.GetSheet("Sheet1");
            ISheet outputSheet = outputXLSX.GetSheet("Sheet1");
            //for (int row = 1; row <= medwayPostcodeSheet.LastRowNum; row++)
            for (int row = 202; row <= 3000; row++)
            {
                if (medwayPostcodeSheet.GetRow(row) != null) //null is when the row only contains empty cells 
                {
                    String postCode = medwayPostcodeSheet.GetRow(row).GetCell(0).StringCellValue;
                    double longPos = medwayPostcodeSheet.GetRow(row).GetCell(13).NumericCellValue;
                    double latPos = medwayPostcodeSheet.GetRow(row).GetCell(14).NumericCellValue;
                    System.Threading.Thread.Sleep(1000);
                    AddStreet(outputSheet, row, postCode, latPos, longPos);
                }
                if (row % 100 == 0)
                {
                    WriteXLSX(outputXLSX, "AddressList.xlsx");
                }
            }
            WriteXLSX(outputXLSX, "AddressList.xlsx");
        }

        public void AddStreet(ISheet sheet, int row, string postCode, double latPos, double longPos)
        {
            IGeoCoder geoCoder = new GoogleGeoCoder();
            Location latLang = geoCoder.GeoCode(postCode).ToArray()[0].Coordinates;
            Address[] addresses = geoCoder.ReverseGeocode(latLang.Latitude, latLang.Longitude).ToArray();
            string streetNameNumber = addresses[0].FormattedAddress.Split(',')[0];
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
            Console.WriteLine(postCode + " " + streetName + " " + town);
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

        public XSSFWorkbook EditXLSX(XSSFWorkbook workbook)
        {
            ISheet sheet1 = workbook.GetSheet("Sheet1");
            sheet1.CreateRow(0).CreateCell(0).SetCellValue("This is a Sample");
            return workbook;
        }

        public void WriteXLSX(XSSFWorkbook workbook, string filename)
        {
            string fileLocation = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\" + filename;
            FileStream file = new FileStream(@fileLocation, FileMode.Create);
            workbook.Write(file);
            file.Close();
        }
    }
}
