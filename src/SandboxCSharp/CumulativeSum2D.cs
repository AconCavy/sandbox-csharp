namespace SandboxCSharp
{
    public class CumulativeSum2D
    {
        private readonly long[,] _graph;
        private readonly long[,] _sum;
        private bool _isUpdated;

        public CumulativeSum2D(int x, int y)
        {
            _graph = new long[y, x];
            _sum = new long[y + 1, x + 1];
        }

        public long this[int y, int x]
        {
            get => _graph[y, x];
            set
            {
                _isUpdated = false;
                _graph[y, x] = value;
            }
        }

        public void Add(int x, int y, long value)
        {
            _graph[y, x] += value;
        }

        public long GetArea(int x1, int y1, int x2, int y2)
        {
            if (!_isUpdated) Build();
            return _sum[y1, x1] + _sum[y2, x2] - _sum[y2, x1] - _sum[y1, x2];
        }

        private void Build()
        {
            for (var i = 1; i < _sum.GetLength(0); i++)
            for (var j = 1; j < _sum.GetLength(1); j++)
                _sum[i, j] = _sum[i, j - 1] + _sum[i - 1, j] - _sum[i - 1, j - 1] + _graph[i - 1, j - 1];
            _isUpdated = true;
        }
    }
}