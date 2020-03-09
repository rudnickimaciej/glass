using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Translate.ViewModels.Helpers
{

    public static class DateTimeExtension
    {
       
        public static DateTime RandomDate(DateTime? start = null, DateTime? end = null)
        {
            if (start.HasValue && end.HasValue && start.Value >= end.Value)
                throw new Exception("start date must be less than end date!");

            DateTime min = start ?? DateTime.MinValue;
            DateTime max = end ?? DateTime.MaxValue;

            // for timespan approach see: http://stackoverflow.com/q/1483670/1698987
            TimeSpan timeSpan = max - min;

            // for random long see: http://stackoverflow.com/a/677384/1698987
            byte[] bytes = new byte[8];
            Random random = new Random();
            random.NextBytes(bytes);

            long int64 = Math.Abs(BitConverter.ToInt64(bytes, 0)) % timeSpan.Ticks;

            TimeSpan newSpan = new TimeSpan(int64);

            return min + newSpan;
        }
    
    }
    public static class DateFormatHelper
    {

        public static string FormatDate(DateTime date)
        {
            TimeSpan s = DateTime.Now.Subtract(date);
            int dayDiff = (int)s.TotalDays;
            int secDiff = (int)s.TotalSeconds;

            if (dayDiff < 0 || dayDiff >= 31)
            {
                return null;
            }
            if (dayDiff == 0)
            {
                // A.
                // Less than one minute ago.
                if (secDiff < 60)
                {
                    return "przed chwilą";
                }
                // B.
                // Less than 2 minutes ago.
                if (secDiff < 120)
                {
                    return "minutę temu";
                }
                // C.
                // Less than one hour ago.
                if (secDiff < 3600)
                {
                    return string.Format("{0} minut temu",
                        Math.Floor((double)secDiff / 60));
                }
                // D.
                // Less than 2 hours ago.
                if (secDiff < 7200)
                {
                    return "1 godzinę temu";
                }
                // E.
                // Less than one day ago.
                if (secDiff < 86400)
                {
                    return string.Format("{0} godzin temu",
                        Math.Floor((double)secDiff / 3600));
                }
            }
            // 6.
            // Handle previous days.
            if (dayDiff == 1)
            {
                return "wczoraj";
            }
            if (dayDiff < 7)
            {
                return string.Format("{0} dni temu",
                    dayDiff);
            }
            if (dayDiff < 31)
            {
                return string.Format("{0} tygodni temu",
                    Math.Ceiling((double)dayDiff / 7));
            }
            return null;
        }
    }
 }
