using System;

namespace SandboxCSharp
{
    public readonly struct Line
    {
        public readonly double Sx;
        public readonly double Sy;
        public readonly double Tx;
        public readonly double Ty;
        public double Length => Math.Sqrt((Tx - Sx) * (Tx - Sx) + (Ty - Sy) * (Ty - Sy));

        public Line(double sx, double sy, double tx, double ty) => (Sx, Sy, Tx, Ty) = (sx, sy, tx, ty);

        public bool Intersect(in Line other) => Intersect(this, other);

        public static bool Intersect(in Line a, in Line b)
        {
            var ta = (b.Sx - b.Tx) * (a.Sy - b.Sy) + (b.Sy - b.Ty) * (b.Sx - a.Sx);
            var tb = (b.Sx - b.Tx) * (a.Ty - b.Sy) + (b.Sy - a.Ty) * (b.Sx - a.Tx);
            var tc = (a.Sx - a.Tx) * (b.Sy - a.Sy) + (a.Sy - a.Ty) * (a.Sx - b.Sx);
            var td = (a.Sx - a.Tx) * (b.Ty - a.Sy) + (a.Sy - a.Ty) * (a.Sx - b.Tx);
            return ta * tb < 0 && tc * td < 0;
        }
    }
}