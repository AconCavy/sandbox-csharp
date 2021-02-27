using System;

namespace Sandbox.Geometry
{
    public readonly struct Line : IEquatable<Line>
    {
        public readonly Point Point1;
        public readonly Point Point2;

        public Line(in Point point1, in Point point2) => (Point1, Point2) = (point1, point2);

        public Line(double x1, double y1, double x2, double y2) =>
            (Point1, Point2) = (new Point(x1, y1), new Point(x2, y2));

        public double Length()
        {
            var (dx, dy) = (Point2.X - Point1.X, Point2.Y - Point1.Y);
            return Math.Sqrt(dx * dx + dy * dy);
        }

        public bool Intersect(in Line other) => Intersect(this, other);

        public static bool Intersect(in Line line1, in Line line2)
        {
            var ta = (line2.Point1.X - line2.Point2.X) * (line1.Point1.Y - line2.Point1.Y) +
                     (line2.Point1.Y - line2.Point2.Y) * (line2.Point1.X - line1.Point1.X);
            var tb = (line2.Point1.X - line2.Point2.X) * (line1.Point2.Y - line2.Point2.Y) +
                     (line2.Point1.Y - line1.Point2.Y) * (line2.Point1.X - line1.Point2.X);
            var tc = (line1.Point1.X - line1.Point2.X) * (line2.Point1.Y - line1.Point1.Y) +
                     (line1.Point1.Y - line1.Point2.Y) * (line1.Point1.X - line2.Point1.X);
            var td = (line1.Point1.X - line1.Point2.X) * (line2.Point2.Y - line1.Point2.Y) +
                     (line1.Point1.Y - line1.Point2.Y) * (line1.Point1.X - line2.Point2.X);
            return ta * tb < 0 && tc * td < 0;
        }

        public double Distance(in Point point) => Distance(this, point);

        public static double Distance(in Line line, in Point point)
        {
            var (dx, dy) = (line.Point2.X - line.Point1.X, line.Point2.Y - line.Point1.Y);
            if (dx == 0) return Math.Abs(line.Point1.X - point.X);
            var m = dy / dx;
            var a = m;
            var c = line.Point1.Y - m * line.Point1.X;
            return Math.Abs(a * point.X - point.Y + c) / Math.Sqrt(a * a + 1);
        }

        public bool Equals(Line other) => Point1.Equals(other.Point1) && Point2.Equals(other.Point2);

        public override bool Equals(object obj) => obj is Line other && Equals(other);

        public override int GetHashCode() => HashCode.Combine(Point1, Point2);
    }
}