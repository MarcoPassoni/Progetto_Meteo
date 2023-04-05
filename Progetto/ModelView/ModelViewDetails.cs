using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.PlatformConfiguration.TizenSpecific;
using Progetto.Model;
using Progetto.View;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace Progetto.ModelView
{
    public partial class ModelViewDetails : ObservableObject
    {
        public delegate Task RemoveHandler(Locations loc);
        public event RemoveHandler Remove;

        private static HttpClient client = new HttpClient();

        [ObservableProperty]
        public Locations location;

        [ObservableProperty]
        public string path;

        [ObservableProperty]
        public CurrentWeather current;

        [ObservableProperty]
        public OpenMeteoForecast meteoForecast;

        public ObservableCollection<DailyMeteo> DailyMeteo { get; set; } = new ObservableCollection<DailyMeteo>(); //creo qui il DailyMeteo

        [ObservableProperty]
        public DailyMeteo clicked;

        [ObservableProperty]
        public Ore clickedHour;

        [ObservableProperty]
        public string weather;


        public ModelViewDetails(Locations location)
        {

            Task.Run(async () =>
            {
                Location = location;
                await SearchWeather(location);
                ViewWeather();
                CreationVariable();
            }).Wait();
        }

        public DateTime UnixTimeStampToDateTime(double? unixTimeStamp)
        {
            if (unixTimeStamp != null)
            {
                DateTime dateTime = new(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                dateTime = dateTime.AddSeconds((double)unixTimeStamp).ToLocalTime();
                return dateTime;
            }
            return new DateTime();
        }

        #region View
        [RelayCommand]
        public async void ViewTheDay(object obj)
        {
            if (obj == null || !(obj is DailyMeteo)) return;
            Clicked = (DailyMeteo)obj;
            App.Current.MainPage.Navigation.RemovePage(App.Current.MainPage);
            await App.Current.MainPage.Navigation.PushAsync(new ViewTheDay(this));
        }

        [RelayCommand]
        public async void ViewTheHour(object obj)
        {
            if (obj == null || !(obj is Ore)) return;
            ClickedHour = (Ore)obj;
            await App.Current.MainPage.Navigation.PushAsync(new ViewTheHour(this));
        }

        [RelayCommand]
        public async void ViewWeatherForHour(object obj)
        {
            if (obj == null || !(obj is DailyMeteo)) return;
            Clicked = (DailyMeteo)obj;
            await App.Current.MainPage.Navigation.PushAsync(new ViewWeatherForHour(this));
        }

        [RelayCommand]
        public async void ViewTheWeek()
        {
            await App.Current.MainPage.Navigation.PushAsync(new ViewTheWeek(this));
        }

        [RelayCommand]
        public void ViewWeather()
        {
            int? code = MeteoForecast.CurrentWeather.Weathercode;
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
        #endregion

        

        public void CreationVariable()
        {
            int j = 0;
            for (int i = 0; i < MeteoForecast.Daily.Sunrise.Count; i++)
            {
                List<Ore> oreList = new List<Ore>(24);
                int k = j + 24;
                for (; j < k; j++)
                {
                    oreList.Add(new Ore() { PressureMsl = meteoForecast.Hourly.PressureMsl[j], Rain = meteoForecast.Hourly.Rain[j], Showers = meteoForecast.Hourly.Showers[j], Temperature2m = meteoForecast.Hourly.Temperature2m[j], Time = UnixTimeStampToDateTime(meteoForecast.Hourly.Time[j]), Visibility = meteoForecast.Hourly.Visibility[j], Weathercode = meteoForecast.Hourly.Weathercode[j], Windspeed10m = meteoForecast.Hourly.Windspeed10m[j]});
                }
                DailyMeteo.Add(new DailyMeteo(MeteoForecast.Daily.Time[i], MeteoForecast.Daily.Weathercode[i], MeteoForecast.Daily.Temperature2mMax[i], MeteoForecast.Daily.Temperature2mMin[i], UnixTimeStampToDateTime(MeteoForecast.Daily.Sunrise[i]), UnixTimeStampToDateTime(MeteoForecast.Daily.Sunset[i]), MeteoForecast.Daily.RainSum[i], MeteoForecast.Daily.ShowersSum[i], MeteoForecast.Daily.PrecipitationProbabilityMax[i], MeteoForecast.Daily.Windspeed10mMax[i], MeteoForecast.Daily.Windgusts10mMax[i], MeteoForecast.Daily.Winddirection10mDominant[i], oreList));
            }
        }

        private async Task SearchWeather(Locations CurrentLocation)
        {
            //DateTime data = DateTime.Now;
            //https://api.open-meteo.com/v1/forecast?latitude=52.52&longitude=13.41&hourly=temperature_2m,temperature_975hPa,cloudcover_975hPa,windspeed_975hPa&daily=temperature_2m_max,temperature_2m_min,sunrise,sunset,precipitation_probability_max&timezone=auto&current_weather=true
            FormattableString tempUrl = $"https://api.open-meteo.com/v1/forecast?latitude={CurrentLocation.Latitude}&longitude={CurrentLocation.Longitude}&hourly=temperature_2m,rain,showers,weathercode,pressure_msl,visibility,windspeed_10m&daily=weathercode,temperature_2m_max,temperature_2m_min,sunrise,sunset,rain_sum,showers_sum,precipitation_probability_max,windspeed_10m_max,windgusts_10m_max,winddirection_10m_dominant&current_weather=true&timeformat=unixtime&timezone=auto";
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
    }
}
