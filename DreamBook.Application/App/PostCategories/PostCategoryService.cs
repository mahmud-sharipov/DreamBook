namespace DreamBook.Application.PostCategories
{
    public class PostCategoryService : TranslatableEntityService<PostCategory, PostCategoryTranslation, PostCategoryResponseModel, PostCategoryWithTranslationsResponseModel>, IPostCategoryService
    {
        public PostCategoryService(IContext context, IMapper mapper, AppLanguageManager appLanguageManager) : base(context, mapper, appLanguageManager) { }

        public async Task<PostCategoryWithTranslationsResponseModel> Create(CreatePostCategoryRequestModel requestModel)
        {
            await Validate(requestModel);
            return await Create<PostCategoryTranslationRequestModel>(requestModel);
        }

        public async Task Update(UpdatePostCategoryRequestModel requestModel)
        {
            await Validate(requestModel, requestModel.Guid);
            await Update(requestModel, requestModel.Guid);
        }

        private async Task Validate(CreatePostCategoryRequestModel requestModel, Guid? entityId = null)
        {
            var names = requestModel.Translations.Select(x => x.Name.ToLower() + x.LanguageGuid).ToArray();
            var wordId = entityId ?? Guid.Empty;
            var categoriesWithSameName = await Context
                .GetAllAsync<PostCategoryTranslation>(wt => wt.CategoryGuid != wordId && names.Contains(wt.Name.ToLower() + wt.LanguageGuid));
            if (categoriesWithSameName.Any())
            {
                var similarNames = string.Join(", ", categoriesWithSameName.Select(b => b.Name));
                throw new BusinessLogicException(ExceptionMessages.CategoryWithSameNameExist.Format(similarNames));
            }
        }

        protected override (bool CanBeDeleted, string Reason) CanEntityBeDeleted(PostCategory entity)
        {
            if (entity.Posts.Any())
                return (false, ExceptionMessages.PostCategoryCanNotBeDeletedReason);

            return base.CanEntityBeDeleted(entity);
        }

        protected override string GetDefaultSearchPropertyName() => nameof(PostCategoryTranslation.Name);
        protected override string GetDefaultPropertyNameToOrderBy() => nameof(PostCategoryTranslation.Name);
        protected override string GetEntityLabel() => ModelsLabel.PostCategory;

    }
}

