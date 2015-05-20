using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gakha.models
{
    public interface ITextReader
    {
        Text Open(string path);
        Text Open(Stream stream);
        Text Open(string path, UInt32 count);
        Text Open(Stream stream, UInt32 count);
        void FillAfter(Syllable s, UInt32 count);
        void FillBefore(Syllable s, UInt32 count);
    }
}
