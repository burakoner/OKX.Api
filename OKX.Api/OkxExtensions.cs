namespace OKX.Api;

/// <summary>
/// OKX Extensions
/// </summary>
internal static class OkxExtensions
{
    /// <summary>
    /// Validate the string is a valid spot symbol.
    /// </summary>
    /// <param name="symbol">string to validate</param>
    /// <param name="messagePrefix"></param>
    /// <param name="messageSuffix"></param>
    public static string ValidateSymbol(this string symbol, string messagePrefix = "", string messageSuffix = "")
    {
        if (string.IsNullOrEmpty(symbol))
            throw new ArgumentException($"{messagePrefix}{(messagePrefix.Length > 0 ? " " : "")}Symbol is not provided{(messageSuffix.Length > 0 ? " " : "")}{messageSuffix}");

        // symbol = symbol.ToLower(CultureInfo.InvariantCulture);
        if (!Regex.IsMatch(symbol, "^(([a-z]|[A-Z]|-|[0-9]){4,})$"))
            throw new ArgumentException($"{messagePrefix}{(messagePrefix.Length > 0 ? " " : "")}{symbol} is not a valid OKX Symbol. Should be [QuoteCurrency]-[BaseCurrency], e.g. ETH-BTC{(messageSuffix.Length > 0 ? " " : "")}{messageSuffix}");

        return symbol;
    }

    /// <summary>
    /// Validate the string is a valid spot currency.
    /// </summary>
    /// <param name="currency">Currency</param>
    /// <param name="messagePrefix"></param>
    /// <param name="messageSuffix"></param>
    /// <returns></returns>
    public static string ValidateCurrency(this string currency, string messagePrefix = "", string messageSuffix = "")
    {
        if (string.IsNullOrEmpty(currency))
            throw new ArgumentException($"{messagePrefix}{(messagePrefix.Length > 0 ? " " : "")}Symbol is not provided{(messageSuffix.Length > 0 ? " " : "")}{messageSuffix}");

        if (!Regex.IsMatch(currency, "^(([a-z]|[A-Z]){2,})$"))
            throw new ArgumentException($"{messagePrefix}{(messagePrefix.Length > 0 ? " " : "")}{currency} is not a valid OKX Currency. Should be [Currency] only, e.g. BTC{(messageSuffix.Length > 0 ? " " : "")}{messageSuffix}");

        return currency;
    }

    public static void ValidateStringLength(this string @this, string argumentName, int minLength, int maxLength, string messagePrefix = "", string messageSuffix = "")
    {
        if (@this.Length < minLength || @this.Length > maxLength)
            throw new ArgumentException(
                $"{messagePrefix}{(messagePrefix.Length > 0 ? " " : "")}{@this} not allowed for parameter {argumentName}, Min Length: {minLength}, Max Length: {maxLength}{(messageSuffix.Length > 0 ? " " : "")}{messageSuffix}");
    }

    #region Null
    public static bool IsNull(this object @this)
    {
        return @this is null || @this.GetType() == typeof(DBNull);
    }

    public static bool IsNotNull(this object @this)
    {
        return !@this.IsNull();
    }
    #endregion

    #region ToStr
    public static string? ToStr(this object? @this, bool nullToEmpty = true)
    {
        bool isNull = @this is null;
        bool isDBNull = @this is not null && @this.GetType() == typeof(DBNull);

        if (isNull) return nullToEmpty ? string.Empty : null;
        else if (isDBNull) return nullToEmpty ? string.Empty : null;
        else return @this?.ToString();
    }
    #endregion

    #region ToOkxString
    public static string ToOkxString(this object? @this)
    {
        bool isNull = @this is null;
        bool isDBNull = @this is not null && @this.GetType() == typeof(DBNull);

        if (isNull) return string.Empty;
        if (isDBNull) return string.Empty;
        if (@this is float s02) return s02.ToString(OkxConstants.OkxCultureInfo);
        if (@this is double s03) return s03.ToString(OkxConstants.OkxCultureInfo);
        if (@this is decimal s01) return s01.ToString(OkxConstants.OkxCultureInfo);
        if (@this is short s06) return s06.ToString(OkxConstants.OkxCultureInfo);
        if (@this is int s04) return s04.ToString(OkxConstants.OkxCultureInfo);
        if (@this is long s05) return s05.ToString(OkxConstants.OkxCultureInfo);

        return @this!.ToString();
    }
    #endregion

