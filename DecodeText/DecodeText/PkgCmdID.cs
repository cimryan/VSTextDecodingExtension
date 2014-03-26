// PkgCmdID.cs
// MUST match PkgCmdID.h
using System;

namespace DecodeText
{
    static class PkgCmdIDList
    {
        public const uint cmdidDecodeCSharpEscapeSequencesSelected = 0x110;
        public const uint cmdidDecodeHTMLCharacterEntitiesSelected = 0x120;
        public const uint cmdidDecodeURLEscapeSequencesSelected = 0x130;

        public const uint cmdidDecodeCSharpEscapeSequencesOnClipboard = 0x140;
        public const uint cmdidDecodeHTMLCharacterEntitiesOnClipboard = 0x150;
        public const uint cmdidDecodeURLEscapeSequencesOnClipboard = 0x160;

    };
}