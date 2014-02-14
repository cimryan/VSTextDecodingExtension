// Guids.cs
// MUST match guids.h
using System;

namespace DecodeText
{
    static class GuidList
    {
        public const string guidDecodeTextPkgString = "bb4edf50-0d0a-41c8-b4b4-47865cdb2575";
        public const string guidDecodeTextCmdSetString = "e5006bed-f878-4b0a-a453-ace1a750fc32";

        public static readonly Guid guidDecodeTextCmdSet = new Guid(guidDecodeTextCmdSetString);
    };
}