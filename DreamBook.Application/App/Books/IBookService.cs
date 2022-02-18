namespace DreamBook.Application.Books
{
    public interface IBookService : ITranslatableEntityService<Book, BookTranslation, BookResponseModel, BookWithTranslationsResponseModel>
    {
        Task<BookWithTranslationsResponseModel> Create(CreateBookRequestModel requestModel);
        Task Update(UpdateBookRequestModel requestModel);
    }
}
