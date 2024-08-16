using ListaTarefasAPI.Data;
using ListaTarefasAPI.Models;
using ListaTarefasAPI.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace ListaTarefasAPI.Services
{
    public class TarefaService
    {
        private readonly AppDbContext _context;

        public TarefaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Tarefa>> GetAllTarefasAsync()
        {
            return await _context.Tarefas.AsNoTracking().ToListAsync();
        }

        public async Task<Tarefa?> GetTarefaByIdAsync(int id)
        {
            return await _context.Tarefas.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Tarefa> AddTarefaAsync(CreateTarefaViewModel createTarefa)
        {
            var tarefa = new Tarefa
            {
                Titulo = createTarefa.Titulo,
                Descricao = createTarefa.Descricao,
                Prioridade = createTarefa?.Prioridade,
                UsuarioId = (int)(createTarefa?.UsuarioId),
                CategoriaId = (int)(createTarefa?.CategoriaId),
                DataCriacao = DateTime.UtcNow,
            };

            await _context.Tarefas.AddAsync(tarefa);
            await _context.SaveChangesAsync();

            return tarefa;
        }

        public async Task UpdateTarefaAsync(Tarefa tarefa)
        {
            _context.Tarefas.Update(tarefa);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTarefaAsync(Tarefa tarefa)
        {
            _context.Tarefas.Remove(tarefa);
            await _context.SaveChangesAsync();
        }
    }
}
