using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiegeOfTheFortress
{
    [Serializable]
    public struct Eff_List
    {
        public Eff_List(TypeofCharacterEffects Efftype, Wizard ident, bool period, int type, int value, short moves)
        {
            this.Efftype = Efftype;
            this.Ident= ident;
            this.Period= period;
            this.Type= type;
            this.Moves= moves;
            this.Value= value;
        }
        public TypeofCharacterEffects Efftype { get; set; } //Тип наложенного эффекта (у каждого "вида" героя-заклинателя он свой)
                     //Character* ident; //Идентификатор того, кто его наложил
        public Wizard Ident { get; set; }
        public bool Period { get; set; }
        public int Type { get; set; }//На что эффект влияет: 0-скорость, 1-здоровье, 2-хотьба, 3-дистанция атаки, 4-сила атаки
        public int Value { get; set; }//Значение
        public short Moves { get; set; } //Количество оставшихся ходов
                                         //BOOLEAN controldeath;//Можно отменить гибелью наложившего?
    }
}
