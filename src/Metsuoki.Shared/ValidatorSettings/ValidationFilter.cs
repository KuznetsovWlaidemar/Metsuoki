using Metsuoki.Shared.Interfaces.Result;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Metsuoki.Shared.ValidatorSettings;

public class ValidationFilter : IActionFilter
{
    public void OnActionExecuted(ActionExecutedContext context) { }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.ModelState.IsValid)
            return;

        var errors = context.ModelState
            .Where(m => m.Value != null && m.Value.Errors.Any())
            .ToDictionary(kvp => kvp.Key, kvp => kvp.Value?.Errors.Select(e => e.ErrorMessage)
            .ToList());

        var result = errors.Select(e =>
        {
            if (e.Value != null)
                return new Dictionary<string, List<string>>
                {
                    { e.Key, e.Value }
                };

            return null;
        });

        context.Result = Result<object>.BadRequest("Validation failed", "ValidationErrors", result);
    }
}