using Sandbox.Extensions;

namespace Sandbox.Mathematics;

public static class NQueensProblem
{
    public static IEnumerable<bool[][]> Permute(int size)
    {
        foreach (var permutation in Enumerable.Range(0, size).Permute())
        {
            var (isSatisfiable, answer) = Identify(size, permutation.Select((x, i) => (i, x)));
            if (isSatisfiable) yield return answer;
        }
    }

    public static (bool isIdentified, bool[][] answer) Identify(int size,
        IEnumerable<(int row, int column)> constraints = null)
    {
        var answer = new bool[size][].Select(_ => new bool[size]).ToArray();
        var queens = constraints?.ToArray() ?? Array.Empty<(int, int)>();
        foreach (var (r, c) in queens) answer[r][c] = true;
        if (queens.Any(x => !CanPlace(answer, x.row, x.column))) return (false, answer);

        var isIdentified = false;

        void Inner(int placed)
        {
            if (isIdentified) return;
            if (placed == size)
            {
                isIdentified = true;
                return;
            }

            for (var r = 0; r < size; r++)
                for (var c = 0; c < size; c++)
                {
                    if (answer[r][c] || !CanPlace(answer, r, c)) continue;
                    answer[r][c] = true;
                    Inner(placed + 1);
                    if (isIdentified) return;
                    answer[r][c] = false;
                }
        }

        Inner(queens.Length);
        return (isIdentified, answer);
    }

    public static bool CanPlace(in bool[][] grid, int r, int c)
    {
        var size = grid.Length;
        var d4 = new[] { (1, 1), (1, -1), (-1, 1), (-1, -1) };
        for (var i = 1; i < size; i++)
            foreach (var (dr, dc) in d4)
            {
                var (nr, nc) = (r + dr * i, c + dc * i);
                if (nr < 0 || size <= nr || nc < 0 || size <= nc) continue;
                if (grid[nr][nc]) return false;
            }

        for (var i = 0; i < size; i++)
        {
            if (i != c && grid[r][i]) return false;
            if (i != r && grid[i][c]) return false;
        }

        return true;
    }
}