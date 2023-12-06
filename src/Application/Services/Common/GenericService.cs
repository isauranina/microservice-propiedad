using Application.Interfaces.Common;
using Domain.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Application.Services.Common
{
    public class GenericService<T> : IGenericService<T> where T : class
    {
        private readonly IGenericRepository<T> _genericRepository;
        public GenericService(IGenericRepository<T> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public async Task<T> BuscarPorNumSec(long numSec)
        {
            return await _genericRepository.BuscarPorNumSec(numSec);
        }

        public async Task<RespuestaDB> Eliminar(T datos)
        {
            RespuestaDB respuestaBD = new RespuestaDB();

            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                respuestaBD = await _genericRepository.Eliminar(datos);
                if (respuestaBD.status == "error")
                {
                    return respuestaBD;
                }
                transaction.Complete();
            }
            return respuestaBD;
        }

        public async Task<RespuestaDB> Guardar(T datos)
        {
            RespuestaDB respuestaBD = new RespuestaDB();

            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                respuestaBD = await _genericRepository.Guardar(datos);
                if (respuestaBD.status == "error")
                {
                    return respuestaBD;
                }
                transaction.Complete();

            }
            return respuestaBD;

        }

        public async Task<RespuestaDB> Modificar(T datos)
        {
            RespuestaDB respuestaBD = new RespuestaDB();

            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                respuestaBD = await _genericRepository.Modificar(datos);
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
