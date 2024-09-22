using Authentication.Core.Models;
using Authentication.Persistence.Entities;
using AutoMapper;

namespace Authentication.Persistence
{
    public class DataBaseMappings: Profile
    {
        public DataBaseMappings()
        {
            CreateMap<UserEntity, User>();
        }
    }
}
