using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using MantaroInclusivo.Domain.Entities;
using MantaroInclusivo.Domain.Interfaces;
using MantaroInclusivo.Infrastructure.Database;

namespace MantaroInclusivo.Infrastructure.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly DatabaseConnection _databaseConnection;

        public UsuarioRepository(DatabaseConnection databaseConnection)
        {
            _databaseConnection = databaseConnection;
        }

        public async Task<int> CrearUsuario(Usuario usuario)
        {
            using var connection = _databaseConnection.CreateConnection();
            var parameters = new DynamicParameters();
            parameters.Add("@Operacion", "INSERT");
            parameters.Add("@Nombres", usuario.Nombres);
            parameters.Add("@Apellidos", usuario.Apellidos);
            parameters.Add("@Email", usuario.Email);
            parameters.Add("@Telefono", usuario.Telefono);
            parameters.Add("@ContraseñaHash", usuario.ContraseñaHash);
            parameters.Add("@PerfilAccesibilidad", usuario.PerfilAccesibilidad);

            var result = await connection.QuerySingleAsync<int>(
                "sp_Usuarios_Crud",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return result;
        }

        public async Task<Usuario> ObtenerUsuarioPorId(int id)
        {
            using var connection = _databaseConnection.CreateConnection();
            var parameters = new DynamicParameters();
            parameters.Add("@Operacion", "SELECTBYID");
            parameters.Add("@Id", id);

            var result = await connection.QuerySingleOrDefaultAsync<Usuario>(
                "sp_Usuarios_Crud",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return result;
        }

        public async Task<Usuario> ObtenerUsuarioPorEmail(string email)
        {
            using var connection = _databaseConnection.CreateConnection();
            var parameters = new DynamicParameters();
            parameters.Add("@Operacion", "SELECTBYEMAIL");
            parameters.Add("@Email", email);

            var result = await connection.QuerySingleOrDefaultAsync<Usuario>(
                "sp_Usuarios_Crud",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return result;
        }

        public async Task<IEnumerable<Usuario>> ObtenerTodosUsuarios(bool activos = true)
        {
            using var connection = _databaseConnection.CreateConnection();
            var parameters = new DynamicParameters();
            parameters.Add("@Operacion", "SELECT");
            parameters.Add("@Activo", activos);

            var result = await connection.QueryAsync<Usuario>(
                "sp_Usuarios_Crud",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return result;
        }

        public async Task<int> ActualizarUsuario(Usuario usuario)
        {
            using var connection = _databaseConnection.CreateConnection();
            var parameters = new DynamicParameters();
            parameters.Add("@Operacion", "UPDATE");
            parameters.Add("@Id", usuario.Id);
            parameters.Add("@Nombres", usuario.Nombres);
            parameters.Add("@Apellidos", usuario.Apellidos);
            parameters.Add("@Email", usuario.Email);
            parameters.Add("@Telefono", usuario.Telefono);
            parameters.Add("@ContraseñaHash", usuario.ContraseñaHash);
            parameters.Add("@PerfilAccesibilidad", usuario.PerfilAccesibilidad);
            parameters.Add("@Activo", usuario.Activo);

            var result = await connection.QuerySingleAsync<int>(
                "sp_Usuarios_Crud",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return result;
        }

        public async Task<int> EliminarUsuario(int id)
        {
            using var connection = _databaseConnection.CreateConnection();
            var parameters = new DynamicParameters();
            parameters.Add("@Operacion", "DELETE");
            parameters.Add("@Id", id);

            var result = await connection.QuerySingleAsync<int>(
                "sp_Usuarios_Crud",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return result;
        }

        public async Task<bool> ExisteEmail(string email)
        {
            var usuario = await ObtenerUsuarioPorEmail(email);
            return usuario != null;
        }

        public async Task<string> ObtenerPerfilUsuario(int id)
        {
            var usuario = await ObtenerUsuarioPorId(id);
            return usuario?.PerfilAccesibilidad;
        }
    }
}