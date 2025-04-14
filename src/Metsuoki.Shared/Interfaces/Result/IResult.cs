using Microsoft.AspNetCore.Mvc;

namespace Metsuoki.Shared.Interfaces.Result;
public interface IResult : IActionResult
{
    IEnumerable<string> Messages { get; init; }

    IEnumerable<IError>? Errors { get; init; }

    bool IsSuccess
    {
        get
        {
            if (Errors != null)
            {
                return !Errors.Any();
            }

            return true;
        }
    }
}

public interface IResult<T> : IResult, IActionResult
{
    T? Value { get; set; }
}