using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Igt.WebConsola.Base.Models;
using Igt.WebConsola.Base.Utilidades;

namespace Igt.WebConsola.Base.Controllers
{
    public class SAPController : Controller
    {
        Utilidades.Conexiones conexiones;
        Models.ConfiguracionDBModel ModeloSAP;
        public ActionResult SAP()
        {
            conexiones = new Conexiones();
            ModeloSAP = new ConfiguracionDBModel();
            try
            {
                ModeloSAP = conexiones.SAPModel("A");
                if (ModeloSAP.Equals(null))
                {
                    return View(ModeloSAP);
                }
                else if (ModeloSAP.Equals(null))
                {
                    ErrorViewModel errorViewModel = new ErrorViewModel();
                    errorViewModel.RequestId = "Ocurrio un error al obtener los datos de conexion. /nx Por favor contactese con su proveedor.";
                    return View("~/Views/Shared/Error.cshtml", errorViewModel);
                }
                else
                {
                    return View(ModeloSAP);
                }
            }
            catch (Exception ex)
            {

                ErrorViewModel errorViewModel = new ErrorViewModel();
                errorViewModel.RequestId = ex.Message;
                return View("~/Views/Shared/Error.cshtml", errorViewModel);
            }
        }

        [HttpPost]
        public ActionResult SaveSap(ConfiguracionDBModel objSave)
        {
            if (ModelState.IsValid)
            {
                conexiones = new Conexiones();
                ModeloSAP = new ConfiguracionDBModel();
                string Resultado;
                try
                {
                    Resultado = conexiones.AlmacenarSap(objSave);
                    if (Resultado.Contains("Exito"))
                    {
                        TempData["EM"] = "Datos SAP almacenados correctamente";
                        return RedirectToAction("SAP");
                    }
                    else
                    {
                        TempData["EM"] = Resultado;
                        return View("SAP", objSave);
                    }
                }
                catch (Exception ex)
                {
                    ErrorViewModel errorViewModel = new ErrorViewModel();
                    errorViewModel.RequestId = ex.Message;
                    return View("~/Views/Shared/Error.cshtml", errorViewModel);
                }
            }
            else
            {
                TempData["EM"] = "Ocurrio un problema con la conexion hacia SAP." +
                    "\n Verifique los datos ingresados.";
                return View("SAP", objSave);
            }
        }


    }
}