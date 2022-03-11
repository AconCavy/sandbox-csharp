using NUnit.Framework;
using Sandbox.Structures;

namespace Sandbox.Tests;

public class GraphTests
{
    [Test]
    public void InitializeTest()
    {
        Assert.DoesNotThrow(() => _ = new Graph(0));
        Assert.DoesNotThrow(() => _ = new Graph(1));
        Assert.Throws<ArgumentOutOfRangeException>(() => _ = new Graph(-1));
        Assert.That(new Graph(0).Length, Is.Zero);
        Assert.That(new Graph(1).Length, Is.EqualTo(1));
    }

    [Test]
    public void AddEdgeTest()
    {
        var graph = new Graph(2);
        Assert.DoesNotThrow(() => graph.AddEdge(0, 1, 0));
        Assert.DoesNotThrow(() => graph.AddEdge(1, 0, 0));
        Assert.Throws<ArgumentOutOfRangeException>(() => graph.AddEdge(-1, 0, 0));
        Assert.Throws<ArgumentOutOfRangeException>(() => graph.AddEdge(0, -1, 0));
    }

    [TestCase(3, new[] { 4, 1, 5 }, new[] { 3, 10, 100 }, new long[] { 0, 3, 7, 8 })]
    [TestCase(4, new[] { 100, 100, 100, 100 }, new[] { 1, 1, 1, 1 }, new long[] { 0, 1, 1, 1, 1 })]
    [TestCase(4, new[] { 1, 2, 3, 4 }, new[] { 1, 2, 4, 7 }, new long[] { 0, 1, 2, 4, 7 })]
    [TestCase(8, new[] { 84, 87, 78, 16, 94, 36, 87, 93 }, new[] { 50, 22, 63, 28, 91, 60, 64, 27 },
        new long[] { 0, 50, 22, 63, 28, 44, 60, 64, 27 })]
    // https://atcoder.jp/contests/abc214/tasks/abc214_c
    public void DijkstraTest(int n, int[] s, int[] t, long[] expected)
    {
        var graph = new Graph(n + 1);
        for (var i = 0; i < n; i++) graph.AddEdge(i + 1, (i + 1) % n + 1, s[i]);
        for (var i = 0; i < n; i++) graph.AddEdge(0, i + 1, t[i]);

        var actual = graph.Dijkstra(0, 0, (int)1e9, (x, y) => x + y);
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void DijkstraArgumentsTest()
    {
        var graph = new Graph(3);
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            graph.Dijkstra(-1, 0, (long)1e9, (x, y) => x + y));
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            graph.Dijkstra(3, 0, (long)1e9, (x, y) => x + y));
        Assert.Throws<ArgumentNullException>(() =>
            graph.Dijkstra(0, 0, (long)1e9, null));
    }

    [TestCase(2, new[] { 0 }, new[] { 1 }, true)]
    [TestCase(3, new[] { 0, 1 }, new[] { 1, 2 }, true)]
    [TestCase(3, new[] { 0, 1, 2 }, new[] { 1, 2, 0 }, false)]
    [TestCase(6, new[] { 0, 1, 1, 2, 3, 4 }, new[] { 1, 2, 3, 4, 4, 5 }, true)]
    public void IsBipartiteTest(int n, int[] us, int[] vs, bool expected)
    {
        var graph = new Graph(n);
        foreach (var (u, v) in us.Zip(vs))
        {
            graph.AddEdge(u, v, 1);
            graph.AddEdge(v, u, 1);
        }

        var (actual, _) = graph.IsBipartite();
        Assert.That(expected, Is.EqualTo(actual));
    }

    [TestCase(7, 8,
        new long[] { 40, 50, 30, 70, 70, 80, 80 },
        new[] { 1, 1, 1, 2, 3, 4, 5, 6 },
        new[] { 2, 3, 4, 5, 4, 5, 6, 7 },
        new long[] { 40, 50, 60, 90, 80, 110, 60, 50 },
        350)]
    [TestCase(3, 3,
        new long[] { 50, 50, 50 },
        new[] { 1, 1, 2 },
        new[] { 2, 3, 3 },
        new long[] { 60, 60, 60 },
        150)]
    [TestCase(5, 7,
        new long[] { 80, 70, 60, 50, 40 },
        new[] { 1, 1, 1, 2, 2, 3, 4 },
        new[] { 3, 4, 5, 3, 4, 4, 5 },
        new long[] { 20, 70, 30, 30, 90, 40, 80 },
        160)]

    // https://atcoder.jp/contests/arc029/tasks/arc029_3
    public void KruskalTest(int n, int m, long[] c, int[] a, int[] b, long[] r, long expected)
    {
        var graph = new Graph(n + 1);
        for (var i = 0; i < n; i++) graph.AddEdge(0, i + 1, c[i]);
        for (var i = 0; i < m; i++) graph.AddEdge(a[i], b[i], r[i]);

        long actual = 0;
        void F(long x) => actual += x;
        graph.Kruskal(F, null, (x, y) => x.CompareTo(y));
        Assert.That(expected, Is.EqualTo(actual));
    }

    [TestCase(3, 3, new[] { 1, 2, 2 }, new[] { 2, 1, 3 }, true)]
    [TestCase(8, 7, new[] { 2, 2, 7, 6, 8, 8, 5 }, new[] { 4, 8, 3, 1, 4, 3, 3 }, false)]
    // https://atcoder.jp/contests/past202107-open/tasks/past202107_j
    public void TopologicalSortTest(int n, int m, int[] a, int[] b, bool expected)
    {
        var graph = new Graph(n);
        for (var i = 0; i < m; i++)
        {
            graph.AddEdge(a[i] - 1, b[i] - 1, 1);
        }

        var (actual, _) = graph.TopologicalSort();
        Assert.That(expected, Is.EqualTo(!actual));
    }
}