using Authorization.Core.Models;
using Authorization.Persistence.Entities;
using AutoMapper;

namespace Authorization.Persistence
{
    public class DataBaseMappings: Profile
    {
        public DataBaseMappings()
        {
            CreateMap<UserEntity, User>();
        }
    }
}
