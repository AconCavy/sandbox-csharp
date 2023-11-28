using System.Numerics;

namespace Sandbox.Structures;

public class CumulativeSum2D<T> where T : INumber<T>
{
    public int Height { get; }
    public int Width { get; }

    private readonly T[] _data;
    private readonly T[] _sum;
    private bool _isUpdated;

    public CumulativeSum2D(int height, int width)
    {
        if (height <= 0) throw new ArgumentOutOfRangeException(nameof(height));
        if (width <= 0) throw new ArgumentOutOfRangeException(nameof(width));
        Height = height;
        Width = width;
        _data = new T[height * width];
        _sum = new T[(height + 1) * (width + 1)];
    }

    public void Add(int height, int width, T value)
    {
        ThrowIfNegative(height);
        ThrowIfGreaterThanOrEqual(height, Height);
        ThrowIfNegative(width);
        ThrowIfGreaterThanOrEqual(width, Width);
        _isUpdated = false;
        _data[height * Width + width] += value;
    }

    public void Set(int height, int width, T value)
    {
        ThrowIfNegative(height);
        ThrowIfGreaterThanOrEqual(height, Height);
        ThrowIfNegative(width);
        ThrowIfGreaterThanOrEqual(width, Width);
        _isUpdated = false;
        _data[height * Width + width] = value;
    }

    public T Get(int height, int width)
    {
        ThrowIfNegative(height);
        ThrowIfGreaterThanOrEqual(height, Height);
        ThrowIfNegative(width);
        ThrowIfGreaterThanOrEqual(width, Width);
        return _data[height * Width + width];
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
        if (!_isUpdated) Build();
        return _sum[height * (Width + 1) + width];
    }

    /// <summary>
    /// Calculate a two-dimensional cumulative sum of [height1, height2), [width1, width2).
    /// </summary>
    public T Sum(int height1, int width1, int height2, int width2)
    {
        ThrowIfGreaterThanOrEqual(height1, height2);
        ThrowIfGreaterThanOrEqual(width1, width2);
        if (!_isUpdated) Build();
        var w1 = Width + 1;
        return _sum[height1 * w1 + width1]
               + _sum[height2 * w1 + width2]
               - _sum[height2 * w1 + width1]
               - _sum[height1 * w1 + width2];
    }

    private void Build()
    {
        _isUpdated = true;
        var w1 = Width + 1;
        _sum[0] = _sum[w1] = _sum[1] = 0;
        for (var i = 1; i <= Height; i++)
        for (var j = 1; j <= Width; j++)
            _sum[i * w1 + j] =
                _sum[i * w1 + (j - 1)]
                + _sum[(i - 1) * w1 + j]
                - _sum[(i - 1) * w1 + (j - 1)]
                + _data[(i - 1) * Width + (j - 1)];
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