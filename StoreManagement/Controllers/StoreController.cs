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
    public class StoreController : ApiController
    {
        SqlConnection connection;
        SqlCommand command;
        SqlDataAdapter dataAdapter;
    
        public HttpResponseMessage Get()
        {
            DataTable dataTable = new DataTable();
            string query = @"Select * from Store";
            connection = new SqlConnection(ConfigurationManager.ConnectionStrings["StoreDb"].ConnectionString);
            command = new SqlCommand(query, connection);
            dataAdapter = new SqlDataAdapter(command);
            command.CommandType = CommandType.Text;
            dataAdapter.Fill(dataTable);
            return Request.CreateResponse(HttpStatusCode.OK, dataTable);
        }
        public string Post(Store str)
        {
            try
            {

                DataTable dataTable = new DataTable();
                string query = @"insert into dbo.Store (StoreName,StoreLocation) values('" + str.StoreName + @"',
                '" + str.StoreLocation + @"')
                ";

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
        public string Put(Store str)
        {
            try
            {

                DataTable dataTable = new DataTable();
                string query = @"Update  dbo.Store set StoreName ='" + str.StoreName + @"'
                ,StoreLocation ='" + str.StoreLocation + @"'
                where StoreId=" + str.StoreId + @"
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
                string query = @"delete from dbo.Store where StoreId=" + id;

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
