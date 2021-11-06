using AutoMapper;
using DreamBook.Application.Abstraction;
using DreamBook.Application.Abstraction.Service;
using DreamBook.Application.LanguageResources;
using DreamBook.Domain.Entities;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DreamBook.Application.Words
{
    public class WordService : TranslatableEntityService<Word, WordTranslation, WordResponseModel, WordWithTranslationsResponseModel>, IWordService
    {
        public WordService(IContext context, IMapper mapper, AppLanguageManager appLanguageManager) : base(context, mapper, appLanguageManager) { }

        public async Task<WordWithTranslationsResponseModel> Create(CreateWordRequestModel requestModel)
        {
            return await Create<WordTranslationRequestModel>(requestModel);
        }

        public virtual async Task<string> GetAllByLanguageInJson(Guid languageGuid)
        {
            var entities = (await Context.GetAllAsync<WordTranslation>(w => w.LanguageGuid == languageGuid))
                .Select(w => new
                {
                    Guid = w.WordGuid,
                    w.Name
                });
            return JsonConvert.SerializeObject(entities);
        }

        public async Task Update(UpdateWordRequestModel requestModel)
        {
            await Update(requestModel, requestModel.Guid);
        }

        protected override (bool CanBeDeleted, string Reason) CanEntityBeDeleted(Word entity)
        {
            if (Context.Count<DreamWord>(dw => dw.WordGuid == entity.Guid) > 0)
                return (false, Messages.DreatemTypeCanNotBeDeletedReason);

            return base.CanEntityBeDeleted(entity);
        }

        protected override string GetDefaultSearchPropertyName() => nameof(WordTranslation.Name);

        protected override string GetDefaultPropertyNameToOrderBy() => nameof(WordTranslation.Name);
    }
}

