using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gakha.models
{
    public interface ICache
    {
        string Get(string name);
        string Get(string name, Func<string, string> populate);
        bool Contains(string name);
        void Set(string name, string value);
    }
}
