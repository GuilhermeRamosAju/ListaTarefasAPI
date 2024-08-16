using ListaTarefasAPI.Services;
using ListaTarefasAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ListaTarefasAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarefaController : ControllerBase
    {
        private readonly TarefaService _tarefaService;

        public TarefaController(TarefaService tarefaService)
        {
            _tarefaService = tarefaService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var tarefas = await _tarefaService.GetAllTarefasAsync();
            return Ok(tarefas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var tarefa = await _tarefaService.GetTarefaByIdAsync(id);
            return tarefa == null ? NotFound() : Ok(tarefa);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] CreateTarefaViewModel createTarefa)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var tarefa = await _tarefaService.AddTarefaAsync(createTarefa);
                return CreatedAtAction(nameof(GetByIdAsync), new { id = tarefa.Id }, tarefa);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.InnerException?.Message ?? ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] CreateTarefaViewModel createTarefa)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var tarefa = await _tarefaService.GetTarefaByIdAsync(id);
            if (tarefa == null)
                return NotFound();

            try
            {
                tarefa.Titulo = createTarefa.Titulo;
                tarefa.Descricao = createTarefa.Descricao;
                await _tarefaService.UpdateTarefaAsync(tarefa);

                return Ok(tarefa);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.InnerException?.Message ?? ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var tarefa = await _tarefaService.GetTarefaByIdAsync(id);
            if (tarefa == null)
                return NotFound();

            try
            {
                await _tarefaService.DeleteTarefaAsync(tarefa);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.InnerException?.Message ?? ex.Message });
            }
        }
    }
}
