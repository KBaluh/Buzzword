using System;
using System.Globalization;

namespace Buzzword.Common.Extensions
{
    /// <summary>
    /// Implements a (helper) class to convert date and time values.
    /// </summary>
    public static class DateTimeExtensions
    {
        public static bool IsEmpty(this DateTime dateTimeOffset)
        {
            return dateTimeOffset == DateTime.MinValue;
        }

        public static bool IsEmpty(this DateTime? dateTime)
        {
            return dateTime == null || IsEmpty(dateTime.Value);
        }

        public static bool IsNotEmpty(this DateTime dateTimeOffset)
        {
            return !IsEmpty(dateTimeOffset);
        }

        public static bool IsNotEmpty(this DateTime? dateTimeOffset)
        {
            return !IsEmpty(dateTimeOffset);
        }

        public static bool IsEmpty(this DateTimeOffset dateTimeOffset)
        {
            return dateTimeOffset == DateTimeOffset.MinValue;
        }

        public static bool IsEmpty(this DateTimeOffset? dateTime)
        {
            return dateTime == null || IsEmpty(dateTime.Value);
        }

        public static bool IsNotEmpty(this DateTimeOffset dateTimeOffset)
        {
            return !IsEmpty(dateTimeOffset);
        }

        public static bool IsNotEmpty(this DateTimeOffset? dateTimeOffset)
        {
            return !IsEmpty(dateTimeOffset);
        }

        /// <summary>
        /// Сравнивает только дату дд.мм.гггг в UTC формате
        /// </summary>
        /// <param name="date"></param>
        /// <param name="inputDate"></param>
        /// <returns></returns>
        public static bool IsEqualByDate(this DateTime date, DateTime inputDate)
        {
            return date.Year == inputDate.Year && date.Month == inputDate.Month && date.Day == inputDate.Day;
        }

        /// <summary>
        /// Сравнивает только дату дд.мм.гггг в UTC формате
        /// </summary>
        /// <param name="date"></param>
        /// <param name="inputDate"></param>
        /// <returns></returns>
        public static bool IsNotEqualByDate(this DateTime date, DateTime inputDate)
        {
            return !IsEqualByDate(date, inputDate);
        }

        /// <summary>
        /// Сравнивает только дату дд.мм.гггг в UTC формате
        /// </summary>
        /// <param name="date"></param>
        /// <param name="inputDate"></param>
        /// <returns></returns>
        public static bool IsEqualByDate(this DateTime? date, DateTime? inputDate)
        {
            if (!date.HasValue || !inputDate.HasValue)
            {
                return false;
            }

            return IsEqualByDate(date.Value, inputDate.Value);
        }

        /// <summary>
        /// Сравнивает только дату дд.мм.гггг в UTC формате
        /// </summary>
        /// <param name="date"></param>
        /// <param name="inputDate"></param>
        /// <returns></returns>
        public static bool IsNotEqualByDate(this DateTime? date, DateTime? inputDate)
        {
            return !IsEqualByDate(date, inputDate);
        }

        /// <summary>
        /// Сравнивает только дату дд.мм.гггг в UTC формате
        /// </summary>
        /// <param name="date"></param>
        /// <param name="inputDate"></param>
        /// <returns></returns>
        public static bool IsEqualByDate(this DateTimeOffset date, DateTimeOffset inputDate)
        {
            var utcSource = date.UtcDateTime;
            var utcInput = inputDate.UtcDateTime;

            return utcSource.Year == utcInput.Year && utcSource.Month == inputDate.Month && utcSource.Day == utcInput.Day;
        }

        /// <summary>
        /// Сравнивает только дату дд.мм.гггг в UTC формате
        /// </summary>
        /// <param name="date"></param>
        /// <param name="inputDate"></param>
        /// <returns></returns>
        public static bool IsNotEqualByDate(this DateTimeOffset date, DateTimeOffset inputDate)
        {
            return !IsEqualByDate(date, inputDate);
        }

        /// <summary>
        /// Сравнивает только дату дд.мм.гггг в UTC формате
        /// </summary>
        /// <param name="date"></param>
        /// <param name="inputDate"></param>
        /// <returns></returns>
        public static bool IsEqualByDate(this DateTimeOffset? date, DateTimeOffset? inputDate)
        {
            if (!date.HasValue || !inputDate.HasValue)
            {
                return false;
            }

            return IsEqualByDate(date.Value, inputDate.Value);
        }

