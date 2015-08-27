using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PashOS.Lib
{
    public class Dictionary<Tkey, Tvalue>
    {

        public List<Tkey> Keys = new List<Tkey>();
        public List<Tvalue> Values = new List<Tvalue>();

        public Tvalue this[Tkey key]
        {
            get
            {
                return Get(key);
            }
            set
            {
                Values[Keys.IndexOf(key)] = value;
            }
        }

        public int Count
        {
            get
            {
                return Keys.Count;
            }
        }

        public bool ContainsKey(Tkey k)
        {
            return Keys.Contains(k);
        }

        public Tvalue Get(Tkey key)
        {
            int index = Keys.IndexOf(key);
            return Values[index];
        }

        public void Add(Tkey key, Tvalue value)
        {
            Keys.Add(key);
            Values.Add(value);
        }

        public void Remove(Tkey key)
        {
            int index = Keys.IndexOf(key);
            Keys.RemoveAt(index);
            Values.RemoveAt(index);
        }

        public void Clear()
        {
            Keys = new List<Tkey>();
            Values = new List<Tvalue>();
        }

      

    }
}
