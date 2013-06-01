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
            IGeoCoder geoCoder = new GoogleGeoCoder();
            //Address[] addresses = geoCoder.ReverseGeocode(51.369479, 0.518109).ToArray();
            ReadXLSX();
            double[] beijingInnCoords = { 51.387155 , 0.548719 };
            /*double xDistance = beijingInnCoords[0] - 51.516853;
            double yDistance = beijingInnCoords[1] - (-0.188693);
            double pythagorasDistance = Math.Sqrt(Math.Pow(xDistance, 2) + Math.Pow(yDistance, 2));
            Distance distance = new Distance(pythagorasDistance, DistanceUnits.Miles);*/
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

        public void ReadXLSX()
        {
            XSSFWorkbook hssfwb;
            string fileLocation = Directory.GetCurrentDirectory() + "\\MedwayPostcodes.xlsx";
            using (FileStream file = new FileStream(@fileLocation, FileMode.Open, FileAccess.Read))
            {
                hssfwb = new XSSFWorkbook(file);
            }

            ISheet sheet = hssfwb.GetSheet("Sheet1");
            for (int row = 0; row <= sheet.LastRowNum; row++)
            {
                if (sheet.GetRow(row) != null) //null is when the row only contains empty cells 
                {
                    //MessageBox.Show(string.Format("Row {0} = {1}", row, sheet.GetRow(row).GetCell(0).StringCellValue));
                    String englishName = sheet.GetRow(row).GetCell(0).StringCellValue;
                    String chineseName = sheet.GetRow(row).GetCell(1).StringCellValue;
                    float price = (float)sheet.GetRow(row).GetCell(2).NumericCellValue;
                    //CreateItem(englishName, chineseName, price);
                }
            }
        }
    }
}
