using System;
using System.Collections.Generic;
using Microsoft.PointOfService;
using Beijing_Inn_Order_System.Items;
using System.Drawing;
using Beijing_Inn_Order_System.Printing.TextDecoration;
using System.Reflection;
using Beijing_Inn_Order_System.Customer;

namespace Beijing_Inn_Order_System.Printing
{
    class ReceiptPrinter
    {
        private static PosPrinter m_Printer = null;
        private enum Alignment { Left, Centre, Right };
        private static Bitmap logo;

        public static void LoadPrinter()
        {
            string strLogicalName = "PosPrinter";
            try
            {
                PosExplorer posExplorer = new PosExplorer();

                DeviceInfo deviceInfo = null;
                DeviceCollection myDevices = posExplorer.GetDevices(DeviceType.PosPrinter);
                try
                {
                    deviceInfo = posExplorer.GetDevice(DeviceType.PosPrinter, strLogicalName);
                    m_Printer = (PosPrinter)posExplorer.CreateInstance(deviceInfo);
                }
                catch (Exception)
                {
                }

                m_Printer.Open();
                m_Printer.Claim(5000);
                m_Printer.DeviceEnabled = true;
            }
            catch (PosControlException)
            {
                Console.WriteLine("Failed to load printer: " + strLogicalName);
            }

            //var a = Assembly.GetExecutingAssembly(); // Or another Get method if you need to get it from some other assembly
            //var image = Bitmap.FromStream(a.GetManifestResourceStream("DefaultNameSpace.Assets.logo.bmp"));
            logo = Beijing_Inn_Order_System.Properties.Resources.logo;
            //Bitmap bmp = new Bitmap(System.Reflection.Assembly.GetEntryAssembly().GetManifestResourceStream("MyProject.Resources.myimage.png"));
        }

        public static void UnloadPrinter()
        {
            try
            {
                m_Printer.Release();
                m_Printer.Close();
            }
            catch (PosControlException)
            {

            }
        }

        private void PrintHeader()
        {

        }

        public static void Print(Basket basket, Address address)
        {
            try
            {
                PrintCustomerVersion(basket, address);
                m_Printer.CutPaper(98);
                PrintKitchenVersion(basket);
                m_Printer.CutPaper(98);
            }
            catch (PosControlException)
            {

            }
        }

        private static void PrintCustomerVersion(Basket basket, Address address)
        {
            List<Tuple<Item, int>> concattedItems = basket.ConcatItems;
            try
            {
                //m_Printer.PrintMemoryBitmap(PrinterStation.Receipt, logo, PosPrinter.PrinterBitmapAsIs, PosPrinter.PrinterBitmapCenter);
                //TextComponent addressLine = new TextCentreAlign(new TextBase("3 King Street, Gillingham, ME7 1EY"));
                //TextComponent phoneNumber = new TextCentreAlign(new TextBase("01634 570 633"));
                //PrintEnglish(addressLine.GetDec());
                //PrintEnglish(phoneNumber.GetDec());
                //PrintEnglish(" ");

                TextComponent forCustomer = new TextBold(new TextCentreAlign(new TextBase("FOR RECORDS")));
                PrintEnglish(forCustomer.GetDec());
                foreach (Tuple<Item, int> item in concattedItems)
                {
                    string englishSizeModifier = "";
                    string chineseSizeModifier = "";
                    if (item.Item1.IsSizeDish)
                    {
                        if (item.Item1.IsLarge)
                        {
                            englishSizeModifier = "(Large)";
                            chineseSizeModifier = "(大)";
                        }
                        else
                        {
                            englishSizeModifier = "(Small)";
                            chineseSizeModifier = "(小)";
                        }
                    }
                    TextComponent englishText = new TextBase(item.Item1.EnglishName);
                    TextComponent priceText = new TextRightAlign(new TextBase("£" + (item.Item1.Price * item.Item2).ToString("0.00")));
                    TextComponent quantityText = new TextRightAlign(new TextBase("X" + item.Item2.ToString() + "    "));
                    TextComponent chineseText = new TextBase(item.Item1.ChineseName);

                    PrintEnglish(englishText.GetDec() + englishSizeModifier);
                    PrintChinese(item.Item1.ChineseName + chineseSizeModifier);
                    PrintEnglish(quantityText.GetDec() + priceText.GetDec());
                }

                float price = basket.CalculatePrice();
                TextComponent total = new TextRightAlign(new TextBold(new TextUnderline(new TextBase("  Total: £" + price.ToString("0.00")))));
                PrintEnglish(total.GetDec());
                if (address != null)
                {
                    PrintEnglish("Deliver to:" + address.Concat);
                }
                else
                {
                    PrintEnglish("Pick Up Order");
                }
                PrintEnglish(" ");
                PrintEnglish(" ");
                PrintEnglish(" ");
                PrintEnglish(" ");
                PrintEnglish(" ");
                PrintEnglish(" ");

            }
            catch (PosControlException)
            {
                Console.WriteLine("Cover open?");
                //throw e;
            }
        }

