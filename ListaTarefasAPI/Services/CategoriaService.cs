using ListaTarefasAPI.Data;
using ListaTarefasAPI.Models;
using ListaTarefasAPI.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace ListaTarefasAPI.Services
{
    public class CategoriaService
    {
        private readonly AppDbContext _context;

        public CategoriaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Categoria>> GetAllCategoriasAsync()
        {
            return await _context.Categorias.AsNoTracking().ToListAsync();
        }

        public async Task<Categoria?> GetCategoriaByIdAsync(int id)
        {
            return await _context.Categorias.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Categoria> AddCategoriaAsync(CreateCategoriaViewModel createCategoria)
        {
            var categoria = new Categoria
            {
                Nome = createCategoria.Nome,
                Descricao = createCategoria?.Descricao,
            };

            await _context.Categorias.AddAsync(categoria);
            await _context.SaveChangesAsync();

            return categoria;
        }

        public async Task UpdateCategoriaAsync(Categoria categoria)
        {
            _context.Categorias.Update(categoria);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCategoriaAsync(Categoria categoria)
        {
            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();
        }
    }
}
