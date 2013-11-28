using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.HSSF.Extractor;
using NPOI.HSSF.UserModel;
using NPOI.HPSF;
using NPOI.XSSF.UserModel;
using NPOI.POIFS.FileSystem;
using System.IO;
using System.Configuration;
using NPOI.SS.UserModel;
using System.Windows;

namespace Beijing_Inn_Order_System.Items
{
    public static class ExcelReader_NPOI
    {
        //private static String TEST_DATA_DIR_SYS_PROPERTY_NAME = "HSSF.testdata.path";
        private static String TEST_DATA_DIR_SYS_PROPERTY_NAME = "c:\\";
        private static string _resolvedDataDir;
        /** <code>true</code> if standard system propery is1 not set, 
         * but the data is1 available on the test runtime classpath */
        private static bool _sampleDataIsAvaliableOnClassPath;

        public static void ReadXLS(String filename)
        {
            HSSFWorkbook workbook = OpenSampleWorkbook(filename);
            ExcelExtractor extractor = new ExcelExtractor(workbook);
            Console.Write(extractor.Text);
            Console.Read();
        }

        public static string[,] ReadAllXLSX(string filename, int[] cellColumnsString, int[] cellColumnNumber, bool local)
        {
            if (cellColumnNumber == null)
            {
                cellColumnNumber = new int[0];
            }

            if (cellColumnsString == null)
            {
                cellColumnsString = new int[0];
            }

            XSSFWorkbook hssfwb = OpenXLSX(filename, local);
            ISheet sheet = hssfwb.GetSheet("Sheet1");

            string[,] output = new string[sheet.LastRowNum + 1, cellColumnsString.Length + cellColumnNumber.Length];

            for (int row = 0; row <= sheet.LastRowNum; row++)
            {
                if (sheet.GetRow(row) != null) //null is when the row only contains empty cells 
                {
                    for (int i = 0; i < cellColumnsString.Length; i++)
                    {
                        output[row, i] = sheet.GetRow(row).GetCell(cellColumnsString[i]).StringCellValue;
                    }

                    for (int i = 0; i < cellColumnNumber.Length; i++)
                    {
                        string number = sheet.GetRow(row).GetCell(cellColumnNumber[i]).NumericCellValue.ToString();
                        output[row, cellColumnsString.Length + i] = number;
                    }
                }
            }
            return output;
        }

        public static string ReadSingleXLSXFromWorkBook(XSSFWorkbook hssfwb, int row, int column, bool isNumeric)
        {
            ISheet sheet = hssfwb.GetSheet("Sheet1");
            if (isNumeric)
            {
                return sheet.GetRow(row).GetCell(column).NumericCellValue.ToString();
            }
            else
            {
                return sheet.GetRow(row).GetCell(column).StringCellValue;
            }
        }

        public static string[] ReadSingleXlSX(string filename, int row, int[] cellColumns)
        {
            XSSFWorkbook hssfwb = OpenXLSX(filename, true);
            ISheet sheet = hssfwb.GetSheet("Sheet1");

            string[] output = new string[cellColumns.Length];

            if (sheet.GetRow(row) != null) //null is when the row only contains empty cells 
            {
                for (int i = 0; i < cellColumns.Length; i++)
                {
                    output[i] = sheet.GetRow(row).GetCell(cellColumns[i]).StringCellValue;
                }
            }

            return output;
        }

        public static void WriteSingleXLXS(XSSFWorkbook hssfwb, int row, int column, string data)
        {
            ISheet sheet = hssfwb.GetSheet("Sheet1");
            sheet.GetRow(row).CreateCell(column).SetCellValue(data);
        }

        public static XSSFWorkbook OpenXLSX(string filename, bool local)
        {
            XSSFWorkbook hssfwb;
            string fileDir;
            if (local == true)
            {
                fileDir = Directory.GetCurrentDirectory();
            }
            else
            {
                string folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                fileDir = Path.Combine(folder, "Beijing Inn");
                if (!Directory.Exists(fileDir)) Directory.CreateDirectory(fileDir);
            }
            string fileLocation = fileDir + "\\" + filename;
            if (!File.Exists(fileLocation))
            {
                XSSFWorkbook newFile = new XSSFWorkbook();
                newFile.CreateSheet("Sheet1");
                WriteXLSX(newFile, filename);
            }
            using (FileStream file = new FileStream(@fileLocation, FileMode.Open, FileAccess.Read))
            {
                hssfwb = new XSSFWorkbook(file);
            }
            return hssfwb;
        }

