using AutoMapper;
using DreamBook.Application.Abstraction;
using DreamBook.Application.Abstraction.Service;
using DreamBook.Application.LanguageResources;
using DreamBook.Domain.Entities;
using System.Threading.Tasks;

namespace DreamBook.Application.Books
{
    public class BookService : TranslatableEntityService<Book, BookTranslation, BookResponseModel, BookWithTranslationsResponseModel>, IBookService
    {
        public BookService(IContext context, IMapper mapper, AppLanguageManager appLanguageManager) : base(context, mapper, appLanguageManager) { }

        public async Task<BookWithTranslationsResponseModel> Create(CreateBookRequestModel requestModel)
        {
            return await Create<BookTranslationRequestModel>(requestModel);
        }

        public async Task Update(UpdateBookRequestModel requestModel)
        {
            await Update(requestModel, requestModel.Guid);
        }

        protected override (bool CanBeDeleted, string Reason) CanEntityBeDeleted(Book entity)
        {
            if (Context.Count<Interpretation>(d => d.BookGuid == entity.Guid) > 0)
                return (false, Messages.DreatemTypeCanNotBeDeletedReason);

            return base.CanEntityBeDeleted(entity);
        }

        protected override string GetDefaultSearchPropertyName() => nameof(BookTranslation.Name);
        protected override string GetDefaultPropertyNameToOrderBy() => nameof(BookTranslation.Name);

    }
}

