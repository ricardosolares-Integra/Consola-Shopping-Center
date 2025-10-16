using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Igt.WebConsola.Base.Models;
using Igt.WebConsola.Base.Utilidades;

namespace Igt.WebConsola.Base.Controllers
{
    public class DocumentosPendientesController : Controller
    {
        public ActionResult DocumentosPendientes()
        {
            Conexiones utilConexiones = new Conexiones();
            DocumentosPendientesModel documentos= new DocumentosPendientesModel();
            documentos.TiposObjetos = utilConexiones.getTipoObjetos();
            return View("DocumentosPendientes", documentos);
        }

        [HttpGet]
        public ActionResult Mostrar(DocumentosPendientesModel datos)
        {
            Conexiones utilConexiones = new Conexiones();
            DocumentosPendientesModel documentos = new DocumentosPendientesModel();
            documentos.TiposObjetos = utilConexiones.getTipoObjetos();

            documentos.FechaDesde = datos.FechaDesde;
            documentos.FechaHasta = datos.FechaHasta;
            documentos.TipoObjeto = datos.TipoObjeto;

            documentos.Tabla = UDocumentosPendientes.Instance.getDocumentosPendientes(datos);
           

            return View("DocumentosPendientes",documentos);
        }
    }
}