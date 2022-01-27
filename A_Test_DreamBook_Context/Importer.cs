using DreamBook.Domain.Entities;
using DreamBook.Persistence.Database;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace A_Test_DreamBook_Context
{
    public class Importer
    {
        internal static Guid RuGuid => new Guid("943fee14-dba5-4195-9afe-20e4fe46f017");
        internal static Guid EnGuid => new Guid("3b7c7370-8759-400c-9624-e5fc947fb07c");
        private readonly IConfiguration _configuration;

        public Importer()
        {
            _configuration = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile($"dbsettings.Development.json", optional: true)
                 .AddJsonFile($"appsettings.json", optional: true)
                 .AddEnvironmentVariables()
                 .Build();

            CreateDataBase();
            CreateLanguages();
            ImportInterpretation();
        }

        public void CreateDataBase()
        {
            using (var context = new DreamBookContext(_configuration))
            {
                context.Database.EnsureCreated();
            }
        }

        void CreateLanguages()
        {
            using (var context = new DreamBookContext(_configuration))
            {
                context.Add(new Language()
                {
                    Guid = RuGuid,
                    Name = "Русский",
                    Code = "ru-RU",
                    IsDefault = true
                });

                context.Add(new Language()
                {
                    Guid = EnGuid,
                    Name = "English",
                    Code = "en-EN"
                });
                context.SaveChanges();
            }
        }

        void ImportInterpretation()
        {
            IEnumerable<InterpretationImportModel> interpretations = JsonConvert.DeserializeObject<IEnumerable<InterpretationImportModel>>(File.ReadAllText(@"sonnik.json"));
            Dictionary<string, (Guid book, Guid bookRu, Guid bookEn)> books =
                    new Dictionary<string, (Guid book, Guid bookRu, Guid bookEn)>();
            Console.WriteLine("Started importing words/books/interpretations");
            var index = 1;
            var interpretationsByWord = interpretations.GroupBy(i => i.Word).ToList();
            var totalcount = interpretationsByWord.Count;
            for (int i = 0; i < totalcount; i++)
            {
                var wordInterpretations = interpretationsByWord[i];
                Console.WriteLine($"Imported {index++} of {totalcount}");
                using (var context = new DreamBookContext(_configuration))
                {
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
                    context.SaveChanges();
                }
            }
            books.Clear();
            interpretations = null;
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