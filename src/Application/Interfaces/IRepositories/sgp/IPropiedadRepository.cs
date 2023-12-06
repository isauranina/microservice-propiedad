using Application.DTOs.sgp;
using Application.Interfaces.Common;
using Domain.Models.Data;
using Domain.Models.sgp;

namespace Application.Interfaces.IRepositories.sgp
{
    public interface IPropiedadRepository: IGenericRepository<Propiedad>
    {
        public Task<IEnumerable<PropiedadDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar);
        public Task<RespuestaDB> EstadoPropiedad(VerificarPropiedad datos);
        public Task<ListaDto> traer_por_id(long codigo);
    }
}
