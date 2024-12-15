using Microsoft.Maui.Controls;
using System;
using System.Threading.Tasks;

namespace MauiApp1
{
    public partial class MainPage : ContentPage
    {
        private readonly WeatherService _weatherService;

        public MainPage()
        {
            InitializeComponent();
            _weatherService = new WeatherService();
        }

        // Event handler for "Get Weather" button click
        private async void OnGetWeatherClicked(object sender, EventArgs e)
        {
            string cityName = CitySearchBar.Text;

            if (string.IsNullOrEmpty(cityName))
            {
                await DisplayAlert("Error", "Please enter a city name.", "OK");
                return;
            }

            await GetWeatherAsync(cityName);
        }

        private async void OnNavigateToTodoPage(object sender, EventArgs e)
        {
            try
            {
                await Shell.Current.GoToAsync("//TodoPage"); // Use absolute route
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to navigate to To-Do page: {ex.Message}", "OK");
            }
        }

        private async Task GetWeatherAsync(string cityName)
        {
            try
            {
                var weatherData = await _weatherService.GetWeatherByCityAsync(cityName);

                TemperatureLabel.Text = $"Temperature: {weatherData.main.temp}°C";
                TempMinLabel.Text = $"Min Temperature: {weatherData.main.temp_min}°C";
                TempMaxLabel.Text = $"Max Temperature: {weatherData.main.temp_max}°C";
                ForecastLabel.Text = $"Forecast: {weatherData.weather[0].main} - {weatherData.weather[0].description}";

                WindAlertLabel.Text = weatherData.wind.speed > 4
                    ? "Wind Alert: High wind speed today!"
                    : "Wind Alert: Normal";

                WindAlertLabel.TextColor = weatherData.wind.speed > 4
                    ? Colors.Red
                    : Colors.Green;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Unable to get weather data: {ex.Message}", "OK");
            }
        }
    }
}
