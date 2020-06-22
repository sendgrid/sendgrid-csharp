using System.Numerics;
using System;

namespace EllipticCurve
{

    public class Signature
    {

        public BigInteger r { get; }
        public BigInteger s { get; }

        public Signature(BigInteger r, BigInteger s)
        {
            this.r = r;
            this.s = s;
        }

        public static Signature fromDer(byte[] bytes)
        {
            Tuple<byte[], byte[]> removeSequence = Utils.Der.removeSequence(bytes);
            byte[] rs = removeSequence.Item1;
            byte[] removeSequenceTrail = removeSequence.Item2;

            if (removeSequenceTrail.Length > 0)
            {
                throw new ArgumentException("trailing junk after DER signature: " + Utils.BinaryAscii.hexFromBinary(removeSequenceTrail));
            }

            Tuple<BigInteger, byte[]> removeInteger = Utils.Der.removeInteger(rs);
            BigInteger r = removeInteger.Item1;
            byte[] rest = removeInteger.Item2;

            removeInteger = Utils.Der.removeInteger(rest);
            BigInteger s = removeInteger.Item1;
            byte[] removeIntegerTrail = removeInteger.Item2;

            if (removeIntegerTrail.Length > 0)
            {
                throw new ArgumentException("trailing junk after DER numbers: " + Utils.BinaryAscii.hexFromBinary(removeIntegerTrail));
            }

            return new Signature(r, s);

        }

        public static Signature fromBase64(string str)
        {
            return fromDer(Utils.Base64.decode(str));
        }

    }

}
