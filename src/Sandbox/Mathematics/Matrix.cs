using System.Text;

namespace Sandbox.Mathematics;

public readonly struct Matrix
{
    public int Height { get; }
    public int Width { get; }

    public long this[int row, int column]
    {
        get => _data[row, column];
        set => _data[row, column] = value;
    }

    private readonly long[,] _data;

    public Matrix(int height, int width)
    {
        Height = height;
        Width = width;
        _data = new long[height, width];
    }

    public static Matrix Identity(int n)
    {
        var matrix = new Matrix(n, n);
        for (var i = 0; i < n; i++) matrix[i, i] = 1;
        return matrix;
    }

    public Matrix Add(in Matrix other) => Add(this, other);

    public static Matrix Add(in Matrix a, in Matrix b)
    {
        if (a.Height != b.Height) throw new ArgumentException("The matrix heights do not match");
        if (a.Width != b.Width) throw new ArgumentException("The matrix widths do not match");
        var c = new Matrix(a.Height, a.Width);
        for (var i = 0; i < b.Height; i++)
        for (var j = 0; j < b.Width; j++)
            c[i, j] = a[i, j] + b[i, j];

        return c;
    }

    public Matrix Add(in Matrix other, long mod) => Add(this, other, mod);

    public static Matrix Add(in Matrix a, in Matrix b, long mod)
    {
        if (a.Height != b.Height) throw new ArgumentException("The matrix heights do not match");
        if (a.Width != b.Width) throw new ArgumentException("The matrix widths do not match");
        var c = new Matrix(a.Height, a.Width);
        for (var i = 0; i < b.Height; i++)
        for (var j = 0; j < b.Width; j++)
            c[i, j] = (c[i, j] + (a[i, j] + b[i, j]) % mod) % mod;

        return c;
    }

    public Matrix Multiply(in Matrix other) => Multiply(this, other);

    public static Matrix Multiply(in Matrix a, in Matrix b)
    {
        if (a.Width != b.Height) throw new ArgumentException();
        var c = new Matrix(a.Height, b.Width);
        for (var i = 0; i < a.Height; i++)
        for (var k = 0; k < b.Height; k++)
        for (var j = 0; j < b.Width; j++)
            c[i, j] += a[i, k] * b[k, j];

        return c;
    }

    public Matrix Multiply(in Matrix other, long mod) => Multiply(this, other, mod);

    public static Matrix Multiply(in Matrix a, in Matrix b, long mod)
    {
        if (a.Width != b.Height) throw new ArgumentException();
        var c = new Matrix(a.Height, b.Width);
        for (var i = 0; i < a.Height; i++)
        for (var k = 0; k < b.Height; k++)
        for (var j = 0; j < b.Width; j++)
            c[i, j] = (c[i, j] + a[i, k] * b[k, j] % mod) % mod;

        return c;
    }

    public Matrix Power(long n) => Power(this, n);

    public static Matrix Power(Matrix matrix, long n)
    {
        if (matrix.Height != matrix.Width) throw new ArgumentException(nameof(matrix));
        var ret = new Matrix(matrix.Height, matrix.Height);
        for (var i = 0; i < matrix.Height; i++) ret[i, i] = 1;
        while (n > 0)
        {
            if ((n & 1) == 1) ret = Multiply(ret, matrix);
            matrix = Multiply(matrix, matrix);
            n >>= 1;
        }

        return ret;
    }

    public Matrix Power(long n, long mod) => Power(this, n, mod);

    public static Matrix Power(Matrix matrix, long n, long mod)
    {
        if (matrix.Height != matrix.Width) throw new ArgumentException(nameof(matrix));
        var ret = new Matrix(matrix.Height, matrix.Height);
        for (var i = 0; i < matrix.Height; i++) ret[i, i] = 1;
        while (n > 0)
        {
            if ((n & 1) == 1) ret = Multiply(ret, matrix, mod);
            matrix = Multiply(matrix, matrix, mod);
            n >>= 1;
        }

        return ret;
    }

    public override string ToString()
    {
        var builder = new StringBuilder();
        for (var i = 0; i < Height; i++)
        {
            for (var j = 0; j < Width; j++)
            {
                builder.Append(_data[i, j]);
                builder.Append(j == Width - 1 ? "\n" : " ");
            }
        }

        return builder.ToString();
    }
}