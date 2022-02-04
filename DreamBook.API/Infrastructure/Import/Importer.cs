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

        public static void ImportInterpretation(IContext mainContext)
        {
            if (mainContext.Count<Book>() > 0) return;

            var factory = new DreamBookSqliteContextFactory();

            List<InterpretationImportModel> interpretations = JsonConvert.DeserializeObject<List<InterpretationImportModel>>(File.ReadAllText(@"Infrastructure/Import/sonnik.json"));
            Dictionary<string, (Guid book, Guid bookRu, Guid bookEn)> books =
                new Dictionary<string, (Guid book, Guid bookRu, Guid bookEn)>();
            Console.WriteLine("Started importing words/books/interpretations");
            var index = 1;
            var interpretationsByWord = interpretations.GroupBy(i => i.Word).ToList();
            var totalcount = interpretationsByWord.Count;
            for (int i = 0; i < totalcount; i++)
            {
                using var context = factory.CreateDbContext(null);
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
                    (Guid book, Guid bookRu, Guid bookEn) bookData;
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
                        BookGuid = bookData.book
                    };


                    context.Add(interpretation);
                    context.Add(new InterpretationTranslation()
                    {
                        InterpretationGuid = interpretation.Guid,
                        BookGuid = bookData.bookRu,
                        Word = wordRu,
                        Description = interpretationData.Interpretation,
                        LanguageGuid = RuGuid
                    });
                    context.Add(new InterpretationTranslation()
                    {
                        InterpretationGuid = interpretation.Guid,
                        BookGuid = bookData.bookEn,
                        Word = wordEn,
                        Description = interpretationData.Interpretation,
                        LanguageGuid = EnGuid
                    });
                }
            }
            Console.WriteLine("Finished import!");
        }

        static (Guid book, Guid bookRu, Guid bookEn) CreateBook(IContext context, string name)
        {
            var book = new Book();
            var bookRu = new BookTranslation() { BookGuid = book.Guid, Name = name, LanguageGuid = RuGuid };
            var bookEn = new BookTranslation() { BookGuid = book.Guid, Name = name, LanguageGuid = EnGuid };
            context.Add(book);
            context.Add(bookRu);
            context.Add(bookEn);
            return (book.Guid, bookRu.Guid, bookEn.Guid);
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