using System.Collections;

namespace Sandbox.Structures;

public class Set<T> : IEnumerable<T>
{
    private readonly RandomizedBinarySearchTree<T> _tree;
    private readonly bool _allowDuplication;

    public Set(bool allowDuplication = false)
        : this(null, comparer: null, allowDuplication)
    {
    }

    public Set(IEnumerable<T> source, bool allowDuplication = false)
        : this(source, comparer: null, allowDuplication)
    {
    }

    public Set(IEnumerable<T> source, Comparer<T> comparer, bool allowDuplication = false)
        : this(source, (comparer ?? Comparer<T>.Default).Compare, allowDuplication)
    {
    }

    public Set(IEnumerable<T> source, Comparison<T> comparison, bool allowDuplication = false)
    {
        _tree = new RandomizedBinarySearchTree<T>(comparison);
        _allowDuplication = allowDuplication;
        if (source is null) return;
        foreach (var value in source) Add(value);
    }

    public void Add(T value)
    {
        if (_allowDuplication || !_tree.Contains(value)) _tree.Insert(value);
    }

    public void Remove(T value)
    {
        _tree.Erase(value);
    }

    public bool Contains(T value)
    {
        return _tree.Contains(value);
    }

    public T ElementAt(int index)
    {
        return _tree.ElementAt(index) ?? throw new ArgumentOutOfRangeException(nameof(index));
    }

    public int Count() => _tree.Count();

    public int Count(T value)
    {
        return _tree.UpperBound(value) - _tree.LowerBound(value);
    }

    public int LowerBound(T value)
    {
        return _tree.LowerBound(value);
    }

    public int UpperBound(T value)
    {
        return _tree.UpperBound(value);
    }

    public IEnumerator<T> GetEnumerator() => _tree.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}