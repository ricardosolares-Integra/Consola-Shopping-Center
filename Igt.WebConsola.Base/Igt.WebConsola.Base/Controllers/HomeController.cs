using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Igt.WebConsola.Base.Models;
using Igt.WebConsola.Base.Utilidades;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Igt.WebConsola.Base.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public List<SelectListItem> listaObj()
        {
            Conexiones utilConexiones = new Conexiones();
            return utilConexiones.getTipoObjetos();
        }

        public string EnvioManual(string id)
        {
            string result = UEnvioManual.Instance.EnvioManual(id);
            if (!result.ToUpper().Contains("ERROR"))
                result = "El Envío Manual ha sido activado exitosamente";
            return result;
        }
    }
}
