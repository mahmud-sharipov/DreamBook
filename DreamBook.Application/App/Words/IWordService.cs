namespace DreamBook.Application.Words
{
    public interface IWordService : ITranslatableEntityService<Word, WordTranslation, WordResponseModel, WordWithTranslationsResponseModel>
    {
        Task<WordWithTranslationsResponseModel> Create(CreateWordRequestModel requestModel);
        Task Update(UpdateWordRequestModel requestModel);
        Task<string> GetAllByLanguageInJson(Guid languageGuid);
    }
}
