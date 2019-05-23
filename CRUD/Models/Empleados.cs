using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD.Models
{
    public class Empleados{
        
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Habilidades { get; set; }
        public decimal Salario { get; set; }
        public DateTime FechaContratacion { get; set; }
    }
}
