using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gakha.models
{
    public static class Extensions
    {
        public static void CrossJoin(this IList<string> list, IList<string> join, string separator = " ")
        {
            if (list == null || join == null || join.Count==0) return;

            if(list.Count==0)
            {
                foreach (var entry in join)
                    list.Add(entry);
                return;
            }

            var save = new string[list.Count];
            list.CopyTo(save, 0);

            for(var i=0;i<list.Count;i++)
            {
                list[i] += separator + join[0];
            }
            for(var i=1;i<join.Count;i++)
            {
                for(var j=0;j<save.Length;j++)
                {
                    list.Add(save[j] + separator + join[i]);
                }
            }
        }

        public static SyllableGroup Split<T>(this T group, Syllable after, Func<Syllable,Syllable,SyllableGroup> creategroup) where T : SyllableGroup, new()
        {
            if (after == null)
                throw new ArgumentNullException("after");
            if (after.Next == null)
                return null;

            var newgroup = creategroup(after.Next, group.Last);

            group.ContractTo(after);

            return newgroup;
        }
    }
}
