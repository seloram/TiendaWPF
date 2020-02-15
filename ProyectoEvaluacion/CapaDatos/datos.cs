using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidades;
using Newtonsoft;
using System.Data;


using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Web.Script.Serialization;





namespace CapaDatos
{
    public class datos
    {
        static HttpClient client = new HttpClient();
        public datos()
        {
            client.BaseAddress = new Uri("https://localhost:44384/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.Timeout = TimeSpan.FromMinutes(10);            
        }

        //leo todos los artículos de la BD
        //public List<articulo> televisores()
        //{
        //    List<tv> listaTv = null;
        //    string aux;

        //    try
        //    {
        //        HttpResponseMessage response = client.GetAsync("api/tv").Result;
        //        if (response.IsSuccessStatusCode)
        //        {
        //            aux = response.Content.ReadAsStringAsync().Result;
        //            listarticulos = JsonConvert.DeserializeObject<List<articulo>>(aux);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine("Error " + e);
        //    }

        //    return listarticulos;
        //}
        //public List<articulo> leer_articulos()
        //{
        //    List<articulo> listarticulos = null;
        //    string aux;

        //    try
        //    {
        //        HttpResponseMessage response = client.GetAsync("api/articulos").Result;
        //        if (response.IsSuccessStatusCode)
        //        {
        //            aux = response.Content.ReadAsStringAsync().Result;
        //            listarticulos = JsonConvert.DeserializeObject<List<articulo>>(aux);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine("Error " + e);
        //    }

        //    return listarticulos;
        //}
        //public List<articulo> leer_articulos()
        //{
        //    List<articulo> listarticulos = null;
        //    string aux;

        //    try
        //    {
        //        HttpResponseMessage response = client.GetAsync("api/articulos").Result;
        //        if (response.IsSuccessStatusCode)
        //        {
        //            aux = response.Content.ReadAsStringAsync().Result;
        //            listarticulos = JsonConvert.DeserializeObject<List<articulo>>(aux);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine("Error " + e);
        //    }

        //    return listarticulos;
        //}
        //public List<articulo> leer_articulos()
        //{
        //    List<articulo> listarticulos = null;
        //    string aux;

        //    try
        //    {
        //        HttpResponseMessage response = client.GetAsync("api/articulos").Result;
        //        if (response.IsSuccessStatusCode)
        //        {
        //            aux = response.Content.ReadAsStringAsync().Result;
        //            listarticulos = JsonConvert.DeserializeObject<List<articulo>>(aux);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine("Error " + e);
        //    }

        //    return listarticulos;
        //}



        public List<articulo> leer_articulos()
        {
            List<articulo> listarticulos = null;
            string aux;

            try
            {
                HttpResponseMessage response = client.GetAsync("api/articulos").Result;
                if (response.IsSuccessStatusCode)
                {
                    aux = response.Content.ReadAsStringAsync().Result;
                    listarticulos = JsonConvert.DeserializeObject<List<articulo>>(aux);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error " + e);
            }

            return listarticulos;
        }

        public List<tipoarticulo> leer_tipoProductos()
        {
            List<tipoarticulo> listaTipoProductos = null;
            string aux;

            try
            {
                HttpResponseMessage response = client.GetAsync("api/tipoArticulos").Result;
                if (response.IsSuccessStatusCode)
                {
                    aux = response.Content.ReadAsStringAsync().Result;

                    listaTipoProductos = JsonConvert.DeserializeObject<List<tipoarticulo>>(aux);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error " + e);
            }

            return listaTipoProductos;
        }
        /*
        public List<linped> leer_lineaspedidos()
        {
            List<linped> lineaspedidos = null;
            string aux;

            try
            {
                HttpResponseMessage response = client.GetAsync("api/linped").Result;
                if (response.IsSuccessStatusCode)
                {
                    aux = response.Content.ReadAsStringAsync().Result;

                    lineaspedidos = JsonConvert.DeserializeObject<List<linped>>(aux);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error " + e);
            }

            return lineaspedidos;
        }



        public List<marca> leer_marcas()
        {
            List<marca> listamarcas = null;
            string aux;

            try
            {
                HttpResponseMessage response = client.GetAsync("api/marcas").Result;
                if (response.IsSuccessStatusCode)
                {
                    aux = response.Content.ReadAsStringAsync().Result;

                    listamarcas = JsonConvert.DeserializeObject<List<marca>>(aux);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error " + e);
            }

            return listamarcas;
        }
        */
        public List<pedido> leer_pedidos()
        {
            List<pedido> listapedidos = null;
            string aux;

            try
            {
                HttpResponseMessage response = client.GetAsync("api/pedidos").Result;
                if (response.IsSuccessStatusCode)
                {
                    aux = response.Content.ReadAsStringAsync().Result;

                    listapedidos = JsonConvert.DeserializeObject<List<pedido>>(aux);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error " + e);
            }

            return listapedidos;
        }
        
        public List<usuario> leer_usuarios()
        {
            List<usuario> listausuarios = null;
            string aux;            

            try
            {
                HttpResponseMessage response = client.GetAsync("api/usuarios").Result;
                if (response.IsSuccessStatusCode)
                {
                    aux = response.Content.ReadAsStringAsync().Result;
                    listausuarios = JsonConvert.DeserializeObject<List<usuario>>(aux);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error " + e);
            }
            return listausuarios;
        }

        public List<provincia> LeerProvincias()
        {
            List<provincia> listaProv = null;
            string aux;

            try
            {
                HttpResponseMessage response = client.GetAsync("api/provincias").Result;
                if (response.IsSuccessStatusCode)
                {
                    aux = response.Content.ReadAsStringAsync().Result;
                    listaProv = JsonConvert.DeserializeObject<List<provincia>>(aux);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error " + e);
            }
            return listaProv;
        }

        public List<localidad> LeerLocalidades()
        {
            List<localidad> listaLocal = null;
            string aux;

            try
            {
                HttpResponseMessage response = client.GetAsync("api/localidades").Result;
                if (response.IsSuccessStatusCode)
                {
                    aux = response.Content.ReadAsStringAsync().Result;
                    listaLocal = JsonConvert.DeserializeObject<List<localidad>>(aux);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error " + e);
            }
            return listaLocal;
        }

        //public bool Modificar (int id, string email, string nom, string pass, 
        //    string apellidos, string dni, string telefono, string calle, string calle2, string codpos,
        //    string puebloID, string provinciaID, string nacido)
        public bool Modificar (usuario usuario)
        {

            try
            {
                //usuario usu = new usuario(id, email, nom, pass, apellidos, dni, 
                //    telefono, calle, calle2, codpos, puebloID, provinciaID, nacido);
                usuario usu = usuario;
                HttpResponseMessage response = client.PutAsJsonAsync($"api/usuarios/{usu.UsuarioID}", usu).Result; 

                if (response.IsSuccessStatusCode)
                {
                    return true;
                } else
                    return false;
            }
            catch(Exception e)
            {
                Console.WriteLine("Error " + e);
            }
            return true;
        }

        public bool Insertar(string email, string nom, string pass,
            string apellidos, string dni, string telefono, string calle, string calle2, string codpos,
            string puebloID, string provinciaID, string nacido)
        {

            try
            {
                usuario usu = new usuario(email, nom, pass, apellidos, dni,
                    telefono, calle, calle2, codpos, puebloID, provinciaID, nacido);
                HttpResponseMessage response = client.PostAsJsonAsync("api/usuarios", usu).Result;

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                    return false;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error " + e);
            }
            return true;
        }

        public bool Eliminar(string id)
        {
            try
            {
                HttpResponseMessage response = client.DeleteAsync($"api/usuarios/{id}").Result;

                if (response.IsSuccessStatusCode)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error " + e);
            }

            return true;
        }
    
    }
}
