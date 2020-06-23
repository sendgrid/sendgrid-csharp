using System.Collections.Generic;
using System.Numerics;
using System;
using System.Text;

namespace EllipticCurve.Utils
{

    public static class Der
    {

        private static readonly int hex31 = 0x1f;
        private static readonly int hex127 = 0x7f;
        private static readonly int hex129 = 0xa0;
        private static readonly int hex160 = 0x80;
        private static readonly int hex224 = 0xe0;

        private static readonly string hexAt = "00";
        private static readonly string hexB = "02";
        private static readonly string hexC = "03";
        private static readonly string hexD = "04";
        private static readonly string hexF = "06";
        private static readonly string hex0 = "30";

        public static Tuple<byte[], byte[]> removeSequence(byte[] bytes)
        {
            checkSequenceError(bytes, hex0, "30");

            Tuple<int, int> readLengthResult = readLength(Bytes.sliceByteArray(bytes, 1));
            int length = readLengthResult.Item1;
            int lengthLen = readLengthResult.Item2;

            int endSeq = 1 + lengthLen + length;

            return new Tuple<byte[], byte[]>(
                Bytes.sliceByteArray(bytes, 1 + lengthLen, length),
                Bytes.sliceByteArray(bytes, endSeq)
            );
        }

        public static Tuple<BigInteger, byte[]> removeInteger(byte[] bytes)
        {
            checkSequenceError(bytes, hexB, "02");

            Tuple<int, int> readLengthResult = readLength(Bytes.sliceByteArray(bytes, 1));
            int length = readLengthResult.Item1;
            int lengthLen = readLengthResult.Item2;

            byte[] numberBytes = Bytes.sliceByteArray(bytes, 1 + lengthLen, length);
            byte[] rest = Bytes.sliceByteArray(bytes, 1 + lengthLen + length);
            int nBytes = numberBytes[0];

            if (nBytes >= hex160)
            {
                throw new ArgumentException("nBytes must be < 160");
            }

            return new Tuple<BigInteger, byte[]>(
                BinaryAscii.numberFromHex(BinaryAscii.hexFromBinary(numberBytes)),
                rest
            );

        }

        public static Tuple<int[], byte[]> removeObject(byte[] bytes)
        {
            checkSequenceError(bytes, hexF, "06");

            Tuple<int, int> readLengthResult = readLength(Bytes.sliceByteArray(bytes, 1));
            int length = readLengthResult.Item1;
            int lengthLen = readLengthResult.Item2;

            byte[] body = Bytes.sliceByteArray(bytes, 1 + lengthLen, length);
            byte[] rest = Bytes.sliceByteArray(bytes, 1 + lengthLen + length);

            List<int> numbers = new List<int>();
            Tuple<int, int> readNumberResult;
            while (body.Length > 0)
            {
                readNumberResult = readNumber(body);
                numbers.Add(readNumberResult.Item1);
                body = Bytes.sliceByteArray(body, readNumberResult.Item2);
            }

            int n0 = numbers[0];
            numbers.RemoveAt(0);

            int first = n0 / 40;
            int second = n0 - (40 * first);
            numbers.Insert(0, first);
            numbers.Insert(1, second);

            return new Tuple<int[], byte[]>(
                numbers.ToArray(),
                rest
            );
        }

        public static Tuple<byte[], byte[]> removeBitString(byte[] bytes)
        {
            checkSequenceError(bytes, hexC, "03");

            Tuple<int, int> readLengthResult = readLength(Bytes.sliceByteArray(bytes, 1));
            int length = readLengthResult.Item1;
            int lengthLen = readLengthResult.Item2;

            byte[] body = Bytes.sliceByteArray(bytes, 1 + lengthLen, length);
            byte[] rest = Bytes.sliceByteArray(bytes, 1 + lengthLen + length);

            return new Tuple<byte[], byte[]>(body, rest);
        }

        public static byte[] fromPem(string pem)
        {
            string[] split = pem.Split(new string[] { "\n" }, StringSplitOptions.None);
            List<string> stripped = new List<string>();

            for (int i = 0; i < split.Length; i++)
            {
                string line = split[i].Trim();
                if (String.substring(line, 0, 5) != "-----")
                {
                    stripped.Add(line);
                }
            }

            return Base64.decode(string.Join("", stripped));
        }

        public static byte[] combineByteArrays(List<byte[]> byteArrayList)
        {
            int totalLength = 0;
            foreach (byte[] bytes in byteArrayList)
            {
                totalLength += bytes.Length;
            }

            byte[] combined = new byte[totalLength];
            int consumedLength = 0;

            foreach (byte[] bytes in byteArrayList)
            {
                Array.Copy(bytes, 0, combined, consumedLength, bytes.Length);
                consumedLength += bytes.Length;
            }

            return combined;
        }

        private static Tuple<int, int> readLength(byte[] bytes)
        {
            int num = extractFirstInt(bytes);

            if ((num & hex160) == 0)
            {
                return new Tuple<int, int>(num & hex127, 1);
            }

            int lengthLen = num & hex127;

            if (lengthLen > bytes.Length - 1)
            {
                throw new ArgumentException("ran out of length bytes");
            }

            return new Tuple<int, int>(
                int.Parse(
                    BinaryAscii.hexFromBinary(Bytes.sliceByteArray(bytes, 1, lengthLen)),
                    System.Globalization.NumberStyles.HexNumber
                ),
                1 + lengthLen
            );
        }

        private static Tuple<int, int> readNumber(byte[] str)
        {
            int number = 0;
            int lengthLen = 0;
            int d;

            while (true)
            {
                if (lengthLen > str.Length)
                {
                    throw new ArgumentException("ran out of length bytes");
                }

                number <<= 7;
                d = str[lengthLen];
                number += (d & hex127);
                lengthLen += 1;
                if ((d & hex160) == 0)
                {
                    break;
                }
            }

            return new Tuple<int, int>(number, lengthLen);
        }

        private static void checkSequenceError(byte[] bytes, string start, string expected)
        {
            if (BinaryAscii.hexFromBinary(bytes).Substring(0, start.Length) != start)
            {
                throw new ArgumentException(
                    "wanted sequence " +
                    expected.Substring(0, 2) +
                    ", got " +
                    extractFirstInt(bytes).ToString("X")
                );
            }
        }

        private static int extractFirstInt(byte[] str)
        {
            return str[0];
        }


    }
}
