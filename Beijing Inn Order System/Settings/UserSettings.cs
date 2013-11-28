using Beijing_Inn_Order_System.Helper_Classes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;

namespace Beijing_Inn_Order_System.Settings
{
    public static class UserSettings
    {
        private static float deliveryCharge = 0;
        private static float deliveryChargeThreshold = 0;
        private static float deliveryRadiusCharge = 0;
        private static float deliveryRadiusThreshold = 0;
        private static string organisationName = "Untitled";

        public static void WriteSettingsFile()
        {
            XDocument doc = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                new XElement("Settings",
                    new XElement("Organisation", organisationName),
                    new XElement("Delivery",
                        new XElement("Amount", deliveryCharge),
                        new XElement("Threshold", deliveryChargeThreshold),
                        new XElement("RadiusAmmount", deliveryRadiusCharge),
                        new XElement("Radius", deliveryRadiusThreshold)),
                    new XElement("PrinterName", "PosPrinter")));

            string fileLocation = Helper.GetAppDataFile("settings.xml");
            doc.Save(fileLocation);
        }

        public static void ReadSettingsFile()
        {
            string fileLocation = Helper.GetAppDataFile("settings.xml");

            XDocument xDoc = new XDocument();
            try
            {
                xDoc = XDocument.Load(fileLocation);
            }
            catch (Exception)
            {
                WriteSettingsFile();
            }

            IEnumerable<XElement> settings = from row in xDoc.Descendants("Settings") select row;

            foreach (XElement setting in settings)
            {
                IEnumerable<XElement> organisationNameElement = from att in setting.Descendants("Organisation") select att;
                foreach (XElement organisation in organisationNameElement)
                {
                    organisationName = organisation.Value;
                }

                IEnumerable<XElement> deliveryHeadElement = from att in setting.Descendants("Delivery") select att;
                foreach (XElement deliveryElement in deliveryHeadElement)
                {
                    IEnumerable<XElement> amountElements = from att in deliveryElement.Descendants("Amount") select att;
                    foreach (XElement amount in amountElements)
                    {
                        Debug.Print(amount.Name + " " + amount.Value);
                        float.TryParse(amount.Value, out deliveryCharge);
                    }

                    IEnumerable<XElement> threshholdElement = from att in deliveryElement.Descendants("Threshold") select att;
                    foreach (XElement threshold in threshholdElement)
                    {
                        Debug.Print(threshold.Name + " " + threshold.Value);
                        float.TryParse(threshold.Value, out deliveryChargeThreshold);
                    }

                    IEnumerable<XElement> radiusChargeElement = from att in deliveryElement.Descendants("RadiusAmmount") select att;
                    foreach (XElement radiusCharge in radiusChargeElement)
                    {
                        Debug.Print(radiusCharge.Name + " " + radiusCharge.Value);
                        float.TryParse(radiusCharge.Value, out deliveryRadiusCharge);
                    }

                    IEnumerable<XElement> radiusThreshholdElement = from att in deliveryElement.Descendants("Radius") select att;
                    foreach (XElement threshold in radiusThreshholdElement)
                    {
                        Debug.Print(threshold.Name + " " + threshold.Value);
                        float.TryParse(threshold.Value, out deliveryRadiusThreshold);
                    }
                }
            }
        }

        #region Properties
        public static float DeliveryCharge
        {
            get
            {
                return deliveryCharge;
            }
            set
            {
                deliveryCharge = value;
            }
        }

        public static float DeliveryChargeThreshold
        {
            get
            {
                return deliveryChargeThreshold;
            }
            set
            {
                deliveryChargeThreshold = value;
            }
        }

        public static float DeliveryRadiusCharge
        {
            get
            {
                return deliveryRadiusCharge;
            }
            set
            {
                deliveryRadiusCharge = value;
            }
        }

        public static float DeliveryRadiusThreshold
        {
            get
            {
                return deliveryRadiusThreshold;
            }
            set
            {
                deliveryRadiusThreshold = value;
            }
        }

        public static string OrganisationName
        {
            get
            {
                return organisationName;
            }
            set
            {
                organisationName = value;
            }
        }
        #endregion
    }
}
