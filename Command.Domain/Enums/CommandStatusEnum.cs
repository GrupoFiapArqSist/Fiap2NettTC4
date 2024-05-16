using System.ComponentModel;

namespace Command.Domain.Enums
{
	public enum CommandStatusEnum
	{
		[Description("Closed")]
		Closed = 1,

		[Description("Open")]
		Open = 2
	}
}
