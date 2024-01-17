using MediatR;


namespace CleanArchitecture.Application.Features.Propiedades.Commands.DeletePropiedad
{
     public class DeletePropiedadCommand : IRequest
     {        
          public int Id { get; set; }
          
     }
}
