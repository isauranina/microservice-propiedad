using Application.DTOs.sgp;
using Application.Interfaces.IRepositories.sgp;
using Application.Interfaces.IServices.sgp;
using Application.Services.Common;
using Application.Utils;
using Domain.Models.Data;
using Domain.Models.sgp;

namespace Application.Services.sgp
{

    public class PropiedadEstadoService : GenericService<PropiedadEstado>,  IPropiedadEstadoService
    {
        private readonly IPropiedadEstadoRepository _propiedadEstadoRepository;

        public PropiedadEstadoService(IPropiedadEstadoRepository propiedadEstadoRepository): base(propiedadEstadoRepository)
        {
            _propiedadEstadoRepository = propiedadEstadoRepository;
        }

        public async Task<RespuestaListado<PropiedadEstadoDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar)
        {
            var respuestaListado = new RespuestaListado<PropiedadEstadoDto>(){
                response = await _propiedadEstadoRepository.BuscarListado(valor, parametro, numeroPagina, cantidadMostrar),
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

