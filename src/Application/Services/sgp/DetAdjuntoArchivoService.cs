using Application.DTOs.sgp;
using Application.Interfaces.IRepositories.sgp;
using Application.Interfaces.IServices.sgp;
using Application.Services.Common;
using Application.Utils;
using Domain.Models.Data;
using Domain.Models.sgp;

namespace Application.Services.sgp
{

    public class DetAdjuntoArchivoService : GenericService<DetAdjuntoArchivo>,  IDetAdjuntoArchivoService
    {
        private readonly IDetAdjuntoArchivoRepository _detAdjuntoArchivoRepository;

        public DetAdjuntoArchivoService(IDetAdjuntoArchivoRepository detAdjuntoArchivoRepository): base(detAdjuntoArchivoRepository)
        {
            _detAdjuntoArchivoRepository = detAdjuntoArchivoRepository;
        }

        public async Task<RespuestaListado<DetAdjuntoArchivoDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar)
        {
            var respuestaListado = new RespuestaListado<DetAdjuntoArchivoDto>(){
                response = await _detAdjuntoArchivoRepository.BuscarListado(valor, parametro, numeroPagina, cantidadMostrar),
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

