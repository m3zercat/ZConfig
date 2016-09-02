using System;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;

namespace ZConfig.Simplistic
{
    public class LocalConfig<T> : IDisposable where T : class, new()
    {
        private readonly string _configFilePath;
        public LocalConfig(string configFilePath=null)
        {
            var isSerializable = Attribute.GetCustomAttribute(typeof(T), typeof(SerializableAttribute)) != null;

            if (!isSerializable)
            {
                throw new Exception($"{typeof(T).Name} is not marked as serializable and cannot be used with SLC");
            }

            if (!string.IsNullOrWhiteSpace(configFilePath))
            {
                _configFilePath = configFilePath;
            }
            else
            {
                var name = Assembly.GetEntryAssembly().GetName().Name;
                _configFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), name, "config.json");
            }
            
            if (!File.Exists(_configFilePath))
            {
                EnsurePathExists();
                WriteOutConfig();
            }

            ReadInConfig();
        }

        public T Instance = new T();

        public void Dispose()
        {
            WriteOutConfig();
            Instance = null;
        }

        private void EnsurePathExists()
        {
            var dir = Path.GetDirectoryName(_configFilePath);
            if (dir == null)
            {
                throw new Exception("Config dir is null!");
            }
            Directory.CreateDirectory(dir);
        }

        private void WriteOutConfig()
        {
            var serialTxt = JsonConvert.SerializeObject(Instance, Formatting.Indented);
            File.WriteAllText(_configFilePath, serialTxt);
        }

        private void ReadInConfig()
        {
            var serialTxt = File.ReadAllText(_configFilePath);
            Instance = JsonConvert.DeserializeObject<T>(serialTxt);
        }
    }
}
