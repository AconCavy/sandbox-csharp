namespace SandboxCSharp
{
    public class CumulativeSum2D
    {
        private readonly int _height;
        private readonly int _width;
        private readonly long[,] _data;
        private readonly long[,] _sum;
        private bool _isUpdated;

        public CumulativeSum2D(int h, int w)
        {
            _height = h;
            _width = w;
            _data = new long[h, w];
            _sum = new long[h + 1, w + 1];
        }

        public void Add(int h, int w, long value)
        {
            _isUpdated = false;
            _data[h, w] += value;
        }

        public void Set(int h, int w, long value)
        {
            _isUpdated = false;
            _data[h, w] = value;
        }

        public long Get(int h, int w)
        {
            return _data[h, w];
        }

        public long Sum(int h1, int w1, int h2, int w2)
        {
            if (!_isUpdated) Build();
            return _sum[h1, w1] + _sum[h2, w2] - _sum[h2, w1] - _sum[h1, w2];
        }

        private void Build()
        {
            for (var i = 1; i < _height; i++)
            for (var j = 1; j < _width; j++)
                _sum[i, j] = _sum[i, j - 1] + _sum[i - 1, j] - _sum[i - 1, j - 1] + _data[i - 1, j - 1];
            _isUpdated = true;
        }
    }
}