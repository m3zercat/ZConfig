using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;

namespace ZConfig.Helpers
{
    internal class RawConfigurationBuilder
    {
        internal class TestRawConfiguration : Dictionary<String, IRawConfigSection>, IRawConfiguration
        {
            
        }

        private IRawConfiguration config;

        public RawConfigurationBuilder()
        {
            Reset();
        }

        public void Reset()
        {
            config = new TestRawConfiguration();
        }

        public RawConfigurationBuilder WithSection(IRawConfigSection section)
        {
            config.Add(section.Name, section);
            return this;
        }

        public IRawConfiguration Build()
        {
            return config;
        }
    }
}
