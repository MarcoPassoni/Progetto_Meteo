using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Progetto.Model
{
    public class Weather
    {
        // Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
        //https://api.open-meteo.com/v1/forecast?latitude=52.52&longitude=13.41&hourly=temperature_2m,temperature_975hPa,cloudcover_975hPa,windspeed_975hPa&daily=temperature_2m_max,temperature_2m_min,sunrise,sunset,precipitation_probability_max&timezone=auto
        public class Daily
        {
            [JsonPropertyName("time")]
            public List<string> Time { get; set; }

            [JsonPropertyName("temperature_2m_max")]
            public List<double?> Temperature2mMax { get; set; }

            [JsonPropertyName("temperature_2m_min")]
            public List<double?> Temperature2mMin { get; set; }

            [JsonPropertyName("sunrise")]
            public List<string> Sunrise { get; set; }

            [JsonPropertyName("sunset")]
            public List<string> Sunset { get; set; }

            [JsonPropertyName("precipitation_probability_max")]
            public List<int?> PrecipitationProbabilityMax { get; set; }
        }

        public class DailyUnits
        {
            [JsonPropertyName("time")]
            public string Time { get; set; }

            [JsonPropertyName("temperature_2m_max")]
            public string Temperature2mMax { get; set; }

            [JsonPropertyName("temperature_2m_min")]
            public string Temperature2mMin { get; set; }

            [JsonPropertyName("sunrise")]
            public string Sunrise { get; set; }

            [JsonPropertyName("sunset")]
            public string Sunset { get; set; }

            [JsonPropertyName("precipitation_probability_max")]
            public string PrecipitationProbabilityMax { get; set; }
        }

        public class Hourly
        {
            [JsonPropertyName("time")]
            public List<string> Time { get; set; }

            [JsonPropertyName("temperature_2m")]
            public List<double?> Temperature2m { get; set; }

            [JsonPropertyName("temperature_975hPa")]
            public List<double?> Temperature975hPa { get; set; }

            [JsonPropertyName("cloudcover_975hPa")]
            public List<int?> Cloudcover975hPa { get; set; }

            [JsonPropertyName("windspeed_975hPa")]
            public List<double?> Windspeed975hPa { get; set; }
        }

        public class HourlyUnits
        {
            [JsonPropertyName("time")]
            public string Time { get; set; }

            [JsonPropertyName("temperature_2m")]
            public string Temperature2m { get; set; }

            [JsonPropertyName("temperature_975hPa")]
            public string Temperature975hPa { get; set; }

            [JsonPropertyName("cloudcover_975hPa")]
            public string Cloudcover975hPa { get; set; }

            [JsonPropertyName("windspeed_975hPa")]
            public string Windspeed975hPa { get; set; }
        }

        public class Root
        {
            [JsonPropertyName("latitude")]
            public double? Latitude { get; set; }

            [JsonPropertyName("longitude")]
            public double? Longitude { get; set; }

            [JsonPropertyName("generationtime_ms")]
            public double? GenerationtimeMs { get; set; }

            [JsonPropertyName("utc_offset_seconds")]
            public int? UtcOffsetSeconds { get; set; }

            [JsonPropertyName("timezone")]
            public string Timezone { get; set; }

            [JsonPropertyName("timezone_abbreviation")]
            public string TimezoneAbbreviation { get; set; }

            [JsonPropertyName("elevation")]
            public int? Elevation { get; set; }

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
}
