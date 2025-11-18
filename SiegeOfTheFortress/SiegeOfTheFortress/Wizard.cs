using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SiegeOfTheFortress
{
    [Serializable]
    public class Wizard : Soldier
    {
        protected int impact, efftype;
        
        public Wizard(int x, int y, int i, int j, int bhealth, int value, int direction, int type, Color color, int bV, int bdamage, int bdistance, bool ally, TypeofCharacter type1, bool openhide) : base(x, y, i, j, bhealth, value, direction, type, color, bV, bdamage, bdistance, ally, type1, openhide)
        {
            this.impact = 40;
            this.efftype = 1;
            MyPicture = new Bitmap[12];
            if (ally)
            {
                MyPicture[0] = new Bitmap(SiegeOfTheFortress.Properties.Resources.idle_wizard);
                MyPicture[1] = new Bitmap(SiegeOfTheFortress.Properties.Resources.walk_wizard1);
                MyPicture[2] = new Bitmap(SiegeOfTheFortress.Properties.Resources.walk_wizard2);
                MyPicture[3] = new Bitmap(SiegeOfTheFortress.Properties.Resources.walk_wizard3);
                MyPicture[4] = new Bitmap(SiegeOfTheFortress.Properties.Resources.attack_wizard1);
                MyPicture[5] = new Bitmap(SiegeOfTheFortress.Properties.Resources.attack_wizard2);
                MyPicture[6] = new Bitmap(SiegeOfTheFortress.Properties.Resources.attack_wizard3);
                MyPicture[7] = new Bitmap(SiegeOfTheFortress.Properties.Resources.attack_wizard4);
                MyPicture[8] = new Bitmap(SiegeOfTheFortress.Properties.Resources.hurt_wizard);
                MyPicture[9] = new Bitmap(SiegeOfTheFortress.Properties.Resources.wizard_died1);
                MyPicture[10] = new Bitmap(SiegeOfTheFortress.Properties.Resources.wizard_died2);
                MyPicture[11] = new Bitmap(SiegeOfTheFortress.Properties.Resources.wizard_died3);
                ObjectOrientation = ++ObjectOrientation % 2;

            }
            else{
                MyPicture[0] = new Bitmap(SiegeOfTheFortress.Properties.Resources.idle_enemywizard);
                MyPicture[1] = new Bitmap(SiegeOfTheFortress.Properties.Resources.walk_enemywizard1);
                MyPicture[2] = new Bitmap(SiegeOfTheFortress.Properties.Resources.walk_enemywizard2);
                MyPicture[3] = new Bitmap(SiegeOfTheFortress.Properties.Resources.walk_enemywizard3);
                MyPicture[4] = new Bitmap(SiegeOfTheFortress.Properties.Resources.attack_enemywizard1);
                MyPicture[5] = new Bitmap(SiegeOfTheFortress.Properties.Resources.attack_enemywizard2);
                MyPicture[6] = new Bitmap(SiegeOfTheFortress.Properties.Resources.attack_enemywizard3);
                MyPicture[7] = new Bitmap(SiegeOfTheFortress.Properties.Resources.attack_enemywizard4);
                MyPicture[8] = new Bitmap(SiegeOfTheFortress.Properties.Resources.hurt_enemywizard1);
                MyPicture[9] = new Bitmap(SiegeOfTheFortress.Properties.Resources.enemywizard_died1);
                MyPicture[10] = new Bitmap(SiegeOfTheFortress.Properties.Resources.enemywizard_died2);
                MyPicture[11] = new Bitmap(SiegeOfTheFortress.Properties.Resources.enemywizard_died3);

                
            }
            for (int ij = 0; ij < 12; ij++)
                MyPicture[ij].MakeTransparent(Color.White);
        }
        public Wizard(Wizard primer):this(primer.x, primer.y, primer.i, primer.j, primer.bhealth, primer.value, primer.direction, primer.battackdist, primer.color/*RGB(218, 112, 214)*/, primer.fV, primer.damage, primer.distance, primer.ally, primer.type, !primer.openhide)
        {

        }
        public override Character Clone2() {
            return new Wizard(this);
        }

        protected override void Yt_Tick(object sender, EventArgs e)
        {
            if (mystate == 0)

                tekp = 0;

            else if (mystate == 1)

                tekp = ++tekp % 3 + 1;

            else if (mystate == 2)

            {
                tekp = ++tekp % 4 + 4;
                countanim++;
                if (countanim > 8)
                {
                    mystate = 0;
                    countanim = 0;
                }
            }

            else if (mystate == 3)

            {
                if (tekp == 0)
                    tekp = 8;
                else
                    tekp = 0;
                countanim++;
                if (countanim > 8)
                {

                    countanim = 0;
                    if (health > 0)
                    {
                        mystate = 0;
                        MyMessage mes = new MyMessage();
                        CharHookIsOver(this, mes);
                    }
                    else

                    {
                        tekp = 0;
                        mystate = 4;
                    }
                }
            }

            else if (mystate == 4)

            {
                if (tekp == 0)
                    tekp = 9;
                else
                    tekp++;
                if (tekp==11)
                {
                    MyMessage mes = new MyMessage();
                    CharHookIsOver(this, mes);
                    
                }
            }
        }
        public override void Createprofile(object sender, MyMessage mes)
        {
            if (mes.Myobject is Wizard &&(Wizard)mes.Myobject == this)
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
                mes.Answer = 1;
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
        public override void ChangeStateforAttack(object sender, MyMessage mes)
        {
            if (mes.Character == this)
            {
                Apply_eff();
                if (health > 0)
                {
                    if (Field.Stateofprocess == 2)
                    {
                        mystate = 2;
                        countanim = 0;
                        MyMessage newmes = new MyMessage();
                        newmes.Profile.I = i;
                        newmes.Profile.J = j;

                        int a = Field.Jgoal - j;
                        Field.Xdelta = a >= 0 ? a == 0 ? 0 : 5 : -5;
                        a = Field.Igoal - i;
                        Field.Ydelta = a >= 0 ? a == 0 ? 0 : 5 : -5;

                        if (Field.Xdelta < 0 && ObjectOrientation == 1 || Field.Xdelta > 0 && ObjectOrientation == 0)



                            RotateMyImages();

                        Attack(this, newmes);
                    }
                    else
                    {
                        mystate = 1;
                        if (Field.Xdelta < 0 && ObjectOrientation == 1 || Field.Xdelta > 0 && ObjectOrientation == 0)

                            RotateMyImages();




                    }
                }
                else
                {
                    tekp = 0;
                    mystate = 4;
                }

            }
        }

        

        protected override void RotateMyImages()
        {
            for (int i = 0; i < 12; i++)
                MyPicture[i].RotateFlip(RotateFlipType.RotateNoneFlipX);
            ObjectOrientation = ++ObjectOrientation % 2;
        }

        public override void Hook(MyMessage mes)
        {
            mes.Impact.Damage = -damage;
            mes.Code = 14;
            mes.Impact.Effect.Efftype = TypeofCharacterEffects.Swirlwind; //Тип наложенного эффекта (у каждого "вида" героя-заклинателя он свой)
            mes.Impact.Effect.Ident = null; //Идентификатор того, кто его наложил
            mes.Impact.Effect.Period = false;//Периодический или нет
            mes.Impact.Effect.Type = 4; //На что эффект влияет
            mes.Impact.Effect.Value = -20; //Значение
            mes.Impact.Effect.Moves = 3; //Количество оставшихся ходов
        }
    }
}
