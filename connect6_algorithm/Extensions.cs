public static class Extensions
{
    public static T MaxBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector) where TKey : IComparable<TKey>
    {
        return source.Aggregate((max, current) => selector(max).CompareTo(selector(current)) > 0 ? max : current);
    }

    public static string Centered(this string text, int width)
    {
        int padding = width - text.Length;
        if (padding <= 0)
        {
            return text;
        }
        else
        {
            int leftPadding = padding / 2;
            int rightPadding = padding - leftPadding;
            return new string(' ', leftPadding) + text + new string(' ', rightPadding);
        }
    }
}