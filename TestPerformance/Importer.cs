using DreamBook.Application.Abstraction;
using DreamBook.Domain.Entities;
using DreamBook.Persistence.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Test
{
    public static class Importer
    {
        internal static Guid RuGuid => new Guid("943fee14-dba5-4195-9afe-20e4fe46f017");
        internal static Guid EnGuid => new Guid("3b7c7370-8759-400c-9624-e5fc947fb07c");

        public static void ImportAll()
        {
            var configuration = new ConfigurationBuilder()
                  .SetBasePath(Directory.GetCurrentDirectory())
                  .AddJsonFile($"dbsettings.Development.json", optional: true)
                  .AddJsonFile($"appsettings.json", optional: true)
                  .AddEnvironmentVariables()
                  .Build();
            using DreamBookContext context = new DreamBookContext(configuration);
            context.Database.EnsureCreated();
            ImportInterpretation(context);
        }

        public static void ImportInterpretation(DreamBookContext context)
        {
            if (context.Count<Book>(null) > 0) return;

            context.AddRange(new[]
            {
                new Language()
                {
                    Guid = RuGuid,
                    Name = "Русский",
                    Code = "ru-RU",
                    IsDefault = true
                },
                new Language()
                {
                    Guid = EnGuid,
                    Name = "English",
                    Code = "en-EN"
                }
            });
            context.SaveChanges();

            IEnumerable<InterpretationImportModel> interpretations = JsonConvert.DeserializeObject<IEnumerable<InterpretationImportModel>>(File.ReadAllText(@"sonnik.json"));
            Dictionary<string, (Guid book, Guid bookRu, Guid bookEn)> books =
                    new Dictionary<string, (Guid book, Guid bookRu, Guid bookEn)>();
            Console.WriteLine("Started importing words/books/interpretations");
            var index = 1;
            var interpretationsByWord = interpretations.GroupBy(i => i.Word).ToList();
            var totalcount = interpretationsByWord.Count;
            return;
            for (int i = 0; i < totalcount; i++)
            {
                var wordInterpretations = interpretationsByWord[i];
                Console.WriteLine($"Imported {index++} of {totalcount}");
                var word = new Word();
                context.Add(word);
                var wordRu = new WordTranslation() { WordGuid = word.Guid, Name = wordInterpretations.Key, LanguageGuid = RuGuid };
                var wordEn = new WordTranslation() { WordGuid = word.Guid, Name = wordInterpretations.Key, LanguageGuid = EnGuid };
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
                if (i % 500 == 0)
                    context.SaveChanges();
            }
            context.SaveChanges();
            System.GC.SuppressFinalize(interpretations);
            System.GC.SuppressFinalize(books);
            Console.WriteLine("Finished import!");
        }

        static (Guid book, Guid bookRu, Guid bookEn) CreateBook(DreamBookContext context, string name)
        {
            var book = new Book();
            var bookRu = new BookTranslation() { BookGuid = book.Guid, Name = name, LanguageGuid = RuGuid };
            var bookEn = new BookTranslation() { BookGuid = book.Guid, Name = name, LanguageGuid = EnGuid };
            context.Add(book);
            context.Add(bookRu);
            context.Add(bookEn);
            return (book.Guid, bookRu.Guid, bookEn.Guid);
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