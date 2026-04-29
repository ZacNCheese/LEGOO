namespace LEGOOO;
using System.Text.Json;

public class RestService
{
    HttpClient _client;
    JsonSerializerOptions _serializerOptions;

    public RestService()
    {
        _client = new HttpClient();
        _serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };
    }
    
}