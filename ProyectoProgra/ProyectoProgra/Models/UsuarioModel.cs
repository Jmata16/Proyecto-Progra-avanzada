using Newtonsoft.Json;
using ProyectoProgra.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Web;

namespace ProyectoProgra.Models
{
    public class UsuarioModel
    {
        public string rutaServidor = ConfigurationManager.AppSettings["RutaApi"];
        public string RegistrarCuenta(UsuarioEnt entidad)
        {
            using (var client = new HttpClient())
            {
                var urlApi = "https://localhost:44307/RegistrarCuenta";
                var jsonData = JsonContent.Create(entidad);
                var res = client.PostAsync(urlApi, jsonData).Result;
                return res.Content.ReadFromJsonAsync<string>().Result;
            }
        }
    }
}