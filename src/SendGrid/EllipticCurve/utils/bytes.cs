using System;


namespace EllipticCurve.Utils
{

    public static class Bytes
    {

        public static byte[] sliceByteArray(byte[] bytes, int start)
        {
            int newLength = bytes.Length - start;
            byte[] result = new byte[newLength];
            Array.Copy(bytes, start, result, 0, newLength);
            return result;
        }

        public static byte[] sliceByteArray(byte[] bytes, int start, int length)
        {
            int newLength = Math.Min(bytes.Length - start, length);
            byte[] result = new byte[newLength];
            Array.Copy(bytes, start, result, 0, newLength);
            return result;
        }

    }

}
