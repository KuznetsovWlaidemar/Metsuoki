namespace Metsuoki.Shared.Interfaces.Result;
public sealed class Error : IError
{
    public string Message { get; init; }

    public IReadOnlyDictionary<string, object> Metadata { get; set; } = new Dictionary<string, object>();


    public Error()
    {
        Message = "Error";
    }

    public Error(string message)
    {
        Message = message;
    }

    public Error(string message, IReadOnlyDictionary<string, object> metadata)
    {
        Message = message;
        Metadata = metadata;
    }

    public Error(IReadOnlyDictionary<string, object> metadata)
    {
        Message = metadata.MessagesToString();
        Metadata = metadata;
    }

    public Error(string key, object value)
    {
        Message = $"{key}: {value}";
        Metadata = new Dictionary<string, object> { { key, value } };
    }
}