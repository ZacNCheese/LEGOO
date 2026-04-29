namespace LEGOOO;
using System.Text.Json;
using System.Diagnostics;
using Microsoft.Maui.Primitives;

public partial class MainPage : ContentPage
{
	public List<LegoColor> LegoColors { get; set; }


	private HttpClient _client = new HttpClient();

	private JsonSerializerOptions _serializerOptions = new JsonSerializerOptions
	{
		PropertyNamingPolicy = JsonNamingPolicy.CamelCase
	};

	public MainPage()
	{
		InitializeComponent();
	}

	public async void OnCounterClicked(object? sender, EventArgs e)
	{
		//we want to test the bricklink API connection here

		List<LegoColor> colors = await GetLegoColorsAsync();

		foreach (var color in colors)
		{
			Debug.WriteLine(color.ColorRGB);
		}

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
}
