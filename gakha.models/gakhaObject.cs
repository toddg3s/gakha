using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gakha.models
{
    public class gakhaObject : Object, INotifyPropertyChanged
    {
        protected Dictionary<string, object> _state = new Dictionary<string, object>(StringComparer.InvariantCultureIgnoreCase);
        public event PropertyChangedEventHandler PropertyChanged;

        protected T Get<T>(string name)
        {
            if (_state.ContainsKey(name))
                return (T)_state[name];
            else
                return default(T);
        }

        protected void Set<T>(string name, T value)
        {
            if(_state.ContainsKey(name))
            {
                if (value.Equals(_state[name])) return;
                _state[name] = value;
                OnPropertyChanged(name);
            }
        }

        protected bool IsSet(string name)
        {
            return _state.ContainsKey(name);
        }

        protected void Clear(params string[] names)
        {
            if (names == null || names.Length == 0) return;
            foreach (var name in names)
                if (_state.ContainsKey(name))
                    _state.Remove(name);
        }

        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach(var key in _state.Keys)
                sb.AppendFormat("{0}:{1}, ", key, _state[key]);
            sb.Length -= 2; // get rid of comma space at the end
            return sb.ToString();
        }
    }
}
