using System.Numerics;

namespace Sandbox.Geometry;

public readonly record struct Triangle<TNumber>(Point<TNumber> Point1, Point<TNumber> Point2, Point<TNumber> Point3)
    where TNumber : INumber<TNumber>
{
    public Triangle(TNumber x1, TNumber y1, TNumber x2, TNumber y2, TNumber x3, TNumber y3) :
        this(new Point<TNumber>(x1, y1), new Point<TNumber>(x2, y2), new Point<TNumber>(x3, y3))
    {
    }

    public double Aria()
    {
        var (dx1, dy1) = (Point2.X - Point1.X, Point2.Y - Point1.Y);
        var (dx2, dy2) = (Point3.X - Point1.X, Point3.Y - Point1.Y);
        return double.Abs(double.CreateSaturating(dx1 * dy2 - dx2 * dy1) / 2);
    }

    public Point<TNumber> Incenter()
    {
        var a = Point<TNumber>.Distance(Point2, Point3);
        var b = Point<TNumber>.Distance(Point3, Point1);
        var c = Point<TNumber>.Distance(Point1, Point2);
        return WeightedPoint(a, b, c);
    }

    public Point<TNumber> Circumcenter()
    {
        var a = Point<TNumber>.Distance(Point2, Point3);
        var b = Point<TNumber>.Distance(Point3, Point1);
        var c = Point<TNumber>.Distance(Point1, Point2);
        var (aa, bb, cc) = (a * a, b * b, c * c);
        var w1 = aa * (bb + cc - aa);
        var w2 = bb * (cc + aa - bb);
        var w3 = cc * (aa + bb - cc);
        return WeightedPoint(w1, w2, w3);
    }

    public Point<TNumber> Orthocenter()
    {
        var a = Point<TNumber>.Distance(Point2, Point3);
        var b = Point<TNumber>.Distance(Point3, Point1);
        var c = Point<TNumber>.Distance(Point1, Point2);
        var (aa, bb, cc) = (a * a, b * b, c * c);
        var w1 = aa * aa - (bb - cc) * (bb - cc);
        var w2 = bb * bb - (cc - aa) * (cc - aa);
        var w3 = cc * cc - (aa - bb) * (aa - bb);
        return WeightedPoint(w1, w2, w3);
    }

    public Point<TNumber> Centroid() => WeightedPoint(1, 1, 1);

    public (Point<TNumber> Point1, Point<TNumber> Point2, Point<TNumber> Point3) Excenters()
    {
        var a = Point<TNumber>.Distance(Point2, Point3);
        var b = Point<TNumber>.Distance(Point3, Point1);
        var c = Point<TNumber>.Distance(Point1, Point2);
        return (WeightedPoint(-a, b, c), WeightedPoint(a, -b, c), WeightedPoint(a, b, -c));
    }

    private Point<TNumber> WeightedPoint(double w1, double w2, double w3)
    {
        var x = (w1 * double.CreateSaturating(Point1.X) +
                 w2 * double.CreateSaturating(Point2.X) +
                 w3 * double.CreateSaturating(Point3.X)) /
                (w1 + w2 + w3);
        var y = (w1 * double.CreateSaturating(Point1.Y) +
                 w2 * double.CreateSaturating(Point2.Y) +
                 w3 * double.CreateSaturating(Point3.Y)) /
                (w1 + w2 + w3);
        return new Point<TNumber>(TNumber.CreateSaturating(x), TNumber.CreateSaturating(y));
    }
}