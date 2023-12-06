using Application.DTOs.sgp;
using Application.Interfaces.IRepositories.sgp;
using Application.Interfaces.IServices.sgp;
using Application.Services.Common;
using Application.Utils;
using Domain.Models.Data;
using Domain.Models.sgp;

namespace Application.Services.sgp
{

    public class ServicioService : GenericService<Servicio>,  IServicioService
    {
        private readonly IServicioRepository _servicioRepository;

        public ServicioService(IServicioRepository servicioRepository): base(servicioRepository)
        {
            _servicioRepository = servicioRepository;
        }

        public async Task<RespuestaListado<ServicioDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar)
        {
            var respuestaListado = new RespuestaListado<ServicioDto>(){
                response = await _servicioRepository.BuscarListado(valor, parametro, numeroPagina, cantidadMostrar),
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

