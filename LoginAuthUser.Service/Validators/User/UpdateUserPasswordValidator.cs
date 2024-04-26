using FluentValidation;
using LoginAuthUser.Domain.Dtos.User;

namespace LoginAuthUser.Service.Validators.User;

public class UpdateUserPasswordValidator : AbstractValidator<UpdateUserPasswordDto>
{
    public UpdateUserPasswordValidator()
    {
        RuleFor(p => p.NewPassword).ValidPassword();
    }
}
