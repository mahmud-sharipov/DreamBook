using AutoMapper;
using DreamBook.Application.Abstraction;
using DreamBook.Application.Abstraction.Service;
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
            return await Create<CreatePostRequestModel>(requestModel);
        }

        public async Task Update(UpdatePostRequestModel requestModel)
        {
            await Update(requestModel, requestModel.Guid);
        }

        protected override (bool CanBeDeleted, string Reason) CanEntityBeDeleted(Post entity)
        {
            return (false, Messages.EntityCanNotBeDeleted);
        }

        protected override string GetDefaultSearchPropertyName() => nameof(Post.Title);
        protected override string GetDefaultPropertyNameToOrderBy() => nameof(Post.CreatedAt);

    }
}
