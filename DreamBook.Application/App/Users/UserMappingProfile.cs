using AutoMapper;
using DreamBook.Domain.Entities;

namespace DreamBook.Application.Users
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            //Requests
            CreateMap<CreateUserRequestModel, User>();
            CreateMap<UpdateUserRequestModel, User>();

            //Responses
            CreateMap<User, UserResponseModel>();
        }
    }
}
