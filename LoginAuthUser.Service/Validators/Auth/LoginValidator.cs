using FluentValidation;
using LoginAuthUser.Domain.Dtos.Auth;

namespace LoginAuthUser.Service.Validators.Auth;

public class LoginValidator : AbstractValidator<LoginDto>
{
    public LoginValidator()
    {
        RuleFor(x => x.Username).NotEmpty().WithMessage("Informe o usuario");
        RuleFor(x => x.Password).NotEmpty().WithMessage("Informe a senha");
    }
}
