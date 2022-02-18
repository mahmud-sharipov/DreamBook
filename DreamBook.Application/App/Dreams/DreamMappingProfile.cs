namespace DreamBook.Application.Dreams
{
    public class DreamMappingProfile : Profile
    {
        public DreamMappingProfile()
        {
            //Requests
            CreateMap<CreateDreamRequestModel, Dream>()
                .ForMember(d => d.Words, s => s.Ignore());
            CreateMap<UpdateDreamRequestModel, Dream>()
                .ForMember(d => d.Words, s => s.Ignore());

            //Responses
            CreateMap<Dream, DreamResponseModel>()
                .ForMember(d => d.Type, s => s.Ignore())
                .ForMember(d => d.Words, s => s.Ignore());
            CreateMap<Dream, DreamShortInfoResponseModel>();
        }
    }
}
