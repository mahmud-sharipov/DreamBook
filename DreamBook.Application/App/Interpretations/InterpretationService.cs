using AutoMapper;
using DreamBook.Application.Abstraction;
using DreamBook.Application.Abstraction.Service;
using DreamBook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            return await Create<InterpretationTranslationRequestModel>(requestModel);
        }

        public async Task Update(UpdateInterpretationRequestModel requestModel)
        {
            await Update(requestModel, requestModel.Guid);
        }

        protected override string GetDefaultSearchPropertyName() => nameof(InterpretationTranslation.Description);
    }
}
