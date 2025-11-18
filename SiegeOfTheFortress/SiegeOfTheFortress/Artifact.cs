using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Security.Policy;
using System.Windows.Forms;


namespace SiegeOfTheFortress
{
    [Serializable]
    public class Artifact : GameObject
    {
        public static event UpdateObject ArtifactisAttacked;
        public Artifact(int x, int y, int i, int j, int bhealth, int value, int direction, TypeofCharacter type, bool openhide) : base(x, y, i, j, bhealth, value, direction, type, openhide)
        {
            MyPicture = new Bitmap[7];
            MyPicture[0] = new Bitmap(SiegeOfTheFortress.Properties.Resources.artifact);
            MyPicture[1] = new Bitmap(SiegeOfTheFortress.Properties.Resources.hit2);
            MyPicture[2] = new Bitmap(SiegeOfTheFortress.Properties.Resources.cannon_died1);
            MyPicture[3] = new Bitmap(SiegeOfTheFortress.Properties.Resources.cannon_died2);
            MyPicture[4] = new Bitmap(SiegeOfTheFortress.Properties.Resources.cannon_died3);
            MyPicture[5] = new Bitmap(SiegeOfTheFortress.Properties.Resources.cannon_died4);
            MyPicture[6] = new Bitmap(SiegeOfTheFortress.Properties.Resources.cannon_died5);
            for (int ih = 1; ih < 7; ih++)
                MyPicture[ih].MakeTransparent(Color.White);
            MyPicture[0].MakeTransparent(Color.FromArgb(230,230,230));
            if(openhide)
                Field.Fieldquery += GetStatus;
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
                Field.Fieldquery += GetStatus;

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
                Field.Fieldquery -= GetStatus;

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

        public Artifact(Artifact primer):this(primer.x, primer.y, primer.i, primer.j, primer.bhealth, primer.value, primer.direction, /*primer.owner,*/ primer.type, !primer.openhide)
        {

        }
        public override GameObject Clone1() {
            return new Artifact(this);
        }
        public override void Show(object sender, MyMessage mes)
        {
            if (x <= mes.right && x+l >= mes.left && y <= mes.bottom && y+w >= mes.top)
            {
                if (mystate != 4)
                {
                    mes.dc1.DrawImage(MyPicture[0], x, y, w, l);
                    if (mystate == 3 && tekp != 0)
                        mes.dc1.DrawImage(MyPicture[1], x, y, w, l);
                }
                else
                    mes.dc1.DrawImage(MyPicture[tekp % 7], x, y, w, l);
            }
        }

        protected override void Yt_Tick(object sender, EventArgs e)
        {
            if (mystate == 3)
            {
                countanim++;
                tekp = ++tekp % 2;
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
                    tekp++;
                if (tekp == 6)
                {
                    MyMessage mes = new MyMessage();
                    CharHookIsOver(this, mes);
                }
            }
        }

        protected override void DeleteMyObject(object sender, MyMessage mes)
        {

            if (mes.Profile.I == i && mes.Profile.J == j)
            {
                if (openhide)
                {
                    Field.Fieldstartgameprocess -= GOstartgame;

                    Form1.FormStopEvent -= ObjectStop;
                    Form1.FormContinueEvent -= ObjectContinue;
                    Field.FieldUpdateObject -= UpdateObj;//Сделать, на самом деле, потом
                    MyT.Tick -= Yt_Tick;//Сделать, на самом деле, потом
                    Field.FieldCreateProfile -= Createprofile;
                    Character.Characterattack -= Taketheattack;
                    Ball.Ballattack -= Taketheattack;
                    Field.FieldShow -= Show;
                    Field.FieldDelete -= DeleteMyObject;

                    Field.FieldMouseHover -= ObjectisHover;

                    Field.Fieldquery -= GetStatus;
                }
                else if(mes.Code==123)
                {
                    Field.FieldDelete -= DeleteMyObject;
                    Field.FieldrectMapmove -= ObjectMouseMove;

                    MyT.Enabled = false;
                }
            }
        }

        public override void UpdateObj(object sender, MyMessage mes)
        {
                Show(sender, mes);
        }

        public void GetStatus(object sender, MyMessage mes)
        {
            mes.Profile.I = i;
            mes.Profile.J = j;
            mes.Annex = health;
        }

        public override void Taketheattack(object sender, MyMessage mes)
        {
            if (Field.Igoal == i && Field.Jgoal == j)
            {
                Changehealth(mes.Impact.Damage);
                mystate = 3;
                ArtifactisAttacked(this, mes);
            }
            
        }
    }
}
