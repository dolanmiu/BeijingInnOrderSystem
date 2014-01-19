using System;
using System.Collections.Generic;
using Microsoft.PointOfService;
using Beijing_Inn_Order_System.Items;
using System.Drawing;
using Beijing_Inn_Order_System.Printing.TextDecoration;
using Beijing_Inn_Order_System.Customer;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Threading;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Beijing_Inn_Order_System.Helper_Classes;

namespace Beijing_Inn_Order_System.Printing
{
    public class ReceiptPrinter : INotifyPropertyChanged
    {
        private PosPrinter m_Printer = null;
        private DeviceCollection myDevices = null;
        private enum Alignment { Left, Centre, Right };
        private Bitmap logo;
        private string statusString;

        public delegate void AsyncLoadPrinterCaller();

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public ReceiptPrinter()
        {
            LoadStatusCheckTicker();
        }

        public void LoadPrinter()
        {
            SetUpPrinter();
            ClaimPrinter();
            //var a = Assembly.GetExecutingAssembly(); // Or another Get method if you need to get it from some other assembly
            //var image = Bitmap.FromStream(a.GetManifestResourceStream("DefaultNameSpace.Assets.logo.bmp"));
            //logo = Beijing_Inn_Order_System.Properties.Resources.logo;
            //Bitmap bmp = new Bitmap(System.Reflection.Assembly.GetEntryAssembly().GetManifestResourceStream("MyProject.Resources.myimage.png"));
        }

        private void ClaimPrinter()
        {
            try
            {
                if (m_Printer == null) throw new PosControlException("No Printer", ErrorCode.NoExist);
                m_Printer.Claim(1000);
                m_Printer.DeviceEnabled = true;
            }
            catch (PosControlException)
            {
                Console.WriteLine("Failed to claim printer. Is it connected?");
            }
        }

        private void SetUpPrinter()
        {
            string strLogicalName = "PosPrinter";
            try
            {
                PosExplorer posExplorer = new PosExplorer();

                DeviceInfo deviceInfo = null;
                myDevices = posExplorer.GetDevices(DeviceType.PosPrinter);
                try
                {
                    deviceInfo = posExplorer.GetDevice(DeviceType.PosPrinter, strLogicalName);
                    m_Printer = (PosPrinter)posExplorer.CreateInstance(deviceInfo);
                }
                catch (Exception)
                {
                }

                m_Printer.Open();
            }
            catch (PosControlException)
            {
                Console.WriteLine("Failed to load printer: " + strLogicalName);
            }
        }

        private void LoadStatusCheckTicker()
        {
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(UpdatePrinterStatusTick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 5);
            dispatcherTimer.Start();
        }

        private void UpdatePrinterStatusTick(object sender, EventArgs e)
        {
            statusString = "Health: " + Health + "\nEnabled: " + DeviceEnabled + "\nPower: " + PowerState + "\nCover: ";
            if (CoverOpen == true)
            {
                statusString += "Open";
            }
            if (CoverOpen == false)
            {
                statusString += "Closed";
            }
            if (CoverOpen == null)
            {
                statusString += "Unknown";
            }

            if (!DeviceEnabled)
            {
                AsyncLoadPrinter();
            }
            NotifyPropertyChanged("StatusText");
        }

        public void AsyncLoadPrinter()
        {
            if (m_Printer == null) return;
            //AsyncLoadPrinterCaller caller = new AsyncLoadPrinterCaller(LoadPrinter);
            //IAsyncResult result = caller.BeginInvoke(null, null);
            Task loadPrinterTask = new Task(() => LoadPrinter());
            loadPrinterTask.Start();
        }

        public void UnloadPrinter()
        {
            try
            {
                if (m_Printer == null) return;
                m_Printer.Release();
                m_Printer.Close();
            }
            catch (PosControlException)
            {
                Debug.WriteLine("Printer cannot unload, perhaps its turned off? Or not connected?");
            }
        }

        public void Print(OrderDetails orderDetails)
        {
            try
            {
                PrintCustomerVersion(orderDetails);
                m_Printer.CutPaper(98);
                PrintKitchenVersion(orderDetails);
                m_Printer.CutPaper(98);
            }
            catch (PosControlException)
            {
                Debug.WriteLine("Failed to print! Check printer status.");
            }
        }

