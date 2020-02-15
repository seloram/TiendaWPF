using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidades;

namespace CapaNegocio
{   
    public class negocio
    {
        private datos dat;
        public usuario usu;

        public negocio()
        {
            dat = new datos();            
        }

        public bool validar(string usu, string pass)
        {            
            List<usuario> lista_usuarios = dat.leer_usuarios();

            if(lista_usuarios != null)
            {
                for(int i =0; i < lista_usuarios.Count; i++)
                {
                    if (lista_usuarios[i].Nombre == usu)
                    {
                        captarUsuario(lista_usuarios[i]);
                        
                        return (true);
                    }
                }
            }
            return false;
        }

        public void captarUsuario(usuario usuario)
        {            
            usu = usuario;
        }

        public List<usuario> usuarios()
        {
            return dat.leer_usuarios();
        }

        public List<provincia> provincias()
        {
            return dat.LeerProvincias();
        }

        public List<localidad> localidades()
        {
            return dat.LeerLocalidades();
        }

        public List<pedido> pedidos()
        {
            return dat.leer_pedidos();
        }

        public List<articulo> articulos()
        {
            return dat.leer_articulos();
        }

        public List<tipoarticulo> tipoArticulos()
        {
            return dat.leer_tipoProductos();
        }

        //public bool modificar(int id, string nom, string email, string pass,
        //    string apellidos, string dni, string telefono, string calle, string calle2, string codpos,
        //    string puebloID, string provinciaID, string nacido)
        public bool modificar(usuario usu)
        {
            //return (dat.Modificar(id, nom, email, pass, apellidos, dni,
            //        telefono, calle, calle2, codpos, puebloID, provinciaID, nacido));
            return (dat.Modificar(usu));
        }

        public bool insertar(string nom, string email, string pass,
            string apellidos, string dni, string telefono, string calle, string calle2, string codpos,
            string puebloID, string provinciaID, string nacido)
        {
            return (dat.Insertar(nom, email, pass, apellidos, dni,
                    telefono, calle, calle2, codpos, puebloID, provinciaID, nacido));
        }

        public bool eliminar(string id)
        {
            return (dat.Eliminar(id));
        }
    }
}
