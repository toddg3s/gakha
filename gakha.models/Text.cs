using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gakha.models
{
    public class Text : gakhaObject
    {
        public string Title { get { return Get<string>("Title"); } set { Set<string>("Title", value); } }

        public string Description { get { return Get<string>("Description"); } set { Set<string>("Description",value); } }

        public List<string> Authors { get { return Get<List<string>>("Authors"); } set { Set<List<string>>("Authors", value); } }

        public string Written { get { return Get<string>("Written"); } set { Set<string>("Written", value); } }

        public LinkedList<Sentence> Sentences { get; set; }
    }
}
