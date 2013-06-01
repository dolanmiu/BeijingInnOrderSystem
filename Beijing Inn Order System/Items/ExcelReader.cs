using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beijing_Inn_Order_System.Items
{
    public static class ExcelReader
    {

        public static void ReadXLS(String filename)
        {
            var fileName = string.Format("{0}\\" + filename, Directory.GetCurrentDirectory());
            var connectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0; data source={0}; Extended Properties=Excel 8.0;", fileName);

            var adapter = new OleDbDataAdapter("SELECT * FROM [workSheetNameHere$]", connectionString);
            var ds = new DataSet();

            adapter.Fill(ds, "anyNameHere");

            DataTable data = ds.Tables["anyNameHere"];
        }
    }
}
