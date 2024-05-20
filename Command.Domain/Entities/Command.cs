using ComandaPro.Domain.Entities;
using ComandaPro.Domain.Interfaces.Entities;
using Command.Domain.Enums;

namespace Command.Domain.Entities
{
	public class Command : BaseEntity, IEntity<int>
	{
		public static readonly decimal ServiceChargePercentage = 10;

		public int Number { get; set; }

		public CommandStatusEnum Status { get; set; }

		public int UserId { get; set; }

		public decimal ServiceChage
		{
			get => ValueTotalBeforeServiceCharge * (ServiceChargePercentage / 100);
		}

		public decimal ValueTotalBeforeServiceCharge { get; set; }

		public decimal ValueTotal
		{
			get => ValueTotalBeforeServiceCharge + ServiceChage;
		}

		public DateTime? ClosedAt { get; set; }
	}
}
