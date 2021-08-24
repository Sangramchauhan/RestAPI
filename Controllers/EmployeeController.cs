using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestFulAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Net.Http;
using System.Web.Http.Description;
using System.Net;
using System.Text;
using System.Diagnostics;

namespace RestFulAPI.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class EmployeeController : Controller
    {
        MySqlConnection conn = new MySqlConnection(GetConfiguration().GetSection("ConnectionString").Value);
        public IActionResult Index()
        {
            return View();
        }

        

        [HttpGet]
        public EmployeeList Get()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            EmployeeList emp = new EmployeeList();
            conn.Open();
            string sql = "SELECT * FROM Employees";

            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();



            while (rdr.Read())
            {
                emp.EmpList.Add(new EmployeeModel { EmpId = Convert.ToString(rdr[0]), EmpName = Convert.ToString(rdr[1]) });

            }
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);


            emp.status = new Responses();
            emp.status.statusCode = 200;
            emp.status.message = "Fetched employees records successfully";
            emp.status.time = elapsedTime;
            rdr.Close();
            conn.Close();
            return emp;
        }

        [HttpGet("/api")]
        public Responses DefaultGet()
        {
            Stopwatch stopWatch = new Stopwatch();
            Responses res = new Responses();
            
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);
            res.statusCode = 200;
            res.message = "OK";
            res.time = elapsedTime;
            return res;
        }

        [HttpGet("/api/[controller]/{id}")]
        public EmployeeList Get(int Id)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            EmployeeList emp = new EmployeeList();
            conn.Open();
            string sql = "SELECT * FROM Employees WHERE EmpId = " + Id;
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                emp.EmpList.Add(new EmployeeModel { EmpId = Convert.ToString(rdr[0]), EmpName = Convert.ToString(rdr[1]) });
            }

            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);

            emp.status = new Responses();
            emp.status.statusCode = 200;
            emp.status.message = "Fetched employee records successfully";
            emp.status.time = elapsedTime;
            rdr.Close();
            conn.Close();
            return emp;
        }

        [HttpPost("/api/[controller]")]
        public Responses Post([FromBody] EmployeeModel employee)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            Responses result = new Responses();
            try
            {
                conn.Open();
                string sql = "INSERT INTO Employees VALUES(" + Convert.ToInt32(employee.EmpId) + ", '" + employee.EmpName + "');";

                string[] str = { "Ant-man", "Black Panther", "Captain America", "Iron Man", "Doctor Strange", "Falcon", "Spiderman", "Hulk", "Loki", "Thor", "Groot", "Thanos", "QuickSilver" };

                for (int i = 1; i <= 1200; i++)
                {
                    Random randomValue = new Random();
                    sql += "INSERT INTO Employees VALUES(" + Convert.ToInt32(i) + ", '" + str[randomValue.Next(0, str.Length)] + "');";
                }

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                rdr.Close();
                conn.Close();
            }
            catch (Exception ex)
            {
                result.statusCode = 403;
                result.message = ex.Message.ToString();
            }
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);
            result.statusCode = 201;
            result.message = "Inserted employee details successfully";
            result.time = elapsedTime;
            return result;
        }

        [HttpPut("/api/[controller]")]
        public Responses Put([FromBody] EmployeeModel employee)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            Responses res = new Responses();
            try
            {
                conn.Open();
                string sql = "UPDATE Employees SET EmpName = '" + employee.EmpName + "' WHERE EmpId = " + Convert.ToString(employee.EmpId) + "";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                rdr.Close();
                conn.Close();
            }
            catch (Exception ex)
            {
                res.statusCode = 403;
                res.message = ex.Message.ToString();
            }
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);
            res.statusCode = 200;
            res.message = "Employee details are updated successfully";
            res.time = elapsedTime;
            return res;
        }

        [HttpDelete("/api/[controller]/{id}")]
        public Responses Delete(int Id)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            Responses res = new Responses();
            try
            {
                conn.Open();
                string sql = "DELETE FROM Employees WHERE EmpId = " + Id + "";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                rdr.Close();
                conn.Close();
            }
            catch (Exception ex)
            {
                res.statusCode = 403;
                res.message = ex.Message.ToString();
            }
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);
            res.statusCode = 200;
            res.message = "Employee details are deleted successfully";
            res.time = elapsedTime;
            return res;
        }

        public static IConfigurationRoot GetConfiguration()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            return builder.Build();
        }
    }
}
