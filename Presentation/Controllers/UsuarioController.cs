using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MantaroInclusivo.Application.DTOs;
using MantaroInclusivo.Application.Services;

namespace MantaroInclusivo.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;

        public UsuarioController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost("registro")]
        public async Task<IActionResult> Registro([FromBody] UsuarioRegistroDto registroDto)
        {
            try
            {
                var usuario = await _usuarioService.RegistrarUsuario(registroDto);
                return Ok(new { success = true, data = usuario });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
            catch (System.Exception)
            {
                return StatusCode(500, new { success = false, message = "Error interno del servidor" });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UsuarioLoginDto loginDto)
        {
            try
            {
                var usuario = await _usuarioService.LoginUsuario(loginDto);
                return Ok(new { success = true, data = usuario });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { success = false, message = ex.Message });
            }
            catch (System.Exception)
            {
                return StatusCode(500, new { success = false, message = "Error interno del servidor" });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerUsuario(int id)
        {
            var usuario = await _usuarioService.ObtenerUsuario(id);
            if (usuario == null)
                return NotFound(new { success = false, message = "Usuario no encontrado" });
            
            return Ok(new { success = true, data = usuario });
        }

        [HttpGet("{id}/perfil")]
        public async Task<IActionResult> ObtenerPerfil(int id)
        {
            var perfil = await _usuarioService.ObtenerPerfilUsuario(id);
            if (perfil == null)
                return NotFound(new { success = false, message = "Usuario no encontrado" });
            
            return Ok(new { success = true, perfil });
        }
    }
}