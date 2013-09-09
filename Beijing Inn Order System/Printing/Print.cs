using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.PointOfService;

namespace Beijing_Inn_Order_System.Printing
{
    class Print
    {
        private PosPrinter oPrinter;
        private PosExplorer myExplorer = new PosExplorer();
        private string printText;

        public void InitPrinter()
        {
            DeviceCollection devices = myExplorer.GetDevices();
            SearchDevices(devices);
            DeviceInfo oDevicePrinter = myExplorer.GetDevice(DeviceType.PosPrinter, "POSPrinter");
            oPrinter = (PosPrinter)myExplorer.CreateInstance(oDevicePrinter);
            try
            {
                if (oPrinter != null)
                {
                    oPrinter.Open();
                    oPrinter.Claim(1000);
                    oPrinter.DeviceEnabled = true;
                }
            }
            catch (Exception exPrinter)
            {
                Console.WriteLine(exPrinter.ToString(), "Warning");
            }
        }

        public void PrintReceipt()
        {
            try
            {
                oPrinter.PrintNormal(PrinterStation.Receipt, printText + DateTime.Now.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void SearchDevices(DeviceCollection devices)
        {
            foreach (DeviceInfo device in devices)
            {
                if (device.Type == DeviceType.Msr)
                {
                    //if (device.ServiceObjectName == currentMsr)
                    //{
                    //CreateMsr(device);
                    Console.WriteLine(device.ServiceObjectName);

                    // It is important that applications close all open
                    // Service Objects before terminating.
                    //msr.Close();
                    //msr = null;
                }
            }
        }
    }
}
