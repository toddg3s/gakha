using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gakha.models
{
    public interface IConverter
    {
        string FromWylie(string wylie);
        string ToWylie(string tibetan);
    }
}
