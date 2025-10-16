using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igt.WebConsola.Base.Models
{
    public class DatosBitacoraModel
    {
        public string Correlativo { get; set; }
        public DateTime Fecha { get; set; }
        public string Resultado { get; set; }
        public string Mensaje { get; set; }
        public string CodigoError { get; set; }
        public string TipoObjeto { get; set; }
        public string IDObjetoSap { get; set; }
        public string IDObjetoIntermedia { get; set; }
        public string TipoTransaccion { get; set; }
        public string TipoOperacion { get; set; }
    }
}
