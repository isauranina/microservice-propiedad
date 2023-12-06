using Application.DTOs.sgp;
using Application.Interfaces.IRepositories.sgp;
using Application.Interfaces.IServices.sgp;
using Application.Services.Common;
using Application.Utils;
using Domain.Models.Data;
using Domain.Models.sgp;

namespace Application.Services.sgp
{

    public class EstadoPropiedadService : GenericService<EstadoPropiedad>,  IEstadoPropiedadService
    {
        private readonly IEstadoPropiedadRepository _estadoPropiedadRepository;

        public EstadoPropiedadService(IEstadoPropiedadRepository estadoPropiedadRepository): base(estadoPropiedadRepository)
        {
            _estadoPropiedadRepository = estadoPropiedadRepository;
        }

        public async Task<RespuestaListado<EstadoPropiedadDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar)
        {
            var respuestaListado = new RespuestaListado<EstadoPropiedadDto>(){
                response = await _estadoPropiedadRepository.BuscarListado(valor, parametro, numeroPagina, cantidadMostrar),
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