        public static void WriteXLSX(XSSFWorkbook workbook, string filename)
        {
            string fileDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Beijing Inn");
            string fileLocation = fileDir + "\\" + filename;
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

        private static HSSFWorkbook OpenSampleWorkbook(String filename)
        {
            try
            {
                return new HSSFWorkbook(OpenSampleFileStream(filename));
            }
            catch (IOException)
            {
                throw;
            }
        }

        private static Stream OpenSampleFileStream(String sampleFileName)
        {
            Initialise();

            if (_sampleDataIsAvaliableOnClassPath)
            {
                Stream result = OpenClasspathResource(sampleFileName);
                if (result == null)
                {
                    throw new Exception("specified test sample file '" + sampleFileName
                            + "' not found on the classpath");
                }
                //			System.out.println("Opening cp: " + sampleFileName);
                // wrap to avoid temp warning method about auto-closing input stream
                return new NonSeekableStream(result);
            }
            if (_resolvedDataDir == "")
            {
                throw new Exception("Must set system property '"
                        + TEST_DATA_DIR_SYS_PROPERTY_NAME
                        + "' properly before running tests");
            }


            if (!File.Exists(_resolvedDataDir + sampleFileName))
            {
                throw new Exception("Sample file '" + sampleFileName
                        + "' not found in data dir '" + _resolvedDataDir + "'");
            }


            //		System.out.println("Opening " + f.GetAbsolutePath());
            try
            {
                return new FileStream(_resolvedDataDir + sampleFileName, FileMode.Open);
            }
            catch (FileNotFoundException)
            {
                throw;
            }
        }

        private static void Initialise()
        {
            String dataDirName = ConfigurationManager.AppSettings[TEST_DATA_DIR_SYS_PROPERTY_NAME];

            if (dataDirName == "")
                throw new Exception("Must set system property '"
                        + TEST_DATA_DIR_SYS_PROPERTY_NAME
                        + "' before running tests");

            if (!Directory.Exists(dataDirName))
            {
                throw new IOException("Data dir '" + dataDirName
                        + "' specified by system property '"
                        + TEST_DATA_DIR_SYS_PROPERTY_NAME + "' does not exist");
            }
            _sampleDataIsAvaliableOnClassPath = true;
            _resolvedDataDir = dataDirName;
        }

        private static Stream OpenClasspathResource(String sampleFileName)
        {
            FileStream file = new FileStream(ConfigurationManager.AppSettings["HSSF.testdata.path"] + sampleFileName, FileMode.Open);
            return file;
        }

        private class NonSeekableStream : Stream
        {

        private Stream _is;

        public NonSeekableStream(Stream is1)
        {
            _is = is1;
        }

        public int Read()
        {
            return _is.ReadByte();
        }
        public override int Read(byte[] b, int off, int len)
        {
            return _is.Read(b, off, len);
        }
        public bool markSupported()
        {
            return false;
        }
        public override void Close()
        {
            _is.Close();
        }
        public override bool CanRead
        {
            get { return _is.CanRead; }
        }
        public override bool CanSeek
        {
            get { return false; }
        }
        public override bool CanWrite
        {
            get { return _is.CanWrite; }
        }
        public override long Length
        {
            get { return _is.Length; }
        }
        public override long Position
        {
            get { return _is.Position; }
            set { _is.Position = value; }
        }
        public override void Write(byte[] buffer, int offset, int count)
        {
            _is.Write(buffer, offset, count);
        }
        public override void Flush()
        {
            _is.Flush();
        }
        public override long Seek(long offset, SeekOrigin origin)
        {
            return _is.Seek(offset, origin);
        }
        public override void SetLength(long value)
        {
            _is.SetLength(value);
        }
    }
    }

}