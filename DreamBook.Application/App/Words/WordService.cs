using AutoMapper;
using DreamBook.Application.Abstraction;
using DreamBook.Application.Abstraction.Service;
using DreamBook.Application.Exceptions;
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

        public async Task<WordWithTranslationsResponseModel> Create(CreateWordRequestModel requestModel)
        {
            await ValidateWordName(requestModel);
            return await Create<WordTranslationRequestModel>(requestModel);
        }

        public async Task Update(UpdateWordRequestModel requestModel)
        {
            await ValidateWordName(requestModel, requestModel.Guid);
            await Update(requestModel, requestModel.Guid);
        }

        private async Task ValidateWordName(CreateWordRequestModel requestModel, Guid? entityId = null)
        {
            var names = requestModel.Translations.Select(x => x.Name.ToLower() + x.LanguageGuid).ToArray();
            var wordId = entityId ?? Guid.Empty;
            var wordsWithSameName = await Context
                .GetAllAsync<WordTranslation>(wt => wt.WordGuid != wordId && names.Contains(wt.Name.ToLower() + wt.LanguageGuid));
            if (wordsWithSameName.Any())
            {
                var similarNames = string.Join(", ", wordsWithSameName.Select(b => b.Name));
                throw new BusinessLogicException(ExceptionMessages.WordWithSameNameExist.Format(similarNames));
            }
        }

        protected override (bool CanBeDeleted, string Reason) CanEntityBeDeleted(Word entity)
        {
            if (Context.Count<DreamWord>(dw => dw.WordGuid == entity.Guid) > 0)
                return (false, ExceptionMessages.WordCanNotBeDeletedReason);

            return base.CanEntityBeDeleted(entity);
        }

        protected override string GetDefaultSearchPropertyName() => nameof(WordTranslation.Name);
        protected override string GetDefaultPropertyNameToOrderBy() => nameof(WordTranslation.Name);
        protected override string GetEntityLabel() => ModelsLabel.Word;
    }
}