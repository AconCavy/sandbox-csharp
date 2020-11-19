using System;
using System.Collections.Generic;
using System.Linq;
using SandboxCSharp.Extensions;

namespace SandboxCSharp
{
    public static class NQueensProblem
    {
        public static IEnumerable<bool[][]> Permute(int count)
        {
            foreach (var permutation in Enumerable.Range(0, count).Permute(count))
            {
                var answer = new bool[count][].Select(_ => new bool[count]).ToArray();
                var isDuplicated = false;
                foreach (var (x, i) in permutation.Select((x, i) => (x, i)))
                {
                    answer[i][x] = true;
                    for (var d = 1; d < count && !isDuplicated; d++)
                    {
                        isDuplicated |= i + d < count && x + d < count && answer[i + d][x + d];
                        isDuplicated |= i - d >= 0 && x + d < count && answer[i - d][x + d];
                        isDuplicated |= i + d < count && x - d >= 0 && answer[i + d][x - d];
                        isDuplicated |= i - d >= 0 && x - d >= 0 && answer[i - d][x - d];
                    }
                }

                if (!isDuplicated) yield return answer;
            }
        }

        public static bool[][] Identify(int count, IEnumerable<(int row, int column)> constraints = null)
        {
            var answer = new bool[count][].Select(_ => new bool[count]).ToArray();
            constraints ??= Array.Empty<(int, int)>();
            var ok = 0;
            var isIdentified = false;
            foreach (var (r, c) in constraints)
            {
                answer[r][c] = true;
                ok++;
            }

            void Inner()
            {
                if (isIdentified) return;
                if (ok == count)
                {
                    isIdentified = true;
                    return;
                }

                for (var r = 0; r < count; r++)
                {
                    for (var c = 0; c < count; c++)
                    {
                        if (answer[r][c]) continue;
                        var isDuplicated = false;
                        for (var i = 0; i < count && !isDuplicated; i++)
                        {
                            isDuplicated |= answer[i][c];
                            isDuplicated |= answer[r][i];
                        }

                        for (var d = 1; d < count && !isDuplicated; d++)
                        {
                            isDuplicated |= r + d < count && c + d < count && answer[r + d][c + d];
                            isDuplicated |= r - d >= 0 && c - d >= 0 && answer[r - d][c - d];
                            isDuplicated |= r + d < count && c - d >= 0 && answer[r + d][c - d];
                            isDuplicated |= r - d >= 0 && c + d < count && answer[r - d][c + d];
                        }

                        if (isDuplicated) continue;
                        answer[r][c] = true;
                        ok++;
                        Inner();
                        if (isIdentified) return;
                        answer[r][c] = false;
                        ok--;
                    }
                }
            }

            Inner();
            return answer;
        }
    }
}