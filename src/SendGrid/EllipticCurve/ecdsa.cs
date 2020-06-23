using System.Security.Cryptography;
using System.Numerics;
using System.Text;

namespace EllipticCurve
{

    public static class Ecdsa
    {

        public static bool verify(string message, Signature signature, PublicKey publicKey)
        {
            string hashMessage = sha256(message);
            BigInteger numberMessage = Utils.BinaryAscii.numberFromHex(hashMessage);
            CurveFp curve = publicKey.curve;
            BigInteger sigR = signature.r;
            BigInteger sigS = signature.s;
            BigInteger inv = EcdsaMath.inv(sigS, curve.N);

            Point u1 = EcdsaMath.multiply(
                curve.G,
                Utils.Integer.modulo((numberMessage * inv), curve.N),
                curve.N,
                curve.A,
                curve.P
            );
            Point u2 = EcdsaMath.multiply(
                publicKey.point,
                Utils.Integer.modulo((sigR * inv), curve.N),
                curve.N,
                curve.A,
                curve.P
            );
            Point add = EcdsaMath.add(
                u1,
                u2,
                curve.A,
                curve.P
            );

            return sigR == add.x;
        }

        private static string sha256(string message)
        {
            byte[] bytes;

            using (SHA256 sha256Hash = SHA256.Create())
            {
                bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(message));
            }

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }

            return builder.ToString();
        }

    }

}
