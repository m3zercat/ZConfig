using System;

namespace ZConfigParser
{
    public class ConfigurationParseException : Exception
    {
        internal ConfigurationParseException(String message, String[] parts)
            : base(message + "[" + String.Join(" :: ", parts) + "]")
        {
        }
    }
}