using Domain.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Common
{
    public interface IGenericService<T> where T : class
    {
        public Task<T> BuscarPorNumSec(long numSec);
        public Task<RespuestaDB> Guardar(T datos);
        public Task<RespuestaDB> Modificar(T datos);
        public Task<RespuestaDB> Eliminar(T datos);

    }

}
