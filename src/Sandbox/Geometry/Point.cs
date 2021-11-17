using System;

namespace Sandbox.Geometry
{
    public readonly struct Point : IEquatable<Point>
    {
        public double X { get; }
        public double Y { get; }
        public Point(double x, double y) => (X, Y) = (x, y);

        public static double Distance(in Point point1, in Point point2)
        {
            var (dx, dy) = (point1.X - point2.X, point1.Y - point2.Y);
            return Math.Sqrt(dx * dx + dy * dy);
        }

        public Point Rotate(double radian) => Rotate(this, radian);

        public static Point Rotate(in Point point, double radian)
        {
            var (sin, cos) = (Math.Sin(radian), Math.Cos(radian));
            return new Point(point.X * cos - point.Y * sin, point.X * sin + point.Y * cos);
        }

        public bool Equals(Point other) => X.Equals(other.X) && Y.Equals(other.Y);
        public override bool Equals(object obj) => obj is Point other && Equals(other);
        public override int GetHashCode() => HashCode.Combine(X, Y);
        public override string ToString() => $"<{X}, {Y}>";
    }
}