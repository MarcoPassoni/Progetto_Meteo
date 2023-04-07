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
using Microsoft.Maui.Devices.Sensors;
using System.Diagnostics;
using System.Net;

//https://api.open-meteo.com/v1/forecast?latitude=52.52&longitude=13.41&hourly=temperature_2m,temperature_975hPa,cloudcover_975hPa,windspeed_975hPa&daily=temperature_2m_max,temperature_2m_min,sunrise,sunset,precipitation_probability_max&timezone=auto
namespace Progetto.ModelView
{
    public partial class WeatherModelView : ObservableObject
    {
        static public HttpClient? client = new HttpClient();

        public ObservableCollection<Locations> PreferencesCities { get; set; } = new ObservableCollection<Locations>();

        [ObservableProperty]
        private Locations currentLocation;

        [ObservableProperty]
        private string text;

        [ObservableProperty]
        public bool state = false;

        [ObservableProperty]
        public bool thereIs = false;

        [ObservableProperty]
        private CurrentWeather current;

        

        public WeatherModelView()
        {
            Preferiti();
        }

        public void Preferiti()
        {
            string path = FileSystem.AppDataDirectory + "/preferencesCities.json";
            if (File.Exists(path))
            {
                PreferencesCities = JsonSerializer.Deserialize<ObservableCollection<Locations>>(File.ReadAllText(path));
            }
            ControlloPreferiti();
        }

        public void ControlloPreferiti()
        {
            if (PreferencesCities.Count == 0)
            {
                ThereIs = true;
                return;
            }
            ThereIs = false;
        }

        [RelayCommand]
        async Task GoToDetails(object loc) //passare il nome della città
        {
            if (loc == null || loc == default)
            {
                return;
            }
            CurrentLocation = (Locations)loc;
            ModelViewDetails viewDetails = new ModelViewDetails(CurrentLocation);
            viewDetails.Remove += RemoveInPreference;
            await App.Current.MainPage.Navigation.PushAsync(new GoToDetails(viewDetails));
        }

        [RelayCommand]
        async Task GoToDetailsWithoutObject()
        {
            if (CurrentLocation == null)
            {
                return;
            }
            ModelViewDetails viewDetails = new ModelViewDetails(CurrentLocation);
            viewDetails.Remove += RemoveInPreference;
            await App.Current.MainPage.Navigation.PushAsync(new GoToDetails(viewDetails));
        }

        #region GeoLocation

        [RelayCommand]
        async Task GetCurrentLocation()
        {
            try
            {
                // Get cached location, else get real location.
                var location = await Geolocation.Default.GetLastKnownLocationAsync();
                if (location == null)
                {
                    location = await Geolocation.Default.GetLocationAsync(new GeolocationRequest
                    {
                        DesiredAccuracy = GeolocationAccuracy.High,
                        Timeout = TimeSpan.FromSeconds(30)
                    });
                }
                IEnumerable<Placemark> places = await Geocoding.Default.GetPlacemarksAsync(location.Latitude,location.Longitude);
                var place = places.FirstOrDefault();
                CurrentLocation = new Locations { Latitude = location.Latitude, Longitude = location.Longitude, Name = place.Locality };
                SearchWeather(CurrentLocation);
                State = true;
                await Task.Delay(5000);
                State = false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to query location: {ex.Message}");
                await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
            }
        }

        #endregion

        [RelayCommand]
        public async void SearchCity()
        {
            if (Text == null || Text == string.Empty)
            {
                return;
            }
            await GeoCod(Text);
        }

        [RelayCommand]
        public async Task PlaceInPreferences()
        {
            if (PreferencesCities.Contains(CurrentLocation) || CurrentLocation == null || CurrentLocation == default)
            {
                return;
            }

            PreferencesCities.Add(CurrentLocation);
            string path = FileSystem.AppDataDirectory + "/preferencesCities.json";
            var json = JsonSerializer.Serialize(PreferencesCities);
            await File.WriteAllTextAsync(path, json);
            Preferiti();
        }

        private async Task RemoveInPreference(Locations loc)
        {
            PreferencesCities.Remove(loc);
            string path = FileSystem.AppDataDirectory + "/preferencesCities.json";
            var json = JsonSerializer.Serialize(PreferencesCities);
            await File.WriteAllTextAsync(path, json);
            Preferiti();
        }

        public async void SearchWeather(Locations CurrentLocation)
        {
            //DateTime data = DateTime.Now;

            FormattableString tempUrl =$"https://api.open-meteo.com/v1/forecast?latitude={CurrentLocation.Latitude}&longitude={CurrentLocation.Longitude}&hourly=temperature_2m,rain,showers,weathercode,pressure_msl,visibility,windspeed_10m&daily=weathercode,temperature_2m_max,temperature_2m_min,sunrise,sunset,rain_sum,showers_sum,precipitation_probability_max,windspeed_10m_max,windgusts_10m_max,winddirection_10m_dominant&current_weather=true&timeformat=unixtime&timezone=auto";
            var url = FormattableString.Invariant(tempUrl);

            var response = await client.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                return;
            }
            OpenMeteoForecast? forecast = await response.Content.ReadFromJsonAsync<OpenMeteoForecast>();
            if (forecast != null) 
            {
                Current = forecast.CurrentWeather;
            }
        }

        public async Task GeoCod(string city)
        {
            string? cityUrlEncoded = HttpUtility.UrlEncode(city);
            string url = $"https://geocoding-api.open-meteo.com/v1/search?name={cityUrlEncoded}&language=it&count=1";
            HttpResponseMessage responseGeocoding = await client.GetAsync(url);
            if (responseGeocoding.IsSuccessStatusCode)
            {
                GeoCoding? geocodingResult = await responseGeocoding.Content.ReadFromJsonAsync<GeoCoding>();
                if (geocodingResult != null && geocodingResult.Results != null)
                {
                    CurrentLocation = new Locations() { Name = geocodingResult.Results[0].Name, Latitude = geocodingResult.Results[0].Latitude, Longitude = geocodingResult.Results[0].Longitude };
                    State = true;
                    await Task.Delay(5000);
                    State = false;
                    //await Task.Delay(1000);
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
    }
}
