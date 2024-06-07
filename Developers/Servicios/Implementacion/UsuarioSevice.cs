
using Microsoft.EntityFrameworkCore;
using Developers.Models;
using Developers.Servicios.Contrato;
using System.Runtime.InteropServices;
using System.Runtime.ExceptionServices;

namespace Developers.Servicios.Implementacion
{
    public class UsuarioSevice : IUsuarioServicio
    {
        private readonly MercyDeveloperContext _context;
        public UsuarioSevice(MercyDeveloperContext context)
        {
            _context = context;
        }
        public async Task<Usuario> GetUsuario(string Correo, string Password)
        {
            Usuario usuarioEncontrado = await _context.Usuarios
                .Where(u => u.Correo == Correo && u.Password == Password)
                .FirstOrDefaultAsync();
            return usuarioEncontrado;
        }

        public async Task<Usuario> SaveUsuario(Usuario modelo)
        {
            _context.Usuarios.Add(modelo);
            await _context.SaveChangesAsync();
            return modelo;
        }
    }
}
