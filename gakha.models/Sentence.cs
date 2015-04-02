using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gakha.models
{
    public class Sentence : gakhaObject
    {
        public string Translation { get { return Get<string>("Translation"); } set { Set<string>("Translation", value); } }
        public ObservableCollection<string> Candidates { get { return Get<ObservableCollection<string>>("Candidates"); } set { Set<ObservableCollection<string>>("Candidates", value); } }
        public ObservableCollection<Word> Words { get { return Get<ObservableCollection<Word>>("Words"); } set { Set<ObservableCollection<Word>>("Words", value); } }
    }
}
