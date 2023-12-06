using Application.DTOs.sgp;
using Application.Interfaces.Common;
using Domain.Models.sgp;

namespace Application.Interfaces.IRepositories.sgp
{
    public interface IReglasPropiedadRepository: IGenericRepository<ReglasPropiedad>
    {
        public Task<IEnumerable<ReglasPropiedadDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar);
    }
}
