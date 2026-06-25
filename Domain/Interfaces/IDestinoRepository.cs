using System.Collections.Generic;
using System.Threading.Tasks;
using MantaroInclusivo.Domain.Entities;

namespace MantaroInclusivo.Domain.Interfaces
{
    public interface IDestinoRepository
    {
        Task<IEnumerable<Destino>> ObtenerTodosDestinos();
        Task<Destino> ObtenerDestinoPorId(int id);
        Task<IEnumerable<Destino>> ObtenerDestinosPorPerfil(string perfilAccesibilidad);
    }
}