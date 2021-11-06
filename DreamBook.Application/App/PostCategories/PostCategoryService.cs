using AutoMapper;
using DreamBook.Application.Abstraction;
using DreamBook.Application.Abstraction.Service;
using DreamBook.Application.LanguageResources;
using DreamBook.Domain.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace DreamBook.Application.PostCategories
{
    public class PostCategoryService : TranslatableEntityService<PostCategory, PostCategoryTranslation, PostCategoryResponseModel, PostCategoryWithTranslationsResponseModel>, IPostCategoryService
    {
        public PostCategoryService(IContext context, IMapper mapper, AppLanguageManager appLanguageManager) : base(context, mapper, appLanguageManager) { }

        public async Task<PostCategoryWithTranslationsResponseModel> Create(CreatePostCategoryRequestModel requestModel)
        {
            return await Create<PostCategoryTranslationRequestModel>(requestModel);
        }

        public async Task Update(UpdatePostCategoryRequestModel requestModel)
        {
            await Update(requestModel, requestModel.Guid);
        }

        protected override (bool CanBeDeleted, string Reason) CanEntityBeDeleted(PostCategory entity)
        {
            if (entity.Posts.Any())
                return (false, ExceptionMessages.DreatemTypeCanNotBeDeletedReason);

            return base.CanEntityBeDeleted(entity);
        }

        protected override string GetDefaultSearchPropertyName() => nameof(PostCategoryTranslation.Name);
        protected override string GetDefaultPropertyNameToOrderBy() => nameof(PostCategoryTranslation.Name);

    }
}

