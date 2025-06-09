namespace MeraStore.Shared.Kernel.Helpers;

public static class DateTimeHelper
{
    /// <summary>
    /// Converts the DateTime to UTC. Assumes Unspecified is Local.
    /// </summary>
    public static DateTime ToUtcSafe(this DateTime dt) => dt.Kind switch
    {
        DateTimeKind.Utc => dt,
        DateTimeKind.Unspecified => DateTime.SpecifyKind(dt, DateTimeKind.Local).ToUniversalTime(),
        _ => dt.ToUniversalTime()
    };

    /// <summary>
    /// Converts the DateTime to Local. Assumes Unspecified is UTC.
    /// </summary>
    public static DateTime ToLocalSafe(this DateTime dt) => dt.Kind switch
    {
        DateTimeKind.Local => dt,
        DateTimeKind.Unspecified => DateTime.SpecifyKind(dt, DateTimeKind.Utc).ToLocalTime(),
        _ => dt.ToLocalTime()
    };

    /// <summary>
    /// Rounds the DateTime to the nearest time span.
    /// </summary>
    public static DateTime RoundTo(this DateTime dt, TimeSpan roundTo)
    {
        if (roundTo <= TimeSpan.Zero)
            throw new ArgumentException("Round interval must be greater than zero.", nameof(roundTo));

        var ticks = (dt.Ticks + (roundTo.Ticks / 2)) / roundTo.Ticks;
        return new DateTime(ticks * roundTo.Ticks, dt.Kind);
    }

    /// <summary>
    /// Converts DateTime to ISO 8601 format in UTC.
    /// </summary>
    public static string ToIso8601(this DateTime dt) =>
        dt.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture);

    /// <summary>
    /// Gets the start of the day (00:00:00) for the DateTime.
    /// </summary>
    public static DateTime StartOfDay(this DateTime dt) =>
        new(dt.Year, dt.Month, dt.Day, 0, 0, 0, dt.Kind);

    /// <summary>
    /// Gets the end of the day (23:59:59.999) for the DateTime.
    /// </summary>
    public static DateTime EndOfDay(this DateTime dt) =>
        new(dt.Year, dt.Month, dt.Day, 23, 59, 59, 999, dt.Kind);

    /// <summary>
    /// Calculates full day difference between two dates (ignores time).
    /// </summary>
    public static int DaysDifference(this DateTime start, DateTime end) =>
        (end.Date - start.Date).Days;

    /// <summary>
    /// Parses a string to DateTime or returns the default value.
    /// </summary>
    public static DateTime ParseOrDefault(string? dateStr, DateTime defaultValue)
    {
        if (string.IsNullOrWhiteSpace(dateStr))
            return defaultValue;

        return DateTime.TryParse(
            dateStr,
            CultureInfo.InvariantCulture,
            DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal,
            out var dt
        ) ? dt : defaultValue;
    }
    /// <summary>
    /// Checks if the given date falls on a weekend (Saturday or Sunday).
    /// </summary>
    public static bool IsWeekend(this DateTime dt)
    {
        return dt.DayOfWeek == DayOfWeek.Saturday || dt.DayOfWeek == DayOfWeek.Sunday;
    }

    /// <summary>
    /// Checks if the given date falls on a weekday (Monday to Friday).
    /// </summary>
    public static bool IsWeekday(this DateTime dt)
    {
        return !dt.IsWeekend();
    }

    /// <summary>
    /// Gets the start of the week for the given date.
    /// </summary>
    /// <param name="dt">The date.</param>
    /// <param name="startOfWeek">The day considered as start of week (default is Monday).</param>
    /// <returns>DateTime at 00:00:00 of the start day.</returns>
    public static DateTime GetStartOfWeek(this DateTime dt, DayOfWeek startOfWeek = DayOfWeek.Monday)
    {
        var diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
        var start = dt.Date.AddDays(-diff);
        return new DateTime(start.Year, start.Month, start.Day, 0, 0, 0, dt.Kind);
    }

    /// <summary>
    /// Gets the end of the week for the given date.
    /// </summary>
    /// <param name="dt">The date.</param>
    /// <param name="startOfWeek">The day considered as start of week (default is Monday).</param>
    /// <returns>DateTime at 23:59:59.999 of the last day.</returns>
    public static DateTime GetEndOfWeek(this DateTime dt, DayOfWeek startOfWeek = DayOfWeek.Monday)
    {
        var start = dt.GetStartOfWeek(startOfWeek);
        var end = start.AddDays(6).Date.AddHours(23).AddMinutes(59).AddSeconds(59).AddMilliseconds(999);
        return new DateTime(end.Year, end.Month, end.Day, end.Hour, end.Minute, end.Second, end.Millisecond, dt.Kind);
    }

    /// <summary>
    /// Adds business days (skipping weekends and optional holidays) to the date.
    /// </summary>
    /// <param name="dt">Starting date.</param>
    /// <param name="days">Number of business days to add.</param>
    /// <param name="holidays">Optional list of holiday dates to skip.</param>
    /// <returns>DateTime after adding business days.</returns>
    public static DateTime AddBusinessDays(this DateTime dt, int days, HashSet<DateTime>? holidays = null)
    {
        if (days == 0) return dt;

        var direction = days > 0 ? 1 : -1;
        var absDays = Math.Abs(days);
        var current = dt;

        while (absDays > 0)
        {
            current = current.AddDays(direction);

            var isWeekend = current.IsWeekend();
            var isHoliday = holidays != null && holidays.Contains(current.Date);

            if (!isWeekend && !isHoliday)
                absDays--;
        }

        return current;
    }

    /// <summary>
    /// Returns a human-readable relative time string, e.g., "3 hours ago", "in 2 days".
    /// </summary>
    public static string ToRelativeTime(this DateTime dt, DateTime? reference = null)
    {
        var now = reference ?? DateTime.UtcNow;
        var ts = now - dt;

        var suffix = ts.TotalMilliseconds >= 0 ? "ago" : "from now";
        var delta = Math.Abs(ts.TotalSeconds);

        return delta switch
        {
            < 60 => $"{Math.Round(delta)} seconds {suffix}",
            < 3600 => $"{Math.Round(delta / 60)} minutes {suffix}",
            < 86400 => $"{Math.Round(delta / 3600)} hours {suffix}",
            // 30 days
            < 2592000 => $"{Math.Round(delta / 86400)} days {suffix}",
            // 12 months
            < 31104000 => $"{Math.Round(delta / 2592000)} months {suffix}",
            _ => $"{Math.Round(delta / 31104000)} years {suffix}"
        };
    }

    /// <summary>
    /// Converts DateTime to Unix timestamp (seconds since Unix epoch).
    /// </summary>
    public static long ToUnixTimestamp(this DateTime dt)
    {
        var utcDate = dt.Kind == DateTimeKind.Utc ? dt : dt.ToUniversalTime();
        var unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, utcDate.Kind);
        return Convert.ToInt64((utcDate - unixEpoch).TotalSeconds);
    }

    /// <summary>
    /// Converts Unix timestamp (seconds since Unix epoch) to DateTime (UTC).
    /// </summary>
    public static DateTime FromUnixTimestamp(long unixTimestamp)
    {
        var unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        return unixEpoch.AddSeconds(unixTimestamp);
    }
}
