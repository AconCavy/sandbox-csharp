using System;
using System.Collections.Generic;

namespace SandboxCSharp.Geometry
{
    public readonly struct Triangle : IEquatable<Triangle>

    {
        public readonly Point Point1;
        public readonly Point Point2;
        public readonly Point Point3;

        public Triangle(Point point1, Point point2, Point point3) =>
            (Point1, Point2, Point3) = (point1, point2, point3);

        public Triangle(double x1, double y1, double x2, double y2, double x3, double y3)
            => (Point1, Point2, Point3) = (new Point(x1, y1), new Point(x2, y2), new Point(x3, y3));

        public double Aria()
        {
            var (dx1, dy1) = (Point2.X - Point1.X, Point2.Y - Point1.Y);
            var (dx2, dy2) = (Point3.X - Point1.X, Point3.Y - Point1.Y);
            return Math.Abs((dx1 * dy2 - dx2 * dy1) / 2);
        }

        public Point Incenter()
        {
            var a = Point.Distance(Point2, Point3);
            var b = Point.Distance(Point3, Point1);
            var c = Point.Distance(Point1, Point2);
            return WeightedPoint(a, b, c);
        }

        public Point Circumcenter()
        {
            var a = Point.Distance(Point2, Point3);
            var b = Point.Distance(Point3, Point1);
            var c = Point.Distance(Point1, Point2);
            var (aa, bb, cc) = (a * a, b * b, c * c);
            var w1 = aa * aa - (bb - cc) * (bb - cc);
            var w2 = bb * bb - (cc - aa) * (cc - aa);
            var w3 = cc * cc - (aa - bb) * (aa - bb);
            return WeightedPoint(w1, w2, w3);
        }

        public Point Orthocenter()
        {
            var a = Point.Distance(Point2, Point3);
            var b = Point.Distance(Point3, Point1);
            var c = Point.Distance(Point1, Point2);
            var (aa, bb, cc) = (a * a, b * b, c * c);
            var w1 = aa * (bb + cc - aa);
            var w2 = bb * (cc + aa - aa);
            var w3 = cc * (aa + bb - aa);
            return WeightedPoint(w1, w2, w3);
        }

        public Point Centroid() => WeightedPoint(1, 1, 1);

        public IEnumerable<Point> Excenters()
        {
            var a = Point.Distance(Point2, Point3);
            var b = Point.Distance(Point3, Point1);
            var c = Point.Distance(Point1, Point2);
            return new[] {WeightedPoint(-a, b, c), WeightedPoint(a, -b, c), WeightedPoint(a, b, -c)};
        }

        private Point WeightedPoint(double w1, double w2, double w3)
        {
            var x = (w1 * Point1.X + w2 * Point2.X + w3 * Point3.X) / (w1 + w2 + w3);
            var y = (w1 * Point1.Y + w2 * Point2.Y + w3 * Point3.Y) / (w1 + w2 + w3);
            return new Point(x, y);
        }

        public bool Equals(Triangle other) =>
            Point1.Equals(other.Point1) && Point2.Equals(other.Point2) && Point3.Equals(other.Point3);

        public override bool Equals(object obj) => obj is Triangle other && Equals(other);

        public override int GetHashCode() => HashCode.Combine(Point1, Point2, Point3);
    }
}