using System.Collections.Generic;

namespace DreamBook.Application.Interpretations
{
    public class InterpretationService : TranslatableEntityService<Interpretation, InterpretationTranslation, InterpretationResponseModel, InterpretationWithTranslationsResponseModel>, IInterpretationService
    {
        public InterpretationService(IContext context, IMapper mapper, AppLanguageManager appLanguageManager) : base(context, mapper, appLanguageManager) { }

        public async Task<IEnumerable<BookInterpretationResponseModel>> GetByBookId(Guid bookId)
        {
            var entities = await Context.GetAllAsync<Interpretation>(i => i.BookGuid == bookId);
            var languagePredicate = LanguagePredicate.Compile();
            var translations = entities.Select(i => i.Translations.SingleOrDefault(languagePredicate)).Where(t => t != null);

            return Mapper.Map<IEnumerable<BookInterpretationResponseModel>>(translations);
        }

        public async Task<IEnumerable<WordInterpretationResponseModel>> GetByWordId(Guid wordId)
        {
            var entities = await Context.GetAllAsync<Interpretation>(i => i.WordGuid == wordId);
            var languagePredicate = LanguagePredicate.Compile();
            var translations = entities.Select(i => i.Translations.SingleOrDefault(languagePredicate)).Where(t => t != null);

            return Mapper.Map<IEnumerable<WordInterpretationResponseModel>>(translations);
        }

        public async Task<InterpretationWithTranslationsResponseModel> Create(CreateInterpretationRequestModel requestModel)
        {
            await ValidateAndSetWordAndBookTranslation(requestModel);
            return await Create<InterpretationTranslationRequestModel>(requestModel);
        }

        public async Task Update(UpdateInterpretationRequestModel requestModel)
        {
            await ValidateAndSetWordAndBookTranslation(requestModel, requestModel.Guid);
            await Update(requestModel, requestModel.Guid);
        }

        private async Task ValidateAndSetWordAndBookTranslation(CreateInterpretationRequestModel requestModel, Guid? entityId = null)
        {
            var guid = entityId ?? Guid.Empty;
            var sameInterpretations = await Context
                .GetAllAsync<Interpretation>(i => i.Guid != guid && i.BookGuid == requestModel.BookGuid && i.WordGuid == requestModel.WordGuid);
            if (sameInterpretations.Any())
                throw new BusinessLogicException(ExceptionMessages.BookAlreadyHasInterpretationForWord.Format(requestModel.BookGuid, requestModel.WordGuid));
            
            var word = Context.GetById<Word>(requestModel.WordGuid);
            if (word == null)
                throw new EntityNotFoundException(ModelsLabel.Word, requestModel.WordGuid);

            var book = Context.GetById<Book>(requestModel.BookGuid);
            if (book == null)
                throw new EntityNotFoundException(ModelsLabel.Book, requestModel.BookGuid);

            foreach (var translation in requestModel.Translations)
            {
                translation.BookGuid = book.Translations.Single(t => t.LanguageGuid == translation.LanguageGuid).Guid;
                translation.WordGuid = word.Translations.Single(t => t.LanguageGuid == translation.LanguageGuid).Guid;
            }
        }

        protected override string GetDefaultSearchPropertyName() => nameof(InterpretationTranslation.Description);
        protected override string GetEntityLabel() => ModelsLabel.Interpretation;
    }
}
