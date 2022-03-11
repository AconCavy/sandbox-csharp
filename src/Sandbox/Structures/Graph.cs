namespace Sandbox.Structures;

public class Graph
{
    public int Length { get; }

    private readonly List<Edge>[] _data;
    private readonly int[] _degree;

    public Graph(int length)
    {
        if (length < 0) throw new ArgumentOutOfRangeException(nameof(length));
        Length = length;
        _data = new List<Edge>[Length].Select(_ => new List<Edge>()).ToArray();
        _degree = new int[Length];
    }

    public void AddEdge(int u, int v, long cost)
    {
        if (u < 0 || Length <= u) throw new ArgumentOutOfRangeException(nameof(u));
        if (v < 0 || Length <= v) throw new ArgumentOutOfRangeException(nameof(v));
        _data[u].Add(new Edge(v, cost));
        _degree[v]++;
    }

    public long[] Dijkstra(int start, long seed, long invalid, Func<long, long, long> calcCost)
    {
        if (start < 0 || Length <= start) throw new ArgumentOutOfRangeException(nameof(start));
        if (calcCost is null) throw new ArgumentNullException(nameof(calcCost));
        var queue = new PriorityQueue<(int U, long C)>((x, y) => y.C.CompareTo(x.C));
        queue.Enqueue((start, seed));
        var costs = new long[Length];
        Array.Fill(costs, invalid);
        costs[start] = seed;
        while (queue.Count > 0)
        {
            var (u, cu) = queue.Dequeue();
            if (cu > costs[u]) continue;
            foreach (var node in _data[u])
            {
                var c = calcCost(costs[u], node.Cost);
                if (c >= costs[node.To]) continue;
                costs[node.To] = c;
                queue.Enqueue((node.To, c));
            }
        }

        return costs;
    }

    public (bool Result, int[] Colors) IsBipartite()
    {
        var queue = new Queue<int>();
        queue.Enqueue(0);
        var colors = new int[Length];
        Array.Fill(colors, -1);
        colors[0] = 0;
        while (queue.Count > 0)
        {
            var from = queue.Dequeue();
            foreach (var node in _data[from])
            {
                if (colors[from] == colors[node.To]) return (false, colors);
                if (colors[node.To] != -1) continue;
                colors[node.To] = colors[from] ^ 1;
                queue.Enqueue(node.To);
            }
        }

        return (true, colors);
    }

    public void Kruskal(Action<long> necessary, Action<long> unnecessary, Comparison<long> comparison)
    {
        var data = _data.SelectMany((edges, u) => edges
                .Select(edge => (U: u, V: edge.To, C: edge.Cost)))
            .ToArray();
        Array.Sort(data, (x, y) => comparison(x.C, y.C));
        var dsu = new DisjointSetUnion(Length);
        foreach (var (u, v, c) in data)
        {
            if (dsu.IsSame(u, v))
            {
                unnecessary?.Invoke(c);
                continue;
            }

            necessary?.Invoke(c);
            dsu.Merge(u, v);
        }
    }

    public (bool, int[]) TopologicalSort()
    {
        var queue = new Queue<int>();
        var degree = new int[Length];
        _degree.CopyTo(degree, 0);
        for (var i = 0; i < degree.Length; i++)
        {
            if (degree[i] == 0) queue.Enqueue(i);
        }

        var result = new int[Length];
        var idx = 0;

        while (queue.Count > 0)
        {
            var v = queue.Dequeue();
            foreach (var node in _data[v])
            {
                degree[node.To]--;
                if (degree[node.To] == 0) queue.Enqueue(node.To);
            }

            result[idx++] = v;
        }

        return (idx == Length, result);
    }

    private readonly struct Edge
    {
        internal int To { get; }
        internal long Cost { get; }
        public Edge(int to, long cost) => (To, Cost) = (to, cost);
    }
}