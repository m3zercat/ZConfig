using System;
using ZConfig.Interpretation;
using ZConfig.Parser;

namespace ZConfig
{
    public static class ConfigManager
    {
        public static IConfiguration Config { get; set; }

        //private static String _configFile = @".\config.zcf";

        public static void SetConfigFile(String configZcf)
        {
            //_configFile = configZcf;
            //_loader = new ZConfigFileParser(_configFile);
            throw new NotImplementedException();
        }

        public static IConfigInterpreter Interpreter { get; set; } = new DefaultInterpreter();

        //public static void SetInterpreter(IConfigInterpreter interpreter)
        //{
        //    _interpreter = interpreter;
        //}

        //private static IRawConfigLoader _loader = new ZConfigFileParser(_configFile);

        //public static void SetRawConfigLoader(IRawConfigLoader loader)
        //{
        //    _loader = loader;
        //}

    }
}
