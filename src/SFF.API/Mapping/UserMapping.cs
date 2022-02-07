using AutoMapper;
using SFF.API.Domain.Entities;
using SFF.API.Transfer;

namespace SFF.API.Mapping
{
    public class UserMapping : Profile
    {
        public UserMapping()
        {
            this.CreateMap<User, UserAuthenticateResponceData>()
            .ReverseMap();

            this.CreateMap<User, UserAuthenticateRequestData>()
            .ReverseMap();

            this.CreateMap<User, UserRegisterRequestData>()
            .ReverseMap();
            
            this.CreateMap<User, UserRegisterResponceData>()
            .ReverseMap();

        }
    }    
}    