using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tecnicell.Server.Context;
using Tecnicell.Server.Logic;
using Tecnicell.Server.Services;


namespace Tecnicell.Server.Controllers.Api.DbController
{
    [Route("api/db")]
    [ApiController]
    [Authorize(Roles = "KKYW_rkaT_Sñ64_jtRK")]
    public class DbController : ControllerBase
    {
        private readonly TecnicellContext _context;

        public DbController(TecnicellContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult SaveDb()
        {
            try
            {
                // Llamar a tu método para guardar la base de datos
                DbDataSave.DbSave(_context);

                // Devolver un resultado OK con un mensaje de éxito
                return Ok(new { message = "La base de datos se ha guardado correctamente en C://DbData/." });
            }
            catch (Exception ex)
            {
                // Manejar cualquier error que pueda ocurrir y devolver un resultado de error
                return StatusCode(500, new { message = "Ocurrió un error al guardar la base de datos.", error = ex.Message });
            }
        }

    }
}
