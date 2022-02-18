namespace DreamBook.Application.Ads
{
    public interface IAdService : ITranslatableEntityService<Ad, AdTranslation, AdResponseModel, AdWithTranslationsResponseModel>
    {
        Task<AdWithTranslationsResponseModel> Create(CreateAdRequestModel requestModel);
        Task Update(UpdateAdRequestModel requestModel);
    }
}
