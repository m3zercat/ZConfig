using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZConfig.Interpretation
{
    public class DefaultInterpreter : IConfigInterpreter
    {
        public IConfiguration Interpret(IRawConfiguration config, String activeSection)
        {
            throw new NotImplementedException();
        }
    }
}
