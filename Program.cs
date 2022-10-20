using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace hddeserializer
{
    internal class Program
    {
        public static string jsonFilea = @"C:\Users\Daddy\Desktop\Work HelpDesk\Modifiable\Separated\productInfos.json";
        // public static string jsonFileb = @"C:\Users\Daddy\Desktop\Work HelpDesk\Modifiable\Separated\issueInfos.json";
        // public static string jsonFilec = @"C:\Users\Daddy\Desktop\Work HelpDesk\Modifiable\Separated\categoyInfos.json";

        static void Main()
        {
            List<ProductInfos> productInfos = new List<ProductInfos>();
            List<IssueInfos> issueInfos = new List<IssueInfos>();
            List<CategoryInfos> categoryInfos = new List<CategoryInfos>();

            DataTable dt = GetDeserializeNullCheck(jsonFilea);

            foreach (DataRow row in dt.Rows)
            {
                productInfos.Add(new ProductInfos(row["name"].ToString(), row["description"].ToString(), Convert.ToInt32(row["productID"]), row["keyWords"].ToString()));
            }

            /*foreach (DataRow row in dt.Rows)
            { 
                issueInfos.Add(new IssueInfos(Convert.ToInt32(row["issueId"]), row["name"].ToString(), row["description"].ToString()));
            }

            foreach (DataRow row in dt.Rows)
            {
                categoryInfos.Add(new CategoryInfos(Convert.ToInt32(row["productID"]), Convert.ToInt32(row["categoryId"]), Convert.ToInt32(row["issueId"]), Convert.ToInt32(row["requestTypeID"])));
            }
            */

            // Validate data
            foreach (var item in productInfos)
            {                
                Console.WriteLine($"\nName: {item.ProductName}\nDescription: {item.Description}\nID: {item.ProductID}\nKeywords: {item.Keywords}");
            }
            Console.ReadLine();
        }

        //
        // Order of operation to understand how query works:
        // Product information => Issue Id => Category ID.
        //

        public static DataTable GetDeserializeNullCheck(string jsonfile)
        { 

            string[] nullProduct = new string[] { "name", "description", "productID", "keyWords" };
            // string[] nullIssue = new string[] { "issueID", "name", "description" };
            // string[] nullCategory = new string[] { "description", "productID", "issueId", "categoryID", "requestTypeID" };

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
        // productInfos.Add(new ProductInfos(row["name"].ToString(), row["description"].ToString(), Convert.ToInt32(row["productID"]), row["keyWords"].ToString()));

        
    }
}