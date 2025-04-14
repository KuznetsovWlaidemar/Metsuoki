namespace Metsuoki.Shared.Interfaces.Result;
public interface IError
{
    string Message { get; }

    IReadOnlyDictionary<string, object> Metadata { get; set; }
}