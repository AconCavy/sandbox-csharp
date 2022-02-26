using System.Collections;

namespace Sandbox.Structures;

public class RandomizedBinarySearchTree<T> : IEnumerable<T>
{
    private readonly Comparison<T> _comparison;
    private readonly Compare _lowerBound;
    private readonly Compare _upperBound;
    private readonly Random _random;

    private Node _root;
    private int _count;

    public RandomizedBinarySearchTree(int seed = 0) : this(comparer: null, seed) { }

    public RandomizedBinarySearchTree(Comparer<T> comparer, int seed = 0) : this(
        (comparer ?? Comparer<T>.Default).Compare, seed)
    {
    }

    public RandomizedBinarySearchTree(Comparison<T> comparison, int seed = 0)
    {
        _comparison = comparison;
        _lowerBound = (x, y) => _comparison(x, y) >= 0;
        _upperBound = (x, y) => _comparison(x, y) > 0;
        _random = new Random(seed);
    }

    public delegate bool Compare(T x, T y);

    public void Insert(T value)
    {
        if (_root is null) _root = new Node(value);
        else InsertAt(LowerBound(value), value);
    }

    public void InsertAt(int index, T value)
    {
        var (l, r) = Split(_root, index);
        _root = Merge(Merge(l, new Node(value)), r);
    }

    public void Erase(T value)
    {
        EraseAt(LowerBound(value));
    }

    public void EraseAt(int index)
    {
        var (l, r1) = Split(_root, index);
        var (_, r2) = Split(r1, 1);
        _root = Merge(l, r2);
    }

    public T ElementAt(int index)
    {
        if (index < 0 || Count(_root) <= index) throw new ArgumentNullException(nameof(index));
        var node = _root;
        var idx = Count(node) - Count(node.R) - 1;
        while (node is { })
        {
            if (idx == index) return node.Value;
            if (idx > index)
            {
                node = node.L;
                idx -= Count(node?.R) + 1;
            }
            else
            {
                node = node.R;
                idx += Count(node?.L) + 1;
            }
        }

        throw new ArgumentOutOfRangeException(nameof(index));
    }

    public bool Contains(T value)
    {
        return Find(value) is { };
    }

    public int Count() => Count(_root);

    public IEnumerator<T> GetEnumerator() => Enumerate(_root).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public int UpperBound(T value) => CommonBound(value, _upperBound);
    public int LowerBound(T value) => CommonBound(value, _lowerBound);

    public int CommonBound(T value, Compare compare)
    {
        var node = _root;
        if (node is null) return -1;
        var bound = Count(node);
        var idx = bound - Count(node.R) - 1;
        while (node is { })
        {
            if (compare(node.Value, value))
            {
                node = node.L;
                bound = Math.Min(bound, idx);
                idx -= Count(node?.R) + 1;
            }
            else
            {
                node = node.R;
                idx += Count(node?.L) + 1;
            }
        }

        return bound;
    }

    private double GetProbability() => _random.NextDouble();

    private Node Merge(Node l, Node r)
    {
        if (l is null || r is null) return l ?? r;
        var (n, m) = (Count(l), Count(r));
        if ((double)n / (n + m) > GetProbability())
        {
            l.R = Merge(l.R, r);
            return l;
        }
        else
        {
            r.L = Merge(l, r.L);
            return r;
        }
    }

    private (Node, Node) Split(Node node, int k)
    {
        if (node is null) return (null, null);

        if (k <= Count(node.L))
        {
            var (l, r) = Split(node.L, k);
            node.L = r;
            return (l, node);
        }
        else
        {
            var (l, r) = Split(node.R, k - Count(node.L) - 1);
            node.R = l;
            return (node, r);
        }
    }

    private Node Find(T value)
    {
        var node = _root;
        while (node is { })
        {
            var cmp = _comparison(node.Value, value);
            if (cmp > 0) node = node.L;
            else if (cmp < 0) node = node.R;
            else break;
        }

        return node;
    }

    private static int Count(Node node) => node?.Count ?? 0;

    private static IEnumerable<T> Enumerate(Node node = null)
    {
        if (node is null) yield break;
        foreach (var value in Enumerate(node.L)) yield return value;
        yield return node.Value;
        foreach (var value in Enumerate(node.R)) yield return value;
    }

    private class Node
    {
        public T Value { get; }

        public Node L
        {
            get => _l;
            set
            {
                _l = value;
                UpdateCount();
            }
        }

        public Node R
        {
            get => _r;
            set
            {
                _r = value;
                UpdateCount();
            }
        }

        public int Count { get; private set; }

        private Node _l;
        private Node _r;

        public Node(T value)
        {
            Value = value;
            Count = 1;
        }

        private void UpdateCount()
        {
            Count = (L?.Count ?? 0) + (R?.Count ?? 0) + 1;
        }
    }
}