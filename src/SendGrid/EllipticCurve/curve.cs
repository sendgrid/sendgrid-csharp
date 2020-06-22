using System.Collections.Generic;
using System.Numerics;
using System;

namespace EllipticCurve
{
    public class CurveFp
    {
        public BigInteger A { get; private set; }
        public BigInteger B { get; private set; }
        public BigInteger P { get; private set; }
        public BigInteger N { get; private set; }
        public Point G { get; private set; }
        public string name { get; private set; }
        public int[] oid { get; private set; }
        public string nistName { get; private set; }


        public CurveFp(BigInteger A, BigInteger B, BigInteger P, BigInteger N, BigInteger Gx, BigInteger Gy, string name, int[] oid, string nistName = "")
        {
            this.A = A;
            this.B = B;
            this.P = P;
            this.N = N;
            G = new Point(Gx, Gy);
            this.name = name;
            this.nistName = nistName;
            this.oid = oid;
        }

        public bool contains(Point p)
        {
            return Utils.Integer.modulo(
                BigInteger.Pow(p.y, 2) - (BigInteger.Pow(p.x, 3) + A * p.x + B),
                P
            ).IsZero;
        }

        public int length()
        {
            return N.ToString("X").Length / 2;
        }

    }

    public static class Curves
    {

        public static CurveFp getCurveByName(string name)
        {
            name = name.ToLower();

            if (name == "secp256k1")
            {
                return secp256k1;
            }
            if (name == "p256" | name == "prime256v1")
            {
                return prime256v1;
            }

            throw new ArgumentException("unknown curve " + name);
        }

        public static CurveFp secp256k1 = new CurveFp(
            Utils.BinaryAscii.numberFromHex("0000000000000000000000000000000000000000000000000000000000000000"),
            Utils.BinaryAscii.numberFromHex("0000000000000000000000000000000000000000000000000000000000000007"),
            Utils.BinaryAscii.numberFromHex("fffffffffffffffffffffffffffffffffffffffffffffffffffffffefffffc2f"),
            Utils.BinaryAscii.numberFromHex("fffffffffffffffffffffffffffffffebaaedce6af48a03bbfd25e8cd0364141"),
            Utils.BinaryAscii.numberFromHex("79be667ef9dcbbac55a06295ce870b07029bfcdb2dce28d959f2815b16f81798"),
            Utils.BinaryAscii.numberFromHex("483ada7726a3c4655da4fbfc0e1108a8fd17b448a68554199c47d08ffb10d4b8"),
            "secp256k1",
            new int[] { 1, 3, 132, 0, 10 }
        );

        public static CurveFp prime256v1 = new CurveFp(
            Utils.BinaryAscii.numberFromHex("ffffffff00000001000000000000000000000000fffffffffffffffffffffffc"),
            Utils.BinaryAscii.numberFromHex("5ac635d8aa3a93e7b3ebbd55769886bc651d06b0cc53b0f63bce3c3e27d2604b"),
            Utils.BinaryAscii.numberFromHex("ffffffff00000001000000000000000000000000ffffffffffffffffffffffff"),
            Utils.BinaryAscii.numberFromHex("ffffffff00000000ffffffffffffffffbce6faada7179e84f3b9cac2fc632551"),
            Utils.BinaryAscii.numberFromHex("6b17d1f2e12c4247f8bce6e563a440f277037d812deb33a0f4a13945d898c296"),
            Utils.BinaryAscii.numberFromHex("4fe342e2fe1a7f9b8ee7eb4a7c0f9e162bce33576b315ececbb6406837bf51f5"),
            "prime256v1",
            new int[] { 1, 2, 840, 10045, 3, 1, 7 },
            "P-256"
        );

        public static CurveFp p256 = prime256v1;

        public static CurveFp[] supportedCurves = { secp256k1, prime256v1 };

        public static Dictionary<string, CurveFp> curvesByOid = new Dictionary<string, CurveFp>() {
            {string.Join(",", secp256k1.oid), secp256k1},
            {string.Join(",", prime256v1.oid), prime256v1}
        };

    }

}
