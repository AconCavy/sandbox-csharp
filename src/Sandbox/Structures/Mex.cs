namespace Sandbox.Structures;

public class Mex
{
    public int RightBound { get; init; }
    private readonly SortedSet<int> _heap;

    public Mex(int rightBound)
    {
        RightBound = rightBound;
        _heap = new SortedSet<int>(Enumerable.Range(0, rightBound + 1));
    }

    public void Add(int x)
    {
        _heap.Remove(x);
    }

    public int Get()
    {
        return _heap.Count > 0 ? _heap.Min : RightBound;
    }

    public void Remove(int x)
    {
        _heap.Add(Math.Min(x, RightBound));
    }
}