using Microsoft.EntityFrameworkCore;
using Developers.Models;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;


namespace Developers.Servicios.Contrato
{
    public interface IUsuarioServicio
    {
        Task<Usuario> GetUsuario(string Correo, string Contrasena);

        Task<Usuario> SaveUsuario(Usuario Modelo);



    }
}
