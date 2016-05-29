using System;
using System.Collections.Generic;

namespace ZConfig
{
    internal class Configuration : IConfiguration
    {
        public Configuration(IDictionary<String, IConfigSection> sections, String activeSection)
        {
            AllSections = sections;
            ActiveConfig = AllSections[activeSection];
        }

        public IConfigSection ActiveConfig { get; }
        public IDictionary<String, IConfigSection> AllSections { get; }

        public String this[String key] => ActiveConfig[key];

        public T Get<T>(String name)
        {
            throw new NotImplementedException();
            //return default(T);
        }

        public void Override(String name, String value)
        {
            ActiveConfig.Override(name, value);
        }
    }
}
