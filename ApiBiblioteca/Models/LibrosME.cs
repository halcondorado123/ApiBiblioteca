using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ApiBiblioteca.Models
{
    public class LibrosME
    {
        [Key]
        public Guid IDLibro { get; set; }
        public string? NombreAutor { get; set; }
        public string? ApellidoAutor { get; set; }
        public string? Tematica { get; set; }
        public string? TituloLibro { get; set; }
        public string? Lugar { get; set; }
        public string? Editorial { get; set; }
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
