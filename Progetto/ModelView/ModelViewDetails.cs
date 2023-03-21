using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using Progetto.Model;
using System;
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

        [ObservableProperty]
        public OpenMeteoForecast meteoForecast;

        [ObservableProperty]
        public string weather;
        public ModelViewDetails(Locations location)
        {
            Task.Run(async () =>
            {
                Location = location;
                await SearchWeather(location);
                ViewWeather();
            });
        }


        private async Task SearchWeather(Locations CurrentLocation)
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
            MeteoForecast = await response.Content.ReadFromJsonAsync<OpenMeteoForecast>();
            if (MeteoForecast != null) Current = MeteoForecast.CurrentWeather;
        }

        [RelayCommand]
        public async void RemoveInPreferences()
        {
            await Remove(Location);
        }

        [RelayCommand]
        public void ViewWeather()
        {
            int code = MeteoForecast.CurrentWeather.Weathercode;
            string result = code switch
            {
                0 => "cielo sereno",
                1 => "prevalentemente limpido",
                2 => "parzialmente nuvoloso",
                3 => "coperto",
                45 => "nebbia",
                48 => "nebbia con brina",
                51 => "pioggerellina di scarsa intensità",
                53 => "pioggerellina di moderata intensità",
                55 => "pioggerellina intensa",
                56 => "pioggerellina gelata di scarsa intensità",
                57 => "pioggerellina gelata intensa",
                61 => "pioggia di scarsa intensità",
                63 => "pioggia di moderata intensità",
                65 => "pioggia molto intensa",
                66 => "pioggia gelata di scarsa intensità",
                67 => "pioggia gelata intensa",
                71 => "venicata di lieve entità",
                73 => "nevicata di media entità",
                75 => "nevicata intensa",
                77 => "granelli di neve",
                80 => "deboli rovesci di pioggia",
                81 => "moderati rovesci di pioggia",
                82 => "violenti rovesci di pioggia",
                85 => "leggeri rovesci di neve",
                86 => "pesanti rovesci di neve",
                95 => "temporale lieve o moderato",
                96 => "temporale con lieve grandine",
                99 => "temporale con forte grandine",
                _ => string.Empty,
            };
            Weather = result;
        }
    }
}
