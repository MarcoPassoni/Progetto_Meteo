using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Progetto.Model;
using Microsoft.Maui.Devices.Sensors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks.Dataflow;
using System.Web;
using System.Text.Json;
using Progetto.View;
using Org.Xml.Sax.Helpers;
using System.Collections.ObjectModel;
using Xamarin.Google.Crypto.Tink.Mac;
using Android.Content.Res;
//https://api.open-meteo.com/v1/forecast?latitude=52.52&longitude=13.41&hourly=temperature_2m,temperature_975hPa,
//cloudcover_975hPa,windspeed_975hPa&daily=temperature_2m_max,temperature_2m_min,sunrise,sunset,precipitation_probability_max&timezone=auto
namespace Progetto.ModelView
{
    public partial class WeatherModelView : ObservableObject
    {
        static public HttpClient? client = new HttpClient();

        public ObservableCollection<Locations> PreferencesCities { get; set; } = new ObservableCollection<Locations>();

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
        async Task GoToDetails()
        {
            await App.Current.MainPage.Navigation.PushAsync(new GoToDetails());
        }

        [RelayCommand]
        public async void SearchCity()
        {
            if (Text == null)
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
                        //Temperature = fd.Temperature2mMin[0].GetValueOrDefault();
                        //for (int i = 0; i < fd.Temperature2mMin.Count; i++)
                        //{
                        //    Console.WriteLine(fd.Temperature2mMin[i].GetValueOrDefault());
                        //    Console.WriteLine(fd.Temperature2mMax[i].GetValueOrDefault());
                        //    Console.WriteLine(UnixTimeStampToDateTime(fd.Sunrise[i].GetValueOrDefault()));
                        //    Console.WriteLine(fd.Sunset[i].GetValueOrDefault());
                        //}
                    }
                }
            }
        }
    }
}
