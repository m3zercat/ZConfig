using System;

namespace ZConfig.Parser
{
    public class ConfigurationParseException : Exception
    {
        public ConfigurationParseException(String message, Exception innerException) : base(message, innerException)
        {
        }

        internal ConfigurationParseException(String message, String[] parts)
            : base(message + "[" + String.Join(" :: ", parts) + "]")
        {
        }
    }
}