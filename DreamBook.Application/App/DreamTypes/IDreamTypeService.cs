namespace DreamBook.Application.DreamTypes
{
    public interface IDreamTypeService : ITranslatableEntityService<DreamType, DreamTypeTranslation, DreamTypeResponseModel, DreamTypeWithTranslationsResponseModel>
    {
        Task<DreamTypeWithTranslationsResponseModel> Create(CreateDreamTypeRequestModel requestModel);
        Task Update(UpdateDreamTypeRequestModel requestModel);
    }
}
