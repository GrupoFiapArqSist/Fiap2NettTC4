namespace Command.Domain.Dtos
{
	public class OrderDto
	{
		public int CommandId { get; set; }

		public int UserId { get; set; }

		public decimal ValueTotal { get; set; }

		public string Observation { get; set; } = string.Empty;

		public virtual ICollection<OrderItemsDto> OrderItems { get; set; } = [];
	}
}
