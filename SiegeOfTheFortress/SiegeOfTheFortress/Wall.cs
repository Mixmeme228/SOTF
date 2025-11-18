using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiegeOfTheFortress
{
    [Serializable]
    public class Wall : GameObject
    {
        public Wall(int x, int y, int i, int j, int bhealth, int value, int direction, TypeofCharacter type, bool openhide) : base(x, y, i, j, bhealth, value, direction, type, openhide)
        {
            MyPicture = new Bitmap[7];
            if (direction == 0)
                MyPicture[0] = new Bitmap(SiegeOfTheFortress.Properties.Resources.wall1);
            else
                if (direction == 1)
            {
                MyPicture[0] = new Bitmap(SiegeOfTheFortress.Properties.Resources.wall1);
                MyPicture[0].RotateFlip(RotateFlipType.Rotate90FlipNone);
            }
            else
                if(direction == 2)
                MyPicture[0] = new Bitmap(SiegeOfTheFortress.Properties.Resources.wall);
            
            MyPicture[1] = new Bitmap(SiegeOfTheFortress.Properties.Resources.hit2);
            MyPicture[2] = new Bitmap(SiegeOfTheFortress.Properties.Resources.cannon_died1);
            MyPicture[3] = new Bitmap(SiegeOfTheFortress.Properties.Resources.cannon_died2);
            MyPicture[4] = new Bitmap(SiegeOfTheFortress.Properties.Resources.cannon_died3);
            MyPicture[5] = new Bitmap(SiegeOfTheFortress.Properties.Resources.cannon_died4);
            MyPicture[6] = new Bitmap(SiegeOfTheFortress.Properties.Resources.cannon_died5);
            for (int ih = 0; ih < 7; ih++)
                MyPicture[ih].MakeTransparent(Color.White);

        }
        public Wall(Wall primer) :this(primer.x, primer.y, primer.i, primer.j, primer.bhealth, primer.value, primer.direction, primer.type, !primer.openhide)
        {

        }

        protected override void Yt_Tick(object sender, EventArgs e)
        {
            if (mystate == 0)
                tekp = 0;
            else if (mystate == 3)
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

        public override void UpdateObj(object sender, MyMessage mes)
        {
            Show(sender, mes);
        }
        public override GameObject Clone1(){
            return new Wall(this);
        }
        public override void Show(object sender, MyMessage mes)
        {

            if (x <= mes.right && x+l >= mes.left && y <= mes.bottom && y+w >= mes.top)
            {
                if (mystate != 4)
                {
                    if (direction == 0)
                        mes.dc1.DrawImage(MyPicture[0], x, y + 5, w, l - 10);
                    else if (direction == 1)
                        mes.dc1.DrawImage(MyPicture[0], x + 5, y, w - 10, l);
                    else
                        mes.dc1.DrawImage(MyPicture[0], x, y, w, l);
                    if (mystate == 3 && tekp != 0)
                        mes.dc1.DrawImage(MyPicture[1], x, y, w, l);
                }
                else
                    mes.dc1.DrawImage(MyPicture[tekp % 7], x, y + 5, w, l - 10);
            }
        }

    };
}
