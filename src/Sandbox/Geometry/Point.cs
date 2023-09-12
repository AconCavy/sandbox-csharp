using System.Numerics;

namespace Sandbox.Geometry;

public readonly record struct Point<TNumber>(TNumber X, TNumber Y) where TNumber : INumber<TNumber>
{
    public static Point<TNumber> Zero => new(TNumber.Zero, TNumber.Zero);

    public double DistanceFrom(in Point<TNumber> point) => Distance(this, point);

    public static double Distance(in Point<TNumber> point1, in Point<TNumber> point2)
    {
        var (dx, dy) = (point1.X - point2.X, point1.Y - point2.Y);
        return double.Sqrt(double.CreateSaturating(dx * dx + dy * dy));
    }

    public static Point<TFloatingNumber> Rotate<TFloatingNumber>(in Point<TFloatingNumber> point,
        TFloatingNumber radian)
        where TFloatingNumber : IFloatingPoint<TFloatingNumber>, ITrigonometricFunctions<TFloatingNumber>
    {
        var (sin, cos) = TFloatingNumber.SinCos(radian);
        return new Point<TFloatingNumber>(point.X * cos - point.Y * sin, point.X * sin + point.Y * cos);
    }
}