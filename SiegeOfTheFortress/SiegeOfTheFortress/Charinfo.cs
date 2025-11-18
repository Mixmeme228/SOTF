using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SiegeOfTheFortress
{
    [Serializable]
    public struct Charinfo
    {
        public Charinfo(bool ally, int health, int damage, int period, int diff, int index, int r, int dist, int i, int j, int value, int direction, TypeofCharacter extra_info, int v, Color color)
        { 
            Ally=ally;  
            Health=health;
            Damage=damage;
            Period=period;
            Diff=diff;
            Index=index;
            R=r; Dist=dist;
            I = i;
            J = j;
            Value=value;
            Direction=direction;
            Extra_info=extra_info;
            V=v; this.color=color;
        }
        public bool Ally;
        public int Health;
        public int Damage;
        public int Period;
        public int Diff;
        public int Index;
        public int R;
        public int Dist;
        public int I;
        public int J;
        public int Value;
        public int Direction;
        public TypeofCharacter Extra_info;
        public int V;
        public Color color;
    }
}
