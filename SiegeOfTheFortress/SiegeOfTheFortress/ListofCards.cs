using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiegeOfTheFortress
{
    [Serializable]
    public class ListofCards
    {
        public Card obj;
        public ListofCards next;
        public ListofCards(Card obj, ListofCards next)
        {
            this.obj = obj;
            this.next = next;
        }
    }
}
