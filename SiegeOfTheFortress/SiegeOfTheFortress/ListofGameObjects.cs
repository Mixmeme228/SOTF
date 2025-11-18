using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiegeOfTheFortress
{
    [Serializable]
    public class ListofGameObjects
    {
        public GameObject obj;
        public ListofGameObjects next;
        public ListofGameObjects(GameObject obj, ListofGameObjects next)
        {
            this.obj = obj;
            this.next = next;
        }
    }
}
