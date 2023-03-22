using Java.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Progetto.Model
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
    public class CurrentWeather
    {
        [JsonPropertyName("temperature")]
        public double Temperature { get; set; }

        [JsonPropertyName("windspeed")]
        public double Windspeed { get; set; }

        [JsonPropertyName("winddirection")]
        public double Winddirection { get; set; }

        [JsonPropertyName("weathercode")]
        public int Weathercode { get; set; }

        [JsonPropertyName("time")]
        public int Time { get; set; }
    }

    public class Daily
    {
        [JsonPropertyName("time")]
        public List<int> Time { get; set; }

        [JsonPropertyName("weathercode")]
        public List<int> Weathercode { get; set; }

        [JsonPropertyName("temperature_2m_max")]
        public List<double> Temperature2mMax { get; set; }

        [JsonPropertyName("temperature_2m_min")]
        public List<double> Temperature2mMin { get; set; }

        [JsonPropertyName("sunrise")]
        public List<long> Sunrise { get; set; }

        [JsonPropertyName("sunset")]
        public List<long> Sunset { get; set; }

        [JsonPropertyName("rain_sum")]
        public List<double> RainSum { get; set; }

        [JsonPropertyName("showers_sum")]
        public List<double> ShowersSum { get; set; }

        [JsonPropertyName("precipitation_probability_max")]
        public List<int> PrecipitationProbabilityMax { get; set; }

        [JsonPropertyName("windspeed_10m_max")]
        public List<double> Windspeed10mMax { get; set; }

        [JsonPropertyName("windgusts_10m_max")]
        public List<double> Windgusts10mMax { get; set; }

        [JsonPropertyName("winddirection_10m_dominant")]
        public List<int> Winddirection10mDominant { get; set; }
    }

    public class DailyUnits
    {
        [JsonPropertyName("time")]
        public string Time { get; set; }

        [JsonPropertyName("weathercode")]
        public string Weathercode { get; set; }

        [JsonPropertyName("temperature_2m_max")]
        public string Temperature2mMax { get; set; }

        [JsonPropertyName("temperature_2m_min")]
        public string Temperature2mMin { get; set; }

        [JsonPropertyName("sunrise")]
        public string Sunrise { get; set; }

        [JsonPropertyName("sunset")]
        public string Sunset { get; set; }

        [JsonPropertyName("rain_sum")]
        public string RainSum { get; set; }

        [JsonPropertyName("showers_sum")]
        public string ShowersSum { get; set; }

        [JsonPropertyName("precipitation_probability_max")]
        public string PrecipitationProbabilityMax { get; set; }

        [JsonPropertyName("windspeed_10m_max")]
        public string Windspeed10mMax { get; set; }

        [JsonPropertyName("windgusts_10m_max")]
        public string Windgusts10mMax { get; set; }

        [JsonPropertyName("winddirection_10m_dominant")]
        public string Winddirection10mDominant { get; set; }
    }

    public class Hourly
    {
        [JsonPropertyName("time")]
        public List<int> Time { get; set; }

        [JsonPropertyName("temperature_2m")]
        public List<double> Temperature2m { get; set; }

        [JsonPropertyName("rain")]
        public List<double> Rain { get; set; }

        [JsonPropertyName("showers")]
        public List<double> Showers { get; set; }

        [JsonPropertyName("weathercode")]
        public List<int> Weathercode { get; set; }

        [JsonPropertyName("pressure_msl")]
        public List<double> PressureMsl { get; set; }

        [JsonPropertyName("visibility")]
        public List<double> Visibility { get; set; }

        [JsonPropertyName("windspeed_10m")]
        public List<double> Windspeed10m { get; set; }
    }

    public class HourlyUnits
    {
        [JsonPropertyName("time")]
        public string Time { get; set; }

        [JsonPropertyName("temperature_2m")]
        public string Temperature2m { get; set; }

        [JsonPropertyName("rain")]
        public string Rain { get; set; }

        [JsonPropertyName("showers")]
        public string Showers { get; set; }

        [JsonPropertyName("weathercode")]
        public string Weathercode { get; set; }

        [JsonPropertyName("pressure_msl")]
        public string PressureMsl { get; set; }

        [JsonPropertyName("visibility")]
        public string Visibility { get; set; }

        [JsonPropertyName("windspeed_10m")]
        public string Windspeed10m { get; set; }
    }

    public class OpenMeteoForecast
    {
        [JsonPropertyName("latitude")]
        public double Latitude { get; set; }

        [JsonPropertyName("longitude")]
        public double Longitude { get; set; }

        [JsonPropertyName("generationtime_ms")]
        public double GenerationtimeMs { get; set; }

        [JsonPropertyName("utc_offset_seconds")]
        public int UtcOffsetSeconds { get; set; }

        [JsonPropertyName("timezone")]
        public string Timezone { get; set; }

        [JsonPropertyName("timezone_abbreviation")]
        public string TimezoneAbbreviation { get; set; }

        [JsonPropertyName("elevation")]
        public double Elevation { get; set; }

        [JsonPropertyName("current_weather")]
        public CurrentWeather CurrentWeather { get; set; }

        [JsonPropertyName("hourly_units")]
        public HourlyUnits HourlyUnits { get; set; }

        [JsonPropertyName("hourly")]
        public Hourly Hourly { get; set; }

        [JsonPropertyName("daily_units")]
        public DailyUnits DailyUnits { get; set; }

        [JsonPropertyName("daily")]
        public Daily Daily { get; set; }
    }


}
