using ApiBiblioteca.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiBiblioteca.Repositories
{
    public interface ILibrosRepository
    {
        Task<IEnumerable<LibrosME>> ObtenerLibros();
        Task<LibrosME> ObtenerPorId(Guid id);
        Task<LibrosME> ObtenerPorTematica(string tematica);
        Task CrearRegistroLibro(LibrosME libro);
        // Este metodo(ActualizarRegistroLibro) puede llegar a validar si el libro regresa a biblioteca o sale de esta
        Task <Guid> ActualizarRegistroLibro(LibrosME libro);
        Task BorrarRegistroLibro(Guid id);

    }
}