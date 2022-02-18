using AutoMapper;
using DreamBook.Application.Abstraction;
using DreamBook.Application.Abstraction.Service;
using DreamBook.Application.LanguageResources;
using DreamBook.Application.Exceptions;
using DreamBook.Domain.Entities;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace DreamBook.Application.Books
{
    public class BookService : TranslatableEntityService<Book, BookTranslation, BookResponseModel, BookWithTranslationsResponseModel>, IBookService
    {
        public BookService(IContext context, IMapper mapper, AppLanguageManager appLanguageManager) : base(context, mapper, appLanguageManager) { }

        public async Task<BookWithTranslationsResponseModel> Create(CreateBookRequestModel requestModel)
        {
            await ValidateBookName(requestModel);
            return await Create<BookTranslationRequestModel>(requestModel);
        }

        public async Task Update(UpdateBookRequestModel requestModel)
        {
            await ValidateBookName(requestModel, requestModel.Guid);
            await Update(requestModel, requestModel.Guid);
        }

        private async Task ValidateBookName(CreateBookRequestModel requestModel, Guid? entityId = null)
        {
            var names = requestModel.Translations.Select(x => x.Name.ToLower() + x.LanguageGuid).ToArray();
            var wordId = entityId ?? Guid.Empty;
            var booksWithSameName = await Context
                .GetAllAsync<BookTranslation>(wt => wt.BookGuid != wordId && names.Contains(wt.Name.ToLower() + wt.LanguageGuid));
            if (booksWithSameName.Any())
            {
                var similarNames = string.Join(", ", booksWithSameName.Select(b => b.Name));
                throw new BusinessLogicException(ExceptionMessages.BookWithSameNameExist.Format(similarNames));
            }
        }

        protected override (bool CanBeDeleted, string Reason) CanEntityBeDeleted(Book entity)
        {
            if (Context.GetAll<Interpretation>(e => e.BookGuid == entity.Id).Any())
                return (false, ExceptionMessages.BookCanNotBeDeletedReason);

            return base.CanEntityBeDeleted(entity);
        }

        protected override string GetDefaultSearchPropertyName() => nameof(BookTranslation.Name);
        protected override string GetDefaultPropertyNameToOrderBy() => nameof(BookTranslation.Name);
        protected override string GetEntityLabel() => ModelsLabel.Book;

    }
}

