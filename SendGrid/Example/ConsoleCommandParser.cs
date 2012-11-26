using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example
{
    public static class ConsoleCommandParser
    {
        public static Int32 PromptInt32(string prompt, Int32 defaultValue)
        {
            Console.Write(prompt);
            String sVal = Console.ReadLine();
            Int32 val;
            if (int.TryParse(sVal, out val))
                return val;

            return defaultValue;
        }
        public static Boolean PromptBoolean(string prompt, Boolean defaultValue)
        {
            Console.Write(prompt);
            String sVal = Console.ReadLine();
            Boolean val;
            if (sVal.ToLower().StartsWith("y"))
                return true;
            else if (sVal.ToLower().StartsWith("n"))
                return false;
            else if (Boolean.TryParse(sVal, out val))
                return val;

            return defaultValue;
        }
        public static String PromptString(string prompt, String defaultValue)
        {
            Console.Write(prompt);
            String sVal = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(sVal))
                return defaultValue;
            return sVal;
        }
        public static DateTime? PromptDate(string prompt, DateTime? defaultValue)
        {
            Console.Write(prompt);
            String sVal = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(sVal))
                return defaultValue;

            DateTime parsed;
            if (DateTime.TryParse(sVal, out parsed))
                return parsed;
            return defaultValue;
        }
        public static List<String> PromptDelimitedList(string prompt, string delimiter)
        {
            return PromptDelimitedList(prompt, new string[] { delimiter });
        }
        public static List<String> PromptDelimitedList(string prompt, string[] delimiter)
        {
            List<String> vals = new List<string>();
            Console.Write(prompt);
            String sVal = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(sVal))
                return vals;

            var list = sVal.Split(delimiter, StringSplitOptions.RemoveEmptyEntries);
            vals.AddRange(list);
            return vals;
        }
    }
}
