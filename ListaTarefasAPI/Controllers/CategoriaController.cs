using ListaTarefasAPI.Services;
using ListaTarefasAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ListaTarefasAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriaController : ControllerBase
    {
        private readonly CategoriaService _categoriaService;

        public CategoriaController(CategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var tarefas = await _categoriaService.GetAllCategoriasAsync();
            return Ok(tarefas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var tarefa = await _categoriaService.GetCategoriaByIdAsync(id);
            return tarefa == null ? NotFound() : Ok(tarefa);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] CreateCategoriaViewModel createCategoria)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var categoria = await _categoriaService.AddCategoriaAsync(createCategoria);
                return CreatedAtAction(nameof(GetByIdAsync), new { id = categoria.Id }, categoria);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.InnerException?.Message ?? ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] CreateCategoriaViewModel createCategoria)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var categoria = await _categoriaService.GetCategoriaByIdAsync(id);
            if (categoria == null)
                return NotFound();

            try
            {
                categoria.Nome = createCategoria.Nome;
                categoria.Descricao = createCategoria?.Descricao;
                await _categoriaService.UpdateCategoriaAsync(categoria);

                return Ok(categoria);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.InnerException?.Message ?? ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var categoria = await _categoriaService.GetCategoriaByIdAsync(id);
            if (categoria == null)
                return NotFound();

            try
            {
                await _categoriaService.DeleteCategoriaAsync(categoria);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.InnerException?.Message ?? ex.Message });
            }
        }
    }
}
