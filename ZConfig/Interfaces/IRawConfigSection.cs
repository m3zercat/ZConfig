using System;
using System.Collections.Generic;

namespace ZConfig
{
    public interface IRawConfigSection
    {
        String InheritsFromSection { get; }
        IDictionary<String, String> Lines { get; }
    }
}