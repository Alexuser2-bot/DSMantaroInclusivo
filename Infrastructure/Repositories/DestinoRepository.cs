using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using MantaroInclusivo.Domain.Entities;
using MantaroInclusivo.Domain.Interfaces;
using MantaroInclusivo.Infrastructure.Database;

namespace MantaroInclusivo.Infrastructure.Repositories
{
    public class DestinoRepository : IDestinoRepository
    {
        private readonly DatabaseConnection _databaseConnection;

        public DestinoRepository(DatabaseConnection databaseConnection)
        {
            _databaseConnection = databaseConnection;
        }

        public async Task<IEnumerable<Destino>> ObtenerTodosDestinos()
        {
            using var connection = _databaseConnection.CreateConnection();
            var parameters = new DynamicParameters();
            parameters.Add("@Operacion", "SELECT");

            var result = await connection.QueryAsync<Destino>(
                "sp_Destinos_Crud",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return result;
        }

        public async Task<Destino> ObtenerDestinoPorId(int id)
        {
            using var connection = _databaseConnection.CreateConnection();
            var parameters = new DynamicParameters();
            parameters.Add("@Operacion", "SELECTBYID");
            parameters.Add("@Id", id);

            var result = await connection.QuerySingleOrDefaultAsync<Destino>(
                "sp_Destinos_Crud",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return result;
        }

        public async Task<IEnumerable<Destino>> ObtenerDestinosPorPerfil(string perfilAccesibilidad)
        {
            using var connection = _databaseConnection.CreateConnection();
            var parameters = new DynamicParameters();
            parameters.Add("@Operacion", "SELECTBYPERFIL");
            parameters.Add("@PerfilAccesibilidad", perfilAccesibilidad);

            var result = await connection.QueryAsync<Destino>(
                "sp_Destinos_Crud",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return result;
        }
    }
}