using System;

namespace Sandbox.Structures
{
    public class CumulativeSum2D
    {
        private readonly long[,] _data;
        private readonly int _height;
        private readonly long[,] _sum;
        private readonly int _width;
        private bool _isUpdated;

        public CumulativeSum2D(int h, int w)
        {
            if (h <= 0) throw new ArgumentOutOfRangeException(nameof(h));
            if (w <= 0) throw new ArgumentOutOfRangeException(nameof(w));
            _height = h;
            _width = w;
            _data = new long[h, w];
            _sum = new long[h + 1, w + 1];
        }

        public void Add(int h, int w, long value)
        {
            if (h < 0 || _height <= h) throw new ArgumentOutOfRangeException(nameof(h));
            if (w < 0 || _width <= w) throw new ArgumentOutOfRangeException(nameof(w));
            _isUpdated = false;
            _data[h, w] += value;
        }

        public void Set(int h, int w, long value)
        {
            if (h < 0 || _height <= h) throw new ArgumentOutOfRangeException(nameof(h));
            if (w < 0 || _width <= w) throw new ArgumentOutOfRangeException(nameof(w));
            _isUpdated = false;
            _data[h, w] = value;
        }

        public long Get(int h, int w)
        {
            if (h < 0 || _height <= h) throw new ArgumentOutOfRangeException(nameof(h));
            if (w < 0 || _width <= w) throw new ArgumentOutOfRangeException(nameof(w));
            return _data[h, w];
        }

        public long Sum(int h, int w)
        {
            if (h < 0 || _height < h) throw new ArgumentOutOfRangeException(nameof(h));
            if (w < 0 || _width < w) throw new ArgumentOutOfRangeException(nameof(w));
            if (!_isUpdated) Build();
            return _sum[h, w];
        }


        public long Sum(int h1, int w1, int h2, int w2)
        {
            if (h1 < 0 || _height < h1) throw new ArgumentOutOfRangeException(nameof(h1));
            if (w1 < 0 || _width < w1) throw new ArgumentOutOfRangeException(nameof(w1));
            if (h2 < 0 || _height < h2) throw new ArgumentOutOfRangeException(nameof(h2));
            if (w2 < 0 || _width < w2) throw new ArgumentOutOfRangeException(nameof(w2));
            if (!_isUpdated) Build();
            return _sum[h1, w1] + _sum[h2, w2] - _sum[h2, w1] - _sum[h1, w2];
        }

        private void Build()
        {
            _isUpdated = true;
            _sum[0, 0] = _sum[1, 0] = _sum[0, 1] = 0;
            for (var i = 1; i <= _height; i++)
                for (var j = 1; j <= _width; j++)
                    _sum[i, j] = _sum[i, j - 1] + _sum[i - 1, j] - _sum[i - 1, j - 1] + _data[i - 1, j - 1];
        }
    }
}