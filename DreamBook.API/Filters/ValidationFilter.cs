using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace DreamBook.API.Filters
{
    public class ValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.ModelState.IsValid)
            {
                await next();
            }
            else
            {
                var errorInModelState = context.ModelState
                       .Where(x => x.Value.Errors.Count > 0)
                       .ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(x => x.ErrorMessage)).ToArray();

                var errorResponse = new ErrorResponse()
                {
                    Title = "One or more validation errors occurred",
                    Status = (int)HttpStatusCode.BadRequest
                };

                foreach (var error in errorInModelState)
                {
                    var propertiValidations = new PropertyValidationErrorModel();
                    propertiValidations.PropertyName = error.Key;

                    foreach (var message in error.Value)
                        propertiValidations.Messages.Add(message);

                    errorResponse.PropertyValidations.Add(propertiValidations);
                }

                context.Result = new BadRequestObjectResult(errorResponse);
            }
        }
    }
}
