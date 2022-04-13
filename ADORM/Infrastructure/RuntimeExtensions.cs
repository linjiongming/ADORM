using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

public static class RuntimeExtensions
{
    public static int? ToInt(this object obj)
    {
        if (obj != null && obj != DBNull.Value)
        {
            if (obj is int equal)
                return equal;
            if (int.TryParse(obj?.ToString(), out var parse))
                return parse;
        }
        return null;
    }

    public static int ToInt(this object obj, int def)
    {
        return obj.ToInt() ?? def;
    }

    public static long? ToLong(this object obj)
    {
        if (obj != null && obj != DBNull.Value)
        {
            if (obj is long equal)
                return equal;
            if (long.TryParse(obj?.ToString(), out var parse))
                return parse;
        }
        return null;
    }

    public static long ToLong(this object obj, long def)
    {
        return obj.ToLong() ?? def;
    }

    public static short? ToShort(this object obj)
    {
        if (obj != null && obj != DBNull.Value)
        {
            if (obj is short equal)
                return equal;
            if (short.TryParse(obj?.ToString(), out var parse))
                return parse;
        }
        return null;
    }

    public static short ToShort(this object obj, short def)
    {
        return obj.ToShort() ?? def;
    }

    public static byte? ToByte(this object obj)
    {
        if (obj != null && obj != DBNull.Value)
        {
            if (obj is byte equal)
                return equal;
            if (byte.TryParse(obj?.ToString(), out var parse))
                return parse;
        }
        return null;
    }

    public static short ToByte(this object obj, short def)
    {
        return obj.ToByte() ?? def;
    }

    public static double? ToDouble(this object obj)
    {
        if (obj != null && obj != DBNull.Value)
        {
            if (obj is double equal)
                return equal;
            if (double.TryParse(obj?.ToString(), out var parse))
                return parse;
        }
        return null;
    }

    public static double ToDouble(this object obj, double def)
    {
        return obj.ToDouble() ?? def;
    }

    public static float? ToFloat(this object obj)
    {
        if (obj != null && obj != DBNull.Value)
        {
            if (obj is float equal)
                return equal;
            if (float.TryParse(obj?.ToString(), out var parse))
                return parse;
        }
        return null;
    }

    public static float ToFloat(this object obj, float def)
    {
        return obj.ToFloat() ?? def;
    }

    public static DateTime? ToDateTime(this object obj)
    {
        if (obj != null && obj != DBNull.Value)
        {
            if (obj is DateTime equal)
                return equal;
            if (DateTime.TryParse(obj?.ToString(), out var parse))
                return parse;
        }
        return null;
    }

    public static int IfZero(this int num, int val)
    {
        return num == 0 ? val : num;
    }

    public static bool IsEmpty(this string str)
    {
        return string.IsNullOrWhiteSpace(str);
    }

    public static string IfEmpty(this string str, string def)
    {
        return string.IsNullOrWhiteSpace(str) ? def : str;
    }

    public static bool IsNotEmpty(this string str)
    {
        return !string.IsNullOrWhiteSpace(str);
    }

    public static string AddSuffixBeforeExt(this string filename, string suffix)
    {
        var index = filename.LastIndexOf('.');
        return filename.Insert(index, suffix);
    }

    public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
    {
        foreach (var item in source)
        {
            action(item);
        }
    }

    public static IEnumerable<T> FirstHalf<T>(this IEnumerable<T> source)
    {
        return source.Take(source.Count() / 2);
    }

    public static IEnumerable<T> SecondHalf<T>(this IEnumerable<T> source)
    {
        return source.Skip(source.Count() / 2);
    }

    public static IEnumerable<IEnumerable<T>> ToBatches<T>(this IEnumerable<T> source, int batchSize)
    {
        var cursor = batchSize - 1;
        var total = source.Count();
        for (int i = 0; i < total; i++)
        {
            if (i == cursor)
            {
                yield return source.Skip(cursor + 1 - batchSize).Take(batchSize);
                cursor += batchSize;
            }
            if (i == total - 1)
            {
                yield return source.Skip(cursor + 1 - batchSize);
                break;
            }
        }
    }

    public static string JoinStrings<T>(this IEnumerable<T> source, string separator)
    {
        if (source == null) return null;
        return string.Join(separator, source);
    }

    public static string Break(this string str, int size, string breaker = "\r\n")
    {
        return str.ToBatches(size).Select(x => new string(x.ToArray())).JoinStrings(breaker);
    }

    public static int GetMonthNum(this DateTime date)
    {
        return date.Year * 100 + date.Month;
    }

    public static string ToUniversalJson(this DateTime dateTime)
    {
        return dateTime.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
    }

    public static IEnumerable<T> Find<T>(this IContainer container)
    {
        foreach (var component in container.Components)
        {
            if (component is T result)
            {
                yield return result;
            }
        }
    }

    public static T First<T>(this IContainer container) where T : class
    {
        foreach (var component in container.Components)
        {
            if (component is T result)
            {
                return result;
            }
        }
        return null;
    }

    public static readonly string[] TrueStrings = new string[] { "true", "1", "yes", "y" };
    public static bool GetBoolValue(this NameValueCollection settings, string key, bool @default = false)
    {
        if (settings != null && !string.IsNullOrWhiteSpace(key) && settings.AllKeys.Contains(key))
        {
            return TrueStrings.Contains(settings[key], StringComparer.CurrentCultureIgnoreCase);
        }
        return @default;
    }
    public static string[] GetArray(this NameValueCollection settings, string key, params string[] separators)
    {
        if (settings != null && !string.IsNullOrWhiteSpace(key) && settings.AllKeys.Contains(key))
        {
            return settings[key].Split(new[] { ",", "，" }.Union(separators).ToArray(), StringSplitOptions.RemoveEmptyEntries).Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
        }
        return null;
    }
    public static T[] GetArray<T>(this NameValueCollection settings, string key, params string[] separators) where T : IConvertible
    {
        if (settings != null && !string.IsNullOrWhiteSpace(key) && settings.AllKeys.Contains(key))
        {
            return settings[key].Split(new[] { ",", "，" }.Union(separators).ToArray(), StringSplitOptions.RemoveEmptyEntries)
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(x => (T)Convert.ChangeType(x, typeof(T))).ToArray();
        }
        return null;
    }
    private static readonly string[] _optKeys = new string[] { "/", "-" };
    public static bool TryGetArg(this string[] args, string key, out string value)
    {
        value = null;
        if (args != null && args.Length > 0)
        {
            if (_optKeys.Contains(key.Substring(0, 1)))
            {
                key = key.Substring(0, 1);
            }
            var index = Array.FindIndex(args, x => _optKeys.Any(y => x.StartsWith(y)) && x.Substring(1).Equals(key, StringComparison.CurrentCultureIgnoreCase));
            if (index >= 0)
            {
                if (args.Length > index && !_optKeys.Any(x => args[index + 1].StartsWith(x)))
                {
                    value = args[index + 1];
                }
                return true;
            }
        }
        return false;
    }
    public static TResult[] SelectParallel<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
    {
        var srcArr = source.ToArray();
        var resArr = new TResult[srcArr.Length];
        Parallel.For(0, srcArr.Length, index => resArr[index] = selector(srcArr[index]));
        return resArr;
    }
}