using System;

namespace ClusterSkimmer
{
    internal static class ExtensionMethods
    {
        public static string RemoveFirstWord(this string s)
        {
            s = s.Trim();

            var firstWord = s.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            return s[(firstWord.Length + 1)..];
        }
    }
}