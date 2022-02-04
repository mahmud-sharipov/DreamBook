using DreamBook.Application.Abstraction;
using DreamBook.Domain.Entities;
using DreamBook.Persistence.Database;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static DreamBook.API.Persistence.SeedDatabase;

namespace DreamBook.API.Infrastructure.Import
{
    public static class Importer
    {
        public static void ImportAll(IContext context)
        {
            ImportInterpretation(context);
            CreateAds(context);
        }

        public static void ImportInterpretation(IContext gdffgfgdf)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DreamBookSqlServerContext>();
            optionsBuilder.UseLazyLoadingProxies();
            optionsBuilder.UseSqlServer("data source=localhost; initial catalog=DreamBook; integrated security=true; persist security info=true;");

            var context1 = new DreamBookSqlServerContext(optionsBuilder.Options);
            context1.Dispose();
            using var context = new DreamBookSqlServerContext(optionsBuilder.Options);

            if (context.Count<Book>() > 0) return;

            List<InterpretationImportModel> interpretations = JsonConvert.DeserializeObject<List<InterpretationImportModel>>(File.ReadAllText(@"Infrastructure/Import/sonnik.json"));
            Dictionary<string, (Book book, BookTranslation bookRu, BookTranslation bookEn)> books =
                new Dictionary<string, (Book book, BookTranslation bookRu, BookTranslation bookEn)>();
            Console.WriteLine("Started importing words/books/interpretations");
            var index = 1;
            var interpretationsByWord = interpretations.GroupBy(i => i.Word).ToList();
            var totalcount = interpretationsByWord.Count;
            for (int i = 0; i < totalcount; i++)
            {
                var wordInterpretations = interpretationsByWord[i];
                Console.WriteLine($"Imported {index++} of {totalcount}");
                var word = new Word();
                var wordRu = new WordTranslation() { WordGuid = word.Guid, Name = wordInterpretations.Key, LanguageGuid = RuGuid };
                var wordEn = new WordTranslation() { WordGuid = word.Guid, Name = wordInterpretations.Key, LanguageGuid = EnGuid };
                context.Add(word);
                context.Add(wordRu);
                context.Add(wordEn);

                foreach (var interpretationData in wordInterpretations)
                {
                    (Book book, BookTranslation bookRu, BookTranslation bookEn) bookData;
                    if (books.ContainsKey(interpretationData.Book))
                        bookData = books[interpretationData.Book];
                    else
                    {
                        bookData = CreateBook(context, interpretationData.Book);
                        books.Add(interpretationData.Book, bookData);
                    }

                    var interpretation = new Interpretation()
                    {
                        Word = word,
                        Book = bookData.book
                    };


                    context.Add(interpretation);
                    context.Add(new InterpretationTranslation()
                    {
                        InterpretationGuid = interpretation.Guid,
                        Book = bookData.bookRu,
                        Word = wordRu,
                        Description = interpretationData.Interpretation,
                        LanguageGuid = RuGuid
                    });
                    context.Add(new InterpretationTranslation()
                    {
                        InterpretationGuid = interpretation.Guid,
                        Book = bookData.bookEn,
                        Word = wordEn,
                        Description = interpretationData.Interpretation,
                        LanguageGuid = EnGuid
                    });
                }

                if (i % 100 == 0)
                    context.SaveChanges();
            }
            Console.WriteLine("Finished import!");
            context.SaveChanges();
            books.Clear();
            interpretations.Clear();
            books = null;
            context.ChangeTracker.Clear();
            interpretations = null;
        }

        static (Book book, BookTranslation bookRu, BookTranslation bookEn) CreateBook(IContext context, string name)
        {
            var book = new Book();
            var bookRu = new BookTranslation() { BookGuid = book.Guid, Name = name, LanguageGuid = RuGuid };
            var bookEn = new BookTranslation() { BookGuid = book.Guid, Name = name, LanguageGuid = EnGuid };
            context.Add(book);
            context.Add(bookRu);
            context.Add(bookEn);
            return (book, bookRu, bookEn);
        }

        static void CreateAds(IContext context)
        {
            if (context.Count<Ad>() > 0) return;

            IEnumerable<Ad> ads = JsonConvert.DeserializeObject<IEnumerable<Ad>>(File.ReadAllText(@"Infrastructure/Import/ad.json"));
            context.AddRange(ads);
            context.SaveChanges();
        }
    }
}

class InterpretationImportModel
{
    public string Id { get; set; }

    public string Word { get; set; }
    public string Book { get; set; }
    public string Interpretation { get; set; }
}