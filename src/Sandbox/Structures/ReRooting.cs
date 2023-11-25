namespace Sandbox.Structures;

public class ReRooting<T>
{
    public int Size { get; init; }
    private readonly List<int>[] _edges;
    private readonly IOperation _operation;

    public ReRooting(int size, IOperation operation)
    {
        Size = size;
        _operation = operation;
        _edges = new List<int>[Size];
        for (var i = 0; i < Size; i++) _edges[i] = new List<int>();
    }

    public void AddEdge(int u, int v)
    {
        _edges[u].Add(v);
        _edges[v].Add(u);
    }

    public IReadOnlyList<T> Calc()
    {
        var result = new T[Size];
        Array.Fill(result, _operation.Identity);
        var dp = new T[Size][];

        Dfs(0);
        Bfs(0, _operation.Identity);
        return result;

        T Dfs(int u, int p = -1)
        {
            dp[u] = new T[_edges[u].Count];
            var cum = _operation.Identity;
            for (var i = 0; i < _edges[u].Count; i++)
            {
                var v = _edges[u][i];
                if (v == p) continue;
                dp[u][i] = Dfs(v, u);
                cum = _operation.Merge(cum, dp[u][i]);
            }

            return _operation.AddRoot(cum);
        }

        void Bfs(int u, T value, int p = -1)
        {
            var n = _edges[u].Count;
            for (var i = 0; i < n; i++)
            {
                if (_edges[u][i] == p) dp[u][i] = value;
            }

            var cumL = new T[n + 1];
            var cumR = new T[n + 1];
            Array.Fill(cumL, _operation.Identity);
            Array.Fill(cumR, _operation.Identity);
            for (var i = 0; i < n; i++)
            {
                var j = n - 1 - i;
                cumL[i + 1] = _operation.Merge(cumL[i], dp[u][i]);
                cumR[j] = _operation.Merge(cumR[j + 1], dp[u][j]);
            }

            result[u] = _operation.AddRoot(cumL[n]);

            for (var i = 0; i < n; i++)
            {
                var v = _edges[u][i];
                if (v != p) Bfs(v, _operation.AddRoot(_operation.Merge(cumL[i], cumR[i + 1])), u);
            }
        }
    }

    public interface IOperation
    {
        public T Identity { get; }
        public T Merge(T left, T right);
        public T AddRoot(T value);
    }
}