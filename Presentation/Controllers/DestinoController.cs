using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MantaroInclusivo.Application.Services;

namespace MantaroInclusivo.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DestinoController : ControllerBase
    {
        private readonly DestinoService _destinoService;

        public DestinoController(DestinoService destinoService)
        {
            _destinoService = destinoService;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerDestinos()
        {
            var destinos = await _destinoService.ObtenerDestinos();
            return Ok(new { success = true, data = destinos });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerDestino(int id)
        {
            var destino = await _destinoService.ObtenerDestinoDetalle(id);
            if (destino == null)
                return NotFound(new { success = false, message = "Destino no encontrado" });
            
            return Ok(new { success = true, data = destino });
        }

        [HttpGet("perfil/{perfil}")]
        public async Task<IActionResult> ObtenerDestinosPorPerfil(string perfil)
        {
            var destinos = await _destinoService.ObtenerDestinosPorPerfil(perfil);
            return Ok(new { success = true, data = destinos });
        }
    }
}