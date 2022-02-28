using System.Collections;

namespace Sandbox.Structures;

public class Set<T> : IReadOnlyCollection<T>
{
    private readonly RandomizedBinarySearchTree<T> _tree;
    private readonly bool _allowDuplication;

    public Set(bool allowDuplication = false) : this(Comparer<T>.Default, allowDuplication) { }

    public Set(IEnumerable<T> source, bool allowDuplication = false) : this(allowDuplication)
    {
        foreach (var value in source) Add(value);
    }

    public Set(IEnumerable<T> source, IComparer<T> comparer, bool allowDuplication = false)
        : this(comparer, allowDuplication)
    {
        foreach (var value in source) Add(value);
    }

    public Set(IEnumerable<T> source, Comparison<T> comparison, bool allowDuplication = false)
        : this(comparison, allowDuplication)
    {
        foreach (var value in source) Add(value);
    }

    public Set(IComparer<T> comparer, bool allowDuplication = false)
        : this((comparer ?? Comparer<T>.Default).Compare, allowDuplication)
    {
    }

    public Set(Comparison<T> comparison, bool allowDuplication = false)
    {
        _tree = new RandomizedBinarySearchTree<T>(comparison);
        _allowDuplication = allowDuplication;
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
        return _tree.ElementAt(index);
    }

    public int LowerBound(T value)
    {
        return _tree.LowerBound(value);
    }

    public int UpperBound(T value)
    {
        return _tree.UpperBound(value);
    }

    public int Count => _tree.Count;

    public IEnumerator<T> GetEnumerator() => _tree.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}