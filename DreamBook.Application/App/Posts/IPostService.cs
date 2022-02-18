namespace DreamBook.Application.Posts
{
    public interface IPostService : IEntityService<Post, PostResponseModel>
    {
        Task<PostResponseModel> Create(CreatePostRequestModel requestModel);
        Task Update(UpdatePostRequestModel requestModel);
    }
}