        private static void PrintKitchenVersion(Basket basket)
        {
            List<Tuple<Item, int>> concattedItems = basket.ConcatItems;
            try
            {
                TextComponent forKitchen = new TextBold(new TextCentreAlign(new TextBase("FOR KITCHEN")));
                PrintEnglish(forKitchen.GetDec());
                foreach (Tuple<Item, int> item in concattedItems)
                {
                    string englishSizeModifier = "";
                    string chineseSizeModifier = "";
                    if (item.Item1.IsSizeDish)
                    {
                        if (item.Item1.IsLarge)
                        {
                            englishSizeModifier = "(Large)";
                            chineseSizeModifier = "(大)";
                        }
                        else
                        {
                            englishSizeModifier = "(Small)";
                            chineseSizeModifier = "(小)";
                        }
                    }
                    TextComponent englishText = new TextBase(item.Item1.EnglishName);
                    TextComponent chineseText = new TextBase(item.Item1.ChineseName);
                    TextComponent specialText = new TextBase(item.Item1.ConcatProperties);
                    TextComponent quantityText = new TextRightAlign(new TextBold(new TextBase("X" + item.Item2.ToString())));

                    PrintEnglish(englishText.GetDec() + englishSizeModifier + quantityText.GetDec());
                    PrintChinese(item.Item1.ChineseName + chineseSizeModifier);
                    PrintEnglish(specialText.GetDec());
                }
                PrintEnglish(" ");
                PrintEnglish(" ");
                PrintEnglish(" ");
                PrintEnglish(" ");
                PrintEnglish(" ");
            }
            catch (PosControlException)
            {
                Console.WriteLine("Cover open?");
            }
        }

        private static void PrintEnglish(string str)
        {
            string text = str.Replace("ESC", ((char)27).ToString()) + "\x1B|1lF";
            m_Printer.PrintNormal(PrinterStation.Receipt, text);
        }

        private static void PrintChinese(string str)
        {
            if (str.Length > 13)
            {
                str = str.Substring(0, 13);
            }
            float oneChar = 37.5f;
            int printWidth = (int)oneChar * str.Length;

            Bitmap thing = TextBitmap.Convert(str, 40, 300, 20);
            //m_Printer.PrintMemoryBitmap(PrinterStation.Receipt, thing, PosPrinter.PrinterBitmapAsIs, PosPrinter.PrinterBarCodeLeft);
            m_Printer.PrintBitmap(PrinterStation.Receipt, "file.bmp", PosPrinter.PrinterBitmapAsIs, PosPrinter.PrinterBitmapLeft); //530 limit
        }

        #region Properties
        public static string Health
        {
            get
            {
                try
                {
                    return m_Printer.CheckHealth(HealthCheckLevel.Internal);
                }
                catch (PosControlException e)
                {
                    return e.;
                }
                return "Unhealthy";
            }
        }

        public static string PowerState
        {
            get
            {
                switch(m_Printer.PowerState) {
                    case Microsoft.PointOfService.PowerState.Off:
                        return "Off";
                    case Microsoft.PointOfService.PowerState.Offline:
                        return "Offline";
                    case Microsoft.PointOfService.PowerState.OffOffline:
                        return "OffOffline";
                    case Microsoft.PointOfService.PowerState.Online:
                        return "Online";
                    case Microsoft.PointOfService.PowerState.Unknown:
                        return "Unknown";
                }
                return null;
            }
        }

        public static bool? CoverOpen
        {
            get
            {
                try
                {
                    return m_Printer.CoverOpen;
                }
                catch (PosControlException)
                {
                    return null;
                }
            }
        }
        #endregion
    }
}
