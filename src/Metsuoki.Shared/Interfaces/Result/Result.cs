using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Metsuoki.Shared.Interfaces.Result;
public sealed class Result<T> : IResult<T>, IResult, IActionResult
{
    public T? Value { get; set; }

    public Locale Locale { get; set; } = Locale.Rus;

    private int StatusCode { get; init; }

    public required IEnumerable<string> Messages { get; init; }

    public IEnumerable<IError>? Errors { get; init; }

    public bool IsSuccess { get; private init; }

    public static IResult<T> Ok(T value) =>
        new Result<T>
        {
            IsSuccess = true,
            StatusCode = (int)HttpStatusCode.OK,
            Value = value,
            Messages = ["Ok"]
        };

    public static IResult<T> Ok(string message, T value) =>
         new Result<T>
         {
             IsSuccess = true,
             StatusCode = (int)HttpStatusCode.OK,
             Value = value,
             Messages = [message]
         };

    public static IResult<T> BadRequest(string message) =>
        new Result<T>
        {
            IsSuccess = false,
            StatusCode = (int)HttpStatusCode.BadRequest,
            Messages = [message]
        };

    public static IResult<T> BadRequest(IError error) =>
        new Result<T>
        {
            IsSuccess = false,
            StatusCode = (int)HttpStatusCode.BadRequest,
            Messages = [error.Message],
            Errors = [error]
        };

    public static IResult<T> BadRequest(IEnumerable<IError> errorsList)
    {
        var errors = errorsList.ToList();
        return new Result<T>
        {
            IsSuccess = false,
            StatusCode = (int)HttpStatusCode.BadRequest,
            Messages = [errors.FirstOrDefault()?.Message ?? "Bad Request"],
            Errors = errors
        };
    }

    public static IResult<T> BadRequest(string errorMessage, string key, object value) =>
        new Result<T>
        {
            IsSuccess = false,
            StatusCode = (int)HttpStatusCode.BadRequest,
            Messages = [errorMessage],
            Errors = [
                new Error
                {
                    Message = errorMessage,
                    Metadata = new Dictionary<string, object> { { key, value } }
                }
            ]
        };

    public static IResult<T> NotFound(string message) =>
        new Result<T>
        {
            IsSuccess = false,
            StatusCode = (int)HttpStatusCode.NotFound,
            Messages = [message]
        };

    public static IResult<T> NotFound(IError error) =>
        new Result<T>
        {
            IsSuccess = false,
            StatusCode = (int)HttpStatusCode.NotFound,
            Messages = [error.Message],
            Errors = [error]
        };

    public static IResult<T> NotFound(IEnumerable<IError> errorsList)
    {
        var errors = errorsList.ToList();
        return new Result<T>
        {
            IsSuccess = false,
            StatusCode = (int)HttpStatusCode.NotFound,
            Messages = [errors.FirstOrDefault()?.Message ?? "Not Found"],
            Errors = errors
        };
    }

    public static IResult<T> NotFound(string errorMessage, string key, object value) =>
        new Result<T>
        {
            IsSuccess = false,
            StatusCode = (int)HttpStatusCode.NotFound,
            Messages = [errorMessage],
            Errors = [
                new Error
                {
                    Message = errorMessage,
                    Metadata = new Dictionary<string, object> { { key, value } }
                }
            ]
        };

    public static IResult<T> NoContent(string message) =>
        new Result<T>
        {
            IsSuccess = true,
            StatusCode = (int)HttpStatusCode.NoContent,
            Messages = [message]
        };


    public async Task ExecuteResultAsync(ActionContext context)
    {
        if (Messages == null)
        {
            throw new ArgumentException("Сообщение не может быть пустым", "Messages");
        }

        context.HttpContext.Response.StatusCode = StatusCode;
        if (Value != null)
        {
            context.HttpContext.Response.ContentType = "application/json";
            string text = JsonSerializer.Serialize(new { IsSuccess, Locale, Messages, Value });
            await context.HttpContext.Response.WriteAsync(text);
        }
        else
        {
            context.HttpContext.Response.ContentType = "application/json";
            string text2 = JsonSerializer.Serialize(new { IsSuccess, Locale, Messages, Errors });
            await context.HttpContext.Response.WriteAsync(text2);
        }
    }
}