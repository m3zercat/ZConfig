using System;
using System.Collections.Generic;

namespace ZConfig
{
    public interface IRawConfiguration : IDictionary<String, IRawConfigSection>
    {
        
    }
}