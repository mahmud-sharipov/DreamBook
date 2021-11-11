using AutoMapper;
using DreamBook.Application.Abstraction;
using DreamBook.Application.Abstraction.Service;
using DreamBook.Application.Exceptions;
using DreamBook.Application.LanguageResources;
using DreamBook.Domain.Entities;
using System.Threading.Tasks;

namespace DreamBook.Application.Posts
{
    public class PostService : EntityService<Post, PostResponseModel>, IPostService
    {
        public PostService(IContext context, IMapper mapper) : base(context, mapper) { }

        public async Task<PostResponseModel> Create(CreatePostRequestModel requestModel)
        {
            await ValidateCategory(requestModel);
            return await Create<CreatePostRequestModel>(requestModel);
        }

        public async Task Update(UpdatePostRequestModel requestModel)
        {
            await ValidateCategory(requestModel);
            await Update(requestModel, requestModel.Guid);
        }

        async Task ValidateCategory(CreatePostRequestModel requestModel)
        {
            var category = await Context.GetByIdAsync<PostCategory>(requestModel.CategoryGuid);
            if (category == null)
                throw new EntityNotFoundException(ModelsLabel.PostCategory, requestModel.CategoryGuid);
        }

        protected override (bool CanBeDeleted, string Reason) CanEntityBeDeleted(Post entity)
        {
            return (false, ExceptionMessages.EntityCanNotBeDeleted);
        }

        protected override string GetDefaultSearchPropertyName() => nameof(Post.Title);
        protected override string GetDefaultPropertyNameToOrderBy() => nameof(Post.CreatedAt);
        protected override string GetEntityLabel() => ModelsLabel.Post;
    }
}
