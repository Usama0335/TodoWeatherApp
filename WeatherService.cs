using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace MauiApp1
{
    public class WeatherService
    {
        private readonly HttpClient _httpClient;
        private const string ApiKey = "dc026567884e19386538bbb1bcb9cf2b"; // Replace with your actual API key
        private const string ApiBaseUrl = "https://api.openweathermap.org/data/2.5/weather";

        public WeatherService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<WeatherModel?> GetWeatherByCityAsync(string cityName)
        {
            try
            {
                // Construct the API URL
                var url = $"{ApiBaseUrl}?q={cityName}&appid={ApiKey}&units=metric";

                // Make the API request
                var response = await _httpClient.GetAsync(url);

                // Ensure the response status is successful
                response.EnsureSuccessStatusCode();

                // Parse the response JSON
                var responseContent = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<WeatherModel>(responseContent);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HTTP Request Error: {ex.Message}");
                throw new Exception("Failed to fetch weather data. Please check your internet connection or city name.");
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"JSON Parsing Error: {ex.Message}");
                throw new Exception("Failed to parse weather data. Please contact the administrator.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected Error: {ex.Message}");
                throw;
            }
        }
    }
}
