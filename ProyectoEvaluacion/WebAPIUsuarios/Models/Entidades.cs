using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WebAPIUsuarios.Models
{
    public class usuarios
    {
        [Key]
        public int usuarioId { get; set; }
        public string nombre { get; set; }
        public string pass { get; set; }
    }

    public class dbContext : DbContext
    {
        public dbContext(DbContextOptions<dbContext> options)
            : base(options)
        {
        }

        // Si tienes mas clases moleos las pones aqui.
        // Luego creas un controlador para cada una de ellas.
        public DbSet<usuarios> Usuarios { get; set; }
    }

}
