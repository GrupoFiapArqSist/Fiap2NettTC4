using Command.Domain.Dtos;
using FluentValidation;

namespace Command.Service.Validators
{
	public class CommandDtoValidator : AbstractValidator<CommandDto>
	{
		public CommandDtoValidator()
		{
			RuleFor(x => x.Number)
				.NotEmpty()
				.NotNull()
				.GreaterThan(0);
		}
	}
}
