using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace ZConfig
{
    public interface IConfiguration
    {
        /// <summary>
        /// Contains the currently active configuration section
        /// </summary>
        IConfigSection ActiveConfig { get; }

        /// <summary>
        /// Contains all config sections that have been loaded including the current active configuration
        /// </summary>
        IDictionary<String, IConfigSection> AllSections { get; }
        
        /// <summary>
        /// Retrieves a setting from the active configuration
        /// </summary>
        /// <param name="key">the name of the setting</param>
        /// <returns>the setting value</returns>
        String this[String key] { get; }

        /// <summary>
        /// Gets a setting from the active configuration by name in the particular type specified by T
        /// If the setting cannot be converted or cast to T then an exception is thrown
        /// </summary>
        /// <typeparam name="T">the type of the setting value expected</typeparam>
        /// <param name="name">the name of the setting</param>
        /// <returns>the setting value</returns>
        T Get<T>(String name);
        
        /// <summary>
        /// Allows a setting from the active configuration to be overridden in code
        /// </summary>
        /// <param name="name">the name of the setting</param>
        /// <param name="value">the new overriding value</param>
        void Override(String name, String value);
    }
}