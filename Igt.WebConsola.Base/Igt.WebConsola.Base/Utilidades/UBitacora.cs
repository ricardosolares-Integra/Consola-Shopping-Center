using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Igt.WebConsola.Base.Utilidades
{
    public class UBitacora
    {
        private readonly static UBitacora _instance = new UBitacora();

        private Conexiones utilConexiones;

        private UBitacora()
        {
            utilConexiones = new Conexiones();
        }
        public static UBitacora Instance
        {
            get
            {
                return _instance;
            }
        }

        /// <summary>
        /// Obtener los datos de la bitacora
        /// </summary>
        /// <param name="modelo"></param>
        /// <returns></returns>
        public List<Models.DatosBitacoraModel> GetBitacora(Models.BitacoraModel modelo)
        {
            List<Models.DatosBitacoraModel> ListBitacora = null;
            HttpWebResponse CreateResponse = null;
            string actual;
            try
            {
                Uri Url = new Uri(utilConexiones.UrlWebServices() + "/IGT/Consola/GetBitacora");
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(Url);
                httpWebRequest.ContentType = "application/json; charset=utf-8";
                httpWebRequest.Method = "GET";
                httpWebRequest.Headers.Add("TipoObjeto", modelo.TipoObjeto);
                //Start Date
                httpWebRequest.Headers.Add("TipoTransaccion", modelo.TipoTransaccion);
                httpWebRequest.Headers.Add("ResultadoTransaccion", modelo.ResultadoTransaccion);
                httpWebRequest.Headers.Add("TipoOperacion", modelo.TipoOperacion);
                httpWebRequest.Headers.Add("FechaDesde", modelo.FechaDesde.ToString("yyyy/MM/dd"));
                httpWebRequest.Headers.Add("FechaHasta", modelo.FechaHasta.ToString("yyyy/MM/dd"));

                CreateResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(CreateResponse.GetResponseStream()))
                {
                    actual = streamReader.ReadToEnd();
                }
                if (CreateResponse.StatusCode.Equals(HttpStatusCode.OK))
                {
                    ListBitacora = ListaBitacora(actual);
                }
                return ListBitacora;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        /// <summary>
        /// Obtiene una lista de las lineas encontradas en la bitacora
        /// </summary>
        /// <param name="Parametros"></param>
        /// <returns></returns>
        public List<Models.DatosBitacoraModel> ListaBitacora(string Parametros)
        {
            List<Models.DatosBitacoraModel> bitacoraListado;
            Models.DatosBitacoraModel ModeloBitacora;
            try
            {
                if (!string.IsNullOrEmpty(Parametros))
                {
                    bitacoraListado = new List<Models.DatosBitacoraModel>();
                    JArray jsonPreservar = JArray.Parse(Parametros);
                    foreach (JObject jsonOperaciones in jsonPreservar.Children<JObject>())
                    {
                        ModeloBitacora = new Models.DatosBitacoraModel();
                        ModeloBitacora.CodigoError = jsonOperaciones["CodigoError"].ToString();
                        ModeloBitacora.Correlativo = (string)jsonOperaciones["Correlativo"];
                        ModeloBitacora.TipoObjeto = (string)jsonOperaciones["TipoObjeto"];
                        ModeloBitacora.Fecha = (DateTime)jsonOperaciones["Fecha"];
                        ModeloBitacora.IDObjetoSap = (string)jsonOperaciones["IDObjetoSap"];
                        ModeloBitacora.IDObjetoIntermedia = (string)jsonOperaciones["IDObjetoIntermedia"];
                        ModeloBitacora.Mensaje = (string)jsonOperaciones["Mensaje"];
                        ModeloBitacora.Resultado = (string)jsonOperaciones["Resultado"];
                        ModeloBitacora.TipoOperacion = (string)jsonOperaciones["TipoOperacion"];
                        ModeloBitacora.TipoTransaccion = (string)jsonOperaciones["TipoTransaccion"];
                        bitacoraListado.Add(ModeloBitacora);
                    }
                }
                else
                {
                    bitacoraListado = new List<Models.DatosBitacoraModel>();
                }


                return bitacoraListado;
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}
