using Command.Domain.Enums;

namespace Command.Domain.Dtos
{
	public class CommandDto
	{
		public int Number { get; set; }

		public CommandStatusEnum Status { get; set; }

		public int UserId { get; set; }

		public IEnumerable<OrderDto> Orders { get; set; } = [];

		public double ServiceChage { get; set; }

		public double ValueTotal { get; set; }

		public DateTime? ClosedAt { get; set; }
	}
}
