using ListaTarefasAPI.Data;
using ListaTarefasAPI.Models;
using ListaTarefasAPI.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ListaTarefasAPI.Services
{
    public class UsuarioService
    {
        private readonly AppDbContext _context;
        private readonly PasswordHasher<Usuario> _passwordHasher;

        public UsuarioService(AppDbContext context)
        {
            _context = context;
            _passwordHasher = new PasswordHasher<Usuario>();
        }

        public async Task<List<Usuario>> GetAllUsuariosAsync()
        {
            return await _context.Usuarios.AsNoTracking().ToListAsync();
        }

        public async Task<Usuario?> GetUsuarioByEmailAsync(string email)
        {
            return await _context.Usuarios.AsNoTracking().FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<Usuario?> GetUsuarioByIdAsync(int id)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Usuario> AddUsuarioAsync(CreateUsuarioViewModel createUsuario)
        {
            var usuario = new Usuario
            {
                Nome = createUsuario.Nome,
                Email = createUsuario.Email,
                DataCriacao = DateTime.UtcNow,
            };

            usuario.Senha = _passwordHasher.HashPassword(usuario, createUsuario.Senha);

            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();

            return usuario;
        }

        public async Task UpdateUsuarioAsync(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUsuarioAsync(Usuario usuario)
        {
            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
        }
    }
}
