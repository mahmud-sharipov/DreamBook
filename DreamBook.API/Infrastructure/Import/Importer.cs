using DreamBook.Application.Abstraction;
using DreamBook.Domain.Entities;
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

        public static void ImportInterpretation(IContext context)
        {
            if (context.Count<Book>() > 0) return;

            IEnumerable<InterpretationImportModel> interpretations = JsonConvert.DeserializeObject<IEnumerable<InterpretationImportModel>>(File.ReadAllText(@"Infrastructure/Import/sonnik.json"));
            Dictionary<string, (Book book, BookTranslation bookRu, BookTranslation bookEn)> books =
                new Dictionary<string, (Book book, BookTranslation bookRu, BookTranslation bookEn)>();
            Console.WriteLine("Started importing words/books/interpretations");
            var index = 1;
            var interpretationsByWord = interpretations.GroupBy(i => i.Word);
            var totalcount = interpretationsByWord.Count();
            foreach (var wordInterpretations in interpretationsByWord)
            {
                Console.WriteLine($"Imported {index++} of {totalcount}");
                var word = new Word();
                context.Add(word);
                var wordRu = new WordTranslation() { Name = wordInterpretations.Key, LanguageGuid = RuGuid };
                var wordEn = new WordTranslation() { Name = wordInterpretations.Key, LanguageGuid = EnGuid };
                word.Translations.Add(wordRu);
                word.Translations.Add(wordEn);

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
                    interpretation.Translations.Add(new InterpretationTranslation()
                    {
                        Book = bookData.bookRu,
                        Word = wordRu,
                        Description = interpretationData.Interpretation,
                        LanguageGuid = RuGuid
                    });
                    interpretation.Translations.Add(new InterpretationTranslation()
                    {
                        Book = bookData.bookEn,
                        Word = wordEn,
                        Description = interpretationData.Interpretation,
                        LanguageGuid = EnGuid
                    });
                }
            }
            Console.WriteLine("Finished import!");
            context.SaveChanges();
        }

        static (Book book, BookTranslation bookRu, BookTranslation bookEn) CreateBook(IContext context, string name)
        {
            var book = new Book();
            var bookRu = new BookTranslation() { Name = name, LanguageGuid = RuGuid };
            var bookEn = new BookTranslation() { Name = name, LanguageGuid = EnGuid };
            book.Translations.Add(bookRu);
            book.Translations.Add(bookEn);
            context.Add(book);
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