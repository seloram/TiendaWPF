using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class cesta
    {

        //cesta
        public string ArticuloID { get; set; }
        public string UsuarioID { get; set; }
        public string Fecha { get; set; }

        public cesta()
        {

        }
        public cesta(string articuloID, string usuarioID, string fecha)
        {
            this.ArticuloID = articuloID;
            this.UsuarioID = usuarioID;
            this.Fecha = fecha;
        }
    }
    public class tlb
    {
        //tlb
        public int A { get; set; }
        public int B { get; set; }

        public tlb() { }
        public tlb(int a, int b)
        {
            this.A = a;
            this.B = b;
        }
    }

    public class tipoarticulo
    {
        //tipoarticulo
        public int TipoArticuloID { get; set; }
        public string Descripcion { get; set; }
        public tipoarticulo()
        {

        }
        public tipoarticulo(int tipo, string desc)
        {
            this.TipoArticuloID = tipo;
            this.Descripcion = desc;
        }
    }

    public class articulo
    {
        //articulo
        public string ArticuloID { get; set; }
        public string Nombre { get; set; }
        public string Pvp { get; set; }
        public string MarcaID { get; set; }
        public string Blob { get; set; }
        public string UrlImagen { get; set; }
        public string Especificaciones { get; set; }
        public string TipoArticuloID { get; set; }

        public articulo()
        {

        }
        public articulo(string articuloID, string nombre, string pvp, string marcaID, string blob,
            string urlImagen, string especif, string tipoArticulo)
        {
            this.ArticuloID = articuloID;
            this.Nombre = nombre;
            this.Pvp = pvp;
            this.MarcaID = marcaID;
            this.Blob = blob;
            this.UrlImagen = urlImagen;
            this.Especificaciones = especif;
            this.TipoArticuloID = tipoArticulo;
        }
    }
    public class localidad
    {
        //localidad
        public string LocalidadID { get; set; }
        public string Nombre { get; set; }
        public string ProvinciaID { get; set; }
        public localidad()
        {

        }

        public localidad(string localidadID, string nombre, string provinciaID)
        {
            this.LocalidadID = localidadID;
            this.Nombre = nombre;
            this.ProvinciaID = provinciaID;
        }
    }

    public class pedido
    {
        //pedido
        public int PedidoID { get; set; }
        public int UsuarioID { get; set; }
        public string Fecha { get; set; }

        public pedido()
        {

        }
        public pedido(int pedidoID, int usuarioID, string fecha)
        {
            this.PedidoID = pedidoID;
            this.UsuarioID = usuarioID;
            this.Fecha = fecha;
        }
    }

    public class memoria
    {
        //memoria
        public string MemoriaID { get; set; }
        public string Tipo { get; set; }
        public memoria()
        {

        }
        public memoria(string memoriaID, string tipo)
        {
            this.MemoriaID = memoriaID;
            this.Tipo = tipo;
        }
    }

    public class pack
    {
        //pack
        public string Cod { get; set; }
        public pack()
        {

        }
        public pack(string cod)
        {
            Cod = cod;
        }
    }
    public class usuario
    {
        //usuario
        public int UsuarioID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Dni { get; set; }
        public string Telefono { get; set; }
        public string Calle { get; set; }
        public string Calle2 { get; set; }
        public string Codpos { get; set; }
        public string PuebloID { get; set; }
        public string ProvinciaID { get; set; }
        public string Date { get; set; }

        public usuario()
        {

        }

        public usuario(int usuarioID, string email, string nombre, string pass, string apellidos, string dni, 
            string telefono, string calle, string calle2, string codpos, string puebloID, 
            string provinciaID, string nacido)
        {
            UsuarioID = usuarioID;
            Email = email;
            Password=pass;
            Nombre = nombre;
            Apellidos = apellidos;
            Dni = dni;
            Telefono = telefono;
            Calle = calle;
            Calle2 = calle2;
            Codpos = codpos;
            PuebloID = puebloID;
            ProvinciaID = provinciaID;
            Date = nacido;
        }

        public usuario(string email, string nombre, string pass, string apellidos, string dni,
            string telefono, string calle, string calle2, string codpos, string puebloID,
            string provinciaID, string nacido)
        {
            
            Email = email;
            Password = pass;
            Nombre = nombre;
            Apellidos = apellidos;
            Dni = dni;
            Telefono = telefono;
            Calle = calle;
            Calle2 = calle2;
            Codpos = codpos;
            PuebloID = puebloID;
            ProvinciaID = provinciaID;
            Date = nacido;
        }

        public bool ComprobarTel(string tel)
        {
            if (tel.Length < 9)
            {
                return false;
            }
            else
                return true;
        }

        public bool ComprobarNom(string nom)
        {
            if (nom.Length == 0)
            {
                return false;
            }
            else
                return true;
        }

        public bool ComprobarApe(string ape)
        {
            if (ape.Length == 0)
            {
                return false;
            }
            else
                return true;
        }

        public bool ComprobarEmail(string email)
        {
            if (!Regex.IsMatch(email,
                @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)))
            {
                return false;
            }
            return true;
        }

        public bool ComprobarCP(string cp)
        {
            if (cp.Length < 5)
            {
                return false;
            }
            return true;
        }

        public string ComprobarDni(string dni)
        {
            switch (dni.Length)
            {
                case 0:
                    return "El campo no puede estar vacío";
                    break;
                default:
                    char letra = ' ';

                    if (dni.Length > 0)
                    {
                        letra = dni[0];
                    }

                    switch (letra)
                    {
                        case ' ':
                            break;
                        default:
                            letra = LetraNIF(dni);
                            if (letra == '0')
                            {
                                return "El formato no es correcto";
                            }
                            if (letra == ' ')
                            {
                                return "El DNI debe contener 9 dígitos.";
                            }
                            else
                            {
                                if (letra != dni[8])
                                {
                                    return "El DNI no está correcto";
                                }
                            }
                            break;
                    }
                    break;
            }
            return "";
        }

        public char LetraNIF(string dni)
        {
            char l = ' ';
            string numero = "";
            if (dni.Length == 9)
            {
                numero = dni.Substring(0, 8);
                try
                {
                    int num = Int32.Parse(numero);
                }
                catch
                {
                    return '0';
                }
            }

            const string CORRESPONDENCIA = "TRWAGMYFPDXBNJZSQVHLCKE";
            Match match = new Regex(@"\b(\d{8})\b").Match(numero);
            if (match.Success)
                l = CORRESPONDENCIA[int.Parse(numero) % 23];
            else
            {

            }
            return l;
        }
    }

    public class stock
    {
        //stock
        public string ArticuloID { get; set; }
        public int Disponible { get; set; }
        public string Entrega { get; set; }

        public stock()
        {

        }

        public stock(string articuloID, int disponible, string entrega)
        {
            ArticuloID = articuloID;
            Disponible = disponible;
            Entrega = entrega;
        }
    }

    public class camara
    {
        //camara
        public string CamaraID { get; set; }
        public string Resolucion { get; set; }
        public string Sensor { get; set; }
        public string Tipo { get; set; }
        public string Factor { get; set; }
        public string Objetivo { get; set; }
        public string Pantalla { get; set; }
        public string Zoom { get; set; }

        public camara()
        {

        }

        public camara(string camaraID, string resolucion, string sensor, string tipo, 
            string factor, string objetivo, string pantalla, string zoom)
        {
            CamaraID = camaraID;
            Resolucion = resolucion;
            Sensor = sensor;
            Tipo = tipo;
            Factor = factor;
            Objetivo = objetivo;
            Pantalla = pantalla;
            Zoom = zoom;
        }
    }

    public class provincia
    {
        //provincia
        public string ProvinciaID { get; set; }
        public string Nombre { get; set; }

        public provincia()
        {

        }

        public provincia(string provinciaID, string nombre)
        {
            ProvinciaID = provinciaID;
            Nombre = nombre;
        }
    }

    public class objetivo
    {
        //objetivo
        public string ObjetivoID { get; set; }
        public string Tipo { get; set; }
        public string Montura { get; set; }
        public string Focal { get; set; }
        public string Apertura { get; set; }
        public string Especiales { get; set; }

        public objetivo()
        {

        }

        public objetivo(string objetivoID, string tipo, string montura, 
            string focal, string apertura, string especiales)
        {
            ObjetivoID = objetivoID;
            Tipo = tipo;
            Montura = montura;
            Focal = focal;
            Apertura = apertura;
            Especiales = especiales;
        }
    }
    public class tv
    {
        //tv
        public string TvID { get; set; }
        public string Panel { get; set; }
        public string Pantalla { get; set; }
        public string Resolucion { get; set; }
        public string Hdreadyfullhd { get; set; }
        public int Tdt { get; set; }

        public tv()
        {

        }

        public tv(string tvID, string panel, string pantalla, 
            string resolucion, string hdreadyfullhd, int tdt)
        {
            TvID = tvID;
            Panel = panel;
            Pantalla = pantalla;
            Resolucion = resolucion;
            Hdreadyfullhd = hdreadyfullhd;
            Tdt = tdt;
        }
    }

    public class marca
    {
        //marca
        public string MarcaID { get; set; }
        public string Empresa { get; set; }
        public string Blob { get; set; }

        public marca()
        {

        }

        public marca(string marcaID, string empresa, string blob)
        {
            MarcaID = marcaID;
            Empresa = empresa;
            Blob = blob;
        }
    }

    public class ptienea
    {
        //ptienea
        public string Pack { get; set; }
        public string Articulo { get; set; }

        public ptienea()
        {

        }

        public ptienea(string pack, string articulo)
        {
            Pack = pack;
            Articulo = articulo;
        }
    }

    public class direnvio
    {
        //direnvio
        public int UsuarioID { get; set; }
        public string Calle { get; set; }
        public string Calle2 { get; set; }
        public string Codpos { get; set; }        
        public string LocalidadID { get; set; }
        public string ProvinciaID { get; set; }

        public direnvio()
        {

        }

        public direnvio(int usuarioID, string calle, string calle2, 
            string codpos, string localidadID, string provinciaID)
        {
            UsuarioID = usuarioID;
            Calle = calle;
            Calle2 = calle2;
            Codpos = codpos;
            LocalidadID = localidadID;
            ProvinciaID = provinciaID;
        }
    }

    public class linped
    {
        //linped
        public int PedidoID { get; set; }
        public int Linea { get; set; }
        public string ArticuloID { get; set; }
        public int Importe { get; set; }
        public int Cantidad { get; set; }

        public linped()
        {

        }

        public linped(int pedidoID, int linea, string articuloID, int importe, 
            int cantidad)
        {
            PedidoID = pedidoID;
            Linea = linea;
            ArticuloID = articuloID;
            Importe = importe;
            Cantidad = cantidad;
        }
    }
}
