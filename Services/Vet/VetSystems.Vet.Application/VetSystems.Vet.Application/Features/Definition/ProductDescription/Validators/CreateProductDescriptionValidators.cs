using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Vet.Application.Features.Definition.ProductDescription.Commands;

namespace VetSystems.Vet.Application.Features.Definition.ProductDescription.Validators
{
    public class CreateProductDescriptionValidators : AbstractValidator<CreateProductDescriptionCommand>
    {
        public CreateProductDescriptionValidators()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Lütfen Ürün Adını Doldurunuz");
        }
    }
}
