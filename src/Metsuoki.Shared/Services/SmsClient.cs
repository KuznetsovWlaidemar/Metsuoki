using RestSharp;

namespace Metsuoki.Shared.Services;
public class SmsClient
{
    private readonly string _apiKey;
    private readonly RestClient _client;

    public SmsClient(string apiKey)
    {
        _apiKey = apiKey;
        _client = new RestClient("https://sms.ru/sms/send");
    }

    public async Task SendSmsAsync(string phoneNumber, string message)
    {
        var request = new RestRequest(Method.Post.ToString());
        request.AddParameter("api_id", _apiKey); // Ваш API ключ SMS.ru
        request.AddParameter("to", phoneNumber);  // Номер получателя
        request.AddParameter("msg", message);     // Сообщение
        request.AddParameter("json", "1");        // Ответ в формате JSON

        try
        {
            var response = await _client.ExecuteAsync(request);
            if (!response.IsSuccessful)
            {
                throw new Exception($"Ошибка при отправке SMS: {response.ErrorMessage}");
            }

            var responseContent = response.Content;
            Console.WriteLine("SMS отправлено успешно: " + responseContent);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ошибка при отправке SMS: " + ex.Message);
            throw;
        }
    }
}