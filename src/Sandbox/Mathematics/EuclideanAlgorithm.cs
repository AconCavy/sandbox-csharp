namespace Sandbox.Mathematics
{
    public static partial class Mathematics
    {
        public static long Gcd(long a, long b) => b == 0 ? a : Gcd(b, a % b);
        public static long Lcm(long a, long b) => a / Gcd(a, b) * b;

        public static long ExtGcd(long a, long b, out long x, out long y)
        {
            if (b == 0)
            {
                x = 1;
                y = 0;
                return a;
            }

            var d = ExtGcd(b, a % b, out y, out x);
            y -= a / b * x;
            return d;
        }
    }
}