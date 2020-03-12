using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace StoreManagement.Controllers
{
    public class SaleInvoiceController : ApiController
    {
        SqlCommand command;
        SqlDataAdapter dataAdapter;
        SqlDataReader reader;
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["StoreDb"].ConnectionString);
        string invoice;

        public string Get()
        {
            string date = DateTime.Now.ToString("yyyyMM");
            int id;
            string invoice = "";
            connection.Open();
            command = new SqlCommand(@"SELECT Max(SUBSTRING(Invoice,7,9)) FROM Stock WHERE Type='Sale'", connection);
            reader = command.ExecuteReader();
            while (reader.Read())
            {

                string i = reader[0].ToString();

                if (i == "")
                {
                    id = 1;
                    invoice = "000" + id;
                }
                else
                {
                    id = int.Parse(i) + 1;
                }
                if (id < 10)
                {
                    invoice = "000" + id;
                }
                else if (id < 100)
                {
                    invoice = "00" + id;
                }
                else if (id < 1000)
                {
                    invoice = "0" + id;

                }
                else
                {
                    invoice = id.ToString();
                }


            }

            reader.Close();
            connection.Close();
            string sl = date + invoice;
            return sl;



        }
    }
}
