using System.Collections;

namespace Sandbox.Structures;

public class Deque<T> : IReadOnlyCollection<T>
{
    private readonly LinkedList<T> _heap = new LinkedList<T>();

    public Deque(IEnumerable<T> source = null)
    {
        if (source is null) return;
        foreach (var value in source) PushBack(value);
    }

    public void PushFront(T value)
    {
        _heap.AddFirst(value);
    }

    public void PushBack(T value)
    {
        _heap.AddLast(value);
    }

    public T PeekFront()
    {
        if (_heap.First is null) throw new InvalidOperationException();
        return _heap.First.Value;
    }

    public T PeekBack()
    {
        if (_heap.Last is null) throw new InvalidOperationException();
        return _heap.Last.Value;
    }

    public T PopFront()
    {
        if (_heap.First is null) throw new InvalidOperationException();
        var item = _heap.First.Value;
        _heap.RemoveFirst();
        return item;
    }

    public T PopBack()
    {
        if (_heap.Last is null) throw new InvalidOperationException();
        var item = _heap.Last.Value;
        _heap.RemoveLast();
        return item;
    }

    public bool TryPeekFront(out T result)
    {
        var node = _heap.First;
        if (node is null)
        {
            result = default;
            return false;
        }

        result = node.Value;
        return true;
    }

    public bool TryPeekBack(out T result)
    {
        var node = _heap.Last;
        if (node is null)
        {
            result = default;
            return false;
        }

        result = node.Value;
        return true;
    }

    public bool TryPopFront(out T result)
    {
        var exist = TryPeekFront(out result);
        if (exist) _heap.RemoveFirst();
        return exist;
    }

    public bool TryPopBack(out T result)
    {
        var exist = TryPeekBack(out result);
        if (exist) _heap.RemoveLast();
        return exist;
    }

    public void Clear()
    {
        _heap.Clear();
    }

    public bool Contains(T value)
    {
        return _heap.Contains(value);
    }

    public IEnumerator<T> GetEnumerator() => _heap.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public int Count => _heap.Count;
}