using System;
using System.IO;

namespace ZConfig.Parser
{
    public class ZConfigFileParser : IRawConfigLoader
    {
        private readonly String _filePath;

        /// <summary>
        /// Creates a parser that can read the zconfig formatted config file from disk and return a object based representation
        /// </summary>
        /// <param name="filePath">the path to the config file</param>
        public ZConfigFileParser(String filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("Could not find the specified file", filePath);
            }
            _filePath = filePath;
        }
        
        public IRawConfiguration Read()
        {
            String[] lines = File.ReadAllLines(_filePath);
            return new Configuration(lines);
        }

    }
}