        private void PrintBeijingInnHeader()
        {
            m_Printer.PrintMemoryBitmap(PrinterStation.Receipt, logo, PosPrinter.PrinterBitmapAsIs, PosPrinter.PrinterBitmapCenter);
            TextComponent addressLine = new TextCentreAlign(new TextBase("3 King Street, Gillingham, ME7 1EY"));
            TextComponent phoneNumber = new TextCentreAlign(new TextBase("01634 570 633"));
            PrintEnglish(addressLine.GetDec());
            PrintEnglish(phoneNumber.GetDec());
            PrintEnglish(" ");
        }

        private void PrintCustomerVersion(OrderDetails orderDetails)
        {
            ObservableCollection<Tuple<IItem, int>> concattedItems = orderDetails.ItemBasket.ConcatItems;
            try
            {
                TextComponent forCustomer = new TextBold(new TextCentreAlign(new TextBase("FOR RECORDS - " + DateTime.UtcNow.Date.ToString("dd/MM/yyyy"))));
                PrintEnglish(" ");
                PrintEnglish(forCustomer.GetDec());
                foreach (Tuple<IItem, int> item in concattedItems)
                {
                    string englishSizeModifier = "";
                    string chineseSizeModifier = "";
                    if (item.Item1.GetType() == typeof(SizeItem))
                    {
                        SizeItem si = (SizeItem)item.Item1;
                        englishSizeModifier = "(" + si.EnglishSizeString + ")";
                        chineseSizeModifier = "(" + si.ChineseSizeString + ")";
                    }

                    if (item.Item1.GetType() == typeof(PieItem))
                    {
                        PieItem p = (PieItem)item.Item1;
                        englishSizeModifier = "(" + p.EnglishSizeString + ")";
                        chineseSizeModifier = "(" + p.ChineseSizeString + ")";
                    }

                    TextComponent englishText = new TextBase(item.Item1.EnglishName);
                    TextComponent priceText = new TextRightAlign(new TextBase("£" + (item.Item1.Price * item.Item2).ToString("0.00")));
                    TextComponent quantityText = new TextRightAlign(new TextBase("X" + item.Item2.ToString() + "    "));
                    TextComponent chineseText = new TextBase(item.Item1.ChineseName);

                    PrintEnglish(englishText.GetDec() + englishSizeModifier);
                    PrintChinese(item.Item1.ChineseName + chineseSizeModifier);
                    PrintEnglish(quantityText.GetDec() + priceText.GetDec());
                }

                TextComponent total = new TextRightAlign(new TextBold(new TextUnderline(new TextBase("  Total: " + orderDetails.PriceText))));
                PrintEnglish(total.GetDec());
                if (orderDetails.CurrentAddress != null)
                {
                    TextComponent phoneNumberText = new TextBold(new TextBase(orderDetails.CurrentAddress.PhoneNumber));
                    TextComponent roadText = new TextRightAlign(new TextBold(new TextBase(orderDetails.CurrentAddress.Number + " " + orderDetails.CurrentAddress.Road)));
                    TextComponent townText = new TextRightAlign(new TextBold(new TextBase(orderDetails.CurrentAddress.Town)));
                    TextComponent postCodeText = new TextRightAlign(new TextBold(new TextBase(orderDetails.CurrentAddress.PostCode)));

                    PrintEnglish("Deliver to: " + roadText.GetDec());
                    PrintEnglish(townText.GetDec());
                    if (orderDetails.CurrentAddress.PhoneNumber != "")
                    {
                        PrintEnglish("Phone Number: " + phoneNumberText.GetDec() + postCodeText.GetDec());
                    }
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

        private void PrintKitchenVersion(OrderDetails orderDetails)
        {
            ObservableCollection<Tuple<IItem, int>> concattedItems = orderDetails.ItemBasket.ConcatItems;
            try
            {
                TextComponent forKitchen = new TextBold(new TextCentreAlign(new TextBase("FOR KITCHEN")));
                PrintEnglish(forKitchen.GetDec());
                foreach (Tuple<IItem, int> item in concattedItems)
                {
                    string englishSizeModifier = "";
                    string chineseSizeModifier = "";
                    if (item.Item1.GetType() == typeof(SizeItem))
                    {
                        SizeItem si = (SizeItem)item.Item1;
                        englishSizeModifier = "(" + si.EnglishSizeString + ")";
                        chineseSizeModifier = "(" + si.ChineseSizeString + ")";
                    }

                    if (item.Item1.GetType() == typeof(PieItem))
                    {
                        PieItem p = (PieItem)item.Item1;
                        englishSizeModifier = "(" + p.EnglishSizeString + ")";
                        chineseSizeModifier = "(" + p.ChineseSizeString + ")";
                    }

                    TextComponent englishText = new TextBase(EllipsisTruncate(item.Item1.EnglishName, 36));
                    TextComponent chineseText = new TextBase(item.Item1.ChineseName);
                    TextComponent specialText = new TextBold(new TextBase(item.Item1.ConcatProperties));
                    TextComponent quantityText = new TextDoubleWidthHeight(new TextBold(new TextBase(" X" + item.Item2.ToString())));

                    PrintEnglish(englishText.GetDec() + englishSizeModifier + quantityText.GetDec());
                    PrintChinese(item.Item1.ChineseName + chineseSizeModifier);
                    PrintEnglish(specialText.GetDec());
                    /*if (string.IsNullOrEmpty(specialText.GetDec()))
                    {
                        PrintEnglish(" ");
                    }*/
                    PrintEnglish(" ");
                }

                if (orderDetails.CurrentAddress != null)
                {
                    PrintEnglish("Deliver to: " + orderDetails.CurrentAddress.Concat);
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
            }
            catch (PosControlException)
            {
                Console.WriteLine("Cover open?");
            }
        }


        private string EllipsisTruncate(string value, int maxLength)
        {
            //return value.Length <= maxLength ? value : value.Substring(0, maxLength); 
            string result = value;
            if (value.Length >= maxLength)
            {
                result = value.Substring(0, maxLength);
                //result += "...";
            }
            return result;
        }

        private void PrintEnglish(string str)
        {
            string text = str.Replace("ESC", ((char)27).ToString()) + "\x1B|1lF";
            m_Printer.PrintNormal(PrinterStation.Receipt, text);
        }

        private void PrintChinese(string str)
        {
            if (str.Length > 13)
            {
                str = str.Substring(0, 13);
            }
            float oneChar = 37.5f;
            int printWidth = (int)oneChar * str.Length;

            Bitmap thing = TextBitmap.Convert(str, 20, 300, 20);
            //m_Printer.PrintMemoryBitmap(PrinterStation.Receipt, thing, PosPrinter.PrinterBitmapAsIs, PosPrinter.PrinterBarCodeLeft);
            m_Printer.PrintBitmap(PrinterStation.Receipt, "file.bmp", PosPrinter.PrinterBitmapAsIs, PosPrinter.PrinterBitmapLeft); //530 limit
        }

        #region Properties
        public string Health
        {
            get
            {
                try
                {
                    if (m_Printer == null) return "Printer not found";
                    return m_Printer.CheckHealth(HealthCheckLevel.Internal);
                }
                catch (PosControlException e)
                {
                    return e.Message;
                }
            }
        }

        public string PowerState
        {
            get
            {
                if (m_Printer == null) return "Unknown";
                switch (m_Printer.PowerState)
                {
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

        public bool? CoverOpen
        {
            get
            {
                try
                {
                    if (m_Printer == null) throw new PosControlException("No Printer Exists", ErrorCode.NoExist, new Exception());
                    return m_Printer.CoverOpen;
                }
                catch (PosControlException)
                {
                    return null;
                }
            }
        }

        public bool DeviceEnabled
        {
            get
            {
                if (m_Printer == null) return false;
                return m_Printer.DeviceEnabled;
            }
        }

        public DeviceCollection PrinterDevices
        {
            get
            {
                return myDevices;
            }
        }

        public string StatusText
        {
            get
            {
                return statusString;
            }
        }
        #endregion
    }
}
