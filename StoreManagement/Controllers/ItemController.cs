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
    public class ItemController : ApiController
    {


        SqlConnection connection;
        SqlCommand command;
        SqlDataAdapter dataAdapter;

        public HttpResponseMessage Get()
        {
            DataTable dataTable = new DataTable();
            string query = @"select ItemId,ItemName,Price,Details,CategoryName
            from Item join 
             Category on
             Category.CategoryId=Item.CategoryId";
            connection = new SqlConnection(ConfigurationManager.ConnectionStrings["StoreDb"].ConnectionString);
            command = new SqlCommand(query, connection);
            dataAdapter = new SqlDataAdapter(command);
            command.CommandType = CommandType.Text;
            dataAdapter.Fill(dataTable);
            return Request.CreateResponse(HttpStatusCode.OK, dataTable);
        }
        public HttpResponseMessage Get(int id)
        {
            DataTable dataTable = new DataTable();
            string query = @"select ItemId,ItemName,Price
            from Item join Where ItemId='"+id+"'";
            connection = new SqlConnection(ConfigurationManager.ConnectionStrings["StoreDb"].ConnectionString);
            command = new SqlCommand(query, connection);
            dataAdapter = new SqlDataAdapter(command);
            command.CommandType = CommandType.Text;
            dataAdapter.Fill(dataTable);
            return Request.CreateResponse(HttpStatusCode.OK, dataTable);
        }
        public string Post(Item itm)
        {
            try
            {

                DataTable dataTable = new DataTable();
                string query = @"insert into Item (ItemName,CategoryId,Price,Details) values('" + itm.ItemName + "','" + itm.CategoryId + 
                    "','" + itm.Price + "', '" + itm.Details + "') ";

                connection = new SqlConnection(ConfigurationManager.ConnectionStrings["StoreDb"].ConnectionString);
                command = new SqlCommand(query, connection);

                dataAdapter = new SqlDataAdapter(command);
                command.CommandType = CommandType.Text;
                dataAdapter.Fill(dataTable);
                return "Added Successfully";
            }
            catch (Exception)
            {

                return "Failed to add";
            }

        }
        public string Put(Item itm)
        {
            try
            {

                DataTable dataTable = new DataTable();
                string query = @"Update  dbo.Item set ItemName ='" + itm.ItemName + @"'
                ,CategoryId ='" + itm.CategoryId + @"'
                ,Price ='" + itm.Price + @"'
                ,Details ='" + itm.Details + @"'
                where ItemId=" + itm.ItemId + @"
                ";
                connection = new SqlConnection(ConfigurationManager.ConnectionStrings["StoreDb"].ConnectionString);
                command = new SqlCommand(query, connection);
                dataAdapter = new SqlDataAdapter(command);
                command.CommandType = CommandType.Text;
                dataAdapter.Fill(dataTable);
                return "Updated Successfully";
            }
            catch (Exception)
            {

                return "Failed to Update";
            }
        }
        public string Delete(int id)
        {
            try
            {

                DataTable dataTable = new DataTable();
                string query = @"delete from dbo.Item where ItemId=" + id;

                connection = new SqlConnection(ConfigurationManager.ConnectionStrings["StoreDb"].ConnectionString);
                command = new SqlCommand(query, connection);
                dataAdapter = new SqlDataAdapter(command);
                command.CommandType = CommandType.Text;
                dataAdapter.Fill(dataTable);
                return "Delete Successfully";
            }
            catch (Exception)
            {

                return "fail to delete";
            }
        }
    }


}

