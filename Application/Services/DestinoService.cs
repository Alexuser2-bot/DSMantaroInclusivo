using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MantaroInclusivo.Domain.Interfaces;
using MantaroInclusivo.Application.DTOs;

namespace MantaroInclusivo.Application.Services
{
    public class DestinoService
    {
        private readonly IDestinoRepository _destinoRepository;

        public DestinoService(IDestinoRepository destinoRepository)
        {
            _destinoRepository = destinoRepository;
        }

        public async Task<IEnumerable<DestinoDto>> ObtenerDestinos()
        {
            var destinos = await _destinoRepository.ObtenerTodosDestinos();
            return destinos.Select(MapToDto);
        }

        public async Task<DestinoDetalleDto> ObtenerDestinoDetalle(int id)
        {
            var destino = await _destinoRepository.ObtenerDestinoPorId(id);
            if (destino == null) return null;

            return new DestinoDetalleDto
            {
                Id = destino.Id,
                Nombre = destino.Nombre,
                Descripcion = destino.Descripcion,
                Latitud = destino.Latitud,
                Longitud = destino.Longitud,
                ImagenURL = destino.ImagenURL,
                Rampas = destino.Rampas,
                BanosAccesibles = destino.BanosAccesibles,
                EstacionamientoReservado = destino.EstacionamientoReservado,
                AudioGuias = destino.AudioGuias,
                RutasTactiles = destino.RutasTactiles,
                Pictogramas = destino.Pictogramas,
                PersonalCapacitado = destino.PersonalCapacitado,
                SenalizacionBraille = destino.SenalizacionBraille,
                ResumenAccesibilidad = GenerarResumenAccesibilidad(destino)
            };
        }

        public async Task<IEnumerable<DestinoDto>> ObtenerDestinosPorPerfil(string perfil)
        {
            var destinos = await _destinoRepository.ObtenerDestinosPorPerfil(perfil);
            return destinos.Select(MapToDto);
        }

        private static DestinoDto MapToDto(Domain.Entities.Destino destino)
        {
            return new DestinoDto
            {
                Id = destino.Id,
                Nombre = destino.Nombre,
                Descripcion = destino.Descripcion,
                Latitud = destino.Latitud,
                Longitud = destino.Longitud,
                ImagenURL = destino.ImagenURL,
                ResumenAccesibilidad = GenerarResumenAccesibilidad(destino)
            };
        }

        private static string GenerarResumenAccesibilidad(Domain.Entities.Destino destino)
        {
            var caracteristicas = new List<string>();
            if (destino.Rampas) caracteristicas.Add("Rampas");
            if (destino.BanosAccesibles) caracteristicas.Add("Baños accesibles");
            if (destino.EstacionamientoReservado) caracteristicas.Add("Estacionamiento reservado");
            if (destino.AudioGuias) caracteristicas.Add("Audio-guías");
            if (destino.RutasTactiles) caracteristicas.Add("Rutas táctiles");
            if (destino.Pictogramas) caracteristicas.Add("Pictogramas");
            if (destino.PersonalCapacitado) caracteristicas.Add("Personal capacitado");
            if (destino.SenalizacionBraille) caracteristicas.Add("Señalización braille");

            return caracteristicas.Any() 
                ? string.Join(" • ", caracteristicas) 
                : "Información de accesibilidad no disponible";
        }
    }
}