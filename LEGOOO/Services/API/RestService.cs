namespace LEGOOO;
using System.Text.Json;
using System.Diagnostics;

public class RestService
{
    HttpClient _client;
    JsonSerializerOptions _serializerOptions;
    public List<LegoColor>? LegoColors { get; set; }
	public List<LegoMinifig>? LegoMinifigs { get; set; }

    public RestService()
    {
        _client = new HttpClient();
        _serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };
    }

    public async Task<List<LegoColor>> GetLegoColorsAsync()
		{
			LegoColors = new List<LegoColor>();

			Uri uri = new Uri(string.Format("https://rebrickable.com/api/v3/lego/colors/?key=2d2ec2792fd89d2b412f795f5705ac5f", string.Empty));
			try
			{
				HttpResponseMessage response = await _client.GetAsync(uri);
				if (response.IsSuccessStatusCode)
				{
					string content = await response.Content.ReadAsStringAsync();
					var result = JsonSerializer.Deserialize<LegoColorResponse>(content, _serializerOptions);
					LegoColors = result?.Results ?? new List<LegoColor>();
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(@"\tERROR {0}", ex.Message);
			}

			return LegoColors;
		}

	public async Task<List<LegoMinifig>> GetLegoMinifigsAsync()
	//this function actually loads ALL figures... takes a long time so it has pauses in it, or the API will throw an error.
	//I am going to update the standard now that all records are in to only grab recent additions/changes. Maybe limited to the last few months.
	{
		var allMinifigs = new List<LegoMinifig>();

		string url = "https://rebrickable.com/api/v3/lego/minifigs/?key=2d2ec2792fd89d2b412f795f5705ac5f&page_size=2000";

		while (!string.IsNullOrEmpty(url))
		{
			var response = await _client.GetAsync(url);

			if (response.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
			{
				Debug.WriteLine("RATE LIMITED - waiting...");
				await Task.Delay(2000); // wait 2 seconds, then retry
				continue;
			}

			response.EnsureSuccessStatusCode();

			var json = await response.Content.ReadAsStringAsync();
			var result = JsonSerializer.Deserialize<LegoMinifigResponse>(json, _serializerOptions);

			if (result != null)
			{
				allMinifigs.AddRange(result.Results);

				url = result.Next != null ? result.Next + "&key=2d2ec2792fd89d2b412f795f5705ac5f": null;
			}

			await Task.Delay(200); // 👈 prevents hitting rate limit
		}

		return allMinifigs;
	}
    
}