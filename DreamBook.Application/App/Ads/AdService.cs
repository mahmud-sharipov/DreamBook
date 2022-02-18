namespace DreamBook.Application.Ads
{
    public class AdService : TranslatableEntityService<Ad, AdTranslation, AdResponseModel, AdWithTranslationsResponseModel>, IAdService
    {
        public AdService(IContext context, IMapper mapper, AppLanguageManager appLanguageManager) : base(context, mapper, appLanguageManager) { }

        public async Task<AdWithTranslationsResponseModel> Create(CreateAdRequestModel requestModel)
        {
            return await Create<AdTranslationRequestModel>(requestModel);
        }

        public async Task Update(UpdateAdRequestModel requestModel)
        {
            await Update(requestModel, requestModel.Guid);
        }

        protected override string GetDefaultSearchPropertyName() => nameof(AdTranslation.Title);

        protected override string GetDefaultPropertyNameToOrderBy() => $"{nameof(AdTranslation.Ad)}.{nameof(Ad.CreatedAt)}";
        protected override string GetEntityLabel() => ModelsLabel.Ad;
    }
}
