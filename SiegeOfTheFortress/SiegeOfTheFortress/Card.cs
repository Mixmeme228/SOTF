using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SiegeOfTheFortress
{
    [Serializable]
    public class Card
    {
        private int x, y, index, l, w, difference, period, health, damage, numberofmove;
        //private bool ally;
        private Color color;
        public Character person;
        public static event UpdateObject CardFace;

        public Card(int l, int w, Color color, int health, int damage, int period, bool ally, Character person)
        {
            this.x = 0;
            this.y = 0;
            this.l = l;
            this.w = w;
            this.health = health;
            this.damage = damage;
            this.index = 0;
            this.color = color;
            this.difference = 0;
            this.period = period;
            this.person = person;
            //this.ally = ally;
            numberofmove = 1;
            Field.Fieldapplyeffects += Endmove;
            Field.FieldDelete += DeleteMyCard;
            Field.MyFieldSizeChanged += CardSizeChanged;
            Field.FieldShow += Show;
            Field.FieldUpdateObject += Show;
            Field.FieldrectCardmove += Cardmove;
            Field.FieldAskingforOrder += AnsweringonOrder;
        }

        public void AnsweringonOrder(object sender, MyMessage mes)
        {
            if (mes.Character == person)
            {
                mes.Data = index;
            }
        }

        public void Cardmove(object sender, MyMessage mes)
        {
            y += mes.Y;
            if (y + w + 30 > mes.bottom)
                mes.Answer = 1;
            if (y < 23)
                mes.Annex = 1;
        }

        public void CardSizeChanged(object sender, MyMessage mes)
        {
            x = mes.right;

        }
        public int Period { get { return period; } }
        public int Difference { get { return difference; } set { difference = value; } }
        public int Index { get { return index; } set { index = value; } }
        public int X { get { return x; } set { x = value; } }
        public int Y {
            get
            {
                return y;
            }
            set {
               y = 50 + (w + 10) * value;
            } 
        }
        public void Endmove(object sender, MyMessage mes)
        {
            if(mes.Character==person)
                numberofmove++;
        }
        public void UpdateCard(MyMessage mes)
        {
            health = mes.Profile.Health;
            damage = mes.Profile.Damage;
            period = mes.Profile.Period * numberofmove;
        }

        public void GetEffStruct(MyMessage mes)
        {
            person.Get_effstruct(mes);
        }

        public void DeleteMyCard(object sender, MyMessage mes)
        {
            if(mes.Character==person)
            {
                Field.Fieldapplyeffects -= Endmove;
                Field.FieldDelete -= DeleteMyCard;
                Field.MyFieldSizeChanged -= CardSizeChanged;
                Field.FieldShow -= Show;
                Field.FieldrectCardmove -= Cardmove;
                Field.FieldAskingforOrder -= AnsweringonOrder;
                Field.FieldUpdateObject -= Show;

            }
        }
        public void Show(object sender, MyMessage mes)
        {
            Pen hPen = new Pen(Brushes.Black);
            hPen.Width = 0.8F;
            mes.dc1.DrawLine(hPen, x, y, x-l, y);
            mes.dc1.DrawLine(hPen, x-l, y, x-l, y+w);
            mes.dc1.DrawLine(hPen, x-l, y+w, x, y+w);
            mes.dc1.DrawLine(hPen, x, y+w, x, y);
            hPen.Dispose();

            SolidBrush myBrush1 = new SolidBrush(Color.FromArgb(160, 160, 160));
            mes.dc1.FillRectangle(myBrush1, new Rectangle(x-l+1, y+1, l-1, w-1));

            mes.dc1.FillRectangle(new SolidBrush(Color.White), new Rectangle(x - 20, y + 1, 20, 20));

            myBrush1.Dispose();

            Font drawFont = new Font("Arial", 14);
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            StringFormat drawFormat = new StringFormat();
            mes.dc1.DrawString(difference.ToString(), drawFont, drawBrush, x-l + 50, y + 20, drawFormat);

            mes.dc1.DrawString(index.ToString(), drawFont, new SolidBrush(Color.Orange), x - 20, y, drawFormat);

            drawFont.Dispose();
            drawBrush.Dispose();
            SolidBrush drawBrush1 = new SolidBrush(Color.Green);
            SolidBrush drawBrush2 = new SolidBrush(Color.Red);


            Font drawFont1 = new Font("Arial", 10);
            mes.dc1.DrawString(health.ToString(), drawFont1, drawBrush1, x -60, y + 30, drawFormat);
            mes.dc1.DrawString(damage.ToString(), drawFont1, drawBrush2, x -30, y + 30, drawFormat);
            drawBrush1.Dispose();
            drawBrush2.Dispose();
            drawFormat.Dispose();

            mes.left = x -l + 5;
            mes.top = y + 5;
            mes.right = x -l + 45;
            mes.bottom = y + 45;
            mes.Character = person;
            CardFace(this, mes);

            SolidBrush myBrush = new SolidBrush(color);
            mes.dc1.FillRectangle(myBrush, new Rectangle(x-l + 5, y + 48, l - 10, 10));
            myBrush.Dispose();
        }
    }
}
