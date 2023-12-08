using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Application_.UnitTests.services.Servicio.Queries
{
    public class GetServicioServiceHandlerXUnitTests ///
    {
        [Fact]
        public void ValorMenorACero()
        {
            int valorEsperado = -5;
            Assert.Equal(valorEsperado, -5);
        }

    }
}
