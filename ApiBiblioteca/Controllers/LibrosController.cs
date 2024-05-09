using ApiBiblioteca.Data;
using ApiBiblioteca.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiBiblioteca.Controllers
{
    [ApiController]
    [Authorize(Roles =("Administrador"))]
    [Route("[controller]")]
    public class LibrosController : Controller
    {
        private readonly LibrosDbContext _dbContext;

        private readonly ILogger<LoginController> _logger;
        public LibrosController(LibrosDbContext dbContext, ILogger<LoginController> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        [HttpGet("Libros")]
        public List<LibrosME> ObtenerLibros()
        {
            _logger.LogDebug("Metodo ObtenerLibros iniciado");
            List<LibrosME> libros = new List<LibrosME>();

            try
            {
                libros = _dbContext.LibrosME.ToList();
                _logger.LogInformation("Se ha generado la consulta general de los registros");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                _logger.LogCritical($"Se ha generado una excepción inesperada {ex}");
            }

            return libros;
        }


        [HttpGet("{id}")]
        public LibrosME? ObtenerLibroPorId(Guid idLibro)
        {
            _logger.LogDebug("Metodo ObtenerLibroPorId iniciado");
            LibrosME? libro = null;

            try
            {
                libro = _dbContext.LibrosME.FirstOrDefault(l => l.IDLibro == idLibro);
                _logger.LogInformation("Se ha generado la consulta por ID del elemento(Libro): " + $"{idLibro}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                _logger.LogCritical($"Se ha generado una excepción inesperada {ex}");
            }

            return libro;
        }

        [HttpGet("Tematica")]
        public LibrosME? ObtenerLibroPorTematica(string tematica)
        {
            _logger.LogDebug("Metodo ObtenerLibroPorTematica iniciado");
            LibrosME? libro = null;

            try
            {
                libro = _dbContext.LibrosME.FirstOrDefault(l => l.Tematica == tematica);
                _logger.LogInformation("Se ha generado la consulta por Tematica del elemento(Libro): " + $"{tematica}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                _logger.LogCritical($"Se ha generado una excepción inesperada {ex}");
            }

            return libro;
        }

        [HttpGet("Fecha")]
        public LibrosME? ObtenerLibroPorFecha(DateTime fecha)
        {
            _logger.LogDebug("Metodo ObtenerLibroPorFecha iniciado");
            LibrosME? libro = null;

            try
            {
                DateTime fechaSinHora = fecha.Date;
                libro = _dbContext.LibrosME.FirstOrDefault(l => l.FechaRegistro.Date == fechaSinHora);
                _logger.LogInformation("Se ha generado la consulta por fecha de registro del elemento(Libro)" + $"{fechaSinHora}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); 
                _logger.LogCritical($"Se ha generado una excepción inesperada {ex}");

            }

            return libro;
        }
  
        [HttpGet("Disponible")]
        public LibrosME? ObteneEstatusLibros(bool disponible)
        {
            _logger.LogDebug("Metodo ObteneEstatusLibros iniciado");
            LibrosME? libro = null;

            try
            {
                libro = _dbContext.LibrosME.FirstOrDefault(l => l.Disponible == disponible);
                _logger.LogInformation("Se ha generado la consulta estatus de disponibilidad del elemento(Libro): " + $"{disponible}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                _logger.LogCritical($"Se ha generado una excepción inesperada {ex}");
            }

            return libro;
        }


        [HttpPost]
        public IActionResult CrearRegistroLibro([FromBody] LibrosME libro)
        {
            libro.IDLibro = Guid.NewGuid();

            _logger.LogDebug("Metodo CrearRegistroLibro iniciado");
            try
            {
                _dbContext.LibrosME.Add(libro);
                _dbContext.SaveChanges();

                _logger.LogDebug("Se ha creado un registro de un nuevo elemento(Libro) exitosamente");
                return Ok("Libro ingresado exitosamente");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                _logger.LogCritical($"Se ha generado una excepción inesperada {ex}");
                return StatusCode(500, $"Error al crear el libro: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public IActionResult ActualizarRegistroLibro([FromBody] LibrosME libro, Guid id )
        {
            _logger.LogDebug("Metodo ActualizarRegistroLibro iniciado");
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
                _logger.LogDebug("Se ha actualizado un registro de un elemento(Libro) existente, exitosamente");
                return Ok("Libro actualizado exitosamente");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                _logger.LogCritical($"Se ha generado una excepción inesperada {ex}");
                return StatusCode(500, $"Error al actualizar el libro: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult EliminarRegistroLibro(Guid id)
        {
            _logger.LogDebug("Metodo EliminarRegistroLibro iniciado");
            try
            {
                var libroExistente = _dbContext.LibrosME.FirstOrDefault(l => l.IDLibro == id);

                if (libroExistente == null)
                {
                    return NotFound("Libro no encontrado");
                }

                _dbContext.LibrosME.Remove(libroExistente);
                _dbContext.SaveChanges();
                _logger.LogWarning("Se ha eliminado un registro de un elemento(Libro) existente, exitosamente");
                return Ok("Libro eliminado exitosamente");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                _logger.LogCritical($"Se ha generado una excepción inesperada {ex}");
                return StatusCode(500, $"Error al eliminar el libro: {ex.Message}");
            }
        }
    }
}
