//using StoreManagement.Models;
//using System;
//using System.Collections.Generic;
//using System.Configuration;
//using System.Data;
//using System.Data.SqlClient;
//using System.Linq;
//using System.Net;
//using System.Net.Http;
//using System.Web.Http;

//namespace StoreManagement.Controllers
//{
//    public class SaleController : ApiController
//    {


//        SqlConnection connection;
//        SqlCommand command;
//        SqlDataAdapter dataAdapter;
//        SqlDataReader reader;

//        // Get method url api/Purchase
//        public HttpResponseMessage Get(string date, string invoice)
//        {
//            DataTable dataTable = new DataTable();
//            string query = @"SELECT stock.Invoice,Stock.StockId, Stock.StoreId, Stock.ToStoreId, Stock.ItemId, Stock.Quantity, Stock.Price, Stock.TotalPrice, Stock.Type, Stock.Date, Item.ItemName
//            FROM  Stock INNER JOIN Item ON Stock.ItemId = Item.ItemId WHERE Type='Sale' AND Date='" + date + "' AND Invoice='" + invoice + "'";
//            connection = new SqlConnection(ConfigurationManager.ConnectionStrings["StoreDb"].ConnectionString);
//            connection.Open();
//            command = new SqlCommand(query, connection);
//            dataAdapter = new SqlDataAdapter(command);
//            command.CommandType = CommandType.Text;
//            dataAdapter.Fill(dataTable);
//            connection.Close();


//            return Request.CreateResponse(HttpStatusCode.OK, dataTable);

//        }
//        public string Post(Stock item)
//        {
//            if (IsExist(item.Invoice, item.ItemId))
//            {
//                return "Item Already Exist For This Invoice";
//            }
//            else
//            {

//                try
//                {
//                    DataTable dataTable = new DataTable();
//                    string query = @"INSERT INTO Stock (Invoice,StoreId, ToStoreId, ItemId, Quantity, Price, TotalPrice, Type, Date) VALUES('" + item.Invoice + "','" + item.StoreId + "','0','" + item.ItemId + "','" + item.Quantity + "','" + item.Price + "','" + item.Quantity * item.Price + "','Sale','" + item.Date + "')";
//                    connection = new SqlConnection(ConfigurationManager.ConnectionStrings["StoreDb"].ConnectionString);
//                    connection.Open();
//                    command = new SqlCommand(query, connection);
//                    dataAdapter = new SqlDataAdapter(command);
//                    command.CommandType = CommandType.Text;
//                    dataAdapter.Fill(dataTable);
//                    connection.Close();
//                    return "Added Successfully";
//                }
//                catch (Exception)
//                {
//                    return "Failed to add";
//                }

//            }
//        }
//        public bool IsExist(string invoice, int id)
//        {
//            connection = new SqlConnection(ConfigurationManager.ConnectionStrings["StoreDb"].ConnectionString);

//            string query = @"SELECT * FROM Stock WHERE Invoice='" + invoice + "' AND ItemId='" + id + "'";
//            command = new SqlCommand(query, connection);
//            connection.Open();
//            reader = command.ExecuteReader();
//            bool exist = reader.HasRows;
//            reader.Close();
//            connection.Close();

//            return exist;
//        }

//        public string Delete(string invoice, int id)
//        {
//            try
//            {
//                DataTable dataTable = new DataTable();
//                string query = @"DELETE FROM Stock WHERE Invoice='" + invoice + "' AND ItemId='" + id + "'";
//                connection = new SqlConnection(ConfigurationManager.ConnectionStrings["StoreDb"].ConnectionString);
//                connection.Open();
//                command = new SqlCommand(query, connection);
//                dataAdapter = new SqlDataAdapter(command);
//                command.CommandType = CommandType.Text;
//                dataAdapter.Fill(dataTable);
//                connection.Close();
//                return "Delete Successfully";
//            }
//            catch (Exception)
//            {
//                return "Failed to delete";
//            }
//        }

//        public string Put(Stock store)
//        {
//            connection = new SqlConnection(ConfigurationManager.ConnectionStrings["StoreDb"].ConnectionString);

