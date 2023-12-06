using Application.Interfaces.Common;
using Application.Interfaces.IRepositories.Administracion;
using Application.Interfaces.IRepositories.sgp;
using Infrastructure.Repositories.Administracion;
using Infrastructure.Repositories.sgp;
using Infrastructure.Repositories.Common;

using Application.Interfaces.IServices.sgp;

using Application.Services.sgp;


namespace WebApi.Ioc
{
    public static class IocRepository
    {
        public static IServiceCollection AddDependency(this IServiceCollection services)
        {

            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<ILoginRepository, LoginRepository>();
       

            // agregar todos los interfaces de repositorio y repositorrios

            // services.AddTransient<ITipoAppRepository, TipoAppRepository>();
            services.AddTransient<IAdjuntoRepository, AdjuntoRepository>();
            services.AddTransient<ICiudadRepository, CiudadRepository>();
            services.AddTransient<IDetAdjuntoArchivoRepository, DetAdjuntoArchivoRepository>();
            services.AddTransient<IEstadoPropiedadRepository, EstadoPropiedadRepository>();
            services.AddTransient<IPaisRepository, PaisRepository>();
            services.AddTransient<IPropiedadEstadoRepository, PropiedadEstadoRepository>();
            services.AddTransient<IPropiedadRepository, PropiedadRepository>();
            services.AddTransient<IPropiedadServicioRepository, PropiedadServicioRepository>();
            services.AddTransient<IReglasPropiedadRepository, ReglasPropiedadRepository>();
            services.AddTransient<IServicioRepository, ServicioRepository>();
            services.AddTransient<ITablaRepository, TablaRepository>();
            services.AddTransient<ITipoPropiedadRepository, TipoPropiedadRepository>();


            return services;
        }
    }
}
