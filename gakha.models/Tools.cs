using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gakha.models
{
    public static class Tools
    {
        private static IConverter _conv;
        public static IConverter Converter { get { return _conv; } }
        private static ICache _cache;
        public static ICache Cache { get { return _cache; } }
        private static ITextReader _reader;
        public static ITextReader Reader { get { return _reader; }}


        public void Initialize(IConverter converter, ICache cache, ITextReader reader)
        {
            _conv = converter;
            _cache = cache;
            _reader = reader;
        }
        public static string FromWylie(string wylie)
        {
            return _cache.Get(wylie, _conv.FromWylie);
        }
        public static string ToWylie(string tibetan)
        {
            return _cache.Get(tibetan, _conv.ToWylie);
        }
    }
}
