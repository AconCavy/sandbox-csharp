namespace Sandbox.Mathematics;

public class Combination
{
    public static IEnumerable<IReadOnlyList<int>> Generate(int n, int k)
    {
        return Inner();

        IEnumerable<IReadOnlyList<int>> Inner()
        {
            var indices = new int[k];
            for (var i = 0; i < indices.Length; i++) indices[i] = i;
            do
            {
                yield return indices;
                var i = k - 1;
                while (i >= 0 && indices[i] == i + n - k) i--;
                if (i < 0) break;
                indices[i]++;
                while (++i < k) indices[i] = indices[i - 1] + 1;
            } while (true);
        }
    }
}