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
    public class Soldier : Character
    {
        
        public Soldier(int x, int y, int i, int j, int bhealth, int value, int direction, int type, Color color, int bV, int bdamage, int bdistance, bool ally, TypeofCharacter type1, bool openhide) : base(bV, bdamage, bdistance, type, color, x, y, i, j, bhealth, value, direction, ally, type1, openhide)
        {
            MyPicture = new Bitmap[13];
            
            
            if (ally)
            {

                MyPicture[0] = new Bitmap(SiegeOfTheFortress.Properties.Resources.idle_allysoldier);
                MyPicture[1] = new Bitmap(SiegeOfTheFortress.Properties.Resources.walk_allysoldier1);
                MyPicture[2] = new Bitmap(SiegeOfTheFortress.Properties.Resources.walk_allysoldier2);
                MyPicture[3] = new Bitmap(SiegeOfTheFortress.Properties.Resources.walk_allysoldier3);
                MyPicture[4] = new Bitmap(SiegeOfTheFortress.Properties.Resources.walk_allysoldier4);
                MyPicture[5] = new Bitmap(SiegeOfTheFortress.Properties.Resources.attack_allysoldier1);
                MyPicture[6] = new Bitmap(SiegeOfTheFortress.Properties.Resources.attack_allysoldier2);
                MyPicture[7] = new Bitmap(SiegeOfTheFortress.Properties.Resources.attack_allysoldier3);
                MyPicture[8] = new Bitmap(SiegeOfTheFortress.Properties.Resources.attack_allysoldier4);
                MyPicture[9] = new Bitmap(SiegeOfTheFortress.Properties.Resources.hurt_allysoldier);
                MyPicture[10] = new Bitmap(SiegeOfTheFortress.Properties.Resources.allysoldier_died1);
                MyPicture[11] = new Bitmap(SiegeOfTheFortress.Properties.Resources.allysoldier_died2);
                MyPicture[12] = new Bitmap(SiegeOfTheFortress.Properties.Resources.allysoldier_died3);
                
            }
            else
            {
                MyPicture[0] = new Bitmap(SiegeOfTheFortress.Properties.Resources.idle_enemysoldier);
                MyPicture[1] = new Bitmap(SiegeOfTheFortress.Properties.Resources.walk_enemysoldier1);
                MyPicture[2] = new Bitmap(SiegeOfTheFortress.Properties.Resources.walk_enemysoldier2);
                MyPicture[3] = new Bitmap(SiegeOfTheFortress.Properties.Resources.walk_enemysoldier3);
                MyPicture[4] = new Bitmap(SiegeOfTheFortress.Properties.Resources.walk_enemysoldier4);
                MyPicture[5] = new Bitmap(SiegeOfTheFortress.Properties.Resources.attack_enemysoldier1);
                MyPicture[6] = new Bitmap(SiegeOfTheFortress.Properties.Resources.attack_enemysoldier2);
                MyPicture[7] = new Bitmap(SiegeOfTheFortress.Properties.Resources.attack_enemysoldier3);
                MyPicture[8] = new Bitmap(SiegeOfTheFortress.Properties.Resources.attack_enemysoldier4);
                MyPicture[9] = new Bitmap(SiegeOfTheFortress.Properties.Resources.hurt_enemysoldier);
                MyPicture[10] = new Bitmap(SiegeOfTheFortress.Properties.Resources.enemysoldier_died1);
                MyPicture[11] = new Bitmap(SiegeOfTheFortress.Properties.Resources.enemysoldier_died2);
                MyPicture[12] = new Bitmap(SiegeOfTheFortress.Properties.Resources.enemysoldier_died3);

               
            }
            for (int ij = 0; ij < 13; ij++)
                MyPicture[ij].MakeTransparent(Color.White);
        }

        public Soldier(Soldier primer):this(primer.x, primer.y, primer.i, primer.j, primer.bhealth /*120*/, primer.value/*100*/, primer.direction, primer.battackdist, primer.color/*RGB(218, 112, 214)*/, primer.fV/*100*/, primer.damage/*40*/, primer.distance/*2*/, primer.ally, primer.type, !primer.openhide)
        {

        }
        public override Character Clone2(){
            return new Soldier(this);
        }

        protected override void Yt_Tick(object sender, EventArgs e)
        {
            if (mystate == 0)

                tekp = 0;

            else if (mystate == 1)

                tekp = ++tekp % 4 + 1;

            else if (mystate == 2)

            {
                tekp = ++tekp % 4 + 5;
                countanim++;
                if (countanim > 8)
                {
                    mystate = 0;
                    countanim = 0;
                }
            }

            else if (mystate == 3)

            {
                if (tekp != 9)
                    tekp = 9;
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
                    tekp = 10;
                else
                    tekp++;
                if (tekp == 13)
                {
                    MyMessage mes = new MyMessage();
                    CharHookIsOver(this, mes);
                    
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

                    mystate = 1;
                    if (Field.Xdelta < 0 && ObjectOrientation == 1 || Field.Xdelta > 0 && ObjectOrientation == 0)
                    {

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
            for (int i = 0; i < 13; i++)
                MyPicture[i].RotateFlip(RotateFlipType.RotateNoneFlipX);
            ObjectOrientation = ++ObjectOrientation % 2;
        }

        protected void RotateinHook(MyMessage mes)
        {
            int b = mes.Profile.J-j;
            if (b < 0 && ObjectOrientation == 1 || b > 0 && ObjectOrientation == 0)
                RotateMyImages();
            
        }

        public override void Taketheattack(object sender, MyMessage mes)
        {
            
            if (i == Field.Igoal && j == Field.Jgoal)
            {
                RotateinHook(mes);
                mystate = 3;
                countanim = 0;
                Changehealth(mes.Impact.Damage);
                if (mes.Impact.Effect.Efftype != 0)
                    Add_eff(mes.Impact.Effect);
            }
        }

        public override void UpdateObj(object sender, MyMessage mes)
        {

            if (mystate == 1)
            {

                //if ((x == -Field.Leftdest+Field.pj[Field.Tekindex] * 40) && (y == -Field.Topdest+Field.pi[Field.Tekindex] * 40 + 23))
                if ((factx == /*-Field.Leftdest + */Field.pj[Field.Tekindex] * 40) && (facty == /*-Field.Topdest + */Field.pi[Field.Tekindex] * 40 + 23))

                {
                    if (Field.pj[Field.Tekindex + 1] != -1)
                    {
                        int a = Field.pj[Field.Tekindex + 1] - Field.pj[Field.Tekindex];
                        Field.Xdelta = a >= 0 ? a == 0 ? 0 : 5 : -5;
                        a = Field.pi[Field.Tekindex + 1] - Field.pi[Field.Tekindex];
                        Field.Ydelta = a >= 0 ? a == 0 ? 0 : 5 : -5;
                        Field.Tekindex = Field.Tekindex + 1;
                        if (Field.Xdelta < 0 && ObjectOrientation == 1 || Field.Xdelta > 0 && ObjectOrientation == 0)
                        {


                            RotateMyImages();
                        }
                    }
                    else
                    {
                        int a = Field.Jgoal - Field.pj[Field.Tekindex];
                        Field.Xdelta = a >= 0 ? a == 0 ? 0 : 5 : -5;
                        a = Field.Igoal - Field.pi[Field.Tekindex];
                        Field.Ydelta = a >= 0 ? a == 0 ? 0 : 5 : -5;
                        if (Field.Stateofprocess == 1)
                        {
                            mystate = 2;
                            countanim = 0;
                            MyMessage newmes = new MyMessage();
                            newmes.Profile.I = i;
                            newmes.Profile.J = j;
                            if (Field.Xdelta < 0 && ObjectOrientation == 1 || Field.Xdelta > 0 && ObjectOrientation == 0)
                            {


                                RotateMyImages();
                            }
                            Attack(this, newmes);
                        }
                        else
                        {
                            mystate = 0;
                            tekp = 0;
                            MyMessage newmes = new MyMessage();
                            if (Field.Xdelta < 0 && ObjectOrientation == 1 || Field.Xdelta > 0 && ObjectOrientation == 0)
                            {

                                RotateMyImages();
                            }
                            CharHookIsOver(this, mes);
                        }
                    }
                }
                else
                {
                    x += Field.Xdelta;
                    y += Field.Ydelta;
                    factx += Field.Xdelta;
                    facty += Field.Ydelta;
                }
            }

            Show(sender, mes);
        }
        public override void Show(object sender, MyMessage mes) {
            if (x <= mes.right && x+l >= mes.left && y <= mes.bottom && y+w >= mes.top)
            {
                mes.dc1.DrawImage(MyPicture[tekp], x, y, w, l);
            }
        }
        
        public override void Paint_face(object sender, MyMessage mes)
        {
            if(mes.Character==this)
                mes.dc1.DrawImage(MyPicture[0], mes.left, mes.top, mes.right-mes.left, mes.bottom-mes.top);
        }
    }
}
