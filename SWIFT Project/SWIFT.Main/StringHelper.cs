using System.Linq;

namespace SWIFT.Main
{
    public static class StringHelper
    {
        public static string BetweenStrings(string value, string start, string end)
        {
            int startIndex = value.IndexOf(start);
            int endIndex = value.IndexOf(end, startIndex);
            if (startIndex == -1)
            {
                return string.Empty;
            }

            if (endIndex == -1)
            {
                return string.Empty;
            }

            int updatedStartIndex = startIndex + start.Length;
            if (updatedStartIndex >= endIndex)
            {
                return string.Empty;
            }

            return value.Substring(updatedStartIndex, endIndex - updatedStartIndex).Trim();

        }

        public static int SumLengths(params string[] words)
        {
            return words.Sum(x => x.Length);
        }
    }
}
