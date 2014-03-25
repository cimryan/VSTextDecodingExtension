using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace DecodeText
{
    internal static class TextDecoding
    {
        internal static string DecodeCSharpEncodedString(string encodedString)
        {
            string a = encodedString
                .Replace(@"\'", "'")
                .Replace("\\\"", "\"")
                .Replace("\\\\", "\\")
                .Replace("\\0", "\0")
                .Replace("\\a", "\a")
                .Replace("\\b", "\b")
                .Replace("\\f", "\f")
                .Replace("\\n", "\n")
                .Replace("\\r", "\r")
                .Replace("\\t", "\t")
                .Replace("\\v", "\v");

            string b = fourHexDigitUnicodeCharacterEscape.Replace(
                a,
                new MatchEvaluator(ReplaceHexStringInMatch));

            string c = variableLengthHexDigitUnicodeCharacterEscape.Replace(
                b,
                new MatchEvaluator(ReplaceHexStringInMatch));

            return eightHexDigitUnicodeCharacterEscape.Replace(
                c,
                new MatchEvaluator(ReplaceHexStringInMatch));
        }

        static Regex fourHexDigitUnicodeCharacterEscape = new Regex("\\\\u(?<hex>[0-9A-Fa-f]{4})");

        static Regex variableLengthHexDigitUnicodeCharacterEscape = new Regex("\\\\x(?<hex>[0-9A-Fa-f]{1,4})");

        static Regex eightHexDigitUnicodeCharacterEscape = new Regex("\\\\U(?<hex>[0-9A-Fa-f]{8})");

        static string ReplaceHexStringInMatch(Match m)
        {
            Group g = m.Groups["hex"];

            if (g.Success)
            {
                Int64 parsed;

                if (Int64.TryParse(g.Value, NumberStyles.HexNumber, null, out parsed))
                {
                    return Convert.ToChar(parsed).ToString();
                }
            }

            return g.Value;
        }

    }
}
