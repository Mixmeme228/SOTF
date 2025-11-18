using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiegeOfTheFortress
{
    [Serializable]
    public struct Character_impact
    {
        public Character_impact(int damage, Eff_List effect)
        {
            Damage = damage;
            Effect = effect;
        }
        public int Damage { get; set; }
        public Eff_List Effect;
    }
}
