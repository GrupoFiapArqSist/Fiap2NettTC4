using AutoMapper;
using Command.Domain.Dtos;

namespace Command.API.Mapper
{
	public class MappingConfig
	{
		public static MapperConfiguration RegisterMaps()
		{
			var mappingConfig = new MapperConfiguration(config =>
			{
				config.CreateMap<CommandDto, Domain.Entities.Command>().ReverseMap();
			});
			return mappingConfig;
		}
	}
}
