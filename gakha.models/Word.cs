using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gakha.models
{
    public class Word : gakhaObject
    {
        public string Wylie { get { return Get<string>("Wylie"); } set { Set<string>("Wylie", value); Transliterate(); } }

        public string Tibetan { get { return Get<string>("Tibetan"); } set { Set<string>("Tibetan", value); } }

        public string English { get { return Get<string>("English"); } set { Set<string>("English", value); } }

        public ObservableCollection<string> Candidates { get; private set; }

        public string Commentary { get { return Get<string>("Commentary"); } set { Set<string>("Commentary", value); } }

        public Sentence Container { get; set; }

        public Word()
        {
            Candidates = new ObservableCollection<string>();
        }
        public void Split(bool saveWork = false)
        {
            if(!saveWork)
            {
                Clear("Tibetan", "English", "Candidates", "Commentary");
            }
            var syllables = Wylie.Split(" ".ToCharArray());
            Wylie = syllables[0];
            var index = Container.Words.IndexOf(this);
            for(var i=1;i<syllables.Length;i++)
            {
                Container.Words.Insert(++index, new Word() { Wylie = syllables[i] });
            }
        }

        public void Join(int words = 1, bool saveWork = false)
        {
            var index = Container.Words.IndexOf(this) + 1;
            for(var i=0;i<words;i++)
            {
                if(index < Container.Words.Count)
                {
                    var nextWord = Container.Words[index];
                    if(!String.IsNullOrWhiteSpace(nextWord.Wylie))
                        Wylie += " " + nextWord.Wylie;
                    if (saveWork)
                    {
                        if (!String.IsNullOrWhiteSpace(nextWord.English))
                            English += " " + nextWord.English;

                        Candidates.CrossJoin(nextWord.Candidates);

                        if (!String.IsNullOrWhiteSpace(nextWord.Commentary))
                            Commentary += "\n" + nextWord.Commentary;
                    }
                }
                Container.Words.RemoveAt(index);
            }
        }

        private void Transliterate()
        {
            var trans = new org.rigpa.wylie.Wylie();
            Tibetan = trans.fromWylie(Wylie);
        }

    }
}
