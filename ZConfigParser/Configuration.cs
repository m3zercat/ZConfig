using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZConfigParser
{
    public class Configuration : List<ConfigSection>
    {
        public Configuration(String[] configFileContent) : base()
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
                    endIndex = startPoints[i + 1] - 1;
                }
                String[] sectionContent = new String[endIndex-startIndex];
                Array.Copy(configFileContent, startIndex, sectionContent, 0, endIndex - startIndex);
                this.Add(new ConfigSection(sectionContent));
            }
            
        }
    }
}
