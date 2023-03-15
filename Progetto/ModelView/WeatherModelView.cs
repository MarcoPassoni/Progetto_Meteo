using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Progetto.Model;
using Microsoft.Maui.Devices.Sensors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Text.Json;
using Progetto.View;
//https://api.open-meteo.com/v1/forecast?latitude=52.52&longitude=13.41&hourly=temperature_2m,temperature_975hPa,
//cloudcover_975hPa,windspeed_975hPa&daily=temperature_2m_max,temperature_2m_min,sunrise,sunset,precipitation_probability_max&timezone=auto
namespace Progetto.ModelView
{
    public partial class WeatherModelView : ObservableObject
    {
        static public HttpClient? client = new HttpClient();
        [ObservableProperty]
        public string text;
        public string city;

        [ObservableProperty]
        public string prova;

        [ObservableProperty]
        public double temperature;

        [ObservableProperty]
        public string coords;

        [RelayCommand]
        public async void SearchCity()
        {
            if (Text == null)
            {
                return;
            }
            city = Text;
            (double? lat, double? lon)? geo = await GeoCod(); //oggetto con latitudine e longitudine, tipo diz
            if (geo != null)
            {
                Coords = geo.Value.lat.ToString() + " " + geo.Value.lon.ToString();
            }
        }

        [RelayCommand]
        public void PlaceInPreferences()
        {
            Preferences.Set("città", city);
            Prova = city;
        }
        public async Task<(double? lat, double? lon)?> GeoCod()
        {
            string? cityUrlEncoded = HttpUtility.UrlEncode(city);
            string url = $"https://geocoding-api.open-meteo.com/v1/search?name={cityUrlEncoded}&language=it&count=1";
            HttpResponseMessage responseGeocoding = await client.GetAsync(url);
            if (responseGeocoding.IsSuccessStatusCode)
            {
                GeoCoding? geocodingResult = await responseGeocoding.Content.ReadFromJsonAsync<GeoCoding>();
                if (geocodingResult != null)
                {
                    return (geocodingResult.Results[0].Latitude, geocodingResult.Results[0].Longitude);
                }
            }
            return null;
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
            (double? lat, double? lon)? geo = await GeoCod();

            string urlAdd = $"https://api.open-meteo.com/v1/forecast?latitude={geo?.lat.ToString().Replace(',', '.')}&longitude={geo?.lon.ToString().Replace(',', '.')}&models=best_match&daily=weathercode,temperature_2m_max,temperature_2m_min,sunrise,sunset&timeformat=unixtime&forecast_days=3&timezone=Europe%2FBerlin";

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
