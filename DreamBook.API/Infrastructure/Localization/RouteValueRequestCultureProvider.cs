using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DreamBook.API.Infrastructure.Localization
{
    public class RouteValueRequestCultureProvider : RequestCultureProvider
    {
        public override Task<ProviderCultureResult> DetermineProviderCultureResult(HttpContext httpContext)
        {
            string cultureCode;
            if (httpContext.Request.Path.HasValue && httpContext.Request.Path.Value == "/")
            {
                cultureCode = GetDefaultCultureCode();
            }
            else if (httpContext.Request.Path.HasValue && httpContext.Request.Path.Value.Length >= 7 && httpContext.Request.Path.Value[0] == '/' && httpContext.Request.Path.Value[6] == '/')
            {
                cultureCode = httpContext.Request.Path.Value.Substring(1, 5);
                if (!CheckCultureCode(cultureCode))
                    cultureCode = GetDefaultCultureCode(); //throw new HttpException(HttpStatusCode.NotFound);
            }
            else
            {
                cultureCode = GetDefaultCultureCode(); //throw new HttpException(HttpStatusCode.NotFound);
            }

            ProviderCultureResult requestCulture = new ProviderCultureResult(cultureCode);

            return Task.FromResult(requestCulture);
        }

        private string GetDefaultCultureCode() =>
            Options.DefaultRequestCulture.Culture.Name;

        private bool CheckCultureCode(string cultureCode) =>
            Options.SupportedCultures.Select(c => c.Name).Contains(cultureCode);
    }
}