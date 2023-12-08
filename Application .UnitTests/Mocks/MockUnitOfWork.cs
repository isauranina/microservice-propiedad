using Application.DTOs.sgp;
using Domain.Factories;
using Domain.Models.sgp;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_.UnitTests.Mocks
{
    internal  class ServicioMockFactory
    {
        public static List<ServicioDto> getServicio()
        {          

            List<ServicioDto> Lista = new List<ServicioDto>()
            { 
                new ServicioDto{ num_sec = 1,descripcion="Wiffi"},
                new ServicioDto{ num_sec = 2,descripcion="Labadora"},
                new ServicioDto{ num_sec = 3,descripcion="Tv Cable"},

            };
            return Lista;

        }
    }
}
