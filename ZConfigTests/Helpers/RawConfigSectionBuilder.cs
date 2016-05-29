using System;
using System.Collections.Generic;
using NSubstitute;

namespace ZConfig.Helpers
{
    internal class RawConfigSectionBuilder
    {
        private String _name;
        private String _inherits;
        private Dictionary<String, String> _lines;

        public RawConfigSectionBuilder()
        {
            Reset();
        }

        public void Reset()
        {
            _name = "DefaultSectionName";
            _inherits = null;
            _lines = new Dictionary<String, String>();
        }

        public RawConfigSectionBuilder WithName(String name)
        {
            _name = name;
            return this;
        }

        public RawConfigSectionBuilder WithInheritanceFrom(String name)
        {
            _inherits = name;
            return this;
        }

        public RawConfigSectionBuilder WithConfigLine(String name, String value)
        {
            _lines.Add(name, value);
            return this;
        }
        
        public IRawConfigSection Build()
        {
            IRawConfigSection config = Substitute.For<IRawConfigSection>();
            config.Name.Returns(_name);
            config.InheritsFromSection.Returns(_inherits);
            config.Lines.Returns(new Dictionary<String, String>(_lines));
            return config;
        }
    }
}
