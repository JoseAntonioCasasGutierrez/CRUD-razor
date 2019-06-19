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
using Rotativa.AspNetCore;

namespace CRUD.Controllers
{
    public class HomeController : Controller
    {
        

        public IConfiguration Configuration { get; }
        
        public HomeController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IActionResult About()
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
                    return RedirectToAction("About");

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
            return RedirectToAction("About");
            
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
            return RedirectToAction("About");
        }


        public IActionResult Index()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            List<SalaJuntas> salajuntasList = new List<SalaJuntas>();
            string connectionString = Configuration["ConnectionStrings:SQLConnection"];

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //SqlDataReader
                connection.Open();

                string sql = "select * from SalaJuntas";
                SqlCommand command = new SqlCommand(sql, connection);

                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        SalaJuntas salajuntas = new SalaJuntas();
                        salajuntas.Id = Convert.ToInt32(dataReader["Id"]);
                        salajuntas.Sala = Convert.ToString(dataReader["Sala"]);
                        salajuntas.NombreEmpleado = Convert.ToString(dataReader["NombreEmpleado"]);
                        salajuntas.FechaRecepcion = Convert.ToDateTime(dataReader["FechaRecepcion"]);
                        salajuntas.TotalPersonas = Convert.ToInt32(dataReader["TotalPersonas"]);
                        salajuntas.Horas = Convert.ToInt32(dataReader["Horas"]);

                        salajuntasList.Add(salajuntas);
                    }
                }
                connection.Close();

            }

            return View(salajuntasList);
        }
        public IActionResult Create2()
        {
            return View();
        }
        [HttpPost]

        public IActionResult Create2(SalaJuntas salajuntas)
        {
            if (ModelState.IsValid)
            {
                string connectionString = Configuration["ConnectionStrings:SQLConnection"];
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string cf = salajuntas.FechaRecepcion.ToString("dd/MM/yyyy");
                    string sql = $"Insert Into SalaJuntas (Sala,NombreEmpleado,FechaRecepcion, TotalPersonas,Horas) Values ('{salajuntas.Sala}','{salajuntas.NombreEmpleado}',CONVERT(datetime,'{cf}',103),'{salajuntas.TotalPersonas}','{salajuntas.Horas}')";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.CommandType = CommandType.Text;
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                        connection.Dispose();
                    }
                    return RedirectToAction("Contact");

                }
            }
            else
                return View();
        }

        public IActionResult Update2(int id)
        {
            string connectionString = Configuration["ConnectionStrings:SQLConnection"];
            SalaJuntas salajuntas = new SalaJuntas();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                string sql = $"Select * From SalaJuntas Where Id='{id}'";
                SqlCommand command = new SqlCommand(sql, connection);
                connection.Open();
                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        salajuntas.Id = Convert.ToInt32(dataReader["Id"]);
                        salajuntas.Sala = Convert.ToString(dataReader["Sala"]);
                        salajuntas.NombreEmpleado = Convert.ToString(dataReader["NombreEmpleado"]);
                        salajuntas.FechaRecepcion = Convert.ToDateTime(dataReader["FechaRecepcion"]);
                        salajuntas.TotalPersonas = Convert.ToInt32(dataReader["TotalPersonas"]);
                        salajuntas.Horas = Convert.ToInt32(dataReader["Horas"]);

                    }
                }
                connection.Close();
            }
            return View(salajuntas);
        }
        [HttpPost]
        [ActionName("Update2")]
        public IActionResult Update_Post2(SalaJuntas salajuntas)
        {
            string connectionString = Configuration["ConnectionStrings:SQLConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string cf = salajuntas.FechaRecepcion.ToString("dd/MM/yyyy");
                string sql = $"Update SalaJuntas SET Sala='{salajuntas.Sala}', NombreEmpleado='{salajuntas.NombreEmpleado}',FechaRecepcion=CONVERT(datetime,'{cf}',103),TotalPersonas='{salajuntas.TotalPersonas}',Horas='{salajuntas.Horas}' Where Id='{salajuntas.Id}'";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return RedirectToAction("Contact");
        }
        [HttpPost]
        public IActionResult Delete2(int id)
        {
            string connectionString = Configuration["ConnectionStrings:SQLConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"Delete From SalaJuntas Where Id='{id}'";
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
            return RedirectToAction("Contact");
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
        public IActionResult About2()
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

            
            return new ViewAsPdf("About", empledosList);
        }
        public IActionResult Contact2()
        {

            List<SalaJuntas> salajuntasList = new List<SalaJuntas>();
            string connectionString = Configuration["ConnectionStrings:SQLConnection"];

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //SqlDataReader
                connection.Open();

                string sql = "select * from SalaJuntas";
                SqlCommand command = new SqlCommand(sql, connection);

                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        SalaJuntas salajuntas = new SalaJuntas();
                        salajuntas.Id = Convert.ToInt32(dataReader["Id"]);
                        salajuntas.Sala = Convert.ToString(dataReader["Sala"]);
                        salajuntas.NombreEmpleado = Convert.ToString(dataReader["NombreEmpleado"]);
                        salajuntas.FechaRecepcion = Convert.ToDateTime(dataReader["FechaRecepcion"]);
                        salajuntas.TotalPersonas = Convert.ToInt32(dataReader["TotalPersonas"]);
                        salajuntas.Horas = Convert.ToInt32(dataReader["Horas"]);

                        salajuntasList.Add(salajuntas);
                    }
                }
                connection.Close();

            }


            return new ViewAsPdf("Contact", salajuntasList);
        }




    }
}
