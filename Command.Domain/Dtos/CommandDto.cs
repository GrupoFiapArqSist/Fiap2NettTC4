using Command.Domain.Enums;

namespace Command.Domain.Dtos
{
	public class CommandDto
	{
		public int Id { get; set; }

		public int Number { get; set; }

		public CommandStatusEnum Status { get; set; }

		public int UserId { get; set; }

		public IEnumerable<OrderDto> Orders { get; set; } = [];

		public decimal ServiceChage { get; set; }

		public decimal ValueTotalBeforeServiceCharge { get; set; }

		public decimal ValueTotal { get; set; }

		public DateTime CreatedAt { get; set; }

		public DateTime? ClosedAt { get; set; }
	}
}
