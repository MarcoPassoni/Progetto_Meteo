using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Progetto.Model;
using System.Net.Http.Json;
using System.Text.Json;

namespace Progetto.ModelView
{
    public partial class ModelViewDetails : ObservableObject
    {
        public delegate Task RemoveHandler(Locations loc);
        public event RemoveHandler Remove;

        private static HttpClient? client = new HttpClient();

        [ObservableProperty]
        public Locations location;

        [ObservableProperty]
        public CurrentWeather current;

        public ModelViewDetails(Locations location)
        {
            Location = location;
            SearchWeather(location);
        }


        private async void SearchWeather(Locations CurrentLocation)
        {
            //DateTime data = DateTime.Now;
            //https://api.open-meteo.com/v1/forecast?latitude=52.52&longitude=13.41&hourly=temperature_2m,temperature_975hPa,cloudcover_975hPa,windspeed_975hPa&daily=temperature_2m_max,temperature_2m_min,sunrise,sunset,precipitation_probability_max&timezone=auto&current_weather=true
            FormattableString tempUrl = $"https://api.open-meteo.com/v1/forecast?latitude={CurrentLocation.Latitude}&longitude={CurrentLocation.Longitude}&hourly=temperature_2m,temperature_975hPa,cloudcover_975hPa,windspeed_975hPa&daily=temperature_2m_max,temperature_2m_min,sunrise,sunset,precipitation_probability_max&timezone=auto&current_weather=true";
            var url = FormattableString.Invariant(tempUrl);

            var response = await client.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                return;
            }
            OpenMeteoForecast? forecast = await response.Content.ReadFromJsonAsync<OpenMeteoForecast>();
            if (forecast != null) Current = forecast.CurrentWeather;
        }

        [RelayCommand]
        public async void RemoveInPreferences()
        {
            await Remove(Location);
        }
    }
}
