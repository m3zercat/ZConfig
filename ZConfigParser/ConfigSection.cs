using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZConfigParser
{
    public class ConfigSection
    {
        public String Name { get; }

        public String InheritsFromSection { get; } = null;

        public Dictionary<String, String> Lines { get; } = new Dictionary<String, String>();

        public ConfigSection(IEnumerable<String> sectionConfig)
        {
            var sectionDefinition = sectionConfig.First().Trim().Trim('[',']').Split(new [] {':'}, StringSplitOptions.RemoveEmptyEntries);
            Name = sectionDefinition.First();
            if (sectionDefinition.Length == 2)
            {
                InheritsFromSection = sectionDefinition[1];
            }

            // for the rest of the lines

        }

    }
}
