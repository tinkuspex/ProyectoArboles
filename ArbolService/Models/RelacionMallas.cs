using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArbolService.Models
{
    public class RelacionMallas
    {
        public int Id { set; get; }
        public string Parent { set; get; }
        public string Child { set; get; }
    }
    public class Dataframe {
        public string eid { set; get; }
        public string Parent { set; get; }
        public string Child { set; get; }

    }
    public class Malla
    {
        public string malla { set; get; }


    }
    public class Jobs
    {
        public string jobs { set; get; }


    }
    public static class metodos{
        public static V SetDefault<K, V>(this IDictionary<K, V> dict, K key, V @default)
        {
            V value;
            if (!dict.TryGetValue(key, out value))
            {
                dict.Add(key, @default);
                return @default;
            }
            else
            {
                return value;
            }
        }
    }
  
}