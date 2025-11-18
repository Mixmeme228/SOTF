using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics.Eventing.Reader;

namespace SiegeOfTheFortress
{
    [Serializable]
    public class Cannon : Character
    {
        private Ball MyBall;
        public static event UpdateObject ActivateBall, CauseDeath;

        public Cannon(int bV, int bdamage, int bdistance, Color color, int x, int y, int i, int j, int bhealth, int value, int direction, bool ally, int r, TypeofCharacter type, bool openhide) : base(bV, bdamage, bdistance, r, color, x, y, i, j, bhealth, value, direction, ally, type, openhide)
        {
            MyPicture = new Bitmap[7];
            attackdist = r;
            if (ally)

                MyPicture[0] = new Bitmap(SiegeOfTheFortress.Properties.Resources.cannon);

            else
                MyPicture[0] = new Bitmap(SiegeOfTheFortress.Properties.Resources.cannonenemy);

            MyPicture[1] = new Bitmap(SiegeOfTheFortress.Properties.Resources.hit2);
            MyPicture[2] = new Bitmap(SiegeOfTheFortress.Properties.Resources.cannon_died1);
            MyPicture[3] = new Bitmap(SiegeOfTheFortress.Properties.Resources.cannon_died2);
            MyPicture[4] = new Bitmap(SiegeOfTheFortress.Properties.Resources.cannon_died3);
            MyPicture[5] = new Bitmap(SiegeOfTheFortress.Properties.Resources.cannon_died4);
            MyPicture[6] = new Bitmap(SiegeOfTheFortress.Properties.Resources.cannon_died5);

            for (int ih = 1; ih < 7; ih++) MyPicture[ih].MakeTransparent(Color.White);

            MyBall = null;
            if (openhide)
                Ball.DeleteMyself += DeleteBall;
        }
        public Cannon(Cannon primer):this(primer.fV, primer.damage, primer.distance, primer.color, primer.x, primer.y, primer.i, primer.j, primer.bhealth, primer.value, primer.direction, primer.ally, primer.battackdist, primer.type, !primer.openhide)
        {

        }
        public override Character Clone2() {
            return new Cannon(this);
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
                Ball.DeleteMyself += DeleteBall;
                Field.FieldMouseHover += ObjectisHover;
                if (MyBall != null)
                {
                    MyBall.ReSignMyself();
                }
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
                Ball.DeleteMyself -= DeleteBall;
                Field.FieldMouseHover -= ObjectisHover;
                if (MyBall != null)
                    MyBall.UnSignMyself();
            }
            Field.FieldDelete -= DeleteMyObject;
            Field.FieldrectMapmove -= ObjectMouseMove;

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
                    if (MyBall != null)
                        DeleteBall(this, mes);

                    Field.FieldUpdateObject -= UpdateObj;
                    MyT.Tick -= Yt_Tick;

                    Character.Characterattack -= Taketheattack;
                    Field.FieldCreateProfile -= Createprofile;
                    Form1.FormStopEvent -= ObjectStop;
                    Form1.FormContinueEvent -= ObjectContinue;
                    Field.FieldShow -= Show;
                    Field.FieldDelete -= DeleteMyObject;
                    Ball.Ballattack -= Taketheattack;
                    Field.Fieldattack -= ChangeStateforAttack;
                    Card.CardFace -= Paint_face;
                    Ball.DeleteMyself -= DeleteBall;
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

