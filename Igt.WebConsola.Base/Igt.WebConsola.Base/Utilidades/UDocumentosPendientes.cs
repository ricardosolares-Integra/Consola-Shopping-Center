using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Igt.WebConsola.Base.Models;
using Newtonsoft.Json;

namespace Igt.WebConsola.Base.Utilidades
{
    public class UDocumentosPendientes
    {
        private readonly static UDocumentosPendientes _instance = new UDocumentosPendientes();

        private Conexiones utilConexiones;

        private UDocumentosPendientes()
        {
            utilConexiones = new Conexiones();
        }
        public static UDocumentosPendientes Instance
        {
            get
            {
                return _instance;
            }
        }

        public DataTable getDocumentosPendientes(DocumentosPendientesModel modelo)
        {
            DataTable Result = new DataTable();
            HttpWebResponse CreateResponse = null;
            string actual;
            try
            {
                Uri Url = new Uri(utilConexiones.UrlWebServices() + "/IGT/Consola/GetDocPendientes");
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(Url);
                httpWebRequest.ContentType = "application/json; charset=utf-8";
                httpWebRequest.Method = "GET";
                httpWebRequest.Headers.Add("TipoObjeto", modelo.TipoObjeto);
                httpWebRequest.Headers.Add("FechaDesde", modelo.FechaDesde.ToString("yyyy/MM/dd"));
                httpWebRequest.Headers.Add("FechaHasta", modelo.FechaHasta.ToString("yyyy/MM/dd"));

                CreateResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(CreateResponse.GetResponseStream()))
                {
                    actual = streamReader.ReadToEnd();
                }
                if (CreateResponse.StatusCode.Equals(HttpStatusCode.OK))
                {
                    Result = (DataTable)JsonConvert.DeserializeObject(actual, (typeof(DataTable)));
                }
                return Result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
