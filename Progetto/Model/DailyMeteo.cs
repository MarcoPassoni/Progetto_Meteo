using Progetto.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Progetto.Model
{
    public class DailyMeteo
    {
        public DailyMeteo(int time, int weathercode, double temperature2mMax, double temperature2mMin, DateTime sunrise, DateTime sunset, double rainSum, double showersSum, int precipitationProbabilityMax, double windspeed10mMax, double windgusts10mMin, int winddirection10mDominant, List<Ore> hourly)
        {
            Time = UnixTimeStampToDateTime(time);
            Weathercode = weathercode;
            Temperature2mMax = temperature2mMax;
            Temperature2mMin = temperature2mMin;
            Sunrise = sunrise;
            Sunset = sunset;
            RainSum = rainSum;
            ShowersSum = showersSum;
            PrecipitationProbabilityMax = precipitationProbabilityMax;
            Windspeed10mMax = windspeed10mMax;
            Windgusts10mMin = windgusts10mMin;
            Winddirection10mDominant = winddirection10mDominant;
            hourlies = hourly;
            TrovaTempo();
            iconPath = ViewWeatherIcon();
        }
        public string iconPath { get; set; }
        public string Weather { get; set; }
        public List<Ore> hourlies { get; set; }
        public DateTime Time { get; set; }
        public int Weathercode { get; set; }
        public double Temperature2mMax { get; set; }
        public double Temperature2mMin { get; set; }
        public DateTime Sunrise { get; set; }
        public DateTime Sunset { get; set; }
        public double RainSum { get; set; }
        public double ShowersSum { get; set; }
        public int PrecipitationProbabilityMax { get; set; }
        public double Windspeed10mMax { get; set; }
        public double Windgusts10mMin { get; set; }
        public int Winddirection10mDominant { get; set; }

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
        private DateTime UnixTimeStampToDateTime(int unixTimeStamp)
        {
            DateTime dateTime = new(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dateTime;
        }

    }
}