using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using Newtonsoft.Json;

namespace hddeserializer
{
    internal class Program
    {
        public static string jsonFilea = "";
        /// <summary>
        /// Deserialize JSON stack retrieved from HD App
        /// Formate then dump to XLS for future use.
        /// </summary>
        static void Main()
        {
            var excel = new Excel.Application();
            excel.Visible = false;
            excel.Workbooks.Add();
            Excel._Worksheet worksheet = excel.ActiveSheet as Excel.Worksheet;

            object misValue = System.Reflection.Missing.Value;            

            List<ProductInfos> productInfos = new List<ProductInfos>();

            DataTable dt = GetDeserializeNullCheck(jsonFilea);

            foreach (DataRow row in dt.Rows)
            {
                productInfos.Add(new ProductInfos(row["name"].ToString(), row["description"].ToString(), Convert.ToInt32(row["productID"]), row["keyWords"].ToString()));
            }

            worksheet.Cells[1, "A"].Value = "Name";
            worksheet.Cells[1, "B"].Value = "Description";
            worksheet.Cells[1, "C"].Value = "ID";
            worksheet.Cells[1, "D"].Value = "Keywords";

            int _row = 2;
            foreach (var item in productInfos)
            {                
                worksheet.Cells[_row++, "A"].Value = item.ProductName;
                worksheet.Cells[_row,   "B"].Value = item.Description;
                worksheet.Cells[_row,   "C"].Value = item.ProductID;
                worksheet.Cells[_row,   "D"].Value = item.Keywords;

                Console.WriteLine($"Adding:\nName: {item.ProductName}\nDescription: {item.Description}\nID: {item.ProductID}\nKeywords: {item.Keywords}");
                Console.SetCursorPosition(0, 0);
            }

            excel.Application.ActiveWorkbook.SaveAs(@"C:\Users\user\repos\hddeserializer\bin\Debug\hdStackDeSerialized.xls", Excel.XlFileFormat.xlWorkbookDefault, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            excel.Application.ActiveWorkbook.Close();
            Console.ReadLine();
        }

        //
        // Order of operation to understand how query works:
        // Product information => Issue Id => Category ID.
        //

        public static DataTable GetDeserializeNullCheck(string jsonfile)
        { 
            string[] nullProduct = new string[] { "name", "description", "productID", "keyWords" };

            // Is file there?
            if (!File.Exists(jsonfile))
                throw new IOException("File not found, ensure file is present and try again.");

            else
            {
                string json = File.ReadAllText(jsonfile);

                // Deserialize data
                DataSet dataSet = JsonConvert.DeserializeObject<DataSet>(json);
                DataTable table = dataSet.Tables[0];


                // Get data in rows
                foreach (DataRow row in table.Rows)
                {
                    foreach (string n in nullProduct)
                    {
                        // Check if data empty
                        if (row[n] == null)
                            throw new DataException("Data null or invalid, stopping program.\nCheck document follows JSON standard.");
                    }
                }

                // Return valid table data.
                return table;
            }
        }       
    }
}