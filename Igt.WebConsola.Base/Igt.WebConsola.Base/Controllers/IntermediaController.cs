using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Igt.WebConsola.Base.Models;
using Igt.WebConsola.Base.Utilidades;

namespace Igt.WebConsola.Base.Controllers
{
    
    public class IntermediaController : Controller
    {
        Utilidades.Conexiones conexiones;
        Models.ConfiguracionDBModel ModeloIntermedia;
        public ActionResult Intermedia()
        {
            conexiones = new Conexiones();
            ModeloIntermedia = new ConfiguracionDBModel();
            try
            {
                ModeloIntermedia = conexiones.IntermediaModel();
                return View(ModeloIntermedia);
            }
            catch (Exception ex)
            {
                ErrorViewModel errorViewModel = new ErrorViewModel();
                errorViewModel.RequestId = ex.Message;
                return View("~/Views/Shared/Error.cshtml", errorViewModel);
            }
        }
        [HttpPost]
        public ActionResult Save(ConfiguracionDBModel objSave)
        {
            if (ModelState.IsValid)
            {
                conexiones = new Conexiones();
                ModeloIntermedia = new ConfiguracionDBModel();
                string Resultado;
                try
                {
                    Resultado = conexiones.CrearIntermedia(objSave);
                    if (Resultado.Contains("Exito"))
                    {
                        TempData["EM"] = "Datos de intermedia almacenados correctamente";
                        return RedirectToAction("Intermedia");
                    }
                    else
                    {
                        ErrorViewModel errorViewModel = new ErrorViewModel();
                        errorViewModel.RequestId = Resultado;
                        return View("~/Views/Shared/Error.cshtml", errorViewModel);
                    }
                }
                catch (Exception ex)
                {
                    ErrorViewModel errorViewModel = new ErrorViewModel();
                    errorViewModel.RequestId = ex.Message;
                    return View("~/Views/Shared/Error.cshtml",errorViewModel);
                }
            }
            else
            {
                TempData["EM"] = "Ocurrio un problema con la conexion a la intermedia."+
                    "\n Verifique los datos ingresados.";
                return View("Intermedia",objSave);
            }
          
           
        }
        [HttpPost]
        public ActionResult Editar(ConfiguracionDBModel objDraft)
        {
            if (ModelState.IsValid)
            {
                conexiones = new Conexiones();
                ModeloIntermedia = new ConfiguracionDBModel();
                string Resultado;
                try
                {
                    Resultado = conexiones.EditarIntermedia(objDraft);
                    if (Resultado.Contains("Exito"))
                    {
                        TempData["EM"] = "Datos de intermedia editados correctamente";
                        return RedirectToAction("Intermedia");
                    }
                    else
                    {
                        ErrorViewModel errorViewModel = new ErrorViewModel();
                        errorViewModel.RequestId = Resultado;
                        return View("~/Views/Shared/Error.cshtml", errorViewModel);
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
                TempData["EM"] = "Ocurrio un problema en la modificacion de los datos para la intermedia." +
                                 "\n Verifique los datos ingresados.";
                return View("Intermedia", objDraft);
            }
            
        }

    }
}