using AutoMapper;
using LoginAuthUser.Domain.Dtos.Auth;
using LoginAuthUser.Domain.Dtos.User;
using LoginAuthUser.Domain.Entities;


namespace LoginAuthUser.API.Mapper
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                #region Auth
                config.CreateMap<RegisterDto, User>().ReverseMap();
                #endregion

                #region User
                config.CreateMap<UserResponseDto, User>().ReverseMap();
                config.CreateMap<UpdateUserDto, User>().ReverseMap();
                #endregion
            });
            return mappingConfig;
        }
    }
}
