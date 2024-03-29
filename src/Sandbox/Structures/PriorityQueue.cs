using System.Collections;

namespace Sandbox.Structures;

public class PriorityQueue<T> : IReadOnlyCollection<T>
{
    private readonly Comparison<T> _comparison;
    private readonly List<T> _heap;

    public PriorityQueue(IEnumerable<T> items, IComparer<T> comparer = null) : this(comparer)
    {
        foreach (var item in items) Enqueue(item);
    }

    public PriorityQueue(IEnumerable<T> items, Comparison<T> comparison) : this(comparison)
    {
        foreach (var item in items) Enqueue(item);
    }

    public PriorityQueue(IComparer<T> comparer = null) : this((comparer ?? Comparer<T>.Default).Compare) { }

    public PriorityQueue(Comparison<T> comparison)
    {
        _heap = new List<T>();
        _comparison = comparison;
    }

    public int Count => _heap.Count;

    public IEnumerator<T> GetEnumerator() => _heap.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public void Enqueue(T item)
    {
        var child = Count;
        _heap.Add(item);
        while (child > 0)
        {
            var parent = (child - 1) / 2;
            if (_comparison(_heap[parent], _heap[child]) <= 0) break;
            (_heap[parent], _heap[child]) = (_heap[child], _heap[parent]);
            child = parent;
        }
    }

    public T Dequeue()
    {
        if (Count == 0) throw new InvalidOperationException();
        var result = _heap[0];
        _heap[0] = _heap[Count - 1];
        _heap.RemoveAt(Count - 1);
        var parent = 0;
        while (parent * 2 + 1 < Count)
        {
            var left = parent * 2 + 1;
            var right = parent * 2 + 2;
            if (right < Count && _comparison(_heap[left], _heap[right]) > 0)
                left = right;
            if (_comparison(_heap[parent], _heap[left]) <= 0) break;
            (_heap[parent], _heap[left]) = (_heap[left], _heap[parent]);
            parent = left;
        }

        return result;
    }

    public T Peek()
    {
        if (Count == 0) throw new InvalidOperationException();
        return _heap[0];
    }

    public bool TryDequeue(out T result)
    {
        if (Count > 0)
        {
            result = Dequeue();
            return true;
        }

        result = default;
        return false;
    }

    public bool TryPeek(out T result)
    {
        if (Count > 0)
        {
            result = Peek();
            return true;
        }

        result = default;
        return false;
    }

    public void Clear() => _heap.Clear();

    public bool Contains(T item) => _heap.Contains(item);
}