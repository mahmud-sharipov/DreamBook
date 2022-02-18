using AutoMapper;
using DreamBook.Application.Abstraction;
using DreamBook.Application.Abstraction.Service;
using DreamBook.Application.Exceptions;
using DreamBook.Application.LanguageResources;
using DreamBook.Domain.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DreamBook.Application.DreamTypes
{
    public class DreamTypeService : TranslatableEntityService<DreamType, DreamTypeTranslation, DreamTypeResponseModel, DreamTypeWithTranslationsResponseModel>, IDreamTypeService
    {
        public DreamTypeService(IContext context, IMapper mapper, AppLanguageManager appLanguageManager) : base(context, mapper, appLanguageManager) { }

        public async Task<DreamTypeWithTranslationsResponseModel> Create(CreateDreamTypeRequestModel requestModel)
        {
            await ValidateBookName(requestModel);
            return await Create<DreamTypeTranslationRequestModel>(requestModel);
        }

        public async Task Update(UpdateDreamTypeRequestModel requestModel)
        {
            await ValidateBookName(requestModel, requestModel.Guid);
            await Update(requestModel, requestModel.Guid);
        }

        private async Task ValidateBookName(CreateDreamTypeRequestModel requestModel, Guid? entityId = null)
        {
            var names = requestModel.Translations.Select(x => x.Name.ToLower() + x.LanguageGuid).ToArray();
            var wordId = entityId ?? Guid.Empty;
            var typesWithSameName = await Context
                .GetAllAsync<DreamTypeTranslation>(wt => wt.DreamTypeGuid != wordId && names.Contains(wt.Name.ToLower() + wt.LanguageGuid));
            if (typesWithSameName.Any())
            {
                var similarNames = string.Join(", ", typesWithSameName.Select(b => b.Name));
                throw new BusinessLogicException(ExceptionMessages.BookWithSameNameExist.Format(similarNames));
            }
        }

        protected override (bool CanBeDeleted, string Reason) CanEntityBeDeleted(DreamType entity)
        {
            if (Context.Count<Dream>(d => d.TypeGuid == entity.Id) > 0)
                return (false, ExceptionMessages.DreatemTypeCanNotBeDeletedReason);

            return base.CanEntityBeDeleted(entity);
        }

        protected override string GetDefaultSearchPropertyName() => nameof(DreamTypeTranslation.Name);
        protected override string GetDefaultPropertyNameToOrderBy() => nameof(DreamTypeTranslation.Name);
        protected override string GetEntityLabel() => ModelsLabel.DreamType;
    }
}
