using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace SiegeOfTheFortress
{
    [Serializable]
    public abstract class Character : GameObject
    {
        protected int ObjectOrientation;
        public static int numberally;
        protected int bV, fV, period, bdamage, damage, bdistance, distance, battackdist, attackdist, numberofeffects;
        protected Color color;
        protected Listofeffects First_eff;
        protected bool /*flag,*/ ally;

        public static event UpdateObject Characterattack;
        static Character()
        {
            numberally = 0;
        }
        public int Numberally
        {
            get
            {
                return numberally;
            }

            set
            {
                numberally = value;
            }
        }
        public int I
        {
            get
            {
                return i;
            }
            set
            {
                i = value;
            }
        }
        public int J
        {
            get
            {
                return j;
            }
            set
            {
                j = value;
            }
        }
        public Character(int bV, int bdamage, int bdistance, int adist, Color color, int x, int y, int i, int j, int bhealth, int value, int direction, bool ally, TypeofCharacter type, bool openhide) : base(x, y, i, j, bhealth, value, direction, type, openhide)
        {
            if (ally && openhide)
                numberally++;
            ObjectOrientation = 1;
            this.bV = bV;
            fV = bV;
            period = 10000 / fV;
            this.bdamage = bdamage;
            damage = bdamage;
            this.bdistance = bdistance;
            distance = bdistance;
            battackdist = adist;
            First_eff = null;
            //flag = false;
            attackdist = battackdist;
            this.ally = ally;
            this.color = color;
            numberofeffects = 0;
            if (openhide)
            {
                Field.Fieldattack += ChangeStateforAttack;
                Card.CardFace += Paint_face;
            }
        }

        public override void ReSignMySelf()
        {
            MyT = new Timer();
            MyT.Enabled = false;
            MyT.Interval = 350;
            if (openhide)
            {
                Field.Fieldstartgameprocess += GOstartgame;
                MyT.Tick += Yt_Tick;
                Field.Fieldattack += ChangeStateforAttack;
                Card.CardFace += Paint_face;
                Field.FieldUpdateObject += UpdateObj;
                Field.FieldCreateProfile += Createprofile;
                Character.Characterattack += Taketheattack;
                Form1.FormStopEvent += ObjectStop;
                Form1.FormContinueEvent += ObjectContinue;
                Field.FieldShow += Show;
                Ball.Ballattack += Taketheattack;
                Field.FieldMouseHover += ObjectisHover;
            }
            Field.FieldDelete += DeleteMyObject;
            Field.FieldrectMapmove += ObjectMouseMove;

        }

        public override void UnSignMySelf()
        {
            if (openhide)
            {
                Field.Fieldstartgameprocess -= GOstartgame;
                MyT.Tick -= Yt_Tick;
                Field.Fieldattack -= ChangeStateforAttack;
                Card.CardFace -= Paint_face;
                Field.FieldUpdateObject -= UpdateObj;
                Field.FieldCreateProfile -= Createprofile;
                Character.Characterattack -= Taketheattack;
                Form1.FormStopEvent -= ObjectStop;
                Form1.FormContinueEvent -= ObjectContinue;
                Field.FieldShow -= Show;
                Ball.Ballattack -= Taketheattack;
                Field.FieldMouseHover -= ObjectisHover;
            }
            Field.FieldDelete -= DeleteMyObject;
            Field.FieldrectMapmove -= ObjectMouseMove;

        }


        public virtual Character Clone2()
        {
            return null;
        }

        protected override void DeleteMyObject(object sender, MyMessage mes)
        {

            if (mes.Profile.I == i && mes.Profile.J == j)
            {
                if (openhide)
                {
                    if (ally)
                        numberally--;
                    Field.Fieldstartgameprocess -= GOstartgame;


                    Field.FieldUpdateObject -= UpdateObj;
                    MyT.Tick -= Yt_Tick;
                    Form1.FormStopEvent -= ObjectStop;
                    Form1.FormContinueEvent -= ObjectContinue;
                    Character.Characterattack -= Taketheattack;
                    Field.FieldCreateProfile -= Createprofile;

                    Field.FieldShow -= Show;
                    Field.FieldDelete -= DeleteMyObject;
                    Ball.Ballattack -= Taketheattack;
                    Field.Fieldattack -= ChangeStateforAttack;
                    Card.CardFace -= Paint_face;
                    MyT.Enabled = false;
                    Field.FieldMouseHover -= ObjectisHover;
                }
                else if(mes.Code==123)
                {
                    Field.FieldDelete -= DeleteMyObject;
                    Field.FieldrectMapmove -= ObjectMouseMove;

                    MyT.Enabled = false;
                }
            }
        }

        protected abstract void RotateMyImages();

        public abstract void ChangeStateforAttack(object sender, MyMessage mes);
        public abstract void Paint_face(object sender, MyMessage mes);
        protected virtual void Attack(object sender, MyMessage mes) //Солдат атакует с близкого расстояния, пушка стреляет и перерисовывает снаряд вместе с клетками, информацию о них пушка получит благодаря полю и некой структуре
        {
            Hook(mes);
            Characterattack(sender, mes);
        }
        public virtual void Hook(MyMessage mes)
        {
            mes.Code = 14;
            mes.Impact.Damage = -damage;
            mes.Impact.Effect.Efftype = 0; //Тип наложенного эффекта (у каждого "вида" героя-заклинателя он свой)
            mes.Impact.Effect.Ident = null; //Идентификатор того, кто его наложил
            mes.Impact.Effect.Period = false;//Периодический или нет
            mes.Impact.Effect.Type = 0; //На что эффект влияет
            mes.Impact.Effect.Value = 0; //Значение
            mes.Impact.Effect.Moves = 0; //Количество оставшихся ходов

        }
        public void Add_eff(Eff_List new_effect)
        {
            Listofeffects curr = First_eff, last=null;
            int d = 1;
            while (curr != null && d != 0)
            {
                if (curr != null)
                    if (curr.Info.Efftype == new_effect.Efftype)
                    {
                        d = 0;
                        curr.Info.Moves = new_effect.Moves;
                    }
                last = curr;
                curr = curr.Next;
            }
            if (d != 0)
            {
                curr = new Listofeffects(new_effect, null);
                numberofeffects++;
                if (First_eff == null)
                    First_eff = curr;
                else
                
                    last.Next = curr;
                if (!new_effect.Period)
                    switch (new_effect.Type)
                    {
                        case 0:
                            {
                                Changev(new_effect.Value); //Изменение скорости
                                break;
                            }
                        case 1:
                            {
                                Changehealth(new_effect.Value); //Изменение здоровья
                                break;
                            }
                        case 2:
                            {
                                Changedistance(new_effect.Value); //Изменение дистанции хотьбы
                                break;
                            }
                        case 3:
                            {
                                Changeattackdist(new_effect.Value); //Изменение дистанции атаки
                                break;
                            }
                        case 4:
                            {
                                Changedamage(new_effect.Value); //Изменение силы атаки
                                break;
                            }
                    }
            }
        }


        private void Changedamage(int diff)
        {
            if (damage + diff >= 0)
                damage += diff;
        }
        private void Changev(int diff)
        {
            fV += diff;
            period = 10000 / fV;
        }
        private void Changedistance(int diff)
        {
            if (bdistance != 0)
                distance += diff;
        }
        private void Changeattackdist(int diff)
        {
            if (battackdist != 0)
                attackdist += diff;
        }


        public void CheckEffects(MyMessage mes)
        {
            Listofeffects curr = First_eff;
            int d = 1;
            while (curr != null && d != 0)
            {
                if (curr != null)
                    if (curr.Info.Efftype == mes.Impact.Effect.Efftype)
                    {
                        d = 0;
                        mes.Answer = 0;
                    }
                curr = curr.Next;
            }
            if (d != 0)
            {
                mes.Answer = 1;
                switch (mes.Impact.Effect.Type)
                {
                    case 0:
                        goto case 4;
                    case 2:
                        goto case 4;
                    case 4:
                        {
                            mes.Annex = 1;
                            break;
                        }
                    case 1:
                        {
                            if (health < bhealth)
                                mes.Annex = 1;
                            else
                                mes.Annex = 0;
                            break;
                        }
                    case 3:
                        {
                            if (battackdist == 0)
                                mes.Annex = 0;
                            else
                                mes.Annex = 1;
                            break;
                        }
                }
            }
        }
        
        public virtual void Get_effstruct(MyMessage mes)
        {
            mes.Impact.Effect.Efftype = 0; //Тип наложенного эффекта (у каждого "вида" героя-заклинателя он свой)
            mes.Impact.Effect.Ident = null; //Идентификатор того, кто его наложил
            mes.Impact.Effect.Period = false;//Периодический или нет
            mes.Impact.Effect.Type = 0; //На что эффект влияет
            mes.Impact.Effect.Value = 0; //Значение
            mes.Impact.Effect.Moves = 0; //Количество оставшихся ходов
        }



        public override void Taketheattack(object sender, MyMessage mes)
        {

            if (i == Field.Igoal && j == Field.Jgoal)
            {

                mystate = 3;
                countanim = 0;
                Changehealth(mes.Impact.Damage);
                if (mes.Impact.Effect.Efftype > 0)
                    Add_eff(mes.Impact.Effect);
            }
        }
        public override void Createprofile(object sender, MyMessage mes)
        {
            if (mes.Myobject as Character == this)
            {
                mes.Profile.Health = health;
                mes.Profile.Damage = damage;
                mes.Profile.Period = period;
                mes.Profile.Ally = ally;
                mes.Profile.Dist = distance;
                mes.Profile.R = attackdist;
                mes.Profile.I = i;
                mes.Profile.J = j;
                mes.Annex = battackdist;
                mes.Info = 1;
                mes.Myobject = this;
                mes.Character = this;
                mes.Profile.Extra_info = type;
                mes.Profile.V = fV;
                mes.Profile.color = color;
                mes.Profile.Value = value;
                if (numberofeffects != 0)
                {
                    mes.Del = numberofeffects;
                    mes.moves = new int[numberofeffects];
                    mes.effs = new TypeofCharacterEffects[numberofeffects];
                    Listofeffects curr = First_eff;
                    int d = 0;
                    while (curr != null)
                    {
                        mes.moves[d] = curr.Info.Moves;
                        mes.effs[d] = curr.Info.Efftype;
                        curr = curr.Next;
                        d++;
                    }
                }
            }
        }
        public void Apply_eff()
        {
            
                Listofeffects curr = First_eff, last = null;
                while (curr != null)
                {
                    curr.Info.Moves--;
                    if (curr.Info.Moves >= 0)
                    {
                        if (curr.Info.Period)
                            switch (curr.Info.Type)
                            {
                                case 0:
                                    {
                                        Changev(curr.Info.Value);
                                        break;
                                    }
                                case 1:
                                    {
                                        Changehealth(curr.Info.Value);
                                        break;
                                    }
                                case 2:
                                    {
                                        Changedistance(curr.Info.Value);
                                        break;
                                    }
                                case 3:
                                    {
                                        Changeattackdist(curr.Info.Value);
                                        break;
                                    }
                                case 4:
                                    {
                                        Changedamage(curr.Info.Value);
                                        break;
                                    }
                            }
                        last = curr;
                        curr = curr.Next;
                    }
                    else
                    {
                        numberofeffects--;
                        if (!curr.Info.Period)
                        {
                            curr.Info.Value *= -1;
                            switch (curr.Info.Type)
                            {
                                case 0:
                                    {
                                        Changev(curr.Info.Value);
                                        break;
                                    }
                                case 1:
                                    {
                                        Changehealth(curr.Info.Value);
                                        break;
                                    }
                                case 2:
                                    {
                                        Changedistance(curr.Info.Value);
                                        break;
                                    }
                                case 3:
                                    {
                                        Changeattackdist(curr.Info.Value);
                                        break;
                                    }
                                case 4:
                                    {
                                        Changedamage(curr.Info.Value);
                                        break;
                                    }
                            }
                        }
                        //if (curr.Info.Ident != null) //Здесь будет отдельное событие
                        //{
                        //    //memcpy(&mes.impact.effect, &(curr->info), sizeof(mes.impact.effect));
                        //    mes.Impact.Effect = curr.Info;
                        //    //mes.Impact.Effect.Efftype = curr.Info.Efftype; //Тип наложенного эффекта (у каждого "вида" героя-заклинателя он свой)
                        //    //mes.Impact.Effect.Ident = curr.Info; //Идентификатор того, кто его наложил
                        //    //mes.Impact.Effect.Period = true;//Периодический или нет
                        //    //mes.Impact.Effect.Type = 1; //На что эффект влияет
                        //    //mes.Impact.Effect.Value = -40; //Значение
                        //    //mes.Impact.Effect.Moves = 3; //Количество оставшихся ходов
                        //    mes.Code = 20;
                        //    mes.Myobject = this;
                        //    curr.Info.Ident.Dispatch(mes.dc, mes);
                        //}
                        if (last != null)
                        {
                            last.Next = curr.Next;
                            curr = last.Next;
                        }
                        else
                        {
                            First_eff = curr.Next;
                            curr = First_eff;
                        }
                    }
                }
            }
        }
}
