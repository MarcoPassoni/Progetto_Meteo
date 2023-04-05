using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Progetto.Model
{
    public class Ore
    {

        public Ore()
        {
            TrovaTempo();
            IconPath = ViewWeatherIcon();
        }

        [JsonPropertyName("weather")]
        public string Weather { get; set; }
        [JsonPropertyName("time")]
        public DateTime Time { get; set; }
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
        public string IconPath { get; set; }    

        public void TrovaTempo()
        {
            int? code = Weathercode;
            Weather = code switch
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
        }
        public string ViewWeatherIcon()
        {
            int? code = Weathercode;
            string result = code switch
            {
                0 => "sole_pieno.png",
                1 => "sole_pieno.png",
                2 => "nuvole.png",
                3 => "nuvole.png",
                45 => "nebbia.png",
                48 => "nebbia.png",
                51 => "pioggerella.png",
                53 => "pioggia_pochissima.png",
                55 => "pioggia_poca",
                56 => "pioggia_neve.png",
                57 => "grandine.png",
                61 => "pioggia_poca",
                63 => "pioggia.png",
                65 => "acquazzone.png",
                66 => "pioggia_neve.png",
                67 => "pioggia_neve.png",
                71 => "nevicata_sottile.png",
                73 => "pioggia_neve.png",
                75 => "nevicata_intensa.png",
                77 => "grandine.png",
                80 => "pioggia_pochissima.png",
                81 => "pioggia.png",
                82 => "acquazzone.png",
                85 => "nevicata_sottile.png",
                86 => "nevicata_intensa.png",
                95 => "pioggia.png",
                96 => "grandine.png",
                99 => "tempesta.png",
                _ => string.Empty,
            };
            return result;
        }
    }
}
