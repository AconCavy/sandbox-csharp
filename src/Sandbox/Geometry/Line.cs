using System.Numerics;

namespace Sandbox.Geometry;

public readonly record struct Line<TNumber> where TNumber : INumber<TNumber>
{
    public Point<TNumber> Point1 { get; }
    public Point<TNumber> Point2 { get; }
    public double Length { get; }

    public Line(Point<TNumber> point1, Point<TNumber> point2)
    {
        (Point1, Point2) = (point1, point2);
        Length = Point<TNumber>.Distance(Point1, Point2);
    }

    public Line(TNumber x1, TNumber y1, TNumber x2, TNumber y2) :
        this(new Point<TNumber>(x1, y1), new Point<TNumber>(x2, y2))
    {
    }

    public bool Intersect(in Line<TNumber> other) => Intersect(this, other);

    public static bool Intersect(in Line<TNumber> line1, in Line<TNumber> line2)
    {
        var zero = TNumber.Zero;
        var ta = (line2.Point1.X - line2.Point2.X) * (line1.Point1.Y - line2.Point1.Y) +
                 (line2.Point1.Y - line2.Point2.Y) * (line2.Point1.X - line1.Point1.X);
        var tb = (line2.Point1.X - line2.Point2.X) * (line1.Point2.Y - line2.Point2.Y) +
                 (line2.Point1.Y - line1.Point2.Y) * (line2.Point1.X - line1.Point2.X);
        var tc = (line1.Point1.X - line1.Point2.X) * (line2.Point1.Y - line1.Point1.Y) +
                 (line1.Point1.Y - line1.Point2.Y) * (line1.Point1.X - line2.Point1.X);
        var td = (line1.Point1.X - line1.Point2.X) * (line2.Point2.Y - line1.Point2.Y) +
                 (line1.Point1.Y - line1.Point2.Y) * (line1.Point1.X - line2.Point2.X);
        return ta * tb < zero && tc * td < zero;
    }

    public double DistanceFrom(in Point<TNumber> point) => Distance(this, point);

    public static double Distance(in Line<TNumber> line, in Point<TNumber> point)
    {
        var (dx, dy) = (line.Point2.X - line.Point1.X, line.Point2.Y - line.Point1.Y);
        if (dx == TNumber.Zero) return double.Abs(double.CreateSaturating(line.Point1.X - point.X));
        var m = dy / dx;
        var c = line.Point1.Y - m * line.Point1.X;
        return double.Abs(double.CreateSaturating(m * point.X - point.Y + c)) /
               double.Sqrt((double.CreateSaturating(m * m) + 1.0));
    }
}