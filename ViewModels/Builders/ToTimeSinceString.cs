using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Translate.Extensions
{
    public static class Extensions
    {


        public static string ToTimeSinceString(this DateTime value)
        {
            const int SECOND = 1;
            const int MINUTE = 60 * SECOND;
            const int HOUR = 60 * MINUTE;
            const int DAY = 24 * HOUR;
            const int MONTH = 30 * DAY;

            TimeSpan ts = new TimeSpan(DateTime.Now.Ticks - value.Ticks);
            double seconds = ts.TotalSeconds;

            // Less than one minute
            if (seconds < 1 * MINUTE)
                return ts.Seconds == 1 ? "1sek" : ts.Seconds + "sek";

            if (seconds < 60 * MINUTE)
                return ts.Minutes + "min";

            if (seconds < 120 * MINUTE)
                return "1h";

            if (seconds < 24 * HOUR)
                return ts.Hours + "h";

            if (seconds < 48 * HOUR)
                return "wczoraj";

            if (seconds < 30 * DAY)
                return ts.Days + "d";

            if (seconds < 12 * MONTH)
            {
                int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                return months <= 1 ? "one month ago" : months + " months ago";
            }

            int years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
            return years <= 1 ? "one year ago" : years + " years ago";
        }
    }
}