using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiegeOfTheFortress
{
    [Serializable]
    public class Listofeffects 
    {
        public Listofeffects(Eff_List info, Listofeffects next)
        {
            Info = info;
            Next = next;
        }
        public Eff_List Info;
        public Listofeffects Next;
    }
}
