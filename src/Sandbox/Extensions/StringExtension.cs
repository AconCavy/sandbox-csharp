namespace Sandbox.Extensions;

public static class StringExtension
{
    public static int[] GetCharacterCounts(this string str, int start = 0, int length = 128)
    {
        const int size = 128;
        if (str == null) throw new ArgumentNullException(nameof(str));
        if (start < 0 || size <= start) throw new ArgumentOutOfRangeException(nameof(start));
        if (length < 0 || size < start + length) throw new ArgumentOutOfRangeException(nameof(length));
        var result = new int[size];
        foreach (var c in str) result[c]++;
        return result[start..(start + length)];
    }

    public static int[] GetAlphabetCounts(this string str, bool isUpperCase = false) =>
        GetCharacterCounts(str, isUpperCase ? 'A' : 'a', 26);

    public static int[] GetNumberCounts(this string str) => GetCharacterCounts(str, '0', 10);
}