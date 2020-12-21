namespace SandboxCSharp
{
    public class FenwickTree2D
    {
        private readonly int _height;
        private readonly int _width;
        private readonly long[,] _data;

        public FenwickTree2D(int h, int w)
        {
            _height = h;
            _width = w;
            _data = new long[_height, _width];
        }

        public void Add(int h, int w, long value)
        {
            for (var i = h + 1; i <= _height; i += i & -i)
            for (var j = w + 1; j <= _width; j += j & -j)
                _data[i - 1, j - 1] += value;
        }

        public long Sum(int h, int w)
        {
            var sum = 0L;
            for (var i = h + 1; i > 0; i -= i & -i)
            for (var j = w + 1; j > 0; j -= j & -j)
                sum += _data[i - 1, j - 1];
            return sum;
        }

        public long Sum(int h1, int w1, int h2, int w2)
        {
            return Sum(h2, w2) - Sum(h2, w1) - Sum(h1, w2) + Sum(h1, w1);
        }
    }
}