        /// <summary>
        /// Сравнивает только дату дд.мм.гггг в UTC формате
        /// </summary>
        /// <param name="date"></param>
        /// <param name="inputDate"></param>
        /// <returns></returns>
        public static bool IsNotEqualByDate(this DateTimeOffset? date, DateTimeOffset? inputDate)
        {
            return !IsEqualByDate(date, inputDate);
        }

        /// <summary>
        /// Converts from DateTime to TimaSpan.
        /// </summary>
        /// <param name="dt">The source DateTime value.</param>
        /// <returns>Returns the time portion of DateTime in the form of TimeSpan if succeeded, null otherwise.</returns>
        public static TimeSpan? DateTimeToTimeSpan(this DateTime dt)
        {
            TimeSpan fResult;
            try
            {
                fResult = dt - dt.Date;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return null;
            }

            return fResult;
        }

        /// <summary>
        /// Converts from Timespan to DateTime.
        /// </summary>
        /// <param name="ts">The source TimeSpan value.</param>
        /// <returns>Returns a DateTime filled with date equals to mindate and time equals to time in timespan if succeeded, null otherwise.</returns>
        public static DateTime? TimeSpanToDateTime(this TimeSpan ts)
        {
            DateTime? fResult = null;
            try
            {
                string year = string.Format("{0:0000}", DateTime.MinValue.Date.Year);
                string month = string.Format("{0:00}", DateTime.MinValue.Date.Month);
                string day = string.Format("{0:00}", DateTime.MinValue.Date.Day);

                string hours = string.Format("{0:00}", ts.Hours);
                string minutes = string.Format("{0:00}", ts.Minutes);
                string seconds = string.Format("{0:00}", ts.Seconds);

                string dSep = "-"; string tSep = ":"; string dtSep = "T";

                // yyyy-mm-ddTHH:mm:ss
                string dtStr = string.Concat(year, dSep, month, dSep, day, dtSep, hours, tSep, minutes, tSep, seconds);

                if (DateTime.TryParseExact(dtStr, "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal, out DateTime dt))
                {
                    fResult = dt;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw;
            }

            return fResult;
        }

        /// <summary>
        /// Converts from DateTime to DateTimeOffSet.
        /// </summary>
        /// <param name="dt">The source DateTime to convert.</param>
        /// <returns>Returns a DateTimeOffSet if succeeded, null otherwise.</returns>
        public static DateTimeOffset? DateTimeToDateTimeOffSet(this DateTime dt)
        {
            return new DateTimeOffset(dt);
        }

        /// <summary>
        /// Converts from DateTimeOffSet to DateTime.
        /// </summary>
        /// <param name="dto">The source DateTimeOffSet to convert.</param>
        /// <returns>Returns a DateTime if succeeded, null otherwise.</returns>
        public static DateTime? DateTimeOffSetToDateTime(this DateTimeOffset dto)
        {
            return dto.DateTime;
        }

        /// <summary>
        /// Объеденить <see cref="DateTimeOffset"/> с <see cref="TimeSpan"/> для получения Даты с временем
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public static DateTimeOffset? CombineDateTimeOffsetWithTime(this DateTimeOffset? dto, TimeSpan time)
        {
            return dto?.CombineDateTimeOffsetWithTime(time);
        }

        public static DateTimeOffset CombineDateTimeOffsetWithTime(this DateTimeOffset dto, TimeSpan time)
        {
            DateTimeOffset dateTime = new DateTimeOffset(dto.Year, dto.Month, dto.Day,
                    time.Hours, time.Minutes, time.Seconds, dto.Offset);
            return dateTime;
        }

        public static TimeSpan GetTimeOfDayOrDefault(this DateTimeOffset? dto)
        {
            if (dto.HasValue)
            {
                return dto.Value.GetTimeOfDayOrDefault();
            }
            return default;
        }

        public static TimeSpan GetTimeOfDayOrDefault(this DateTimeOffset dto)
        {
            if (dto.TimeOfDay == TimeSpan.MinValue)
            {
                return default;
            }
            return dto.TimeOfDay;
        }

        /// <summary>
        /// Date format "dd.MM.yyyy"
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static string ToDateString(this DateTimeOffset? dto)
        {
            if (dto.HasValue)
            {
                return ToDateString(dto.Value);
            }
            return string.Empty;
        }

        /// <summary>
        /// Date format "dd.MM.yyyy"
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static string ToDateString(this DateTimeOffset dto)
        {
            return dto.ToString("dd.MM.yyyy");
        }

        /// <summary>
        /// DateTime format "dd.MM.yyyy HH:mm:ss"
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static string ToDateTimeString(this DateTimeOffset? dto)
        {
            if (dto.HasValue)
            {
                return ToDateTimeString(dto.Value);
            }
            return string.Empty;
        }

        /// <summary>
        /// DateTime format "dd.MM.yyyy HH:mm:ss"
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static string ToDateTimeString(this DateTimeOffset dto)
        {
            return dto.ToString("dd.MM.yyyy HH:mm:ss");
        }

        /// <summary>
        /// Date format "yyyy-MM-dd"
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static string ToSqlDateString(this DateTimeOffset dto)
        {
            return dto.ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// DateTime format "yyyy-MM-dd HH:mm:ss"
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static string ToSqlDateTimeString(this DateTimeOffset dto)
        {
            return dto.ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// DateTime format "yyy-MM-dd HH:mm:ss K"
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static string ToSqlDateTimeOffsetString(this DateTimeOffset dto)
        {
            return dto.ToString("yyy-MM-dd HH:mm:ss K");
        }

        /// <summary>
        /// DateTime format "yyy-MM-dd HH:mm:ss K"
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static string ToSqlDateTimeOffsetString(this DateTimeOffset? dto)
        {
            if (dto.HasValue)
            {
                return ToSqlDateTimeOffsetString(dto.Value);
            }
            return string.Empty;
        }

        public static DateTimeOffset GetBeginOfDay(this DateTimeOffset dto)
        {
            DateTime date = new DateTime(dto.Year, dto.Month, dto.Day, 0, 0, 0, 0);
            return new DateTimeOffset(date, dto.Offset);
        }

        public static DateTimeOffset GetEndOfDay(this DateTimeOffset dto)
        {
            DateTime date = new DateTime(dto.Year, dto.Month, dto.Day, 23, 59, 59);
            return new DateTimeOffset(date, dto.Offset);
        }

        public static DateTimeOffset GetBeginOfWeek(this DateTimeOffset dto)
        {
            if (dto.DayOfWeek == DayOfWeek.Monday)
            {
                return GetBeginOfDay(dto);
            }

            if (dto.DayOfWeek == DayOfWeek.Sunday)
            {
                return GetBeginOfDay(dto.AddDays(-7));
            }

            int dayOfWeekIndex = (int)dto.DayOfWeek;
            int mondayIndex = (int)DayOfWeek.Monday;
            int daysCount = mondayIndex - dayOfWeekIndex;

            return GetBeginOfDay(dto.AddDays(daysCount));
        }

        public static DateTimeOffset GetEndOfWeek(this DateTimeOffset dto)
        {
            if (dto.DayOfWeek == DayOfWeek.Sunday)
            {
                return GetEndOfDay(dto);
            }

            if (dto.DayOfWeek == DayOfWeek.Monday)
            {
                return GetEndOfDay(dto.AddDays(7));
            }

            int dayOfWeekIndex = (int)dto.DayOfWeek;
            int sundayIndex = 7;
            int daysCount = sundayIndex - dayOfWeekIndex;

            return GetEndOfDay(dto.AddDays(daysCount));
        }

        public static DateTimeOffset GetBeginOfMonth(this DateTimeOffset dto)
        {
            int firstDay = 1;
            int currentDay = dto.Day;
            int daysToMinus = firstDay - currentDay;

            return GetBeginOfDay(dto.AddDays(daysToMinus));
        }

        public static DateTimeOffset GetEndOfMonth(this DateTimeOffset dto)
        {
            int maxDays = DateTime.DaysInMonth(dto.Year, dto.Month);
            int currentDay = dto.Day;
            int daysToAdd = maxDays - currentDay;

            return GetEndOfDay(dto.AddDays(daysToAdd));
        }
    }
}
