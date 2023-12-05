using GO_WEB.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Web;

namespace GO_WEB.Models
{
    public class ProductoNoLogModel
    {
        string urlWebApi = ConfigurationManager.AppSettings["urlWebApi"].ToString();

        public List<ProductoEnt> ConsultarProductosNOLogIn()
        {
            using (var client = new HttpClient())
            {
                string url = urlWebApi + "api/ConsultarProductosNoLog";


                HttpResponseMessage resp = client.GetAsync(url).Result;

                if (resp.IsSuccessStatusCode)
                {
                    return resp.Content.ReadFromJsonAsync<List<ProductoEnt>>().Result;
                }

                return new List<ProductoEnt>();
            }
        }
    }
}