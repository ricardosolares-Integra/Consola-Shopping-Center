using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Igt.WebConsola.Base.Models;
using Igt.WebConsola.Base.Utilidades;

namespace Igt.WebConsola.Base.Controllers
{
    public class ParametrosEnvioController : Controller
    {
        public IActionResult Index()
        {
            ParametrosEnvioModel model = new ParametrosEnvioModel();
            model = UParametrosEnvio.Instance.GetConfiguracionParametroGeneral();
            if (model.TipoEnvio == null)
            {
                ViewBag.Accion = "Crear";
                model.Horarios.Add(string.Empty);
            }
            else
                ViewBag.Accion = "Ver";
            return View(model);
        }

        public IActionResult AgregarHorario(ParametrosEnvioModel model)
        {
            model.CargarDias();
            model.Horarios.Add(string.Empty);
            ViewBag.Accion = "Editar";
            return View("Index", model);
        }

        public IActionResult RemoverHorario(ParametrosEnvioModel model,int id)
        {
            model.CargarDias();
            model.Horarios.RemoveAt(id);
            ViewBag.Accion = "Editar";
            return View("Index", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ParametrosEnvioModel model)
        {
            ViewBag.Accion = "Crear";
            string resultado = string.Empty;
            try
            {
                if (string.IsNullOrEmpty(model.Horario))
                    model.Horario = DateTime.Now.ToString("h:mm");

                model.DiasEjecucion = string.Empty;
                for(int i = 0; i <= 6; i++)
                {
                    if (model.ListaDiasEjecucion[i].Selected)
                    {
                        model.DiasEjecucion += "1,";
                    }
                    else
                    {
                        model.DiasEjecucion += "0,";
                    }
                }
                model.DiasEjecucion = model.DiasEjecucion.TrimEnd(',');

                model.Horario = string.Empty;
                foreach(string item in model.Horarios)
                {
                    model.Horario += item + ",";
                }
                model.Horario = model.Horario.TrimEnd(',');

                if (ModelState.IsValid)
                {
                    resultado = UParametrosEnvio.Instance.setParametrosEnvio(model);
                    if (!resultado.ToUpper().Contains("ERROR"))
                    {
                        ViewBag.Accion = "Ver";

                        TempData["SM"] = "Parametros de Envio Guardados con Exito. "+resultado;
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["EM"] = "Error al guardar datos, no se guardo ningun dato. Error: "+resultado;
                    }
                }
                else
                {
                    TempData["EM"] = "Error, Datos incompletos";
                }
            }
            catch (Exception ex)
            {
                TempData["EM"] = "Error Inesperado: " + ex.Message;
            }

            model.CargarDias();
            return View("Index", model);
        }

        public IActionResult Editar(string id)
        {
            ParametrosEnvioModel model = new ParametrosEnvioModel();
            model = UParametrosEnvio.Instance.GetConfiguracionParametroGeneral();

                ViewBag.Accion = "Editar";
            return View("Index",model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(ParametrosEnvioModel model)
        {
            ViewBag.Accion = "Editar";
            string resultado = string.Empty;
            try
            {
                if (string.IsNullOrEmpty(model.Horario))
                    model.Horario = DateTime.Now.ToString("h:mm");

                model.DiasEjecucion = string.Empty;
                for (int i = 0; i <= 6; i++)
                {
                    if (model.ListaDiasEjecucion[i].Selected)
                    {
                        model.DiasEjecucion += "1,";
                    }
                    else
                    {
                        model.DiasEjecucion += "0,";
                    }
                }
                model.DiasEjecucion = model.DiasEjecucion.TrimEnd(',');

                model.Horario = string.Empty;
                foreach (string item in model.Horarios)
                {
                    model.Horario += item + ",";
                }
                model.Horario= model.Horario.TrimEnd(',');

                if (ModelState.IsValid)
                {
                    resultado = UParametrosEnvio.Instance.updateParametrosEnvio(model);
                    if (!resultado.ToUpper().Contains("ERROR"))
                    {
                        ViewBag.Accion = "Ver";
                        TempData["SM"] = "Parametros de Envio Guardados con Exito. " + resultado;
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["EM"] = "Error al guardar datos, no se guardo ningun dato. Error: " + resultado;
                    }
                }
                else
                {
                    TempData["EM"] = "Error, Datos incompletos";
                }
            }
            catch (Exception ex)
            {
                TempData["EM"] = "Error Inesperado: " + ex.Message;
            }

            model.CargarDias();
            return View("Index", model);
        }

    }
}