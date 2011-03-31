// Guids.cs
// MUST match guids.h
using System;

namespace OpenSource.CcdAddIn
{
    static class GuidList
    {
        public const string guidCcdAddInPkgString = "1ee21130-cda5-4ed4-854b-c44aeef87421";
        public const string guidCcdAddInCmdSetString = "7ef0ea02-3f7b-4fad-8e69-1e0726ea3c79";
        public const string guidToolWindowPersistanceString = "f76daf6f-5ac3-47a8-87d4-19c3136ef40f";

        public static readonly Guid guidCcdAddInCmdSet = new Guid(guidCcdAddInCmdSetString);
    };
}