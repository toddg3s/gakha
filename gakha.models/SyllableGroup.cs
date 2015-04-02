using gakha.common;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gakha.models
{
    public class SyllableGroup : gakhaObject, IEnumerable<Syllable>, INotifyCollectionChanged
    {
        private Syllable _first = null;
        private Syllable _last = null;
        public SyllableGroup()
        {
            _first = _last = new Syllable();
        }
        public SyllableGroup(Syllable first, Syllable last = null)
        {
            _first = first;
            _last = last ?? first;
        }
        public SyllableGroup(Syllable first, int count)
        {
            _first = first;
            _last = first;
            for(var i=0;i<count;i++)
            {
                if (_last.Next == null) break;
                _last = _last.Next;
            }
        }

        public Syllable First { get { return _first; } }
        public Syllable Last { get { return _last; } }


        public string Wylie
        {
            get
            {
                var sb = new StringBuilder();
                var curr = _first;
                while(1==1)
                {
                    sb.Append(curr.Wylie);
                    if (curr == _last)
                        break;
                    else
                        sb.Append(curr.Separator);

                    curr = curr.Next;
                }
                return sb.ToString();
            }
        }

        public string Tibetan
        {
            get
            {
                var sb = new StringBuilder();
                var curr = _first;
                while(1==1)
                {
                    sb.Append(curr.Tibetan);
                    if (curr == _last)
                        break;
                    else
                    {
                        if (curr.Separator == " ")
                            sb.Append("\u0f0b");  // tsek
                        else
                        {
                            sb.Append(Tools.FromWylie(curr.Separator));
                        }
                    }

                    curr = curr.Next;
                }
                return sb.ToString();
            }
        }

        #region IEnumerable
        public IEnumerator<Syllable> GetEnumerator()
        {
            return new SyllableEnumerator(_first, _last);
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
        #endregion

        #region INotifyCollectionChanged
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        protected void OnCollectionChanged(NotifyCollectionChangedAction action, params Syllable[] changeditems)
        {
            if (CollectionChanged != null)
                CollectionChanged(this, new NotifyCollectionChangedEventArgs(action, changeditems));
        }
        #endregion

        public SyllableGroup Clear()
        {
            _first = new Syllable();
            _last = null;
            OnPropertyChanged("First");
            OnPropertyChanged("Last");
            OnCollectionChanged(NotifyCollectionChangedAction.Reset);
            return this;
        }

        public SyllableGroup Append(Syllable s)
        {
            if (s == _last) return this;

            _last.Next = s;
            _last = s;
            OnPropertyChanged("Last");
            OnCollectionChanged(NotifyCollectionChangedAction.Add, _last);
            return this;
        }

        public SyllableGroup Append(IEnumerable<Syllable> syllables)
        {
            foreach (var s in syllables)
                Append(s);
            return this;
        }

        public SyllableGroup Append(string Wylie)
        {
            foreach(var ws in Wylie.Split(" ".ToCharArray()))
            {
                if (!String.IsNullOrWhiteSpace(ws))
                    Append(new Syllable() { Wylie = ws, Separator = " " });
            }
            return this;
        }

        public SyllableGroup Extend(int count = 1)
        {
            for(var i=0;i<count;i++)
            {
                if (_last.Next == null)
                    break;
                _last = _last.Next;
                OnPropertyChanged("Last");
                OnCollectionChanged(NotifyCollectionChangedAction.Add, _last);
            }
            return this;
        }
        public SyllableGroup ExtendTo(Syllable dest)
        {
            if (IndexOf(dest) >= 0)
                throw new ArgumentException("Cannot extendto.  Syllable is already in this group.");
            var curr = _last.Next;
            while(curr!=null)
            {
                _last = curr;
                if (curr == dest) return this;
                curr = curr.Next;
            }
            return this;
        }

        public SyllableGroup Contract(int count = 1)
        {
            for (var i = 0; i < count;i++)
            {
                if (_last == _first) break;
                var s = _last;
                _last = _last.Prev;
                OnPropertyChanged("Last");
                OnCollectionChanged(NotifyCollectionChangedAction.Remove, s);
            }
            return this;
        }

        public SyllableGroup ContractTo(Syllable last)
        {
            if (last == _last) return this;
            if (IndexOf(last) < 0)
                throw new ArgumentException("Cannot contractto. Syllable is not contained in this group.");

            var removed = new List<Syllable>();
            while(_last!=last)
            {
                removed.Add(_last);
                _last = _last.Prev;
            }
            OnPropertyChanged("Last");
            OnCollectionChanged(NotifyCollectionChangedAction.Remove, removed.ToArray());
            return this;
        }
        public void Insert(Syllable s, Syllable after = null)
        {
            if(after == null)
            {
                s.Next = _first;
                _first = s;
                OnPropertyChanged("First");
                OnCollectionChanged(NotifyCollectionChangedAction.Add,s);
                return;
            }

            if(IndexOf(after) < 0)
            {
                throw new ArgumentException("Cannot insert.  Syllable is not contained within the group", "s");
            }
            else
            {
                s.Next = after.Next;
                after.Next = s;
                OnCollectionChanged(NotifyCollectionChangedAction.Add, s);
            }

        }

        public void Remove(Syllable s)
        {
            if(_first.Next==null)
                throw new Exception("Cannot remove the only syllable in the group.");

            if(s==_first)
            {
                _first = _first.Next;
                OnPropertyChanged("First");
                OnCollectionChanged(NotifyCollectionChangedAction.Remove, s);
            }

            if(s==_last)
            {
                _last = _last.Prev;
                OnPropertyChanged("Last");
                OnCollectionChanged(NotifyCollectionChangedAction.Remove, s);
            }

            var prev = _first;
            var curr = _first.Next;
            while(curr!=null)
            {
                if (curr == s)
                {
                    prev.Next = curr.Next;
                    curr.Next = null;
                    OnCollectionChanged(NotifyCollectionChangedAction.Remove, curr);
                }

            }
            if (IndexOf(s) < 0)
                throw new ArgumentException("Cannot remove.  Syllable is not contained within this group", "s");

        }

        public Syllable this[int index]
        {
            get
            {
                var ret = _first;
                var i = 0;
                while(i < index)
                {
                    if (ret.Next == null)
                        throw new IndexOutOfRangeException();
                    ret = ret.Next;
                }
                return ret;
            }
        }

        public int IndexOf(Syllable s)
        {
            var item = _first;
            int i=0;
            while(item!=null)
            {
                if (item == s) return i;
                item = item.Next;
            }
            return -1;
        }

        public override string ToString()
        {
            return this.Wylie;
        }

        public List<Syllable> ToList()
        {
            var list = new List<Syllable>(this);
            return list;
        }

    }
}
