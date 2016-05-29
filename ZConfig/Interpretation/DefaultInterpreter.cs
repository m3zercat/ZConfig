using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZConfig.Interpretation
{
    public class DefaultInterpreter : IConfigInterpreter
    {
        public IConfiguration Interpret(IRawConfiguration rawConfig, String activeSection)
        {
            Dictionary<String, IConfigSection> sections = new Dictionary<String, IConfigSection>();
            IEnumerable<KeyValuePair<String, IRawConfigSection>> roots = rawConfig.Where(x => x.Value.InheritsFromSection == null).ToList();
            foreach (KeyValuePair<String, IRawConfigSection> root in roots)
            {
                rawConfig.Remove(root.Key);
                sections.Add(root.Key, InterpretConfigSection(root.Value));
            }
            while (rawConfig.Any())
            {
                List<KeyValuePair<String, IRawConfigSection>> rawSections = rawConfig.Where(x => sections.ContainsKey(x.Value.InheritsFromSection)).ToList();
                if (!rawSections.Any())
                {
                    throw new Exception("some sections appear to have incorrect inheritance");
                }
                foreach (KeyValuePair<String, IRawConfigSection> section in rawSections)
                {
                    sections.Add(section.Key, InterpretConfigSection(section.Value, sections[section.Value.InheritsFromSection]));
                }
            }
            return new Configuration(sections, activeSection);
        }

        private IConfigSection InterpretConfigSection(IRawConfigSection value, IConfigSection parent = null)
        {
            return null;
        }
    }
}
