using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CrudMVC.Models.ViewModels
{
    public class TablaViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name="Nombre")]
        public string Nombre { get; set; }

        [Required]
        [StringLength(50)]
        [EmailAddress]
        [Display(Name = "Correo Electronico")]
        public string Correo { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "fecha_nacimiento")]
        public DateTime fecha_nacimiento { get; set;}
    }
}