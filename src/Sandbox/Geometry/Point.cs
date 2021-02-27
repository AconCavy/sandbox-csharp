using System;

namespace Sandbox.Geometry
{
    public readonly struct Point : IEquatable<Point>
    {
        public readonly double X;
        public readonly double Y;
        public Point(double x = 0, double y = 0) => (X, Y) = (x, y);

        public static double Distance(in Point p1, in Point p2)
        {
            var (dx, dy) = (p1.X - p2.X, p1.Y - p2.Y);
            return Math.Sqrt(dx * dx + dy * dy);
        }

        public bool Equals(Point other) => X.Equals(other.X) && Y.Equals(other.Y);
        public override bool Equals(object obj) => obj is Point other && Equals(other);
        public override int GetHashCode() => HashCode.Combine(X, Y);
        public override string ToString() => $"<{X}, {Y}>";
    }
}