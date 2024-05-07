using ApiBiblioteca.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ApiBiblioteca.Repositories
{
    public class LibrosRepository : ILibrosRepository
    {
        private readonly LibrosDbContext _dbContext;

        public LibrosRepository(LibrosDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<LibrosME>> ObtenerLibros()
        {
            try
            {
                return await _dbContext.LibrosME.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

        }
        public Task<LibrosME> ObtenerPorId(Guid id)
        {
            try
            {
                return _dbContext.LibrosME.FirstOrDefaultAsync(l => l.IDLibro == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        public Task<LibrosME> ObtenerPorTematica(string tematica)
        {
            try
            {
                return _dbContext.LibrosME.FirstOrDefaultAsync(l => l.Tematica == tematica);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        public async Task CrearRegistroLibro(LibrosME libro)
        {
            try
            {
                _dbContext.LibrosME.Add(libro);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<Guid> ActualizarRegistroLibro(LibrosME libro)
        {
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
                throw;
            }
        }
        public async Task BorrarRegistroLibro(Guid id)
        {
            try
            {
                var borradoLibro = new LibrosME() { IDLibro = id };
                _dbContext.LibrosME.Remove(borradoLibro);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
