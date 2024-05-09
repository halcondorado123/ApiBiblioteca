using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ApiBiblioteca.Models
{
    public class LibrosME
    {
        [Key]
        [Required]
        public Guid IDLibro { get; set; }
        [Required]
        public DateTime FechaRegistro { get; set; }
        [Required]
        public string? NombreAutor { get; set; }
        [Required]
        public string? ApellidoAutor { get; set; }
        public string? Tematica { get; set; }
        [Required]
        public string? TituloLibro { get; set; }
        public string? Lugar { get; set; }
        public string? Editorial { get; set; }
        [Required]
        public bool Disponible { get; set; }


    }

    // Contexto de datos
    public class LibrosDbContext : DbContext
    {
        public LibrosDbContext(DbContextOptions<LibrosDbContext> options) : base(options)
        {
        }

        public DbSet<LibrosME> LibrosME { get; set; }
    }
}
