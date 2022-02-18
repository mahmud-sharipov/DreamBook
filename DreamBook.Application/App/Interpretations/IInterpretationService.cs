using System.Collections.Generic;

namespace DreamBook.Application.Interpretations
{
    public interface IInterpretationService : ITranslatableEntityService<Interpretation, InterpretationTranslation, InterpretationResponseModel, InterpretationWithTranslationsResponseModel>
    {
        Task<IEnumerable<BookInterpretationResponseModel>> GetByBookId(Guid bookId);
        Task<IEnumerable<WordInterpretationResponseModel>> GetByWordId(Guid wordId);

        Task<InterpretationWithTranslationsResponseModel> Create(CreateInterpretationRequestModel requestModel);
        Task Update(UpdateInterpretationRequestModel requestModel);
    }
}
