using System.Numerics;

namespace Sandbox.Structures;

public class FenwickTree2D<T> where T : INumber<T>
{
    public int Height { get; }
    public int Width { get; }

    private readonly T[] _data;

    public FenwickTree2D(int height, int width)
    {
        ThrowIfNegative(height);
        ThrowIfNegative(width);
        Height = height;
        Width = width;
        _data = new T[Height * Width];
    }

    public void Add(int height, int width, T value)
    {
        ThrowIfNegative(height);
        ThrowIfGreaterThanOrEqual(height, Height);
        ThrowIfNegative(width);
        ThrowIfGreaterThanOrEqual(width, Width);
        for (var i = height + 1; i <= Height; i += i & -i)
        {
            for (var j = width + 1; j <= Width; j += j & -j)
            {
                _data[(i - 1) * Width + (j - 1)] += value;
            }
        }
    }

    /// <summary>
    /// Calculate a two-dimensional cumulative sum of [0, height), [0, width).
    /// </summary>
    public T Sum(int height, int width)
    {
        ThrowIfNegative(height);
        ThrowIfGreaterThan(height, Height);
        ThrowIfNegative(width);
        ThrowIfGreaterThan(width, Width);

        var sum = T.Zero;
        for (var i = height; i > 0; i -= i & -i)
        {
            for (var j = width; j > 0; j -= j & -j)
            {
                sum += _data[(i - 1) * Width + (j - 1)];
            }
        }

        return sum;
    }

    /// <summary>
    /// Calculate a two-dimensional cumulative sum of [height1, height2), [width1, width2).
    /// </summary>
    public T Sum(int height1, int width1, int height2, int width2)
    {
        ThrowIfGreaterThan(height1, height2);
        ThrowIfGreaterThan(width1, width2);

        return Sum(height1, width1) + Sum(height2, width2) - Sum(height2, width1) - Sum(height1, width2);
    }

    private static void ThrowIfNegative(int value)
    {
        if (value < 0) throw new ArgumentOutOfRangeException(nameof(value));
    }

    private static void ThrowIfGreaterThan(int value, int other)
    {
        if (value > other) throw new ArgumentOutOfRangeException(nameof(value));
    }

    private static void ThrowIfGreaterThanOrEqual(int value, int other)
    {
        if (value >= other) throw new ArgumentOutOfRangeException(nameof(value));
    }
}