using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD.Models
{
    public class SalaJuntas{
        public int Id { get; set; }
        [Required(ErrorMessage = "Ingresa la sala.")]
        public string Sala { get; set; }
        [Required(ErrorMessage = "Ingresa el nombre del empleado.")]
        public string NombreEmpleado { get; set; }
        [Required]
        public DateTime FechaRecepcion { get; set; }
        [Required(ErrorMessage = "Ingresa el total de personas .")]
        public int TotalPersonas { get; set; }
        [Required(ErrorMessage = "Ingresa el total de horas .")]
        public int Horas { get; set; }
        public string CFechaRecepcion { get; set; }

       


    }
}
