using Application.DTOs.sgp;
using Application.Interfaces.IRepositories.sgp;
using Application.Interfaces.IServices.sgp;
using Application.Services.Common;
using Application.Utils;
using Domain.Models.Data;
using Domain.Models.sgp;

namespace Application.Services.sgp
{

    public class AdjuntoService : GenericService<Adjunto>,  IAdjuntoService
    {
        private readonly IAdjuntoRepository _adjuntoRepository;

        public AdjuntoService(IAdjuntoRepository adjuntoRepository): base(adjuntoRepository)
        {
            _adjuntoRepository = adjuntoRepository;
        }

        public async Task<RespuestaListado<AdjuntoDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar)
        {
            var respuestaListado = new RespuestaListado<AdjuntoDto>(){
                response = await _adjuntoRepository.BuscarListado(valor, parametro, numeroPagina, cantidadMostrar),
                status = Status.Success
            };

            if (respuestaListado.response.Count() > 0)
            {
                int elementosTotales = respuestaListado.response.ElementAt(0).total;
                respuestaListado.total = elementosTotales;
            }

            return respuestaListado;
        }

    }

}

