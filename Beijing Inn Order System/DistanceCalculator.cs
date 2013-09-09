using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beijing_Inn_Order_System
{
    public static class DistanceCalculator
    {
        private static double[] beijingInnCoords = { 51.387155, 0.548719 };
        //double distance = getDistanceFromLatLonInKm(51.369479, 0.518109, beijingInnCoords[0], beijingInnCoords[1]);

        public static double getDistanceFromLatLonInKm(double lat1, double lon1, double lat2, double lon2)
        {
            double R = 6378.16; // Radius of the earth in km
            double dLat = deg2rad(lat2 - lat1);  // deg2rad below
            double dLon = deg2rad(lon2 - lon1);
            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) + Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) * Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            double d = R * c; // Distance in km
            return RoundToSignificantDigits(d, 3);
        }

        static double RoundToSignificantDigits(double d, int digits)
        {
            if (d == 0)
                return 0;

            double scale = Math.Pow(10, Math.Floor(Math.Log10(Math.Abs(d))) + 1);
            return scale * Math.Round(d / scale, digits);
        }

        private static double deg2rad(double deg)
        {
            return deg * (Math.PI / 180);
        }

        public static double[] BeijingInnCoords
        {
            get
            {
                return beijingInnCoords;
            }
        }

    }
}
