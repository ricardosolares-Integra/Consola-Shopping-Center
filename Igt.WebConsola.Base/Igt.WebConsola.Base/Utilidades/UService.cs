using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace Igt.WebConsola.Base.Utilidades
{
    //servicio timer
    public class UService : IHostedService, IDisposable
    {
        private Timer _timer;
        private readonly ILogger<UService> _logger;

        public UService(ILogger<UService> logger)
        {
            _logger = logger;
        }

        public void Dispose()
        {
            _logger.LogInformation("Hilo de escucha, ingreso metodo Dispose");
            _timer?.Dispose(); //si existe 
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Hilo de escucha, ingreso metodo StartAsync");
            _timer = new Timer(doWork, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(120));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Hilo de escucha, ingreso metodo StopAsync");
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void doWork(Object state) {
            _logger.LogInformation("Hilo de escucha, ingreso metodo DoWork");
            HttpWebResponse CreateResponse = null;
            string actual;
            try {

                Uri Url = new Uri(UrlWebServices() + "/IGT/Servicio/ServiceTimer");
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
                    var a = "ok";
                    return;
                }

            }
            catch (WebException e)
            {
                _logger.LogError(e.Message);
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
                    Url = Url + ":" + PORT[0].InnerText;
                }

                return Url;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
