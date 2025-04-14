namespace Metsuoki.Shared.Interfaces.Result;
internal static class ResultExtensions
{
    public static string MessagesToString(this IEnumerable<string> value)
    {
        List<string> list = value.ToList();
        if (list.Count == 0)
        {
            throw new ArgumentException("Список ошибок не может быть пустым при попытке получить строку ошибок", "value");
        }

        return string.Join(Environment.NewLine, list);
    }

    public static string MessagesToString(this IReadOnlyDictionary<string, object> value)
    {
        if (value.Count == 0)
        {
            throw new ArgumentException("Метаданные не могут быть пустыми при попытке получить строку метаданных", "value");
        }

        return string.Join(Environment.NewLine, value.Select<KeyValuePair<string, object>, string>((KeyValuePair<string, object> x) => x.Key + ": " + x.Value));
    }
}