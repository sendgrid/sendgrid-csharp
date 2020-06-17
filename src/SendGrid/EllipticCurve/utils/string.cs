namespace EllipticCurve.Utils
{

    public static class String
    {

        public static string substring(string str, int index, int length)
        {
            if (str.Length > index + length)
            {
                return str.Substring(index, length);
            }
            if (str.Length > index)
            {
                return str.Substring(index);
            }
            return "";
        }

    }

}
