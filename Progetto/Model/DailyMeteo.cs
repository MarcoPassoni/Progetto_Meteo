using Progetto.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Progetto.Model
{
    public class DailyMeteo
    {
        public DailyMeteo(int time, int weathercode, double temperature2mMax, double temperature2mMin, long sunrise, long sunset, double rainSum, double showersSum, int precipitationProbabilityMax, double windspeed10mMax, double windgusts10mMax, int winddirection10mDominant)
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
            Windgusts10mMax = windgusts10mMax;
            Winddirection10mDominant = winddirection10mDominant;
        }

        public DateTime Time { get; set; }
        public int Weathercode { get; set; }
        public double Temperature2mMax { get; set; }
        public double Temperature2mMin { get; set; }
        public long Sunrise { get; set; }
        public long Sunset { get; set; }
        public double RainSum { get; set; }
        public double ShowersSum { get; set; }
        public int PrecipitationProbabilityMax { get; set; }
        public double Windspeed10mMax { get; set; }
        public double Windgusts10mMax { get; set; }
        public int Winddirection10mDominant { get; set; }

        private DateTime UnixTimeStampToDateTime(int unixTimeStamp)
        {
            DateTime dateTime = new(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dateTime;
        }

    }
}