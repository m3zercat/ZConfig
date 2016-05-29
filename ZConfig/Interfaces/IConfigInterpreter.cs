using System;

namespace ZConfig
{
    public interface IConfigInterpreter
    {
        IConfiguration Interpret(IRawConfiguration config, String activeSection);
    }
}
