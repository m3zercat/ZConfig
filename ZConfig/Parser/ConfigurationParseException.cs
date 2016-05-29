using System;

namespace ZConfig.Parser
{
    public class ConfigurationParseException : Exception
    {
        internal ConfigurationParseException(String message, String[] parts)
            : base(message + "[" + String.Join(" :: ", parts) + "]")
        {
        }
    }
}