namespace Command.Test.UnitTests
{
	[TestClass]
	public class CommandUnitTests
	{

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
