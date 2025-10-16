using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Igt.WebConsola.Base.Models;
using Igt.WebConsola.Base.Utilidades;

namespace Igt.WebConsola.Base.Controllers
{
    public class ParametrosGeneralesController : Controller
    {
        Utilidades.Conexiones conexiones;
        // GET: SalidaMercancia
        public ActionResult Index()
        {
            try
            {
                IEnumerable<Models.ParametroGeneralModel> modelo = UParametrosGenerales.Instance.GetConfiguracionParametroGeneral();
                return View(modelo);
            }
            catch (Exception ex)
            {
                ErrorViewModel errorViewModel = new ErrorViewModel();
                errorViewModel.RequestId = ex.Message;
                return View("~/Views/Shared/Error.cshtml", errorViewModel);
            }

        }

        // GET: SalidaMercancia/Details/5
        public ActionResult Details(string id)
        {
            try
            {
                Models.ParametroGeneralModel modelo = UParametrosGenerales.Instance.GetConfiguracionParametroGeneral(id);
                return View("Details", modelo);
            }
            catch (Exception ex)
            {
                ErrorViewModel errorViewModel = new ErrorViewModel();
                errorViewModel.RequestId = ex.Message;
                return View("~/Views/Shared/Error.cshtml", errorViewModel);

            }
        }

        // GET: SalidaMercancia/Create
        public ActionResult Create()
        {
            ParametroGeneralModel model = new ParametroGeneralModel();
            return View("Create",model);
        }

        // POST: SalidaMercancia/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Models.ParametroGeneralModel model)
        {
            if (ModelState.IsValid)
            {

                string Resultado = string.Empty;
                try
                {

                    Resultado = UParametrosGenerales.Instance.AddConfiguracionParametroGeneral(model);
                    if (!Resultado.Contains("Error"))
                    {
                        TempData["SM"] = "Datos de intermedia almacenados correctamente";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["EM"] = Resultado;
                        return View("Create", model);
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
                TempData["EM"] = "Ocurrio un problema con los datos ingresados." +
                    "\n Por favor verifique los datos.";
                return View("Create", model);
            }



        }

        // GET: SalidaMercancia/Edit/5
        public ActionResult Edit(string id)
        {

            try
            {
                Models.ParametroGeneralModel modelo = UParametrosGenerales.Instance.GetConfiguracionParametroGeneral(id);
                return View("Edit", modelo);
            }
            catch (Exception ex)
            {
                ErrorViewModel errorViewModel = new ErrorViewModel();
                errorViewModel.RequestId = ex.Message;
                return View("~/Views/Shared/Error.cshtml", errorViewModel);

            }

        }

        // POST: SalidaMercancia/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, Models.ParametroGeneralModel collection)
        {
            try
            {
                // TODO: Add update logic here
                string Resultado;
                collection.Codigo = Convert.ToInt32(id);
                Resultado = UParametrosGenerales.Instance.UpdateConfiguracionParametroGeneral(id, collection);
                if (!Resultado.Contains("Error"))
                {
                    TempData["SM"] = "Datos editados Exitosamente";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["EM"] = Resultado;
                    return RedirectToAction("Edit", collection);
                }
            }
            catch (Exception ex)
            {
                ErrorViewModel errorViewModel = new ErrorViewModel();
                errorViewModel.RequestId = ex.Message;
                return View("~/Views/Shared/Error.cshtml", errorViewModel);
            }
        }

        // GET: SalidaMercancia/Delete/5
        public ActionResult Delete(string id)
        {
            try
            {
                Models.ParametroGeneralModel modelo = UParametrosGenerales.Instance.GetConfiguracionParametroGeneral(id);
                return View("Delete", modelo);
            }
            catch (Exception ex)
            {
                ErrorViewModel errorViewModel = new ErrorViewModel();
                errorViewModel.RequestId = ex.Message;
                return View("~/Views/Shared/Error.cshtml", errorViewModel);

            }
        }

        // POST: SalidaMercancia/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id, Models.ParametroGeneralModel collection)
        {
            try
            {
                // TODO: Add update logic here
                string Resultado;
                collection.Codigo = Convert.ToInt32(id);
                Resultado = UParametrosGenerales.Instance.DeleteConfiguracionParametroGeneral(id);
                if (!Resultado.Contains("Error"))
                {
                    TempData["SM"] = "Configuracion eliminada Exitosamente";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["EM"] = Resultado;
                    return RedirectToAction("Delete", collection);
                }
            }
            catch (Exception ex)
            {
                ErrorViewModel errorViewModel = new ErrorViewModel();
                errorViewModel.RequestId = ex.Message;
                return View("~/Views/Shared/Error.cshtml", errorViewModel);
            }
        }
    }
}