        public override void Createprofile(object sender, MyMessage mes)
        {
            if (mes.Myobject is Cannon && (Cannon)mes.Myobject == this)
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
                mes.Answer = 0;
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

        protected override void Attack(object sender, MyMessage mes)
        {
            Hook(mes);
            MyBall = new Ball(x, y, i, j, w, l, mes);
            ActivateBall(this, mes);
        }

        public override void ChangeStateforAttack(object sender, MyMessage mes)
        {
            if (mes.Character == this)
            {
                Apply_eff();

                if(health>0){
                    if (Field.Stateofprocess == 2)
                    {
                        mystate = 2;
                        countanim = 0;
                        MyMessage newmes = new MyMessage();
                        newmes.Profile.I = i;
                        newmes.Profile.J = j;
                        newmes.Info = mes.Info;
                        int a = Field.Jgoal - j;
                        Field.Xdelta = a >= 0 ? a == 0 ? 0 : 5 : -5;
                        a = Field.Igoal - i;
                        Field.Ydelta = a >= 0 ? a == 0 ? 0 : 5 : -5;
                        AnRotateMyImages();
                        Attack(this, newmes);
                    }
                    else
                    {
                        mystate = 1;
                        RotateMyImages();
                    }
                }
                else{
                    tekp = 0;
                    mystate = 4;
                }


            }
        }

        protected virtual void AnRotateMyImages()
        {
            float koeff = 180.0f / 3.1416f;
            int ig = Field.Igoal;
            int jg = Field.Jgoal;
            float angle;
            if (ig >= i && jg > j)
                angle = (float)Math.Atan2(ig-i, jg-j) * koeff + 90.0f;
            else if (ig < i && jg >= j)
                angle = (float)Math.Atan2(jg-j, i-ig) * koeff;
            else if (ig <= i && jg < j)
                angle = 270.0f+ (float)Math.Atan2(i-ig, j-jg) * koeff;
            else
                angle = 180.0f + (float)Math.Atan2(j-jg, ig-i) * koeff;

            float alpha = angle;

            while (alpha < 0) alpha += 360;

            float gamma = 90;
            float beta = 180 - angle - gamma;
            if (ally)
                MyPicture[0] = new Bitmap(SiegeOfTheFortress.Properties.Resources.cannon);
            else
                MyPicture[0] = new Bitmap(SiegeOfTheFortress.Properties.Resources.cannonenemy);

            float c1 = MyPicture[0].Height;
            float a1 = (float)(c1 * Math.Sin(alpha * Math.PI / 180));
            float b1 = (float)(c1 * Math.Sin(beta * Math.PI / 180));

            float c2 = MyPicture[0].Width;
            float a2 = (float)(c2 * Math.Sin(alpha * Math.PI / 180));
            float b2 = (float)(c2 * Math.Sin(beta * Math.PI / 180));

            int width = Convert.ToInt32(Math.Abs(b2) + Math.Abs(a1));
            int height = Convert.ToInt32(Math.Abs(b1) + Math.Abs(a2));

            Bitmap rotatedImage = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(rotatedImage))
            {
                g.TranslateTransform(rotatedImage.Width / 2, rotatedImage.Height / 2); //set the rotation point as the center into the matrix
                g.RotateTransform(angle); //rotate
                g.TranslateTransform(-rotatedImage.Width / 2, -rotatedImage.Height / 2); //restore rotation point into the matrix
                g.DrawImage(MyPicture[0], new Point((width - MyPicture[0].Width) / 2, (height - MyPicture[0].Height) / 2)); //draw the image on the new bitmap
            }
            MyPicture[0] = rotatedImage;
        }

        protected override void RotateMyImages()
        {
            int oldObjectOrientation = 1;
            if (Field.Xdelta == 0 && Field.Ydelta < 0)

                ObjectOrientation = 1;

            else if (Field.Xdelta > 0 && Field.Ydelta < 0)

                ObjectOrientation = 2;

            else if (Field.Xdelta > 0 && Field.Ydelta == 0)

                ObjectOrientation = 3;

            else if (Field.Xdelta > 0 && Field.Ydelta > 0)

                ObjectOrientation = 4;

            else if (Field.Xdelta == 0 && Field.Ydelta > 0)

                ObjectOrientation = 5;

            else if (Field.Xdelta < 0 && Field.Ydelta > 0)

                ObjectOrientation = 6;

            else if (Field.Xdelta < 0 && Field.Ydelta == 0)

                ObjectOrientation = 7;

            else if (Field.Xdelta < 0 && Field.Ydelta < 0)

                ObjectOrientation = 8;
            float angle = (ObjectOrientation - oldObjectOrientation) * 45.0f;
            float alpha = (ObjectOrientation - oldObjectOrientation) * 45.0f;
            while (alpha < 0) alpha += 360;

            float gamma = 90;
            float beta = 180 - angle - gamma;
            if (ally)
                MyPicture[0] = new Bitmap(SiegeOfTheFortress.Properties.Resources.cannon);
            else
                MyPicture[0] = new Bitmap(SiegeOfTheFortress.Properties.Resources.cannonenemy);

            float c1 = MyPicture[0].Height;
            float a1 = (float)(c1 * Math.Sin(alpha * Math.PI / 180));
            float b1 = (float)(c1 * Math.Sin(beta * Math.PI / 180));

            float c2 = MyPicture[0].Width;
            float a2 = (float)(c2 * Math.Sin(alpha * Math.PI / 180));
            float b2 = (float)(c2 * Math.Sin(beta * Math.PI / 180));

            int width = Convert.ToInt32(Math.Abs(b2) + Math.Abs(a1));
            int height = Convert.ToInt32(Math.Abs(b1) + Math.Abs(a2));

            Bitmap rotatedImage = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(rotatedImage))
            {
                g.TranslateTransform(rotatedImage.Width / 2, rotatedImage.Height / 2); //set the rotation point as the center into the matrix
                g.RotateTransform(angle); //rotate
                g.TranslateTransform(-rotatedImage.Width / 2, -rotatedImage.Height / 2); //restore rotation point into the matrix
                g.DrawImage(MyPicture[0], new Point((width - MyPicture[0].Width) / 2, (height - MyPicture[0].Height) / 2)); //draw the image on the new bitmap
            }
            MyPicture[0] = rotatedImage;
        }

