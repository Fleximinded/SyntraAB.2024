using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqExamples.Source
{
    public static class OwnLinq
    {
        public static IEnumerable<T> OwnSort<T>(this IEnumerable<T> source, Func<T, string> sort) {
            return [];        
        }
        public static IEnumerable<T> OwnWhere<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            List<T> result = new();
            foreach(T euh in source)
            {
                if(predicate(euh)) {
                    result.Add(euh);
                }
            }
            return result;
        }
        public static IEnumerable<T> TupperWhere<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            foreach(T euh in source)
            {
                if(predicate(euh))
                {
                    yield return euh;
                }
            }
        }
    }
}
