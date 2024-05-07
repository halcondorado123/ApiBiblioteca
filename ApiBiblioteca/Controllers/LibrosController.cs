using Microsoft.AspNetCore.Mvc;

namespace ApiBiblioteca.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LibrosController : Controller
    {
        public LibrosController()
        {

        }

        [HttpGet]
        public async Task<IActionResult> Obtener()
        {
            await Task.CompletedTask;
            return Ok();
        }

        //[HttpGet]
        //public async Task<IActionResult> ObtenerPorId()
        //{
        //    await Task.CompletedTask;
        //    return Ok();
        //}

        //[HttpGet]
        //public async Task<IActionResult> ObtenerPorTematica()
        //{
        //    await Task.CompletedTask;
        //    return Ok();
        //}

        //[HttpPost]
        //public async Task<IActionResult> Crear()
        //{

        //    await Task.CompletedTask;
        //    return Ok();
        //}

        //[HttpPut]
        //public async Task<IActionResult> Actualizar()
        //{
        //    await Task.CompletedTask;
        //    return Ok();
        //}

        //[HttpDelete]
        //public async Task<IActionResult> Borrar()
        //{
        //    await Task.CompletedTask;
        //    return Ok();
        //}
    }
}
