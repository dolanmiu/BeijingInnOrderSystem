using System;
using System.Collections.Generic;
using Microsoft.PointOfService;
using Beijing_Inn_Order_System.Items;
using System.Drawing;
using Beijing_Inn_Order_System.Printing.TextDecoration;
using System.Reflection;

namespace Beijing_Inn_Order_System.Printing
{
    class PrintReceipt
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
            catch (FieldAccessException)
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
            m_Printer.Release();
            m_Printer.Close();
        }

        private void PrintHeader()
        {

        }

        public static void Print(Basket basket)
        {
            //m_Printer.PrintBitmap(PrinterStation.Receipt, "Assets/logo.bmp", PosPrinter.PrinterBitmapAsIs, PosPrinter.PrinterBitmapCenter);
            PrintCustomerVersion(basket);
            m_Printer.CutPaper(98);
            PrintKitchenVersion(basket);
            m_Printer.CutPaper(98);
        }

        private static void PrintCustomerVersion(Basket basket)
        {
            try
            {
                m_Printer.PrintMemoryBitmap(PrinterStation.Receipt, logo, PosPrinter.PrinterBitmapAsIs, PosPrinter.PrinterBitmapCenter);
                TextComponent addressLine = new TextCentreAlign(new TextBase("3 King Street, Gillingham, ME7 1EY"));
                TextComponent phoneNumber = new TextCentreAlign(new TextBase("01634 570 633"));
                PrintEnglish(addressLine.GetDec());
                PrintEnglish(phoneNumber.GetDec());

                foreach (Item item in basket.Items)
                {
                    TextComponent englishText = new TextBase(item.EnglishName);
                    TextComponent priceText = new TextRightAlign((new TextBase("£" + item.Price.ToString("0.00"))));
                    TextComponent chineseText = new TextBase(item.ChineseName);

                    PrintEnglish(englishText.GetDec() + priceText.GetDec());
                    PrintChinese(item.ChineseName);
                }

                float price = basket.CalculatePrice();
                TextComponent total = new TextRightAlign(new TextBold(new TextUnderline(new TextBase("  Total: £" + price.ToString("0.00")))));
                PrintEnglish(total.GetDec());
                PrintEnglish(" ");
                PrintEnglish(" ");
                PrintEnglish(" ");
            }
            catch (PosControlException e)
            {
                Console.WriteLine("Cover open?");
                throw e;
            }
        }

        private static void PrintKitchenVersion(Basket basket)
        {
            try
            {
                TextComponent forKitchen = new TextBold(new TextCentreAlign(new TextBase("FOR KITCHEN")));
                PrintEnglish(forKitchen.GetDec());
                foreach (Item item in basket.Items)
                {
                    TextComponent englishText = new TextBase(item.EnglishName);
                    TextComponent chineseText = new TextBase(item.ChineseName);

                    PrintEnglish(englishText.GetDec());
                    PrintChinese(item.ChineseName);
                }
                PrintEnglish(" ");
                PrintEnglish(" ");
                PrintEnglish(" ");
            }
            catch (PosControlException)
            {

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
    }
}
