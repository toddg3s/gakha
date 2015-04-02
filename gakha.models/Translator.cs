using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gakha.models
{
    public class Translator : gakhaObject
    {
        public string UserId { get { return Get<string>("UserId"); } set { Set<string>("UserId", value); } }

        public string Name { get { return Get<string>("Name"); } set { Set<string>("Name", value); } }

    }
}
