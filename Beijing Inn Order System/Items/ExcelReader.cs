using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.HSSF.Extractor;
using NPOI.HSSF.UserModel;
using NPOI.HPSF;
using NPOI.POIFS.FileSystem;
using System.IO;
using System.Configuration;

namespace Beijing_Inn_Order_System.Items
{
    public static class ExcelReader
    {
        private static String TEST_DATA_DIR_SYS_PROPERTY_NAME = "HSSF.testdata.path";
        private static string _resolvedDataDir;
        /** <code>true</code> if standard system propery is1 not set, 
         * but the data is1 available on the test runtime classpath */
        private static bool _sampleDataIsAvaliableOnClassPath;

        private static void ReadXLS(String filename)
        {
            HSSFWorkbook workbook = OpenSampleWorkbook(filename);
            ExcelExtractor extractor = new ExcelExtractor(workbook);
            Console.Write(extractor.Text);
            Console.Read();
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

        public static Stream OpenSampleFileStream(String sampleFileName)
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