        protected override void Yt_Tick(object sender, EventArgs e)
        {
            if (mystate == 0)
                tekp = 0;
            else if (mystate == 3)
            {
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
                        mystate = 4;
                        tekp = 0;
                    }
                }
            }
            else if (mystate == 4)
            {
                if (tekp == 0)
                    tekp = 2;
                else
                {
                    tekp++;
                }
                if (tekp==6)
                {
                    MyMessage mes = new MyMessage();
                    CharHookIsOver(this, mes);
                }
            }
        }

        public override void UpdateObj(object sender, MyMessage mes)
        {
            if (mystate == 1)
            {

                //if ((x == -Field.Leftdest + Field.pj[Field.Tekindex] * 40) && (y == -Field.Topdest + Field.pi[Field.Tekindex] * 40 + 23))
                if ((factx == /*-Field.Leftdest + */Field.pj[Field.Tekindex] * 40) && (facty == /*-Field.Topdest + */Field.pi[Field.Tekindex] * 40 + 23))

                {
                    if (Field.pj[Field.Tekindex + 1] != -1)
                    {

                        int a = Field.pj[Field.Tekindex + 1] - Field.pj[Field.Tekindex];
                        Field.Xdelta = a >= 0 ? a == 0 ? 0 : 5 : -5;
                        a = Field.pi[Field.Tekindex + 1] - Field.pi[Field.Tekindex];
                        Field.Ydelta = a >= 0 ? a == 0 ? 0 : 5 : -5;
                        Field.Tekindex = Field.Tekindex + 1;
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

                            AnRotateMyImages();
                            //Потом вызвать сообщение атаковать
                            MyMessage newmes = new MyMessage();
                            newmes.Profile.I = Field.Igoal;
                            newmes.Profile.J = Field.Jgoal;

                            Attack(this, newmes);
                        }
                        else
                        {
                            mystate = 0;


                            RotateMyImages();
                            //Потом вызвать сообщение свободен, окончание хода
                            MyMessage newmes = new MyMessage();

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
                if (mystate != 3 || mystate == 3 && countanim % 2 == 0)
                {
                    if (ObjectOrientation % 2 == 1)
                        mes.dc1.DrawImage(MyPicture[tekp], x + 5, y + 5, w - 10, l - 10);
                    else
                        mes.dc1.DrawImage(MyPicture[tekp], x, y, w, l);
                }
                else
                {
                    if (ObjectOrientation % 2 == 1)
                    {
                        mes.dc1.DrawImage(MyPicture[0], x + 5, y + 5, w - 10, l - 10);
                        mes.dc1.DrawImage(MyPicture[1], x + 5, y + 5, w - 10, l - 10);
                    }
                    else
                    {
                        mes.dc1.DrawImage(MyPicture[0], x, y, w, l);
                        mes.dc1.DrawImage(MyPicture[1], x, y, w, l);
                    }
                }
            }
        }
        public override void Paint_face(object sender, MyMessage mes)
        {
            if (mes.Character == this)
                mes.dc1.DrawImage(MyPicture[0], mes.left, mes.top, mes.right - mes.left, mes.bottom - mes.top);

        }
        public override void Hook(MyMessage mes)
        {
            if (mes.Info != 0)
                mes.Impact.Damage = -damage * 2;
            else
                mes.Impact.Damage = -damage;
            mes.Impact.Effect.Efftype = TypeofCharacterEffects.Bleeding; //Тип наложенного эффекта (у каждого "вида" героя-заклинателя он свой)
            mes.Impact.Effect.Ident = null; //Идентификатор того, кто его наложил
            mes.Impact.Effect.Period = true;//Периодический или нет
            mes.Impact.Effect.Type = 1; //На что эффект влияет
            mes.Impact.Effect.Value = -40; //Значение
            mes.Impact.Effect.Moves = 3; //Количество оставшихся ходов
        }

        public void DeleteBall(object sender, MyMessage mes)
        {
            if (MyBall != null)
            {
                if(CauseDeath!=null)
                    CauseDeath(this, mes);
                MyBall = null;
            }
        }
    }
}
