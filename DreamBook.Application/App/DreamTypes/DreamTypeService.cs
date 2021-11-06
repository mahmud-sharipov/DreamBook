using AutoMapper;
using DreamBook.Application.Abstraction;
using DreamBook.Application.Abstraction.Service;
using DreamBook.Application.LanguageResources;
using DreamBook.Domain.Entities;
using System.Threading.Tasks;

namespace DreamBook.Application.DreamTypes
{
    public class DreamTypeService : TranslatableEntityService<DreamType, DreamTypeTranslation, DreamTypeResponseModel, DreamTypeWithTranslationsResponseModel>, IDreamTypeService
    {
        public DreamTypeService(IContext context, IMapper mapper, AppLanguageManager appLanguageManager) : base(context, mapper, appLanguageManager) { }

        public async Task<DreamTypeWithTranslationsResponseModel> Create(CreateDreamTypeRequestModel requestModel)
        {
            return await Create<DreamTypeTranslationRequestModel>(requestModel);
        }

        public async Task Update(UpdateDreamTypeRequestModel requestModel)
        {
            await Update(requestModel, requestModel.Guid);
        }

        protected override (bool CanBeDeleted, string Reason) CanEntityBeDeleted(DreamType entity)
        {
            if (Context.Count<Dream>(d => d.TypeGuid == entity.Guid) > 0)
                return (false, ExceptionMessages.DreatemTypeCanNotBeDeletedReason);

            return base.CanEntityBeDeleted(entity);
        }

        protected override string GetDefaultSearchPropertyName() => nameof(DreamTypeTranslation.Name);
        protected override string GetDefaultPropertyNameToOrderBy() => nameof(DreamTypeTranslation.Name);
    }
}
