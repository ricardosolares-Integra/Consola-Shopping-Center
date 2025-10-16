using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Igt.WebConsola.Base.Utilidades
{
    public class UEnvioManual
    {
        private readonly static UEnvioManual _instance = new UEnvioManual();

        private Conexiones utilConexiones;

        private UEnvioManual()
        {
            utilConexiones = new Conexiones();
        }
        public static UEnvioManual Instance
        {
            get
            {
                return _instance;
            }
        }

        public string EnvioManual(string IdObjeto)
        {
            string result = string.Empty;
            HttpWebResponse CreateResponse = null;
            string actual;
            try
            {
                Uri Url = new Uri(utilConexiones.UrlWebServices() + "/IGT/Consola/EnvioManual");
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(Url);
                httpWebRequest.ContentType = "application/json; charset=utf-8";
                httpWebRequest.Method = "PUT";
                httpWebRequest.Headers.Add("IdObjeto", IdObjeto);

                CreateResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(CreateResponse.GetResponseStream()))
                {
                    actual = streamReader.ReadToEnd();
                }
                if (CreateResponse.StatusCode.Equals(HttpStatusCode.OK))
                {
                    result = actual;
                }
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
