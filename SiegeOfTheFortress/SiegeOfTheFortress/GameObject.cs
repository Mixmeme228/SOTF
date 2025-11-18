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
    public abstract class GameObject
    {
        protected int x, y, i, j, bhealth, value, direction, health, w,l,/*stage,*/ countanim, factx, facty;
        protected TypeofCharacter type;
        protected Bitmap[] MyPicture;
        protected int mystate, tekp;
        [NonSerialized] protected Timer MyT;
        protected bool openhide;
        public static event UpdateObject Charhook;
        public GameObject(int x, int y, int i, int j, int bhealth, int value, int direction, TypeofCharacter type, bool openhide)
        {
            countanim = 0;
            this.x = x;
            this.y = y;
            factx = j*40;
            facty = i*40+23;
            this.i = i;
            this.j = j;
            this.bhealth = bhealth;
            health = bhealth;
            this.value = value;
            this.direction = direction;
            this.type = type;
            w = 40;
            l = 40;
            //stage = 0;
            MyPicture = null;
            mystate = 0;
            MyT = new Timer();
            MyT.Enabled = false;
            MyT.Interval = 350;
            tekp = 0;
            this.openhide = openhide;
            
            if (openhide)
            {
                Field.Fieldstartgameprocess += GOstartgame;
                MyT.Tick += Yt_Tick;

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
        

        public void ObjectMouseMove(object sender, MyMessage mes)
        {
            y += mes.Y;
            x += mes.X;
        }

        public void ObjectStop(object sender, MyMessage mes)
        {
            MyT.Enabled = false;
        }

        public void ObjectContinue(object sender, MyMessage mes)
        {
            MyT.Enabled = true;
        }

        public void ObjectisHover(object sender, MyMessage mes)
        {
            if (mes.X >= x && mes.X <= x + w && mes.Y >= y && mes.Y <= y + l)
            {
                mes.Myobject = this;
                mes.Code = 1;
                Createprofile(this, mes);
            }
        }

        public virtual void ReSignMySelf()
        {
            MyT = new Timer();
            MyT.Enabled = false;
            MyT.Interval = 350;

            if (openhide)
            {
                Field.Fieldstartgameprocess += GOstartgame;
                MyT.Tick += Yt_Tick;

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

        public virtual void UnSignMySelf()
        {
            
            if (openhide)
            {
                Field.Fieldstartgameprocess -= GOstartgame;
                MyT.Tick -= Yt_Tick;

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

        protected virtual void DeleteMyObject(object sender, MyMessage mes)
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
                    MyT.Enabled = false;
                }
                else if(mes.Code==123)
                {
                    Field.FieldDelete -= DeleteMyObject;
                    Field.FieldrectMapmove -= ObjectMouseMove;

                    MyT.Enabled = false;
                }
            }

        }

        public void GOstartgame(object sender, MyMessage mes)
        {
            MyT.Enabled = true;
            Field.FieldShow -= Show;

        }
        public abstract void Show(object sender, MyMessage mes);
        protected abstract void Yt_Tick(object sender, EventArgs e);
        public abstract void UpdateObj(object sender, MyMessage mes); //В зависимости от состояния, если 0 - то просто перерисовка на месте, если передвижение (1), то изменение своих координат, все необходимые повороты, если (2), то это атака и рисование других картинок на месте, а если (3), то это принятие атаки и по прошествии определенного времени будет сгенерировано событие приянтия атаки(?) - или не здесь, а в другом тике. Кроме того, есть состояние 4 - гибель, после этого будет отписка от всех событий
        protected void Changehealth(int diff)
        {
            if (diff + health > bhealth)
                health = bhealth;
            else
                health += diff;
        }
        public virtual void Createprofile(object sender, MyMessage mes)
        {
            if (mes.Myobject == this)
            {
                mes.Profile.Health = health;
                mes.Profile.Value = value;
                mes.Profile.I = i;
                mes.Profile.J = j;
                mes.Profile.Extra_info = type;
                mes.Profile.Direction = direction;
                mes.Tpoint.X = x;
                mes.Tpoint.Y = y;
            }
        }
        public virtual void Taketheattack(object sender, MyMessage mes) {
            if (i == Field.Igoal && j == Field.Jgoal)
            {
                mystate = 3;
                countanim = 0;
                Changehealth(mes.Impact.Damage);
            }
        }

        protected virtual void CharHookIsOver(object sender, MyMessage mes)
        {
            Charhook(sender, mes);
        }
        public virtual GameObject Clone1()
        {
            return null;
        }
    }
}
