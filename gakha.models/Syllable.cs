using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gakha.models
{
    public class Syllable : gakhaObject
    {
        private Syllable _prev = null;
        private Syllable _next = null;
        public string Wylie { get { return Get<string>("Wylie"); } set { Set<string>("Wylie", value); } }
        public string Tibetan { get { return Get<string>("Tibetan"); } set { Set<string>("Tibetan", value); } }
        public string Separator { get { return Get<string>("Separator"); } set { Set<string>("Separator", value); } }

        public Syllable Next
        {
            get { return _next; }
            set
            {
                if (_next == value) return;
                _next = value;
                if (_next != null)
                    _next._prev = this;
                OnPropertyChanged("Next");
            }
        }
        public Syllable Prev { get { return _prev; } }
    }
}
