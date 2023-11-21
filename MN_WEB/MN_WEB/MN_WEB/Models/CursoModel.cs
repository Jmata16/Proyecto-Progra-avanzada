using MN_WEB.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Web;

namespace MN_WEB.Models
{
    public class ProductoModel
    {
        string urlWebApi = ConfigurationManager.AppSettings["urlWebApi"].ToString();

        public List<ProductoEnt> ConsultarProductos()
        {
            using (var client = new HttpClient())
            {
                string url = urlWebApi + "api/ConsultarProductos";
                string token = HttpContext.Current.Session["TokenSesion"].ToString();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage resp = client.GetAsync(url).Result;

                if (resp.IsSuccessStatusCode)
                {
                    return resp.Content.ReadFromJsonAsync<List<ProductoEnt>>().Result;
                }

                return new List<ProductoEnt>();
            }
        }

        public ProductoEnt ConsultarProducto(long q)
        {
            using (var client = new HttpClient())
            {
                string url = urlWebApi + "api/ConsultarProducto?q=" + q;
                string token = HttpContext.Current.Session["TokenSesion"].ToString();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage resp = client.GetAsync(url).Result;

                if (resp.IsSuccessStatusCode)
                {
                    return resp.Content.ReadFromJsonAsync<ProductoEnt>().Result;
                }

                return null;
            }
        }

        public long RegistrarProducto(ProductoEnt entidad)
        {
            using (var client = new HttpClient())
            {
                string url = urlWebApi + "api/RegistrarProducto";
                string token = HttpContext.Current.Session["TokenSesion"].ToString();
                JsonContent body = JsonContent.Create(entidad); //serializar

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage resp = client.PostAsync(url, body).Result;

                if (resp.IsSuccessStatusCode)
                {
                    return resp.Content.ReadFromJsonAsync<long>().Result;
                }

                return 0;
            }
        }

        public int ActualizarProducto(ProductoEnt entidad)
        {
            using (var client = new HttpClient())
            {
                string url = urlWebApi + "api/ActualizarProducto";
                string token = HttpContext.Current.Session["TokenSesion"].ToString();
                JsonContent body = JsonContent.Create(entidad); //serializar

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage resp = client.PutAsync(url, body).Result;

                if (resp.IsSuccessStatusCode)
                {
                    return resp.Content.ReadFromJsonAsync<int>().Result;
                }

                return 0;
            }
        }        

        public void ActualizarRuta(ProductoEnt entidad)
        {
            using (var client = new HttpClient())
            {
                string url = urlWebApi + "api/ActualizarRuta";
                string token = HttpContext.Current.Session["TokenSesion"].ToString();
                JsonContent body = JsonContent.Create(entidad); //serializar

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage resp = client.PutAsync(url, body).Result;
            }
        }
        

    }
}