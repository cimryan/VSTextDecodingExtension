// PkgCmdID.cs
// MUST match PkgCmdID.h
using System;

namespace DecodeText
{
    static class PkgCmdIDList
    {
        public const uint cmdidDecodeCSharpEscapeSequences = 0x104;
        public const uint cmdidDecodeHTMLCharacterEntities = 0x105;
        public const uint cmdidDecodeURLEscapeSequences = 0x110;

        public const uint cmdidDecodeCSharpEscapeSequencesOnClipboard = 0x120;
        public const uint cmdidDecodeHTMLCharacterEntitiesOnClipboard = 0x125;
        public const uint cmdidDecodeURLEscapeSequencesOnClipboard = 0x130;

    };
}