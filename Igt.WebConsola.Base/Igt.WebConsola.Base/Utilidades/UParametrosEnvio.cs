using Igt.WebConsola.Base.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Igt.WebConsola.Base.Utilidades
{
    public class UParametrosEnvio
    {
        private readonly static UParametrosEnvio _instance = new UParametrosEnvio();

        private Conexiones utilConexiones;

        private UParametrosEnvio()
        {
            utilConexiones = new Conexiones();
        }
        public static UParametrosEnvio Instance
        {
            get
            {
                return _instance;
            }
        }

        public ParametrosEnvioModel GetConfiguracionParametroGeneral()
        {
            ParametrosEnvioModel modelo = new ParametrosEnvioModel();
            HttpWebResponse CreateResponse = null;
            string actual;
            try
            {
                Uri Url = new Uri(utilConexiones.UrlWebServices() + "/IGT/Consola/GetParametrosEnvio");
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(Url);
                httpWebRequest.ContentType = "application/json; charset=utf-8";
                httpWebRequest.Method = "GET";

                CreateResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(CreateResponse.GetResponseStream()))
                {
                    actual = streamReader.ReadToEnd();
                }
                if (CreateResponse.StatusCode.Equals(HttpStatusCode.OK))
                {
                    if (!string.IsNullOrEmpty(actual) && !actual.Equals("{}"))
                    {
                        JObject jObject = JObject.Parse(actual);


                        modelo.IdServicio = (int)jObject["IdServicio"];
                        modelo.TipoEnvio = (string)jObject["TipoEnvio"];
                        modelo.Ejecucion = (string)jObject["Ejecucion"];
                        modelo.ListaDiasEjecucion = ValidarListaDias(modelo.ListaDiasEjecucion,(string)jObject["DiasEjecucion"]);
                        modelo.Horario = (string)jObject["Horario"];
                        modelo.Horarios = modelo.Horario.Split(',').ToList();

                        modelo.TiempoEspera = (int)jObject["TiempoEspera"];
                    }
                    else
                    {
                        modelo = new Models.ParametrosEnvioModel();
                    }
                }
                return modelo;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<SelectListItem> ValidarListaDias(List<SelectListItem> lista, string dias)
        {
            string[] split = dias.Split(',');
            for(int i=0;i<=6;i++)
            {
                if(split[i].Equals("1"))
                {
                    lista[i].Selected = true;
                }
                else
                {
                    lista[i].Selected = false;
                }
            }

            return lista;
        }

        public string setParametrosEnvio(ParametrosEnvioModel model)
        {
            HttpWebResponse CreateResponse = null;
            string actual;
            try
            {
                Uri Url = new Uri(utilConexiones.UrlWebServices() + "/IGT/Consola/SetParametrosEnvio");
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(Url);
                httpWebRequest.ContentType = "application/json; charset=utf-8";
                httpWebRequest.Method = "POST";
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string dummy = Json(model);
                    streamWriter.Write(Json(model));
                    streamWriter.Flush();
                    streamWriter.Close();
                }
                CreateResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(CreateResponse.GetResponseStream()))
                {
                    actual = streamReader.ReadToEnd();
                }
                return actual;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string updateParametrosEnvio(ParametrosEnvioModel model)
        {
            HttpWebResponse CreateResponse = null;
            string actual;
            try
            {
                Uri Url = new Uri(utilConexiones.UrlWebServices() + "/IGT/Consola/UpdateParametrosEnvio");
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(Url);
                httpWebRequest.ContentType = "application/json; charset=utf-8";
                httpWebRequest.Method = "PUT";
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string dummy = Json(model);
                    streamWriter.Write(Json(model));
                    streamWriter.Flush();
                    streamWriter.Close();
                }
                CreateResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(CreateResponse.GetResponseStream()))
                {
                    actual = streamReader.ReadToEnd();
                }
                return actual;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }



        private string Json(object Temp)
        {
            string json;
            try
            {
                json = JsonConvert.SerializeObject(Temp, Newtonsoft.Json.Formatting.Indented,
                             new JsonSerializerSettings
                             {
                                 Formatting = Newtonsoft.Json.Formatting.Indented,
                                 ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore,
                                 NullValueHandling = NullValueHandling.Ignore, //used to remove empty or null properties
                                 DefaultValueHandling = DefaultValueHandling.Ignore
                             });

                return json;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
