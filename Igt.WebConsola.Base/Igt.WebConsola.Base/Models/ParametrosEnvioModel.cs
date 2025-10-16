using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igt.WebConsola.Base.Models
{
    public class ParametrosEnvioModel
    {
        public int IdServicio { get; set; }
        public string Ejecucion { get; set; }
        public List<SelectListItem> ListaDiasEjecucion { get; set; }
        public string DiasEjecucion { get; set; }
        public string TipoEnvio { get; set; }
        public List<string> Horarios { get; set; }
        public string Horario { get; set; }
        public int TiempoEspera { get; set; }


        public ParametrosEnvioModel()
        {
            IdServicio = -1;
            TiempoEspera = 60;
            DiasEjecucion = "1,1,1,1,1,1,1";
            Horarios = new List<string>();
            ListaDiasEjecucion = new List<SelectListItem>();
            ListaDiasEjecucion.Add(new SelectListItem("Domingo", "0", true));
            ListaDiasEjecucion.Add(new SelectListItem("Lunes", "1", true));
            ListaDiasEjecucion.Add(new SelectListItem("Martes", "2", true));
            ListaDiasEjecucion.Add(new SelectListItem("Miercoles", "3", true));
            ListaDiasEjecucion.Add(new SelectListItem("Jueves", "4", true));
            ListaDiasEjecucion.Add(new SelectListItem("Viernes", "5", true));
            ListaDiasEjecucion.Add(new SelectListItem("Sabado", "6", true));
        }

        public void CargarDias()
        {
            ListaDiasEjecucion = new List<SelectListItem>();
            ListaDiasEjecucion.Add(new SelectListItem("Domingo", "0", true));
            ListaDiasEjecucion.Add(new SelectListItem("Lunes", "1", true));
            ListaDiasEjecucion.Add(new SelectListItem("Martes", "2", true));
            ListaDiasEjecucion.Add(new SelectListItem("Miercoles", "3", true));
            ListaDiasEjecucion.Add(new SelectListItem("Jueves", "4", true));
            ListaDiasEjecucion.Add(new SelectListItem("Viernes", "5", true));
            ListaDiasEjecucion.Add(new SelectListItem("Sabado", "6", true));
        }
    }
}
