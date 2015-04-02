using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gakha.models
{
    public class Annotation : gakhaObject
    {
        public Translator Who { get { return Get<Translator>("Who"); } set { Set<Translator>("Who", value); } }

        public DateTime When { get { return Get<DateTime>("When"); } set { Set<DateTime>("When", value); } }

        public string What { get { return Get<string>("What"); } set { Set<string>("What", value); } }

        private ObservableCollection<Annotation> _replies;
        public ObservableCollection<Annotation> Replies
        {
            get
            {
                if (_replies != null) return _replies;

                // TODO: lazy loading

                return _replies;
            }
        }

        public void LoadThread()
        {
            if (Replies != null)
                Replies.ToList().ForEach(a => a.LoadThread());
        }

    }
}
