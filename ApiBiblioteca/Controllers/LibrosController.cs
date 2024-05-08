using ApiBiblioteca.Data;
using ApiBiblioteca.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiBiblioteca.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LibrosController : Controller
    {
        private readonly LibrosDbContext _dbContext;

        public LibrosController(LibrosDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [Authorize]
        [HttpGet("Libros")]
        public List<LibrosME> ObtenerLibros()
        {
            List<LibrosME> libros = new List<LibrosME>();

            try
            {
                libros = _dbContext.LibrosME.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return libros;
        }

        [Authorize]
        [HttpGet("{id}")]
        public LibrosME? ObtenerLibroPorId(Guid idLibro)
        {
            LibrosME? libro = null;

            try
            {
                libro = _dbContext.LibrosME.FirstOrDefault(l => l.IDLibro == idLibro);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return libro;
        }
        [Authorize]
        [HttpGet("Tematica")]
        public LibrosME? ObtenerLibroPorTematica(string tematica)
        {
            LibrosME? libro = null;

            try
            {
                libro = _dbContext.LibrosME.FirstOrDefault(l => l.Tematica == tematica);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return libro;
        }
        [Authorize]
        [HttpGet("Fecha")]
        public LibrosME? ObtenerLibroPorFecha(DateTime fecha)
        {
            LibrosME? libro = null;

            try
            {
                DateTime fechaSinHora = fecha.Date;

                libro = _dbContext.LibrosME.FirstOrDefault(l => l.FechaRegistro.Date == fechaSinHora);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return libro;
        }
        [Authorize]
        [HttpGet("Disponible")]
        public LibrosME? ObteneEstatusLibros(bool disponible)
        {
            LibrosME? libro = null;

            try
            {
                libro = _dbContext.LibrosME.FirstOrDefault(l => l.Disponible == disponible);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return libro;
        }

        [Authorize]
        [HttpPost]
        public IActionResult CrearRegistroLibro([FromBody] LibrosME libro)
        {
            try
            {
                _dbContext.LibrosME.Add(libro);
                _dbContext.SaveChanges();

                return Ok("Libro ingresado exitosamente");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, $"Error al crear el libro: {ex.Message}");
            }
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult ActualizarRegistroLibro([FromBody] LibrosME libro, Guid id )
        {
            try
            {
                var libroExistente = _dbContext.LibrosME.FirstOrDefault(l => l.IDLibro == id);

                if (libroExistente == null)
                {
                    return NotFound("Libro no encontrado");
                }

                libroExistente.NombreAutor = libro.NombreAutor;
                libroExistente.ApellidoAutor = libro.ApellidoAutor;
                libroExistente.Tematica = libro.Tematica;
                libroExistente.TituloLibro = libro.TituloLibro;
                libroExistente.Lugar = libro.Lugar;
                libroExistente.Editorial = libro.Editorial;
                libroExistente.Disponible = libro.Disponible;

                _dbContext.SaveChanges();

                return Ok("Libro actualizado exitosamente");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, $"Error al actualizar el libro: {ex.Message}");
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult EliminarRegistroLibro(Guid id)
        {
            try
            {
                var libroExistente = _dbContext.LibrosME.FirstOrDefault(l => l.IDLibro == id);

                if (libroExistente == null)
                {
                    return NotFound("Libro no encontrado");
                }

                _dbContext.LibrosME.Remove(libroExistente);
                _dbContext.SaveChanges();

                return Ok("Libro eliminado exitosamente");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, $"Error al eliminar el libro: {ex.Message}");
            }
        }
    }
}
