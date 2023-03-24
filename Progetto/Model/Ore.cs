using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Progetto.Model
{
    public class Ore
    {
        [JsonPropertyName("time")]
        public int Time { get; set; }
        [JsonPropertyName("temperature_2m")]
        public double Temperature2m { get; set; }
        [JsonPropertyName("rain")]
        public double Rain { get; set; }
        [JsonPropertyName("showers")]
        public double Showers { get; set; }
        [JsonPropertyName("weathercode")]
        public int Weathercode { get; set; }
        [JsonPropertyName("pressure_msl")]
        public double PressureMsl { get; set; }
        [JsonPropertyName("visibility")]
        public double Visibility { get; set; }
        [JsonPropertyName("windspeed_10m")]
        public double Windspeed10m { get; set; }
    }
}
