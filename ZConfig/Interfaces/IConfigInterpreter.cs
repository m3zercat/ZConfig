using System;

namespace ZConfig
{
    public interface IConfigInterpreter
    {
        IConfiguration Interpret(IRawConfiguration rawConfig, String activeSection);
    }
}
