using ListaTarefasAPI.Services;
using ListaTarefasAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ListaTarefasAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;

        public UsuarioController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var usuarios = await _usuarioService.GetAllUsuariosAsync();
            return Ok(usuarios);
        }

        [HttpGet("{email}")]
        public async Task<IActionResult> GetByEmailAsync(string email)
        {
            var usuario = await _usuarioService.GetUsuarioByEmailAsync(email);
            return usuario == null ? NotFound() : Ok(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] CreateUsuarioViewModel createUsuario)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var usuario = await _usuarioService.AddUsuarioAsync(createUsuario);
                return CreatedAtAction(nameof(GetByEmailAsync), new { email = usuario.Email }, usuario);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.InnerException?.Message ?? ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] CreateUsuarioViewModel createUsuario)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var usuario = await _usuarioService.GetUsuarioByIdAsync(id);
            if (usuario == null)
                return NotFound();

            try
            {
                usuario.Nome = createUsuario.Nome;
                usuario.Email = createUsuario.Email;
                await _usuarioService.UpdateUsuarioAsync(usuario);

                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.InnerException?.Message ?? ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var usuario = await _usuarioService.GetUsuarioByIdAsync(id);
            if (usuario == null)
                return NotFound();

            try
            {
                await _usuarioService.DeleteUsuarioAsync(usuario);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.InnerException?.Message ?? ex.Message });
            }
        }
    }
}
