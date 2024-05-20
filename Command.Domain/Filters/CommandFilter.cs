using ComandaPro.Domain.Filters;
using Command.Domain.Enums;

namespace Command.Domain.Filters
{
	public class CommandFilter : _BaseFilter
	{
		public int Number { get; set; }
		public CommandStatusEnum Status { get; set; }
	}
}
