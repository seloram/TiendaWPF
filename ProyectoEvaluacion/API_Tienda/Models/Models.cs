using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using API_Tienda.Models;


namespace API_Tienda.Models
{
    //Usuario
    public class Usuario
    {
        [Key]
        public string usuarioID { get; set; }
        public string email {get; set; }
        public string password { get; set; }
        public string nombre { get; set; }
        public string apellidos { get; set; }
        public string dni { get; set; }
        public string telefono { get; set; }
        public string calle { get; set; }
        public string calle2 { get; set; }
        public string codpos { get; set; }
        [ForeignKey("localidad")]
        public string puebloID { get; set; }
        [ForeignKey("provincia")]
        public string provinciaID { get; set; }
        public string nacido { get; set; }
    }

    // Tipo de articulo
    public class tipoArticulo
    {
        [Key]
        public int tipoArticuloID {get; set;}
        public string descripcion { get; set; }
    }

    // Articulo
    public class Articulo
    {
        [Key]
        public string articuloID { get; set; }
        public string nombre { get; set; }
        public int? pvp { get; set; }
        [ForeignKey("marca")]
        public string marcaID { get; set; }
        public string imagen { get; set; }
        public string urlimagen { get; set; }
        public string especificaciones { get; set; }
        [ForeignKey("tipoArticulo")]
        public int? tipoArticuloID { get; set; }
    }

    // Pedidos
    public class Pedido
    {
        [Key]
        public string pedidoID { get; set; }
        [ForeignKey("Usuario")]
        public string usuarioID { get; set; }
        public string fecha { get; set; }
    }

    // Lineas de pedido
    public class Linped
    {
        [ForeignKey("Pedido")]
        public string pedidoID { get; set; }
        public int linea { get; set; }
        [ForeignKey("Articulo")]
        public string articuloID { get; set; }
        public int importe { get; set; }
        public int cantidad { get; set; }
    }

    // Provinvias
    public class Provincia
    {
        [Key]
        public string provinciaID { get; set; }
        public string nombre { get; set; }
    }

    // Localidad
    public class Localidad
    {
        public string localidadID { get; set; }
        public string nombre { get; set; }

        [ForeignKey("Provincia")]
        public string provinciaID { get; set; }
    }

    // Marca
    public class Marca
    {
        [Key]
        public string marcaID { get; set; }
        public string empresa { get; set; }
        public string logo { get; set; }
    }

    // Stock
    public class Stock
    {
        [Key]
        [ForeignKey("Articulo")]
        public string articuloID { get; set; }
        public int disponible { get; set; }
        public string entrega { get; set; }
    }

    // Memoria
    public class Memoria
    {
        [Key]
        public string memoriaID { get; set; }
        public string tipo { get; set; }
    }

    // Memoria
    public class Objetivo
    {
        [Key]
        public string objetivoID { get; set; }
        public string tipo { get; set; }
        public string montura { get; set; }
        public string focal { get; set; }
        public string apertura { get; set; }
        public string especiales { get; set; }

    }

    public class Tv
    {
        [Key]
        public string tvID { get; set; }
        public string panel { get; set; }
        public string pantalla { get; set; }
        public string resolucion { get; set; }
        public string hdreadyfullhd { get; set; }
        public string tdt { get; set; }

    }

    public class Camara
    {
        [Key]
        public string camaraID { get; set; }
        public string resolucion { get; set; }
        public string sensor { get; set; }
        public string tipo { get; set; }
        public string factor { get; set; }
        public string objetivo { get; set; }
        public string pantalla { get; set; }
        public string zoom { get; set; }

    }
            
    // Contexto de la Base de datos
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Linped>().HasKey(l => new { l.pedidoID, l.linea });

            modelBuilder.Entity<Localidad>().HasKey(lo => new { lo.provinciaID, lo.localidadID });
        }

        // El campo debe tener el mismo nombre que la tabla de la BD
        public DbSet<Articulo> Articulo { get; set; }
        public DbSet<Provincia> Provincia { get; set; }
        public DbSet<API_Tienda.Models.Localidad> Localidad { get; set; }
        public DbSet<tipoArticulo> tipoArticulo { get; set; }
        public DbSet<API_Tienda.Models.Marca> Marca { get; set; }
        public DbSet<API_Tienda.Models.Usuario> Usuario { get; set; }
        public DbSet<API_Tienda.Models.Pedido> Pedido { get; set; }
        public DbSet<API_Tienda.Models.Linped> Linped { get; set; }
        public DbSet<API_Tienda.Models.Stock> Stock { get; set; }
        public DbSet<API_Tienda.Models.Memoria> Memoria { get; set; }
        public DbSet<API_Tienda.Models.Objetivo> Objetivo { get; set; }
        public DbSet<API_Tienda.Models.Tv> Tv { get; set; }
        public DbSet<API_Tienda.Models.Camara> Camara { get; set; }

    }
}
