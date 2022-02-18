using AutoMapper;

namespace DreamBook.Persistence.Services
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            //Requests
            CreateMap<UserRequestModel, User>();
            CreateMap<CreateUserRequestModel, User>();
            CreateMap<UpdateUserRequestModel, User>();

            //Responses
            CreateMap<User, UserResponseModel>();
        }
    }
}
