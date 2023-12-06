using Application.DTOs.sgp;
using Application.Interfaces.IRepositories.sgp;
using Application.Interfaces.IServices.sgp;
using Application.Services.Common;
using Application.Utils;
using Domain.Models.Data;
using Domain.Models.sgp;

namespace Application.Services.sgp
{

    public class CiudadService : GenericService<Ciudad>,  ICiudadService
    {
        private readonly ICiudadRepository _ciudadRepository;

        public CiudadService(ICiudadRepository ciudadRepository): base(ciudadRepository)
        {
            _ciudadRepository = ciudadRepository;
        }

        public async Task<RespuestaListado<CiudadDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar)
        {
            var respuestaListado = new RespuestaListado<CiudadDto>(){
                response = await _ciudadRepository.BuscarListado(valor, parametro, numeroPagina, cantidadMostrar),
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

