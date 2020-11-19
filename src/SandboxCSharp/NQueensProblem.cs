using System.Collections.Generic;
using System.Linq;
using SandboxCSharp.Extensions;

namespace SandboxCSharp
{
    public static class NQueensProblem
    {
        public static IEnumerable<bool[][]> Solve(int count = 8)
        {
            foreach (var permutation in Enumerable.Range(0, count).Permute(count))
            {
                var answer = new bool[count][];
                var ok = true;
                foreach (var (x, i) in permutation.Select((x, i) => (x, i)))
                {
                    answer[i] = new bool[count];
                    answer[i][x] = true;
                    for (var k = 1; k < count && ok; k++)
                    {
                        if (i + k < count && x + k < count && answer[i + k][x + k]) ok = false;
                        if (i - k >= 0 && x + k < count && answer[i - k][x + k]) ok = false;
                        if (i + k < count && x - k >= 0 && answer[i + k][x - k]) ok = false;
                        if (i - k >= 0 && x - k >= 0 && answer[i - k][x - k]) ok = false;
                    }
                }

                if (ok) yield return answer;
            }
        }
    }
}