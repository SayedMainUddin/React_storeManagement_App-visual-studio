
using StoreManagement.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace StoreManagement.Controllers
{
    public class StockController : ApiController
    {

        SqlCommand command;
        SqlDataAdapter dataAdapter;
        SqlDataReader reader;
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["StoreDb"].ConnectionString);
        string invoice;
        // Get method url api/store
        public HttpResponseMessage Get()
        {
            DataTable dataTable = new DataTable();
            connection.Open();
            string query = @"SELECT A.Invoice,A.ITEMID,A.StoreId,Store.StoreName,Item.ItemName, SUM(PQTY) as PurchaseQuantity,SUM(SRQTY) as SaleReturnQuantity,SUM(SQTY) as SaleQuantity,
SUM(TRNSQTY) as TransferQuantity,SUM(PRQTY) as PurchaseReturnQuantity,	CAST(ROUND((SUM(AVGPrice)/SUM(PQTY)),2) as numeric(18,2)) AS AverageRate,((SUM(PQTY)+SUM(SRQTY))-(SUM(SQTY)+SUM(TRNSQTY)+SUM(PRQTY))) 
AS AvailableQuantity  FROM  (
SELECT ITEMID,StoreId,Invoice,SUM(Quantity) PQTY,0 SQTY,0 TRNSQTY,0 SRQTY,0 PRQTY,SUM(TotalPrice) AVGPrice  FROM Stock where  Type='Purchase'  GROUP BY ITEMID,StoreId,Invoice
 UNION 
SELECT  ITEMID,StoreId,Invoice,0 PQTY,SUM(Quantity) SQTY,0 TRNSQTY,0 SRQTY,0 PRQTY,0 AVGPrice  FROM Stock   WHERE Type='Sale' GROUP BY ITEMID,StoreId,Invoice
 UNION 
 SELECT  ITEMID,StoreId,Invoice,0 PQTY,0 SQTY,SUM(Quantity) TRNSQTY,0 SRQTY,0 PRQTY,0 AVGPrice   FROM Stock WHERE Type='Transfer' GROUP BY ITEMID,StoreId,Invoice
 UNION
 SELECT  ITEMID,StoreId,Invoice,0 PQTY,0 SQTY,0 TRNSQTY,SUM(Quantity) SRQTY,0 PRQTY,0 AVGPrice   FROM Stock  WHERE Type='Sale Return' GROUP BY ITEMID,StoreId,Invoice
 UNION
 SELECT  ITEMID,StoreId,Invoice,0 PQTY,0 SQTY,0 TRNSQTY,0 SRQTY,SUM(Quantity) PRQTY,0 AVGPrice   FROM Stock  WHERE Type='Purchase Return' GROUP BY ITEMID,StoreId,Invoice
 )A INNER JOIN  Item ON A.ItemId = Item.ItemId INNER JOIN Store ON A.StoreId=Store.StoreId  
 GROUP BY A.ITEMID,A.StoreId,Store.StoreName,A.Invoice,Item.ItemName
 Order by Invoice DESC ";
            connection = new SqlConnection(ConfigurationManager.ConnectionStrings["StoreDb"].ConnectionString);
            command = new SqlCommand(query, connection);
            dataAdapter = new SqlDataAdapter(command);
            command.CommandType = CommandType.Text;
         
            dataAdapter.Fill(dataTable);
            connection.Close();
            return Request.CreateResponse(HttpStatusCode.OK, dataTable);



        }
        // Post method url api/store
        public string Post(Stock item)
        {

            try
            {

                string date = DateTime.Now.ToString("yyyy-MM-dd");
                //if (item.Intime == date)
                //{
                //    invoice = LastInvoice();
                //}
                //else
                //{
                //    invoice = Invoice();

                //}

                DataTable dataTable = new DataTable();
                connection.Open();
                string query = @"INSERT INTO Stock (Invoice,StoreId, ToStoreId, ItemId, Quantity, Price, TotalPrice, Type, Date) VALUES('" + item.Invoice + "','" + item.StoreId + "','0','" + item.ItemId + "','" + item.Quantity + "','" + item.Price + "','" + item.Quantity * item.Price + "','Purchase','" + item.Date + "')";
                connection = new SqlConnection(ConfigurationManager.ConnectionStrings["StoreDb"].ConnectionString);
                command = new SqlCommand(query, connection);
                dataAdapter = new SqlDataAdapter(command);
                command.CommandType = CommandType.Text;
                connection.Close();
                dataAdapter.Fill(dataTable);



                return "Added Successfully";
            }
            catch (Exception)
            {
                return "Failed to add";
            }
        }
        private string Invoice()
        {

            string date = DateTime.Now.ToString("yyyyMM");
            int id;
            string invoice = "";
            connection.Open();
            command = new SqlCommand(@"SELECT Max(SUBSTRING(Invoice,7,9)) FROM Stock", connection);
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
        private string LastInvoice()
        {

            string invoice = "";
            connection.Open();
            command = new SqlCommand(@"SELECT Max(Invoice) FROM Stock", connection);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                invoice = reader[0].ToString();
            }

            reader.Close();
            connection.Close();

            return invoice;

        }

    }
}