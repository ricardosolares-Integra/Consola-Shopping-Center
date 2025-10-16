using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Igt.WebConsola.Base.Models
{
    public class ParametroGeneralModel
    {
        [Key]
        public int Codigo { get; set; }
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }
        [Display(Name = "Valor")]
        public string Valor { get; set; }
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }
    }
}
