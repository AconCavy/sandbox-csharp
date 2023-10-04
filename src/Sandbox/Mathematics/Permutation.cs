namespace Sandbox.Mathematics;

public static class Permutation
{
    public static bool NextPermutation(Span<int> indices)
    {
        var n = indices.Length;
        var (i, j) = (n - 2, n - 1);
        while (i >= 0 && indices[i] >= indices[i + 1]) i--;
        if (i == -1) return false;
        while (j > i && indices[j] <= indices[i]) j--;
        (indices[i], indices[j]) = (indices[j], indices[i]);
        indices[(i + 1)..].Reverse();
        return true;
    }

    public static bool PreviousPermutation(Span<int> indices)
    {
        var n = indices.Length;
        var (i, j) = (n - 2, n - 1);
        while (i >= 0 && indices[i] <= indices[i + 1]) i--;
        if (i == -1) return false;
        indices[(i + 1)..].Reverse();
        while (j > i && indices[j - 1] < indices[i]) j--;
        (indices[i], indices[j]) = (indices[j], indices[i]);
        return true;
    }

    public static IEnumerable<IReadOnlyList<int>> GeneratePermutation(int length)
    {
        return Inner();

        IEnumerable<IReadOnlyList<int>> Inner()
        {
            var indices = new int[length];
            for (var i = 0; i < indices.Length; i++) indices[i] = i;
            do { yield return indices; } while (NextPermutation(indices));
        }
    }

    public static IEnumerable<IReadOnlyList<int>> GeneratePermutationDescending(int length)
    {
        return Inner();

        IEnumerable<IReadOnlyList<int>> Inner()
        {
            var indices = new int[length];
            for (var i = 0; i < indices.Length; i++) indices[i] = length - 1 - i;
            do { yield return indices; } while (PreviousPermutation(indices));
        }
    }
}