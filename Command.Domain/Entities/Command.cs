using ComandaPro.Domain.Entities;
using ComandaPro.Domain.Interfaces.Entities;
using Command.Domain.Enums;

namespace Command.Domain.Entities
{
	public class Command : BaseEntity, IEntity<int>
	{
		public int Number { get; set; }

		public CommandStatusEnum Status { get; set; }

		public int UserId { get; set; }

		public decimal ServiceChage { get; set; }

		public decimal ValueTotal { get; set; }

		public DateTime? ClosedAt { get; set; }
	}
}
