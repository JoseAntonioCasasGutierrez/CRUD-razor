using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using CRUD.Models;


namespace CRUD.Controllers
{
    public class HomeController : Controller
    {
        public IConfiguration Configuration { get; }
        public HomeController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IActionResult Index()
        {

            List<Empleados> empledosList = new List<Empleados>();
            string connectionString = Configuration["ConnectionStrings:SQLConnection"];

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //SqlDataReader
                connection.Open();

                string sql = "select * from Empleados";
                SqlCommand command = new SqlCommand(sql, connection);

                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        Empleados empledos = new Empleados();
                        empledos.Id = Convert.ToInt32(dataReader["Id"]);
                        empledos.Nombre = Convert.ToString(dataReader["Nombre"]);
                        empledos.Habilidades = Convert.ToString(dataReader["Habilidades"]);
                        empledos.Salario = Convert.ToDecimal(dataReader["Salario"]);
                        empledos.FechaContratacion = Convert.ToDateTime(dataReader["FechaContratacion"]);
                        empledosList.Add(empledos);
                    }
                }
                connection.Close();

            }
            
             return View(empledosList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]

        public IActionResult Create(Empleados empleados)
        {
            if (ModelState.IsValid)
            {
                string connectionString = Configuration["ConnectionStrings:SQLConnection"];
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string sql = $"Insert Into Empleados (Nombre, Habilidades, Salario) Values ('{empleados.Nombre}','{empleados.Habilidades}','{empleados.Salario}')";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.CommandType = CommandType.Text;
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                        connection.Dispose();
                    }
                    return RedirectToAction("Index");

                }
            }
            else
                return View();
        }

        public IActionResult Update(int id)
        {
            string connectionString = Configuration["ConnectionStrings:SQLConnection"];
            Empleados empledos = new Empleados();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"Select * From Empleados Where Id='{id}'";
                SqlCommand command = new SqlCommand(sql, connection);
                connection.Open();
                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        empledos.Id = Convert.ToInt32(dataReader["Id"]);
                        empledos.Nombre = Convert.ToString(dataReader["Nombre"]);
                        empledos.Habilidades  = Convert.ToString(dataReader["Habilidades"]);
                        empledos.Salario = Convert.ToDecimal(dataReader["Salario"]);
                        empledos.FechaContratacion = Convert.ToDateTime(dataReader["FechaContratacion"]);
                        
                    }
                }
                connection.Close();
            }
            return View(empledos);
        }
        [HttpPost]
        [ActionName("Update")]
        public IActionResult Update_Post(Empleados empleados)
        {
            string connectionString = Configuration["ConnectionStrings:SQLConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                string sql = $"Update Empleados SET Nombre='{empleados.Nombre}', Habilidades='{empleados.Habilidades}', Salario='{empleados.Salario}' Where Id='{empleados.Id}'";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            string connectionString = Configuration["ConnectionStrings:SQLConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"Delete From Empleados Where Id='{id}'";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        ViewBag.Result = "Operation got error:" + ex.Message;
                    }
                    connection.Close();
                }
            }
            return RedirectToAction("Index");
        }


        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
