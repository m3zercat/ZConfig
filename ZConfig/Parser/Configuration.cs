using System;
using System.Collections.Generic;

namespace ZConfig.Parser
{
    public class Configuration : Dictionary<String, IRawConfigSection>, IRawConfiguration
    {
        internal Configuration(String[] configFileContent) : base()
        {
            List<Int32> startPoints = new List<Int32>();
            for (Int32 i = 0; i < configFileContent.Length; i++)
            {
                String line = configFileContent[i].Trim();
                if (line.StartsWith("[") && line.EndsWith("]"))
                {
                    startPoints.Add(i);
                }
            }

            for (Int32 i = 0; i < startPoints.Count; i++)
            {
                Int32 startIndex = startPoints[i];
                Int32 endIndex = configFileContent.Length;
                if (startPoints.Count-1 > i)
                {
                    endIndex = startPoints[i + 1];
                }
                Int32 sectionLength = endIndex - startIndex;
                String[] sectionContent = new String[sectionLength];
                Array.Copy(configFileContent, startIndex, sectionContent, 0, sectionLength);
                ConfigSection section = new ConfigSection(sectionContent);
                this.Add(section.Name, section);
            }
        }
    }
}
