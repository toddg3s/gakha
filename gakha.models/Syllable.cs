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
        private UInt32? _index = null;

        public string Wylie { get { return Get<string>("Wylie"); } set { Set<string>("Wylie", value); } }
        public string Tibetan { get { return Get<string>("Tibetan"); } set { Set<string>("Tibetan", value); } }
        public string Separator { get { return Get<string>("Separator"); } set { Set<string>("Separator", value); } }
        public bool BOT { get { return Get<bool>("BOT"); } set { Set<bool>("BOT", value); } }
        public bool EOT { get { return Get<bool>("EOT"); } set { Set<bool>("EOT", value); } }

        public Syllable Next
        {
            get { return _next; }
/*            set
            {
                if (_next == value) return;
                _next = value;
                if (_next != null)
                {
                    _next._prev = this;
                    _next.OnPropertyChanged("Prev");
                    if(EOT)
                    {
                        _next.EOT = true;
                        EOT = false;
                    }
                    if(_next.BOT)
                    {
                        _next.BOT = false;
                        BOT = true;
                    }
                }
                OnPropertyChanged("Next");
            } */
        }
        public Syllable Prev { get { return _prev; } }
        public UInt32? Index { get { return _index; } }


        public void InsertAfter(Syllable s)
        {
            var list = s.Traverse();
            if(EOT)
            {
                EOT = false;
                var currindex = _index;
                foreach(var syl in list)
                {
                    currindex += 1024;
                    syl._index = currindex;
                    if (syl.Next == null)
                        syl.EOT = true;
                }
                return;
            }
            if(_next==null)
            {
                Tools.Reader.FillAfter(this, 1);
            }
            list[list.Count - 1]._next = Next;
            list[list.Count - 1].OnPropertyChanged("Next");
            _next = list[0];
            OnPropertyChanged("Next");
            list[0]._prev = this;
            list[0].OnPropertyChanged("Prev");
            if(list.Count==1)
            {
                list[0]._index = (_index + list[list.Count - 1].Next._index) / 2;
            }
            Renumber(this, list, list[list.Count - 1].Next);
        }

        public List<Syllable> Traverse()
        {
            var list = new List<Syllable>();
            var curr = this;
            while(curr!=null)
            {
                list.Add(curr);
                curr = curr.Next;
            }
            return list;
        }

        private void Renumber(Syllable Before, List<Syllable> List, Syllable After)
        {
            if(List==null || List.Count == 0) return;
            if(Before._index==null || !Before._index.HasValue || After._index==null || !After._index.HasValue)
            {
                throw new Exception("Cannot renumber syllables in a list with no beginning or end");
            }
            
            int middleindex = (int)Math.Ceiling(Convert.ToDouble(List.Count) / 2.0d);
            var middle = List[middleindex];
            
            var before = Before._index.Value;
            var after = After._index.Value;

            middle._index = (before + after) / 2;

            var leftlist = new List<Syllable>();
            var rightlist = new List<Syllable>();
            for(var i=0;i<List.Count;i++)
            {
                if(i < middleindex) leftlist.Add(List[i]);
                if(i > middleindex) rightlist.Add(List[i]);
            }
            Renumber(Before, leftlist, middle);
            Renumber(middle, rightlist, After);
        }
    }
}
