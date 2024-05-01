using FluentValidation;
using Order.Domain.Dtos;

namespace Order.Service.Validators.Order;

internal class AddOrderValidatorItemsValidator : AbstractValidator<OrderItemsDto>
{
    public AddOrderValidatorItemsValidator()
    {
        RuleFor(x => x.ValueUnit)
            .NotEmpty().WithMessage("Informe o valor unitário do produto.");

        RuleFor(x => x.ProductId)
            .NotEmpty().WithMessage("Informe o id do produto.");

        RuleFor(x => x.Amount)
            .NotEmpty().WithMessage("Informe a quantidade do produto.");
    }
}
