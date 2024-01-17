using FluentValidation;

namespace CleanArchitecture.Application.Features.Propiedades.Commands.UpdatePropiedad
{
     public class UpdatePropiedadCommandValidator: AbstractValidator<UpdatePropiedadCommand>
     {
          public UpdatePropiedadCommandValidator()
          {
               RuleFor(p => p.Descripcion)
                .NotNull().WithMessage("{Descripcion} no permite valores nulos");

               RuleFor(p => p.Direccion)
                   .NotNull().WithMessage("{Direccion;} no permite valores nulos");

          }

     }
}
