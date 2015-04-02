using org.rigpa.wylie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gakha.common
{
    public static class Tools
    {
        private static Wylie _conv;
        public static Wylie Converter { get { return _conv ?? (_conv = new Wylie()); } }

        public static string FromWylie(string wylie) { return Converter.fromWylie(wylie); }  // placeholders for now.  Will add caching later.
        public static string ToWylie(string tibetan) { return Converter.toWylie(tibetan); }
    }
}
