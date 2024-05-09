using ApiBiblioteca.Controllers;
using ApiBiblioteca.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ApiBiblioteca.Repositories
{
    public class LibrosRepository : ILibrosRepository
    {
        private readonly LibrosDbContext _dbContext;

        private readonly ILogger<LoginController> _logger;

        public LibrosRepository(LibrosDbContext dbContext, ILogger<LoginController> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<IEnumerable<LibrosME>> ObtenerLibros()
        {
            _logger.LogDebug("Metodo ObtenerLibros iniciado");
            try
            {
                _logger.LogInformation("Se ha generado la consulta general de los registros");
                return await _dbContext.LibrosME.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                _logger.LogCritical($"Se ha generado una excepción inesperada {ex}");
                throw;
            }

        }
        public Task<LibrosME> ObtenerPorId(Guid id)
        {
            _logger.LogDebug("Metodo ObtenerPorId iniciado");
            try
            {
                _logger.LogInformation("Se ha generado la consulta por ID del elemento(Libro): " + $"{id}");
                return _dbContext.LibrosME.FirstOrDefaultAsync(l => l.IDLibro == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                _logger.LogCritical($"Se ha generado una excepción inesperada {ex}");
                throw;
            }
        }
        public Task<LibrosME> ObtenerPorTematica(string tematica)
        {
            _logger.LogDebug("Metodo ObtenerPorTematica iniciado");
            try
            {
                _logger.LogInformation("Se ha generado la consulta por Tematica del elemento(Libro): " + $"{tematica}");
                return _dbContext.LibrosME.FirstOrDefaultAsync(l => l.Tematica == tematica);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                _logger.LogCritical($"Se ha generado una excepción inesperada {ex}");
                throw;
            }
        }

        public Task<LibrosME> ObtenerPorFechas(DateTime fecha)
        {
            _logger.LogDebug("Metodo ObtenerPorFechas iniciado");
            try
            {
                _logger.LogInformation("Se ha generado la consulta por fecha de registro del elemento(Libro)" + $"{fecha}");
                return _dbContext.LibrosME.FirstOrDefaultAsync(l => l.FechaRegistro == fecha);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                _logger.LogCritical($"Se ha generado una excepción inesperada {ex}");
                throw;
            }
        }
        public Task<LibrosME> ObtenerPorDisponibilidad(bool disponible)
        {
            _logger.LogDebug("Metodo ObtenerPorDisponibilidad iniciado");
            try
            {
                _logger.LogInformation("Se ha generado la consulta estatus de disponibilidad del elemento(Libro): " + $"{disponible}");
                return _dbContext.LibrosME.FirstOrDefaultAsync(l => l.Disponible == disponible);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                _logger.LogCritical($"Se ha generado una excepción inesperada {ex}");
                throw;
            }
        }

        public async Task CrearRegistroLibro(LibrosME libro)
        {
            _logger.LogDebug("Metodo CrearRegistroLibro iniciado");
            try
            {
                _dbContext.LibrosME.Add(libro);
                await _dbContext.SaveChangesAsync();
                _logger.LogDebug("Se ha creado un registro de un nuevo elemento(Libro)");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                _logger.LogCritical($"Se ha generado una excepción inesperada {ex}");
                throw;
            }
        }

        public async Task<Guid> ActualizarRegistroLibro(LibrosME libro)
        {
            _logger.LogDebug("Metodo ActualizarRegistroLibro iniciado");
            // Buscar el libro existente por su ID
            var libroExistente = await _dbContext.LibrosME.FirstOrDefaultAsync(l => l.IDLibro == libro.IDLibro);

            try
            {
                if (libroExistente != null)
                {
                    // Actualizar las propiedades del libro existente con los valores proporcionados
                    libroExistente.NombreAutor = libro.NombreAutor;
                    libroExistente.ApellidoAutor = libro.ApellidoAutor;
                    libroExistente.Tematica = libro.Tematica;
                    libroExistente.TituloLibro = libro.TituloLibro;
                    libroExistente.Lugar = libro.Lugar;
                    libroExistente.Editorial = libro.Editorial;
                    libroExistente.Disponible = libro.Disponible;

                    // Guardar los cambios en la base de datos
                    await _dbContext.SaveChangesAsync();
                    _logger.LogDebug("Se ha actualizado un registro de un elemento(Libro) existente");
                    return libroExistente.IDLibro;
                }
                else
                {
                    // Manejar el caso en el que el libro no existe
                    throw new Exception("El libro no existe en la base de datos.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                _logger.LogCritical($"Se ha generado una excepción inesperada {ex}");
                throw;
            }
        }
        public async Task BorrarRegistroLibro(Guid id)
        {
            _logger.LogDebug("Metodo BorrarRegistroLibro iniciado");
            try
            {
                var borradoLibro = new LibrosME() { IDLibro = id };
                _dbContext.LibrosME.Remove(borradoLibro);
                await _dbContext.SaveChangesAsync();
                _logger.LogWarning("Se ha eliminado un registro de un elemento(Libro) existente");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                _logger.LogCritical($"Se ha generado una excepción inesperada {ex}");
                throw;
            }
        }
    }
}
