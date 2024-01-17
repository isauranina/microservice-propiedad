using AutoMapper;
using CleanArchitecture.Application.Features.Directors.Commands.CreateDirector;
using CleanArchitecture.Application.Features.Propiedades.Commands.CreatePropiedad;
using CleanArchitecture.Application.Features.Propiedades.Commands.DeletePropiedad;
using CleanArchitecture.Application.Features.Propiedades.Commands.UpdatePropiedad;
using CleanArchitecture.Application.Features.Propiedad.Query.GetPropiedadList;
using CleanArchitecture.Application.Features.Streamers.Commands;
using CleanArchitecture.Application.Features.Streamers.Commands.UpdateStreamer;
using CleanArchitecture.Application.Features.Videos.Queries.GetVideosList;
using CleanArchitecture.Domain;
using CleanArchitecture.Domain.Models.sgp;

namespace CleanArchitecture.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
               //borrar
            //CreateMap<Video, VideosVm>();
            //CreateMap<CreateStreamerCommand, Streamer>();
            //CreateMap<UpdateStreamerCommand, Streamer>();
            //CreateMap<CreateDirectorCommand, Director>();
               //fin 
               CreateMap<Propiedad,PropiedadVm>();
               CreateMap<CreatePropiedadCommand, Propiedad>();
               CreateMap<UpdatePropiedadCommand, Propiedad>();
               CreateMap<DeletePropiedadCommand, Propiedad>();
          }
    }
}
