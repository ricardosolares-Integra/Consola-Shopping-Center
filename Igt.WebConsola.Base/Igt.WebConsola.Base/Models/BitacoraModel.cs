using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Igt.WebConsola.Base.Models
{
    public class BitacoraModel
    {


        public string TipoObjeto { get; set; }

        public string TipoOperacion { get; set; }
        public string TipoTransaccion { get; set; }
        public string ResultadoTransaccion { get; set; }
        [DataType(DataType.Date)]
        public DateTime FechaDesde { get; set; }
        [DataType(DataType.Date)]
        public DateTime FechaHasta { get; set; }
        public List<Models.DatosBitacoraModel> datosBitacoras { get; set; }
        public List<SelectListItem> TiposObjetos { get; set; }

        public BitacoraModel()
        {
            datosBitacoras = new List<DatosBitacoraModel>();
            FechaDesde = DateTime.Now;
            FechaHasta = DateTime.Now;

            TiposObjetos = new List<SelectListItem>();
        }

    }
}
