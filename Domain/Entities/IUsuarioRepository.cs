using System.Collections.Generic;
using System.Threading.Tasks;
using MantaroInclusivo.Domain.Entities;

namespace MantaroInclusivo.Domain.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<int> CrearUsuario(Usuario usuario);
        Task<Usuario> ObtenerUsuarioPorId(int id);
        Task<Usuario> ObtenerUsuarioPorEmail(string email);
        Task<IEnumerable<Usuario>> ObtenerTodosUsuarios(bool activos = true);
        Task<int> ActualizarUsuario(Usuario usuario);
        Task<int> EliminarUsuario(int id);
        Task<bool> ExisteEmail(string email);
        Task<string> ObtenerPerfilUsuario(int id);
    }
}