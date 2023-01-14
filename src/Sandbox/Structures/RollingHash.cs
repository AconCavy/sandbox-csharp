namespace Sandbox.Structures;

// https://qiita.com/keymoon/items/11fac5627672a6d6a9f6
public class RollingHash
{
    private const ulong Mask30 = (1UL << 30) - 1;
    private const ulong Mask31 = (1UL << 31) - 1;
    private const ulong Modulo = (1UL << 61) - 1;
    private const ulong Positivizer = Modulo * ((1UL << 3) - 1);
    public static readonly ulong Base;

    static RollingHash()
    {
        Base = (ulong)new Random().Next(1 << 8, int.MaxValue);
    }

    private readonly ulong[] _powers;
    private readonly ulong[] _hash;

    public RollingHash(ReadOnlySpan<char> s)
    {
        _powers = new ulong[s.Length + 1];
        _powers[0] = 1;
        _hash = new ulong[s.Length + 1];

        for (var i = 0; i < s.Length; i++)
        {
            _powers[i + 1] = CalcModulo(Multiply(_powers[i], Base));
            _hash[i + 1] = CalcModulo(Multiply(_hash[i], Base) + s[i]);
        }
    }

    public ulong SlicedHash(int start, int length)
    {
        return CalcModulo(_hash[start + length] + Positivizer - Multiply(_hash[start], _powers[length]));
    }

    private static ulong Multiply(ulong a, ulong b)
    {
        var au = a >> 31;
        var ad = a & Mask31;
        var bu = b >> 31;
        var bd = b & Mask31;
        var m = ad * bu + au * bd;
        var mu = m >> 30;
        var md = m & Mask30;
        return ((au * bu) << 1) + mu + (md << 31) + ad * bd;
    }

    private static ulong CalcModulo(ulong v)
    {
        var vu = v >> 61;
        var vd = v & Modulo;
        var x = vu + vd;
        return x < Modulo ? x : x - Modulo;
    }
}