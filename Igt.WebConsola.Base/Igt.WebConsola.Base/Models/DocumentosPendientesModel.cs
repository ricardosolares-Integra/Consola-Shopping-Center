using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Igt.WebConsola.Base.Models
{
    public class DocumentosPendientesModel
    {
        public string TipoObjeto { get; set; }

        [DataType(DataType.Date)]
        public DateTime FechaDesde { get; set; }
        [DataType(DataType.Date)]
        public DateTime FechaHasta { get; set; }
        public List<SelectListItem> TiposObjetos { get; set; }

        public DataTable Tabla { get; set; }
        public DocumentosPendientesModel()
        {
            TiposObjetos = new List<SelectListItem>();
            Tabla = new DataTable();
            FechaDesde = DateTime.Now;
            FechaHasta = DateTime.Now;
        }
    }
}
