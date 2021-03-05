using System;
using System.Collections.Generic;
using System.Linq;

namespace Sandbox.Structures
{
    public class Graph
    {
        private readonly List<(int, long)>[] _data;
        private readonly int[] _degree;

        public Graph(int count)
        {
            Count = count;
            _data = new List<(int, long)>[Count].Select(_ => new List<(int, long)>()).ToArray();
            _degree = new int[Count];
        }

        public int Count { get; }

        public void AddEdge(int u, int v, long cost = 1)
        {
            if (u < 0 || Count <= u) throw new ArgumentOutOfRangeException(nameof(u));
            if (v < 0 || Count <= v) throw new ArgumentOutOfRangeException(nameof(v));
            _data[u].Add((v, cost));
            _degree[v]++;
        }

        public IEnumerable<int> TopologicalSort()
        {
            var queue = new Queue<int>();
            var degree = new int[Count];
            _degree.CopyTo(degree, 0);
            for (var i = 0; i < degree.Length; i++)
                if (degree[i] == 0)
                    queue.Enqueue(i);

            while (queue.Any())
            {
                var v = queue.Dequeue();
                for (var i = 0; i < _data[v].Count; i++)
                {
                    var (u, _) = _data[v][i];
                    degree[u]--;
                    if (degree[u] == 0) queue.Enqueue(u);
                }

                yield return v;
            }
        }

        public IEnumerable<long> Dijkstra(int start, Func<long, long, long> func = null)
        {
            if (start < 0 || Count <= start) throw new ArgumentOutOfRangeException(nameof(start));
            func ??= (x, y) => x + y;
            var queue = new PriorityQueue<(int U, long Cost)>((x, y) => x.Cost.CompareTo(y.Cost));
            queue.Enqueue((start, 0));
            var costs = new long[Count];
            Array.Fill(costs, long.MaxValue);
            costs[start] = 0;
            while (queue.Any())
            {
                var (u, cu) = queue.Dequeue();
                if (cu >= costs[u]) continue;
                foreach (var (v, cv) in _data[u])
                {
                    var c = func(costs[u], cv);
                    if (costs[v] <= c) continue;
                    costs[v] = c;
                    queue.Enqueue((v, c));
                }
            }

            return costs;
        }

        public long Kruskal()
        {
            var flatten = _data.SelectMany((x, i) => x.Select(y => (i, y.Item1, y.Item2))).ToArray();
            Array.Sort(flatten, (x, y) => x.Item3.CompareTo(y.Item3));
            var dsu = new DisjointSetUnion(Count);
            var cost = 0L;
            foreach (var (u, v, c) in flatten)
            {
                if (dsu.IsSame(u, v)) continue;
                cost += c;
                dsu.Merge(u, v);
            }

            return cost;
        }

        public (bool, IEnumerable<int>) IsBipartite()
        {
            var queue = new Queue<int>();
            queue.Enqueue(0);
            var colors = new int[Count];
            Array.Fill(colors, -1);
            colors[0] = 0;
            while (queue.Any())
            {
                var u = queue.Dequeue();
                foreach (var (v, _) in _data[u])
                {
                    if (colors[u] == colors[v]) return (false, colors);
                    if (colors[v] != -1) continue;
                    colors[v] = colors[u] ^ 1;
                    queue.Enqueue(v);
                }
            }

            return (true, colors);
        }
    }
}