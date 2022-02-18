namespace DreamBook.Application.PostCategories
{
    public interface IPostCategoryService : ITranslatableEntityService<PostCategory, PostCategoryTranslation, PostCategoryResponseModel, PostCategoryWithTranslationsResponseModel>
    {
        Task<PostCategoryWithTranslationsResponseModel> Create(CreatePostCategoryRequestModel requestModel);
        Task Update(UpdatePostCategoryRequestModel requestModel);
    }
}
