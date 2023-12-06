using Application.DTOs.Administracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IServices.Administracion
{
    public interface IMenuService
    {
        public Task<IEnumerable<MenuDto>> TraerMenuPorUsuario(long num_sec, string cuenta, string routerlink);
    }
}
