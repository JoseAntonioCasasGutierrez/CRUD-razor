using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD.Models
{
    public class Empleados{
      
        public int Id { get; set; }
        [Required(ErrorMessage = "Ingresa el nombre del empleado.")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Ingresa el habilidades del empleado.")]
        public string Habilidades { get; set; }
        [Required(ErrorMessage = "Ingresa el salario del empleado.")]
        public decimal Salario { get; set; }
        public DateTime FechaContratacion { get; set; }
    }
}
