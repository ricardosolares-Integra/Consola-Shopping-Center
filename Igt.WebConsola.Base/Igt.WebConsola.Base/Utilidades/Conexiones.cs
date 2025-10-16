using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Xml;

namespace Igt.WebConsola.Base.Utilidades
{
    public class Conexiones
    {

        /// <summary>
        /// Almacena los datos de conexion a SAP
        /// </summary>
        /// <param name="ModeloSap"></param>
        /// <returns></returns>
        public string AlmacenarSap(Models.ConfiguracionDBModel ModeloSap)
        {
            HttpWebResponse CreateResponse = null;
            string actual;
            try
            {
                Uri Url = new Uri(UrlWebServices() + "/IGT/Consola/GuardarSap");
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(Url);
                httpWebRequest.ContentType = "application/json; charset=utf-8";
                httpWebRequest.Method = "POST";
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    streamWriter.Write(Json(ModeloSap));
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
        /// Obtiene los datos de conexion hacia SAP
        /// </summary>
        /// <returns></returns>
        public Models.ConfiguracionDBModel SAPModel(string Empresa)
        {
            Models.ConfiguracionDBModel ModeloSAP;
            HttpWebResponse CreateResponse = null;
            string actual;
            try
            {
                Uri Url = new Uri(UrlWebServices()+ "/IGT/Consola/ConexionSAP");
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(Url);
                httpWebRequest.ContentType = "application/json; charset=utf-8";
                httpWebRequest.Method = "GET";
                CreateResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(CreateResponse.GetResponseStream()))
                {
                    actual = streamReader.ReadToEnd();
                }
                if (!string.IsNullOrEmpty(actual))
                {
                    ModeloSAP = new Models.ConfiguracionDBModel(actual);
                }
                else
                {
                    ModeloSAP = new Models.ConfiguracionDBModel();
                }             
                return ModeloSAP;
            }
            catch (Exception ex)
            {
                ModeloSAP = new Models.ConfiguracionDBModel();
                return ModeloSAP;
            }
        }
        /// <summary>
        /// Obtiene los parametros de conexion hacia la base de datos intermedia
        /// </summary>
        /// <returns></returns>
        public Models.ConfiguracionDBModel IntermediaModel()
        {
            Models.ConfiguracionDBModel intermedia;
            HttpWebResponse CreateResponse = null;

            string actual;
            try
            {
                Uri Url = new Uri(UrlWebServices() + "/IGT/Consola/ConexionIntermedia");
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(Url);
                httpWebRequest.ContentType = "application/json; charset=utf-8";
                httpWebRequest.Method = "GET";
                CreateResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(CreateResponse.GetResponseStream()))
                {
                    actual = streamReader.ReadToEnd();
                }

                intermedia = new Models.ConfiguracionDBModel(actual);
                return intermedia;
            }
            catch (WebException e)
            {
                string text;
                WebResponse response;
                try
                {
                    using (response = e.Response)
                    {
                        using (Stream data = response.GetResponseStream())
                        {
                            text = new StreamReader(data).ReadToEnd();
                        }
                    }
                    return null;
                }
                catch (Exception)
                {

                    return null;
                }
            }
        }
        /// <summary>
        /// Almacena los datos de conexion a la base de datos Intermedia
        /// Crea la base de datos intermedia
        /// </summary>
        /// <param name="IntermediaModelo"></param>
        /// <returns></returns>
        public string CrearIntermedia(Models.ConfiguracionDBModel IntermediaModelo)
        {
            HttpWebResponse CreateResponse = null;
            string actual;
            try
            {
                Uri Url = new Uri(UrlWebServices()+ "/IGT/Consola/CrearIntermedia");
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(Url);
                httpWebRequest.ContentType = "application/json; charset=utf-8";
                httpWebRequest.Method = "POST";           
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    streamWriter.Write(Json(IntermediaModelo));
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
            catch (WebException e)
            {
                string text;
                WebResponse response;
                try
                {
                    using (response = e.Response)
                    {
                        using (Stream data = response.GetResponseStream())
                        {
                            text = new StreamReader(data).ReadToEnd();
                        }
                    }
                    return text;
                }
                catch (Exception)
                {

                    return "La consola no ha podido tener comunicacion con el Web Services.";
                }
            }
        }
        /// <summary>
        /// Obtiene la URL de conexion hacia el WebServices
        /// </summary>
        /// <returns></returns>
        public string EditarIntermedia(Models.ConfiguracionDBModel IntermediaModelo)
        {
            HttpWebResponse CreateResponse = null;
            string actual;
            try
            {

                Uri Url = new Uri(UrlWebServices() + "/IGT/Consola/EditarIntermedia");
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(Url);
                httpWebRequest.ContentType = "application/json; charset=utf-8";
                httpWebRequest.Method = "POST";
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    streamWriter.Write(Json(IntermediaModelo));
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
            catch (WebException e)
            {

                string text;
                WebResponse response;
                try
                {
                    using (response = e.Response)
                    {
                        using (Stream data = response.GetResponseStream())
                        {
                            text = new StreamReader(data).ReadToEnd();
                        }
                    }
                    return text;
                }
                catch (Exception)
                {

                    return "La consola no ha podido tener comunicacion con el Web Services.";
                }
            }
        }
        public string UrlWebServices()
        {
            try
            {
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load(@"Igt.WebServices.Base.xml");
                XmlNodeList Informacion = xDoc.GetElementsByTagName("Informacion");
                ArrayList arrText = new ArrayList();
                string Url = string.Empty;
                foreach (XmlElement nodo in Informacion)
                {
                    XmlNodeList Server =
                    nodo.GetElementsByTagName("SERVER");
                    Url = Server[0].InnerText;
                    XmlNodeList PORT =
                     nodo.GetElementsByTagName("PORT");
                    Url =Url+":"+PORT[0].InnerText;
                }
               
                return Url;
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

        public List<SelectListItem> getTipoObjetos()
        {
            List<SelectListItem> listado = new List<SelectListItem>();
            HttpWebResponse CreateResponse = null;
            string actual;
            try
            {
                Uri Url = new Uri(UrlWebServices() + "/IGT/Consola/GetTipoIbjetos");
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

                    if (!string.IsNullOrEmpty(actual))
                    {
                        JArray jsonPreservar = JArray.Parse(actual);
                        foreach (JObject jsonOperaciones in jsonPreservar.Children<JObject>())
                        {
                            listado.Add(new SelectListItem(jsonOperaciones["Text"].ToString(), jsonOperaciones["Value"].ToString()));
                        }

                    }

                }
                return listado;
            }
            catch (WebException e)
            {
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