    #region ToNumber
    public static int ToIntegerSafe(this object @this)
    {
        var result = 0;
        if (@this.IsNull()) return result;
        
        int.TryParse(@this.ToStr(), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out result);
        return result;
    }

    public static long ToLongSafe(this object @this)
    {
        var result = 0L;
        if (@this.IsNotNull()) long.TryParse(@this.ToStr(), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out result);
        return result;
    }

    public static double ToDoubleSafe(this object @this)
    {
        if (@this is null) return 0.0;

        var result = 0.0;
        if (@this.IsNotNull()) double.TryParse(@this.ToStr(), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out result);
        return result;
    }
    public static double? ToDoubleNullable(this object @this)
    {
        if (@this is null) return null;

        var result = 0.0;
        if (@this.IsNotNull()) double.TryParse(@this.ToStr(), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out result);
        return result;
    }

    public static decimal ToDecimalSafe(this object @this)
    {
        if (@this is null) return 0;

        var result = 0.0m;
        if (@this.IsNotNull()) decimal.TryParse(@this.ToStr(), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out result);
        return result;
    }
    public static decimal? ToDecimalNullable(this object @this)
    {
        if (@this is null) return null;

        decimal result = 0.0m;
        if (@this.IsNotNull()) decimal.TryParse(@this.ToStr(), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out result);
        return result;
    }

    public static float ToFloatSafe(this object @this)
    {
        if (@this is null) return 0;

        float result = 0;
        if (@this.IsNotNull()) float.TryParse(@this.ToStr(), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out result);
        return result;
    }
    #endregion

    #region Epoch TimeStamp
    public static DateTime FromUnixTimeSeconds(this int @this)
    {
        var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        return epoch.AddSeconds(@this);
    }

    public static DateTime FromUnixTimeSeconds(this long @this)
    {
        var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        return epoch.AddSeconds(@this);
    }

    public static long ToUnixTimeSeconds(this DateTime @this)
    {
        var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        return Convert.ToInt64((@this - epoch).TotalSeconds);
    }
    public static DateTime FromUnixTimeMilliSeconds(this long @this)
    {
        var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        return epoch.AddSeconds(@this / 1000);
    }

    public static long ToUnixTimeMilliSeconds(this DateTime @this)
    {
        var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        return Convert.ToInt64((@this - epoch).TotalSeconds) * 1000 + @this.Millisecond;
    }
    #endregion

    #region ISO 8601 DateTime
    public static string DateTimeToIso8601String(this DateTime @this)
    {
        return @this.ToString(OkxConstants.OkxDatetimeFormat);
    }

    public static DateTime Iso8601StringToDateTime(this string @this)
    {
        return DateTime.ParseExact(@this, OkxConstants.OkxDatetimeFormat, CultureInfo.InvariantCulture);
    }
    #endregion

    #region String IsOneOf
    public static bool IsOneOf(this string @this, params string[] values)
    {
        foreach (var v in values)
        {
            if (@this == v)
            {
                return true;
            }
        }

        return false;
    }

    #endregion

    #region String IsNotOneOf
    public static bool IsNotOneOf(this string @this, params string[] values)
    {
        return !@this.IsOneOf(values);
    }

    #endregion

    #region Integer IsOneOf
    public static bool IsOneOf(this int @this, params int[] values)
    {
        foreach (var v in values)
        {
            if (@this == v)
            {
                return true;
            }
        }

        return false;
    }

    #endregion

    #region Integer IsNotOneOf
    public static bool IsNotOneOf(this int @this, params int[] values)
    {
        return !@this.IsOneOf(values);
    }

    #endregion

    public static bool IsIn<T>(this T @this, params T[] values)
    {
        return Array.IndexOf(values, @this) != -1;
    }

    public static bool IsNotIn<T>(this T @this, params T[] values)
    {
        return Array.IndexOf(values, @this) == -1;
    }
}