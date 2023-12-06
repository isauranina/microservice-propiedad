using Application.DTOs.sgp;
using Application.Interfaces.IRepositories.sgp;
using Application.Interfaces.IServices.sgp;
using Application.Services.Common;
using Application.Utils;
using Domain.Models.Data;
using Domain.Models.sgp;
using System.Transactions;

namespace Application.Services.sgp
{

    public class PropiedadService : GenericService<Propiedad>,  IPropiedadService
    {
        private readonly IPropiedadRepository _propiedadRepository;

        public PropiedadService(IPropiedadRepository propiedadRepository): base(propiedadRepository)
        {
            _propiedadRepository = propiedadRepository;
        }

        public async Task<ListaDto> traer_por_id(long codigo)
        {
            var eCaja = await _propiedadRepository.traer_por_id(codigo);
            return eCaja;
        }
        public async Task<RespuestaListado<PropiedadDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar)
        {
            var respuestaListado = new RespuestaListado<PropiedadDto>(){
                response = await _propiedadRepository.BuscarListado(valor, parametro, numeroPagina, cantidadMostrar),
                status = Status.Success
            };

            if (respuestaListado.response.Count() > 0)
            {
                int elementosTotales = respuestaListado.response.ElementAt(0).total;
                respuestaListado.total = elementosTotales;
            }

            return respuestaListado;
        }
        public async Task<RespuestaDB> EstadoPropiedad(VerificarPropiedad datos)
        {
            RespuestaDB respuestaBD = new RespuestaDB();

            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                respuestaBD = await _propiedadRepository.EstadoPropiedad(datos);
                if (respuestaBD.status == "error")
                {
                    return respuestaBD;
                }        

                

                transaction.Complete();

            }

            return respuestaBD;

        }
    }

}

