using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiegeOfTheFortress
{
    [Serializable]
    public class ListofCharacters
    {
        public Character obj;
        public bool eye, near_c, near_h;
        public ListofCharacters next;
        public ListofCharacters(Character obj, ListofCharacters next, bool eye, bool near_c, bool near_h)
        {
            this.obj = obj;
            this.eye = eye;
            this.near_c = near_c;
            this.near_h = near_h;
            this.next = next;
        }
    };
}
