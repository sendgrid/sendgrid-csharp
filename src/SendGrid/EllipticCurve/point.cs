using System.Numerics;


namespace EllipticCurve
{

    public class Point
    {

        public BigInteger x { get; }
        public BigInteger y { get; }
        public BigInteger z { get; }

        public Point(BigInteger x, BigInteger y, BigInteger? z = null)
        {
            BigInteger zeroZ = z ?? BigInteger.Zero;

            this.x = x;
            this.y = y;
            this.z = zeroZ;
        }
    }
}
