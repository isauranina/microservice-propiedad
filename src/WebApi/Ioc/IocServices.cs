using Application.Interfaces.IServices.Administracion;
using Application.Services.Administracion;
using Application.Services.Administracion.Login;
using Application.Interfaces.IServices.sgp;
using Application.Interfaces.IServices.Rabbit;
using Application.Services.sgp;
using Application.Services.Rabbit;


namespace WebApi.Ioc
{
    public static class IocServices
    {
        public static IServiceCollection AddDependency(this IServiceCollection services)
        {
            //
            services.AddScoped<LoginFactory>();
            
            services.AddScoped<LoginService>()
                .AddScoped<ILoginService, LoginService>(s => s.GetService<LoginService>()!);

           


           

            // agregar para usar el controlador          
            services.AddTransient <IAdjuntoService, AdjuntoService>();
            services.AddTransient<ICiudadService, CiudadService>();
            services.AddTransient<IDetAdjuntoArchivoService, DetAdjuntoArchivoService>();
            services.AddTransient <IEstadoPropiedadService, EstadoPropiedadService>();
            services.AddTransient <IPaisService, PaisService>();
            services.AddTransient <IPropiedadEstadoService, PropiedadEstadoService>();
            services.AddTransient<IPropiedadService, PropiedadService>();
            services.AddTransient <IPropiedadServicioService, PropiedadServicioService>();
            services.AddTransient <IReglasPropiedadService, ReglasPropiedadService>();
            services.AddTransient <IServicioService, ServicioService>();
            services.AddTransient <ITablaService, TablaService>();
            services.AddTransient<ITipoPropiedadService, TipoPropiedadService>();
            services.AddTransient<IAmqpService, AmqpService>();
            









            return services;
        }
    }
}
