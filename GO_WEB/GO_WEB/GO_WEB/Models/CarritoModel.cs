using MN_WEB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net.Http;
using System.Web;
using System.Configuration;
using System.Net.Http.Headers;

namespace MN_WEB.Models
{
    public class CarritoModel
    {
        string urlWebApi = ConfigurationManager.AppSettings["urlWebApi"].ToString();

        public int AgregarProductoCarrito(CarritoEnt entidad)
        {
            using (var client = new HttpClient())
            {
                string url = urlWebApi + "api/AgregarProductoCarrito";
                string token = HttpContext.Current.Session["TokenSesion"].ToString();
                JsonContent body = JsonContent.Create(entidad); //serializar

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage resp = client.PostAsync(url, body).Result;

                if (resp.IsSuccessStatusCode)
                {
                    return resp.Content.ReadFromJsonAsync<int>().Result;
                }

                return 0;
            }
        }

        public int RemoverProductoCarrito(long q)
        {
            using (var client = new HttpClient())
            {
                string url = urlWebApi + "api/RemoverProductoCarrito?q=" + q;
                string token = HttpContext.Current.Session["TokenSesion"].ToString();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage resp = client.DeleteAsync(url).Result;

                if (resp.IsSuccessStatusCode)
                {
                    return resp.Content.ReadFromJsonAsync<int>().Result;
                }

                return 0;
            }
        }

        public List<CarritoEnt> ConsultarCarrito(long q)
        {
            using (var client = new HttpClient())
            {
                string url = urlWebApi + "api/ConsultarCarrito?q=" + q;
                string token = HttpContext.Current.Session["TokenSesion"].ToString();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage resp = client.GetAsync(url).Result;

                if (resp.IsSuccessStatusCode)
                {
                    return resp.Content.ReadFromJsonAsync<List<CarritoEnt>>().Result;
                }

                return new List<CarritoEnt>();
            }
        }

        public List<CarritoEnt> ConsultarCompras(long q)
        {
            using (var client = new HttpClient())
            {
                string url = urlWebApi + "api/ConsultarCompras?q=" + q;
                string token = HttpContext.Current.Session["TokenSesion"].ToString();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage resp = client.GetAsync(url).Result;

                if (resp.IsSuccessStatusCode)
                {
                    return resp.Content.ReadFromJsonAsync<List<CarritoEnt>>().Result;
                }

                return new List<CarritoEnt>();
            }
        }

        public int ConfirmarPago(CarritoEnt entidad)
        {
            using (var client = new HttpClient())
            {
                string url = urlWebApi + "api/ConfirmarPago";
                string token = HttpContext.Current.Session["TokenSesion"].ToString();
                JsonContent body = JsonContent.Create(entidad); //serializar

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage resp = client.PostAsync(url, body).Result;

                if (resp.IsSuccessStatusCode)
                {
                    return resp.Content.ReadFromJsonAsync<int>().Result;
                }

                return 0;
            }
        }

        internal void ActualizarCarrito(List<CarritoEnt> carrito)
        {
            throw new NotImplementedException();
        }
    }
}