//            try
//            {
//                DataTable dataTable = new DataTable();
//                string query = @"UPDATE Stock SET Quantity = '" + store.Quantity + "',Price = '" + store.Price + "',TotalPrice = '" + store.Quantity * store.Price + "' WHERE Invoice ='" + store.Invoice + "' and ItemId='" + store.ItemId + "'";
//                // connection = new SqlConnection(ConfigurationManager.ConnectionStrings["StoreManagement"].ConnectionString);
//                connection.Open();
//                command = new SqlCommand(query, connection);
//                dataAdapter = new SqlDataAdapter(command);
//                command.CommandType = CommandType.Text;
//                dataAdapter.Fill(dataTable);
//                connection.Close();
//                return "Update Successfully";
//            }
//            catch (Exception)
//            {
//                return "Failed to update";
//            }
//        }
//    }
//}
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
    public class SaleController : ApiController
    {
        SqlConnection connection;
        SqlCommand command;
        SqlDataAdapter dataAdapter;
        SqlDataReader reader;

        public HttpResponseMessage Get(string date, string invoice)
        {

            DataTable dataTable = new DataTable();
            string query = @"SELECT stock.Invoice,Stock.StockId, Stock.StoreId, Stock.ToStoreId, Stock.ItemId, Stock.Quantity, Stock.Price, Stock.TotalPrice, Stock.Type, Stock.Date, Item.ItemName
FROM  Stock INNER JOIN Item ON Stock.ItemId = Item.ItemId WHERE Type='Sale' AND Date='" + date + "' AND Invoice='" + invoice + "' ORDER BY Stock.StockId ASC";
            connection = new SqlConnection(ConfigurationManager.ConnectionStrings["StoreDb"].ConnectionString);
            connection.Open();
            command = new SqlCommand(query, connection);
            dataAdapter = new SqlDataAdapter(command);
            command.CommandType = CommandType.Text;
            dataAdapter.Fill(dataTable);
            connection.Close();


            return Request.CreateResponse(HttpStatusCode.OK, dataTable);



        }
        public string Post(Stock item)
        {
            if (IsExist(item.Invoice, item.ItemId))
            {
                return "Item Already Exist For This Invoice";
            }
            else
            {

                try
                {
                    DataTable dataTable = new DataTable();
                    string query = @"INSERT INTO Stock (Invoice,StoreId, ToStoreId, ItemId, Quantity, Price, TotalPrice, Type, Date) VALUES('" + item.Invoice + "','" + item.StoreId + "','0','" + item.ItemId + "','" + item.Quantity + "','" + item.Price + "','" + item.Quantity * item.Price + "','Sale','" + item.Date + "')";
                    connection = new SqlConnection(ConfigurationManager.ConnectionStrings["StoreDb"].ConnectionString);
                    connection.Open();
                    command = new SqlCommand(query, connection);
                    dataAdapter = new SqlDataAdapter(command);
                    command.CommandType = CommandType.Text;
                    dataAdapter.Fill(dataTable);
                    connection.Close();
                    return "Added Successfully";
                }
                catch (Exception)
                {
                    return "Failed to add";
                }

            }
        }
        public bool IsExist(string invoice, int id)
        {
            connection = new SqlConnection(ConfigurationManager.ConnectionStrings["StoreDb"].ConnectionString);

            string query = @"SELECT * FROM Stock WHERE Invoice='" + invoice + "' AND ItemId='" + id + "' AND Type='Sale'";
            command = new SqlCommand(query, connection);
            connection.Open();
            reader = command.ExecuteReader();
            bool exist = reader.HasRows;
            reader.Close();
            connection.Close();

            return exist;
        }

        public string Delete(string invoice, int id)
        {
            try
            {
                DataTable dataTable = new DataTable();
                string query = @"DELETE FROM Stock WHERE Invoice='" + invoice + "' AND ItemId='" + id + "'";
                connection = new SqlConnection(ConfigurationManager.ConnectionStrings["StoreDb"].ConnectionString);
                connection.Open();
                command = new SqlCommand(query, connection);
                dataAdapter = new SqlDataAdapter(command);
                command.CommandType = CommandType.Text;
                dataAdapter.Fill(dataTable);
                connection.Close();
                return "Delete Successfully";
            }
            catch (Exception)
            {
                return "Failed to delete";
            }
        }

        public string Put(Stock store)
        {
            connection = new SqlConnection(ConfigurationManager.ConnectionStrings["StoreDb"].ConnectionString);

            try
            {
                DataTable dataTable = new DataTable();
                string query = @"UPDATE Stock SET Quantity = '" + store.Quantity + "',Price = '" + store.Price + "',TotalPrice = '" + store.Quantity * store.Price + "' WHERE Invoice ='" + store.Invoice + "' and ItemId='" + store.ItemId + "'";
                // connection = new SqlConnection(ConfigurationManager.ConnectionStrings["StoreManagement"].ConnectionString);
                connection.Open();
                command = new SqlCommand(query, connection);
                dataAdapter = new SqlDataAdapter(command);
                command.CommandType = CommandType.Text;
                dataAdapter.Fill(dataTable);
                connection.Close();
                return "Update Successfully";
            }
            catch (Exception)
            {
                return "Failed to update";
            }
        }

    }
}
