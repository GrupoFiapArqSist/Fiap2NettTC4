using Command.Domain.Dtos;
using Command.Service.Validators;

namespace Command.Test.UnitTests
{
	[TestClass]
	public class CommandUnitTests
	{
		private CommandDtoValidator _validator = new();

		[TestMethod]
		public void ValidateCommandDto_ShouldReturInvalid_WhenNumberLowerThanZero()
		{
			// Arrange
			var dto = new CommandDto { Number = -10 };

			// Act
			var result = _validator.Validate(dto);

			// Assert
			Assert.IsFalse(result.IsValid);
		}


		[TestMethod]
		public void CalculateServiceCharge_ShouldReturnZero_WhenValueTotalEqualsZero()
		{
			// Arrange
			var command = new Domain.Entities.Command
			{
				ValueTotalBeforeServiceCharge = 0
			};

			// Act

			// Assert
			Assert.AreEqual(0, command.ServiceChage);
		}

		[TestMethod]
		public void CalculateServiceCharge_ShouldReturnValue_WhenValueTotalGreaterThanZero()
		{
			// Arrange
			var command = new Domain.Entities.Command
			{
				ValueTotalBeforeServiceCharge = 500
			};

			// Act
			var expected = command.ValueTotalBeforeServiceCharge * (Domain.Entities.Command.ServiceChargePercentage / 100);

			// Assert
			Assert.AreEqual(expected, command.ServiceChage);
		}
	}
}
