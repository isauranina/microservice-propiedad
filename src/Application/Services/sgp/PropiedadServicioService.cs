using Application.DTOs.sgp;
using Application.Interfaces.IRepositories.sgp;
using Application.Interfaces.IServices.sgp;
using Application.Services.Common;
using Application.Utils;
using Domain.Models.Data;
using Domain.Models.sgp;

namespace Application.Services.sgp
{

    public class PropiedadServicioService : GenericService<PropiedadServicio>,  IPropiedadServicioService
    {
        private readonly IPropiedadServicioRepository _propiedadServicioRepository;

        public PropiedadServicioService(IPropiedadServicioRepository propiedadServicioRepository): base(propiedadServicioRepository)
        {
            _propiedadServicioRepository = propiedadServicioRepository;
        }

        public async Task<RespuestaListado<PropiedadServicioDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar)
        {
            var respuestaListado = new RespuestaListado<PropiedadServicioDto>(){
                response = await _propiedadServicioRepository.BuscarListado(valor, parametro, numeroPagina, cantidadMostrar),
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

