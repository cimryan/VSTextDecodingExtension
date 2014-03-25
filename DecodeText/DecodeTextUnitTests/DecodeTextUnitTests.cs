using System;
using FluentAssertions;
using Xunit;
using Xunit.Extensions;
using System.Text.RegularExpressions;
using System.Globalization;

namespace DecodeText
{
    public class DecodeTextUnitTests
    {
        [Theory]
        [InlineData(@"\'", "'")]
        [InlineData("\\\"", "\"")]
        [InlineData("\\\\", "\\")]
        [InlineData("\\0", "\0")]
        [InlineData("\\a", "\a")]
        [InlineData("\\b", "\b")]
        [InlineData("\\f", "\f")]
        [InlineData("\\n", "\n")]
        [InlineData("\\r", "\r")]
        [InlineData("\\t", "\t")]
        [InlineData("\\v", "\v")]
        [InlineData("\\u0041", "A")]
        [InlineData("\\x41", "A")]
        [InlineData("\\U00000041", "A")]
        [InlineData("\\u005C\\a", "\\\a")]
        public void SingleCharacterEscapesAreReplaced(string encodedString, string decodedString)
        {
            TextDecoding.DecodeCSharpEncodedString(encodedString)
                .ShouldBeEquivalentTo(decodedString);
        }

        [Theory]
        [InlineData(@"\'\'", "''")]
        [InlineData("\\\"\\\"", "\"\"")]
        [InlineData("\\\\\\\\", "\\\\")]
        [InlineData("\\0\\0", "\0\0")]
        [InlineData("\\a\\a", "\a\a")]
        [InlineData("\\b\\b", "\b\b")]
        [InlineData("\\f\\f", "\f\f")]
        [InlineData("\\n\\n", "\n\n")]
        [InlineData("\\r\\r", "\r\r")]
        [InlineData("\\t\\t", "\t\t")]
        [InlineData("\\v\\v", "\v\v")]
        [InlineData("\\u0041\\u0041", "AA")]
        [InlineData("\\x41\\x41", "AA")]
        [InlineData("\\U00000041\\U00000041", "AA")]
        public void DoubledCharacterEscapesAreReplaced(string encodedString, string decodedString)
        {
            TextDecoding.DecodeCSharpEncodedString(encodedString)
                .ShouldBeEquivalentTo(decodedString);
        }

        [Theory]
        [InlineData("\\u005C\\a", "\\\a")]
        public void Replacements_are_not_reevaluated(string encodedString, string decodedString)
        {
            TextDecoding.DecodeCSharpEncodedString(encodedString)
                .ShouldBeEquivalentTo(decodedString);
        }
    }
}
