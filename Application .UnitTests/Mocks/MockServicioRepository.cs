using System;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces.IRepositories.sgp;
using AutoFixture;
using Application.DTOs.sgp;

namespace Application_.UnitTests.Mocks
{
    public static class MockServicioRepository
    {
        public static Mock<IServicioRepository> GetServicioRepository()
        {
            var fixture = new Fixture();
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            var servicio = fixture.CreateMany<ServicioDto>().ToList();
            var mockRepository= new Mock<IServicioRepository>();
            mockRepository.Setup(r => r.BuscarListado("", "s.descripcion", 0, 10)).ReturnsAsync(servicio);
            return mockRepository;
        }
        //application.contrac.persistence IUnitofWork
    }
}
