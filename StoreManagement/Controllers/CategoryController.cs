using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using StoreManagement.Models;

namespace StoreManagement.Controllers
{
    public class CategoryController : ApiController
    {


        SqlConnection connection;
        SqlCommand command;
        SqlDataAdapter dataAdapter;
        [ActionName("Get")]
        public HttpResponseMessage Get()
        {
            //            DataTable table = new DataTable();
            //            string query = @"Select DepartmentId,DepartmentName from
            //dbo.Department";
            //            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeDb"].ConnectionString))
            //            using (var cmd = new SqlCommand(query, con))
            //            using (var da = new SqlDataAdapter(cmd))
            //            {
            //                cmd.CommandType = CommandType.Text;
            //                da.Fill(table);
            //            }
            //            return Request.CreateResponse(HttpStatusCode.OK, table);

            DataTable dataTable = new DataTable();
            string query = @"Select * from Category";
            connection = new SqlConnection(ConfigurationManager.ConnectionStrings["StoreDb"].ConnectionString);
            command = new SqlCommand(query, connection);
            dataAdapter = new SqlDataAdapter(command);
            command.CommandType = CommandType.Text;
            dataAdapter.Fill(dataTable);
            return Request.CreateResponse(HttpStatusCode.OK, dataTable);

        }

        public string Post(Category cat)
        {
            try
            {

                DataTable dataTable = new DataTable();
                string query = @"insert into dbo.Category values('" + cat.CategoryName + @"')
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
        public string Put(Category cat)
        {
            try
            {

                DataTable dataTable = new DataTable();
                string query = @"Update  dbo.Category set CategoryName ='" + cat.CategoryName + @"'
                            where CategoryId=" + cat.CategoryId + @"
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
                string query = @"delete from dbo.Category where CategoryId=" + id;

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