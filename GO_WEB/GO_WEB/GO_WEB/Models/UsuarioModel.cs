using MN_WEB.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MN_WEB.Models
{
    public class UsuarioModel
    {
        string urlWebApi = ConfigurationManager.AppSettings["urlWebApi"].ToString();

        public UsuarioEnt IniciarSesion(UsuarioEnt entidad)
        {
            using (var client = new HttpClient())
            {
                string url = urlWebApi + "api/IniciarSesion";
                JsonContent body = JsonContent.Create(entidad); //serializar

                HttpResponseMessage resp = client.PostAsync(url, body).Result;

                if(resp.IsSuccessStatusCode)
                {
                    return resp.Content.ReadFromJsonAsync<UsuarioEnt>().Result;
                }

                return null;
            }
        }

        public int Registrarse(UsuarioEnt entidad)
        {
            using (var client = new HttpClient())
            {
                string url = urlWebApi + "api/Registrarse";
                JsonContent body = JsonContent.Create(entidad); //serializar

                HttpResponseMessage resp = client.PostAsync(url, body).Result;

                if (resp.IsSuccessStatusCode)
                {
                    return resp.Content.ReadFromJsonAsync<int>().Result;
                }

                return 0;
            }
        }

        public bool RecuperarContrasenna(UsuarioEnt entidad)
        {
            using (var client = new HttpClient())
            {
                string url = urlWebApi + "api/RecuperarContrasenna";
                JsonContent body = JsonContent.Create(entidad); //serializar

                HttpResponseMessage resp = client.PostAsync(url, body).Result;

                if (resp.IsSuccessStatusCode)
                {
                    return resp.Content.ReadFromJsonAsync<bool>().Result;
                }

                return false;
            }
        }

        public int CambiarContrasenna(UsuarioEnt entidad)
        {
            using (var client = new HttpClient())
            {
                string url = urlWebApi + "api/CambiarContrasenna";
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

        public List<UsuarioEnt> ConsultarUsuarios()
        {
            using (var client = new HttpClient())
            {
                string url = urlWebApi + "api/ConsultarUsuarios";
                string token = HttpContext.Current.Session["TokenSesion"].ToString();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage resp = client.GetAsync(url).Result;

                if (resp.IsSuccessStatusCode)
                {
                    return resp.Content.ReadFromJsonAsync<List<UsuarioEnt>>().Result;
                }

                return new List<UsuarioEnt>();
            }
        }

        public UsuarioEnt ConsultarUsuario(long q)
        {
            using (var client = new HttpClient())
            {
                string url = urlWebApi + "api/ConsultarUsuario?q=" + q;
                string token = HttpContext.Current.Session["TokenSesion"].ToString();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage resp = client.GetAsync(url).Result;

                if (resp.IsSuccessStatusCode)
                {
                    return resp.Content.ReadFromJsonAsync<UsuarioEnt>().Result;
                }

                return null;
            }
        }

        public int CambiarEstado(UsuarioEnt entidad)
        {
            using (var client = new HttpClient())
            {
                string url = urlWebApi + "api/CambiarEstado";
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

        public List<RolEnt> ConsultarRoles()
        {
            using (var client = new HttpClient())
            {
                string url = urlWebApi + "api/ConsultarRoles";
                string token = HttpContext.Current.Session["TokenSesion"].ToString();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage resp = client.GetAsync(url).Result;

                if (resp.IsSuccessStatusCode)
                {
                    return resp.Content.ReadFromJsonAsync<List<RolEnt>>().Result;
                }

                return new List<RolEnt>();
            }
        }

        public int EditarUsuario(UsuarioEnt entidad)
        {
            using (var client = new HttpClient())
            {
                string url = urlWebApi + "api/EditarUsuario";
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

    }
}