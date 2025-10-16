using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Igt.WebConsola.Base.Utilidades;
using Microsoft.AspNetCore.Mvc;

namespace Igt.WebConsola.Base.Controllers
{
    public class BitacoraController : Controller
    {

        public IActionResult Bitacora()
        {
            Conexiones utilConexiones = new Conexiones();
            Models.BitacoraModel bitacora = new Models.BitacoraModel();
            bitacora.TiposObjetos = utilConexiones.getTipoObjetos();
            return View("Mostrar",bitacora);
        }

        [HttpGet]
        public ActionResult Mostrar(Models.BitacoraModel modelo)
        {
            Conexiones utilConexiones = new Conexiones();
            Models.BitacoraModel bitacora = new Models.BitacoraModel();
            bitacora.TiposObjetos = utilConexiones.getTipoObjetos();

            bitacora.FechaDesde = modelo.FechaDesde;
            bitacora.FechaHasta = modelo.FechaHasta;
            bitacora.ResultadoTransaccion = modelo.ResultadoTransaccion;
            bitacora.TipoObjeto = modelo.TipoObjeto;
            bitacora.TipoOperacion = modelo.TipoOperacion;
            bitacora.TipoTransaccion = modelo.TipoTransaccion;

            bitacora.datosBitacoras = UBitacora.Instance.GetBitacora(modelo);

            return View("Mostrar", bitacora);
        }
    }
}