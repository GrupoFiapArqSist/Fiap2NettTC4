using FluentValidation;
using Order.Domain.Dtos;

namespace Order.Service.Validators.Order;

public class AddOrderValidator : AbstractValidator<AddOrderDto>
{
    public AddOrderValidator()
    {
        RuleFor(x => x.CommandId)
            .NotEmpty().WithMessage("Informe o id da comanda.");
    }
}