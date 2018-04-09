using System;

namespace JARASOFT.CMDB.Core.Extensions
{
    public static class DateTimeExtensions
    {
        public static string CustomLongFormat(this DateTime datetime, TimeZoneInfo timeZone)
        {
            return string.Format("Date: {0} Time: {1} Time Zone: ({2}{3})",
                                    datetime.ToShortDateString(),
                                    datetime.ToShortTimeString(),
                                    TimeZoneUTC(timeZone),
                                    timeZone.IsDaylightSavingTime(datetime) ? " DST" : string.Empty);
        }

        public static string CustomShortFormat(this DateTime datetime, TimeZoneInfo timeZone)
        {
            return string.Format("{0} ({1}{2})",
                                    datetime.ToShortTimeString(),
                                    TimeZoneUTC(timeZone),
                                    timeZone.IsDaylightSavingTime(datetime) ? " DST" : string.Empty);
        }

        public static string TimeZoneUTC(TimeZoneInfo timeZone)
        {
            if (timeZone.Id.Equals("UTC"))
            {
                return "UTC";
            }
            return string.Format("UTC {0}{1:D2}:{2:D2}",
                (timeZone.BaseUtcOffset >= TimeSpan.Zero) ? "+" : "-",
                Math.Abs(timeZone.BaseUtcOffset.Hours),
                Math.Abs(timeZone.BaseUtcOffset.Minutes));
        }

        public static DateTime ConvertToUserTimeZone(this DateTime datetime, TimeZoneInfo zone)
        {
            if (datetime == DateTime.MinValue) return DateTime.MinValue;
            if (datetime == DateTime.MaxValue) return DateTime.MaxValue;

            var datetime2 = DateTime.SpecifyKind(datetime, DateTimeKind.Utc);

            return TimeZoneInfo.ConvertTimeToUtc(datetime2, zone);
        }

        public static DateTime ConvertToUserTimeZone(this DateTime datetime, string zoneId)
        {
            if (datetime == DateTime.MinValue) return DateTime.MinValue;
            if (datetime == DateTime.MaxValue) return DateTime.MaxValue;

            var tz = TimeZoneInfo.FindSystemTimeZoneById(zoneId);

            return ConvertToUserTimeZone(datetime, tz);
        }

        public static DateTime ConvertToUTCFromSpecificTimeZone(this DateTime datetime, string zoneId)
        {
            TimeZoneInfo userTimeZone = TimeZoneInfo.Utc;
            if (zoneId != null && zoneId != "undefined")
            {
                userTimeZone = TimeZoneInfo.FindSystemTimeZoneById(zoneId);
            }

            return datetime.ConvertToUTCFromSpecificTimeZone(userTimeZone);
        }

        public static DateTime ConvertToUTCFromSpecificTimeZone(this DateTime datetime, TimeZoneInfo timeZone)
        {
            if (datetime == DateTime.MinValue) return DateTime.MinValue;
            if (datetime == DateTime.MaxValue) return DateTime.MaxValue;

            return TimeZoneInfo.ConvertTimeToUtc(DateTime.SpecifyKind(datetime, DateTimeKind.Unspecified), timeZone);
        }

        public static double GetOffSet(string TimeZone)
        {
            var timeZone = TimeZoneInfo.FindSystemTimeZoneById(TimeZone);

            var datetime = DateTime.UtcNow.ConvertToUserTimeZone(timeZone);

            var offset = timeZone.GetUtcOffset(datetime);

            return offset.TotalHours;
        }

        public static string InternalNameWithDST(string internalName, string TimeZone)
        {
            string newInternalName = string.Empty;

            if (internalName.Trim().Length > 0)
            {
                int p1 = internalName.IndexOf(")");
                string IN_firstPart = internalName.Substring(0, p1); // (UTC-05:00
                string IN_secondPart = internalName.Substring(p1, internalName.Length - p1); // Eastern Time (US & Canada)

                TimeZoneInfo timeZone = TimeZoneInfo.FindSystemTimeZoneById(TimeZone);
                var time = DateTime.UtcNow.ConvertToUserTimeZone(timeZone);

                newInternalName = timeZone.IsDaylightSavingTime(time)
                    ? IN_firstPart + " DST" + IN_secondPart
                    : IN_firstPart + IN_secondPart;
            }

            return newInternalName;
        }
    }
}
