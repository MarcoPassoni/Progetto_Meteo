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
    }
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


        private DateTime UnixTimeStampToDateTime(int unixTimeStamp)
        {
            DateTime dateTime = new(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dateTime;
        }

    }
}