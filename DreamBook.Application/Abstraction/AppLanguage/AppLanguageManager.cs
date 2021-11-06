using DreamBook.Domain.Entities;
using DreamBook.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace DreamBook.Application.Abstraction
{
    public class AppLanguageManager
    {
        public AppLanguageManager(IContext context)
        {
            Context = context;
            SupportLanguages = Context.GetAll<Language>().OrderByDescending(l => l.IsDefault).ThenBy(l => l.Code).ToList();
            AppSupportLanguages = SupportLanguages;
            var currentLanguageName = Thread.CurrentThread.CurrentCulture.Name;
            CurrentLanguage = SupportLanguages.FirstOrDefault(l => l.Name.ToLower().Contains(currentLanguageName));
            CurrentAppLanguage = CurrentLanguage;
        }

        IContext Context { get; }

        public IReadOnlyList<IAppLanguage> SupportLanguages { get; }

        public static IReadOnlyList<IAppLanguage> AppSupportLanguages { get; private set; }

        public IEnumerable<Guid> SupportLanguagesGuid => SupportLanguages?.Select(l => l.Guid) ?? Enumerable.Empty<Guid>();

        IAppLanguage _currentLanguage = null;
        public IAppLanguage CurrentLanguage
        {
            get
            {
                if (_currentLanguage == null)
                    _currentLanguage = DefaultLanguage;
                return _currentLanguage;
            }
            private set => _currentLanguage = value;
        }

        public static IAppLanguage CurrentAppLanguage { get; set; }

        public IAppLanguage DefaultLanguage => SupportLanguages.FirstOrDefault(l => l.IsDefault) ?? SupportLanguages.FirstOrDefault();

        public void SetCurentLaguage(string lancuageCode)
        {
            SetCurentLaguage(SupportLanguages.SingleOrDefault(l => l.Code.ToLower() == lancuageCode.ToLower()));
        }

        public void SetCurentLaguage(IAppLanguage laguage)
        {
            CurrentLanguage = laguage ?? throw new ArgumentNullException(nameof(laguage));
            CurrentAppLanguage = CurrentLanguage;
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(laguage.Code);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
        }
    }
}
