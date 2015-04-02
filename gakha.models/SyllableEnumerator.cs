using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gakha.models
{
    class SyllableEnumerator : IEnumerator<Syllable>
    {
        private Syllable _current = null;
        private Syllable _first = null;
        private Syllable _last = null;

        public SyllableEnumerator(Syllable first, Syllable Last = null)
        {
            if (first == null)
                throw new ArgumentException("Cannot establish a syllable enumerator based on a null syllable.","first");
            _first = first;
            _last = Last;
        }

        public SyllableEnumerator(Syllable first, int count)
        {
            if (first == null)
                throw new ArgumentException("Cannot establish a syllable enumerator based on a null syllable.","first");
            _first = first;

            if(count < 1)
                throw new ArgumentException("Invalid count value specified (" + count.ToString() + ")","count");

            for(var i=1;i<count;i++)
            {

            }
        }

        public Syllable Current
        {
            get { return _current; }
        }

        public void Dispose() {  }

        object System.Collections.IEnumerator.Current
        {
            get { return _current; }
        }

        public bool MoveNext()
        {
            if (_current == null)
            {
                _current = _first;
                return true;
            }

            if(_current.Next == null && _current == _last) return false;

            _current = _current.Next;
            return true;
        }

        public void Reset()
        {
            _current = null;
        }
    }
}
