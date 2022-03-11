namespace Sandbox.Structures;

public class CumulativeSum2D
{
    public int Height { get; }
    public int Width { get; }

    private readonly long[] _data;
    private readonly long[] _sum;
    private bool _isUpdated;

    public CumulativeSum2D(int height, int width)
    {
        if (height <= 0) throw new ArgumentOutOfRangeException(nameof(height));
        if (width <= 0) throw new ArgumentOutOfRangeException(nameof(width));
        Height = height;
        Width = width;
        _data = new long[height * width];
        _sum = new long[(height + 1) * (width + 1)];
    }

    public void Add(int height, int width, long value)
    {
        if (height < 0 || Height <= height) throw new ArgumentOutOfRangeException(nameof(height));
        if (width < 0 || Width <= width) throw new ArgumentOutOfRangeException(nameof(width));
        _isUpdated = false;
        _data[height * Width + width] += value;
    }

    public void Set(int height, int width, long value)
    {
        if (height < 0 || Height <= height) throw new ArgumentOutOfRangeException(nameof(height));
        if (width < 0 || Width <= width) throw new ArgumentOutOfRangeException(nameof(width));
        _isUpdated = false;
        _data[height * Width + width] = value;
    }

    public long Get(int height, int width)
    {
        if (height < 0 || Height <= height) throw new ArgumentOutOfRangeException(nameof(height));
        if (width < 0 || Width <= width) throw new ArgumentOutOfRangeException(nameof(width));
        return _data[height * Width + width];
    }

    public long Sum(int height, int width)
    {
        if (height < 0 || Height < height) throw new ArgumentOutOfRangeException(nameof(height));
        if (width < 0 || Width < width) throw new ArgumentOutOfRangeException(nameof(width));
        if (!_isUpdated) Build();
        return _sum[height * (Width + 1) + width];
    }


    public long Sum(int height1, int width1, int height2, int width2)
    {
        if (height1 < 0 || Height < height1) throw new ArgumentOutOfRangeException(nameof(height1));
        if (width1 < 0 || Width < width1) throw new ArgumentOutOfRangeException(nameof(width1));
        if (height2 < 0 || Height < height2) throw new ArgumentOutOfRangeException(nameof(height2));
        if (width2 < 0 || Width < width2) throw new ArgumentOutOfRangeException(nameof(width2));
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
}