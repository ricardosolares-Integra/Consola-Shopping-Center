using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Igt.WebConsola.Base.Models
{
    public class ConfiguracionDBModel
    {
        [Required(ErrorMessage = "Campo requerido")]
        [Display(Name = "Servidor Base de Datos")]
        public string Server { get; set; }

        [Required(ErrorMessage = "Campo requerido")]
        [Display(Name = "Puerto")]
        public string Port { get; set; }

        [Required(ErrorMessage = "Campo requerido")]
        [Display(Name = "DNS")]
        public string DNS { get; set; }

        [Required(ErrorMessage = "Campo requerido")]
        [Display(Name = "Usuario")]
        public string UserHana { get; set; }

        [Required(ErrorMessage = "Campo requerido")]
        [Display(Name = "Contraseña")]
        public string PasswordHana { get; set; }

        [Required(ErrorMessage = "Campo requerido")]
        [Display(Name = "Nombre Base de Datos")]
        public string DBHana { get; set; }

        public ConfiguracionDBModel(string Json)
        {
            JObject jObject = JObject.Parse(Json);
            Server = (string)jObject["Server"];
            Port = (string)jObject["Port"];
            DNS = (string)jObject["DNS"];
            UserHana = (string)jObject["UserHana"];
            PasswordHana = (string)jObject["PasswordHana"];
            DBHana = (string)jObject["DBHana"];
        }
        public ConfiguracionDBModel()
        {
            Server = "";
            Port = "";
            DNS = "";
            UserHana = "";
            PasswordHana = "";
            DBHana = "";
        }
    }
}
