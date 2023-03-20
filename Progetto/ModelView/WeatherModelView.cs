using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Progetto.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks.Dataflow;
using System.Web;
using System.Text.Json;
using Progetto.View;
using System.Collections.ObjectModel;
using System.Security.Cryptography;

//https://api.open-meteo.com/v1/forecast?latitude=52.52&longitude=13.41&hourly=temperature_2m,temperature_975hPa,cloudcover_975hPa,windspeed_975hPa&daily=temperature_2m_max,temperature_2m_min,sunrise,sunset,precipitation_probability_max&timezone=auto
namespace Progetto.ModelView
{
    public partial class WeatherModelView : ObservableObject
    {
        static public HttpClient? client = new HttpClient();

        public ObservableCollection<Locations> PreferencesCities { get; set; } = new ObservableCollection<Locations>();

        public string data1;

        public string tempMinima;

        public string tempMaxima;

        [ObservableProperty]
        public string città;

        [ObservableProperty]
        public Locations currentLocation;

        [ObservableProperty]
        public string text;
        public string city;

        [ObservableProperty]
        public Locations location;

        [ObservableProperty]
        public string prova;

        [ObservableProperty]
        public double temperature;

        [ObservableProperty]
        public string coords;

        public WeatherModelView()
        {
            string path = FileSystem.AppDataDirectory + "/preferencesCities.json";
            if (File.Exists(path))
            {
                PreferencesCities = JsonSerializer.Deserialize<ObservableCollection<Locations>>(File.ReadAllText(path));
            }
        }

        [RelayCommand]
        async Task GoToDetails(object loc) //passare il nome della città
        {
            if (loc == null || loc == default)
            {
                return;
            }
            CurrentLocation = (Locations)loc;
            SearchWeather(CurrentLocation);
            await App.Current.MainPage.Navigation.PushAsync(new GoToDetails((Locations)loc, data1, tempMinima, tempMaxima));
        }

        [RelayCommand]
        async Task GoToDetailsWithoutObject()
        {
            SearchWeather(Location);
            await App.Current.MainPage.Navigation.PushAsync(new GoToDetails(Location, data1, tempMinima, tempMaxima));
        }

        [RelayCommand]
        public async void SearchCity()
        {
            if (Text == null || Text == string.Empty)
            {
                return;
            }
            city = Text;
            await GeoCod();
        }

        [RelayCommand]
        public void PlaceInPreferences()
        {
            if (PreferencesCities.Contains(Location))
            {
                return;
            }
            PreferencesCities.Add(Location);
            string path = FileSystem.AppDataDirectory + "/preferencesCities.json";
            var json = JsonSerializer.Serialize(PreferencesCities);
            File.WriteAllText(path, json);
        }

        public async void SearchWeather(Locations CurrentLocation)
        {
            DateTime data = DateTime.Now;
            //https://api.open-meteo.com/v1/forecast?latitude=52.52&longitude=13.41&hourly=temperature_2m,temperature_975hPa,cloudcover_975hPa,windspeed_975hPa&daily=temperature_2m_max,temperature_2m_min,sunrise,sunset,precipitation_probability_max&timezone=auto
            string url = $"https://api.open-meteo.com/v1/forecast?latitude={CurrentLocation.Latitude}&longitude={CurrentLocation.Longitude}&hourly=temperature_2m,temperature_975hPa,cloudcover_975hPa,windspeed_975hPa&daily=temperature_2m_max,temperature_2m_min,sunrise,sunset,precipitation_probability_max&timezone=auto";
            var response = await client.GetAsync($"{url}");
            if (!response.IsSuccessStatusCode)
            {
                return;
            }
            OpenMeteoForecast? forecast = await response.Content.ReadFromJsonAsync<OpenMeteoForecast>();
            if (forecast != null)
            {
                if (forecast.Daily != null)
                {
                    for (int i = 0; i < forecast.Daily.Temperature2mMin.Count; i++)
                    {

                        data1 = UnixTimeStampToDateTime(forecast.Daily.Time[i]).ToString();
                        tempMinima = forecast.Daily.Temperature2mMin[i].GetValueOrDefault().ToString();
                        tempMaxima = forecast.Daily.Temperature2mMax[i].GetValueOrDefault().ToString().ToString();
                    }
                }
            }
        }

        public async Task GeoCod()
        {
            string? cityUrlEncoded = HttpUtility.UrlEncode(city);
            string url = $"https://geocoding-api.open-meteo.com/v1/search?name={cityUrlEncoded}&language=it&count=1";
            HttpResponseMessage responseGeocoding = await client.GetAsync(url);
            if (responseGeocoding.IsSuccessStatusCode)
            {
                GeoCoding? geocodingResult = await responseGeocoding.Content.ReadFromJsonAsync<GeoCoding>();
                if (geocodingResult != null)
                {
                    Location = new Locations() { Name = geocodingResult.Results[0].Name, Latitude = geocodingResult.Results[0].Latitude, Longitude = geocodingResult.Results[0].Longitude};
                    Città = Location.Name;
                    currentLocation= Location;
                }
            }
        }
        public DateTime? UnixTimeStampToDateTime(double? unixTimeStamp)
        {
            if (unixTimeStamp != null)
            {
                DateTime dateTime = new(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                dateTime = dateTime.AddSeconds((double)unixTimeStamp).ToLocalTime();
                return dateTime;
            }
            return null;
        }   
        public async Task PrevisioniOpenGeoCoding()
        {
            await GeoCod();
            string urlAdd = $"https://api.open-meteo.com/v1/forecast?latitude={Location?.Latitude.ToString().Replace(',', '.')}&longitude={Location?.Longitude.ToString().Replace(',', '.')}&models=best_match&daily=weathercode,temperature_2m_max,temperature_2m_min,sunrise,sunset&timeformat=unixtime&forecast_days=3&timezone=Europe%2FBerlin";

            var response = await client.GetAsync($"{urlAdd}");
            {
                if (response.IsSuccessStatusCode)
                {
                    ForecastDaily? forecastDaily = await response.Content.ReadFromJsonAsync<ForecastDaily>();
                    JsonSerializerOptions options = new(JsonSerializerDefaults.Web) { WriteIndented = true };
                    if (forecastDaily != null)
                    {
                        var fd = forecastDaily.Daily;
                    }
                }
            }
        }
    }
}
