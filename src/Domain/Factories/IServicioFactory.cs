using Domain.Models.sgp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Factories
{
    public interface IServicioFactory
    {
        Servicio create(long id, string descripcion);

    }
}
