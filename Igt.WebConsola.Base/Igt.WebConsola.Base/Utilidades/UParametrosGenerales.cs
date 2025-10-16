using Igt.WebConsola.Base.Models;
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
    public class UParametrosGenerales
    {

        private readonly static UParametrosGenerales _instance = new UParametrosGenerales();

        private Conexiones utilConexiones;

        private UParametrosGenerales()
        {
            utilConexiones = new Conexiones();
        }
        public static UParametrosGenerales Instance
        {
            get
            {
                return _instance;
            }
        }

        /// <summary>
        /// Obtiene listado de configuraciones para las salidas de mercancia
        /// </summary>
        /// <returns></returns>
        public List<Models.ParametroGeneralModel> GetConfiguracionParametroGeneral()
        {
            List<Models.ParametroGeneralModel> parametroGenerals = null;
            HttpWebResponse CreateResponse = null;
            string actual;
            try
            {
                Uri Url = new Uri(utilConexiones.UrlWebServices() + "/IGT/Consola/GetConfiguracionParametroGeneral");
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
                    parametroGenerals = ParametroGeneralList(actual);
                }
                return parametroGenerals;

            }
            catch (Exception)
            {

                throw;
            }

        }

        /// <summary>
        /// Unicamente crea la lista de las configuraciones de las salidas de mercancia
        /// </summary>
        /// <param name="Parametros"></param>
        /// <returns></returns>
        public List<Models.ParametroGeneralModel> ParametroGeneralList(string Parametros)
        {
            List<Models.ParametroGeneralModel> ListadoConfiguraciones;
            Models.ParametroGeneralModel Modelo;
            try
            {
                if (!string.IsNullOrEmpty(Parametros))
                {
                    ListadoConfiguraciones = new List<Models.ParametroGeneralModel>();
                    JArray jsonPreservar = JArray.Parse(Parametros);
                    foreach (JObject jsonOperaciones in jsonPreservar.Children<JObject>())
                    {
                        Modelo = new Models.ParametroGeneralModel();
                        Modelo.Codigo = (int)jsonOperaciones["Codigo"];
                        Modelo.Nombre = (string)jsonOperaciones["Nombre"];
                        Modelo.Valor = (string)jsonOperaciones["Valor"];
                        Modelo.Descripcion = (string)jsonOperaciones["Descripcion"];
                        ListadoConfiguraciones.Add(Modelo);
                    }
                }
                else
                {
                    ListadoConfiguraciones = new List<Models.ParametroGeneralModel>();
                }

                return ListadoConfiguraciones;
            }
            catch (Exception)
            {

                throw;
            }


        }

        /// <summary>
        /// Obtiene una configuracion filtrada por ID
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public Models.ParametroGeneralModel GetConfiguracionParametroGeneral(string Id)
        {
            Models.ParametroGeneralModel modelo = null;
            HttpWebResponse CreateResponse = null;
            string actual;
            try
            {
                Uri Url = new Uri(utilConexiones.UrlWebServices() + "/IGT/Consola/GetConfiguracionParametroGeneralID");
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(Url);
                httpWebRequest.ContentType = "application/json; charset=utf-8";
                httpWebRequest.Method = "GET";
                httpWebRequest.Headers.Add("Id", Id);

                CreateResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(CreateResponse.GetResponseStream()))
                {
                    actual = streamReader.ReadToEnd();
                }
                if (CreateResponse.StatusCode.Equals(HttpStatusCode.OK))
                {

                    if (!string.IsNullOrEmpty(actual))
                    {
                        JObject jObject = JObject.Parse(actual);

                        modelo = new Models.ParametroGeneralModel();

                        modelo.Codigo = (int)jObject["Codigo"];
                        modelo.Nombre = (string)jObject["Nombre"];
                        modelo.Descripcion = (string)jObject["Descripcion"];
                        modelo.Valor = (string)jObject["Valor"]; ;
                    }
                    else
                    {
                        modelo = new Models.ParametroGeneralModel();
                    }

                }
                return modelo;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Agrega una configuracion de una salida de mercancia
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string AddConfiguracionParametroGeneral(Models.ParametroGeneralModel model)
        {
            HttpWebResponse CreateResponse = null;
            string actual;
            try
            {
                Uri Url = new Uri(utilConexiones.UrlWebServices() + "/IGT/Consola/AddConfiguracionParametroGeneral");
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

        /// <summary>
        /// Actualizar una ConfiguracionSalidaMercancia
        /// </summary>
        /// <returns></returns>
        public string UpdateConfiguracionParametroGeneral(string Id, Models.ParametroGeneralModel modelo)
        {
            HttpWebResponse CreateResponse = null;
            string actual;
            try
            {
                Uri Url = new Uri(utilConexiones.UrlWebServices() + "/IGT/Consola/EditConfiguracionParametroGeneral");
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(Url);
                httpWebRequest.ContentType = "application/json; charset=utf-8";
                httpWebRequest.Method = "PUT";
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    streamWriter.Write(Json(modelo));
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

        /// <summary>
        /// Eliminar una configuracion de Salidas de mercancia
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string DeleteConfiguracionParametroGeneral(string Id)
        {
            HttpWebResponse CreateResponse = null;
            string actual;
            try
            {
                Uri Url = new Uri(utilConexiones.UrlWebServices() + "/IGT/Consola/DeleteConfiguracionParametroGeneral");
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(Url);
                httpWebRequest.ContentType = "application/json; charset=utf-8";
                httpWebRequest.Method = "DELETE";
                httpWebRequest.Headers.Add("Id", Id.ToString());
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


        /// <summary>
        /// Metodo que serializa un Objeto en formato Json
        /// </summary>
        /// <param name="Temp"></param>
        /// <returns></returns>
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
