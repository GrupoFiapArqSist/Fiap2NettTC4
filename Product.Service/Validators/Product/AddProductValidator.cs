using FluentValidation;
using Product.Domain.Dtos.Product;

namespace Product.Service.Validators.Product
{
    public class AddProductValidator : AbstractValidator<AddProductDto>
    {
        public AddProductValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                .WithMessage("Informe o nome do produto.");

            RuleFor(x => x.Description)
                .NotEmpty()
                .NotNull()
                .WithMessage("Informe a descrição do produto.");

            RuleFor(x => x.Price)
               .NotEmpty()
               .NotNull()
               .WithMessage("Informe o preço do produto.");

            RuleFor(x => x.CategoryId)
              .NotEmpty()
              .NotNull()
              .GreaterThan(0)
              .WithMessage("Informe o id da categoria.");
        }
    }
}
