﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace ZConfig.Parser
{
    public class ConfigSection : IRawConfigSection
    {
        public String Name { get; }

        public String InheritsFromSection { get; } = null;

        public IDictionary<String, String> Lines { get; } = new Dictionary<String, String>();

        internal ConfigSection(IEnumerable<String> sectionConfig)
        {
            List<String> sectionConfigStrings = sectionConfig.ToList();
            Int32 index = 0;
            String[] sectionDefinition = sectionConfigStrings[index++].Trim().Trim('[',']').Split(new [] {':'}, StringSplitOptions.RemoveEmptyEntries);
            Name = sectionDefinition.First();
            if (sectionDefinition.Length == 2)
            {
                InheritsFromSection = sectionDefinition[1];
            }

            while (index < sectionConfigStrings.Count)
            {
                String line = sectionConfigStrings[index++];
                if (!LineIsValid(line)) continue;
                String[] parts = line.Split(new[] {'='}, 2, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length != 2)
                {
                    throw new ConfigurationParseException("Incorrect number of string parts from split!", parts);
                }
                String variableName = CleanVariableName(parts[0]);
                String variableValue = CleanVariableValue(parts[1]);
                try
                {
                    Lines.Add(variableName, variableValue);
                }
                catch (ArgumentException ae)
                {
                    if (ae.Message.Contains("An item with the same key has already been added."))
                    {
                        throw new ConfigurationParseException($"Duplicate variable '{variableName}' in section '{Name}'", ae);
                    }
                    throw;
                }
            }

        }

        private static String CleanVariableValue(String part)
        {
            part = part.Trim();
            if (part.StartsWith("\"") && part.EndsWith("\""))
            {
                part = part.Substring(1, part.Length - 2);
            }
            if (part.StartsWith("'") && part.EndsWith("'"))
            {
                part = part.Substring(1, part.Length - 2);
            }
            return part;
        }

        private static String CleanVariableName(String part)
        {
            part = part.Trim();
            return part;
        }

        private static Boolean LineIsValid(String line)
        {
            String trimmedLine = line.Trim();
            return line.Contains("=")
                   && !trimmedLine.StartsWith("#")
                   && !trimmedLine.StartsWith("=")
                   && !trimmedLine.EndsWith("=");
                ;
        }
    }
}
