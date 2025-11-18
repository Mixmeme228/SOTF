using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SiegeOfTheFortress
{
    public class Aegrotat
    {
        private int x, y, l, w, speed, health, damage, area, points, areadamage, bareadamage, order, number;
        private int[] moves;
        private string name;
        private Bitmap speedimage, healthimage, damageimage, areaimage, areadamageimage, pointsimage, orderimage;
        private Color color;
        private TypeofCharacterEffects[] effs;
        private Bitmap[] images;

        public Aegrotat(int x, int y, int l, int w, int speed, int health, int damage, int area, int points, int areadamage, int bareadamage, int order, int number, int[] cmoves, TypeofCharacterEffects[] ceffs, string name, Color color)
        {
            this.x = x;
            this.y = y;
            this.l = l;
            this.w = w;
            this.speed = speed;
            this.health = health;
            this.damage = damage;
            this.area = area;
            this.points = points;
            this.areadamage = areadamage;
            this.bareadamage = bareadamage;
            this.order = order;
            this.number = number;
            this.name = name;
            this.color = color;
            moves = new int[number];
            effs = new TypeofCharacterEffects[number];
            images = new Bitmap[number];
            for(int i = 0; i < number; i++)
            {
                moves[i] = cmoves[i];
                effs[i] = ceffs[i];
                switch (effs[i])
                {
                    case TypeofCharacterEffects.Bleeding:
                        images[i] = new Bitmap(SiegeOfTheFortress.Properties.Resources.Bleeding);
                        break;
                    case TypeofCharacterEffects.Swirlwind:
                        images[i] = new Bitmap(SiegeOfTheFortress.Properties.Resources.Swirlwind);
                        break;
                }
                images[i].MakeTransparent(Color.White);

            }
            speedimage = new Bitmap(SiegeOfTheFortress.Properties.Resources.speed);
            healthimage = new Bitmap(SiegeOfTheFortress.Properties.Resources.health);
            damageimage = new Bitmap(SiegeOfTheFortress.Properties.Resources.damage);
            areaimage = new Bitmap(SiegeOfTheFortress.Properties.Resources.area);
            areadamageimage = new Bitmap(SiegeOfTheFortress.Properties.Resources.areadamage);
            pointsimage = new Bitmap(SiegeOfTheFortress.Properties.Resources.points);
            orderimage = new Bitmap(SiegeOfTheFortress.Properties.Resources.order);

            speedimage.MakeTransparent(Color.White);
            healthimage.MakeTransparent(Color.White);
            damageimage.MakeTransparent(Color.White);
            areaimage.MakeTransparent(Color.White);
            areadamageimage.MakeTransparent(Color.White);
            pointsimage.MakeTransparent(Color.White);
            orderimage.MakeTransparent(Color.White);


            Field.FieldUpdateObject += Show;
            Field.FieldDontHover += DeleteMyself;
            Field.FieldShow += Show;

        }

        public void DeleteMyself(object sedner, MyMessage mes)
        {
            Field.FieldUpdateObject -= Show;
            Field.FieldDontHover -= DeleteMyself;
            Field.FieldShow -= Show;

        }

        public void Show(object sender, MyMessage mes)
        {
            Pen hPen = new Pen(Brushes.WhiteSmoke);
            hPen.Width = 0.8F;
            int end;
            mes.dc1.DrawLine(hPen, x, y, x + l, y);
            mes.dc1.DrawLine(hPen, x + l, y, x + l, y + w);
            mes.dc1.DrawLine(hPen, x + l, y + w, x, y + w);
            mes.dc1.DrawLine(hPen, x, y + w, x, y);
            hPen.Dispose();

            SolidBrush myBrush1 = new SolidBrush(Color.AntiqueWhite);
            mes.dc1.FillRectangle(myBrush1, new Rectangle(x + 1, y + 1, l - 1, w - 1));
            myBrush1.Dispose();




            Font drawFont = new Font("Arial", 8);
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            StringFormat drawFormat = new StringFormat();
            mes.dc1.DrawString(name, drawFont, drawBrush, x+10, y + 5, drawFormat);
            
            drawBrush.Dispose();
            SolidBrush drawBrush1 = new SolidBrush(Color.Green);
            SolidBrush drawBrush2 = new SolidBrush(Color.Red);
            SolidBrush drawBrush3 = new SolidBrush(Color.Violet);
            SolidBrush drawBrush4 = new SolidBrush(Color.Blue);
            SolidBrush drawBrush5 = new SolidBrush(Color.Firebrick);
            SolidBrush drawBrush6 = new SolidBrush(Color.Orange);
            SolidBrush drawBrush7 = new SolidBrush(Color.DeepSkyBlue);




            mes.dc1.DrawImage(healthimage, x, y + 20, 20, 20);
            string str = health.ToString();
            mes.dc1.DrawString(str, drawFont, drawBrush1, x + 25, y + 25);

            mes.dc1.DrawImage(damageimage, x, y + 45, 20, 20);
            mes.dc1.DrawString(damage.ToString(), drawFont, drawBrush2, x + 25, y + 50, drawFormat);

            mes.dc1.DrawImage(speedimage, x, y + 70, 20, 20);
            mes.dc1.DrawString(speed.ToString(), drawFont, drawBrush3, x + 25, y + 75, drawFormat);

            mes.dc1.DrawImage(areaimage, x, y + 95, 20, 20);
            mes.dc1.DrawString(area.ToString(), drawFont, drawBrush4, x + 25, y + 100, drawFormat);

            if (bareadamage > 0)
            {
                mes.dc1.DrawImage(areadamageimage, x, y + 120, 20, 20);
                mes.dc1.DrawString(areadamage.ToString(), drawFont, drawBrush5, x + 25, y + 125, drawFormat);

                mes.dc1.DrawImage(pointsimage, x, y + 145, 20, 20);
                mes.dc1.DrawString(points.ToString(), drawFont, drawBrush6, x + 25, y + 150, drawFormat);
                end = y+175;
                if (order != -1)
                {
                    mes.dc1.DrawImage(orderimage, x, y + 170, 20, 20);
                    mes.dc1.DrawString(order.ToString(), drawFont, drawBrush7, x + 25, y + 175, drawFormat);
                    end = y+200;
                }
            }
            else
            {
                mes.dc1.DrawImage(pointsimage, x, y + 120, 20, 20);
                mes.dc1.DrawString(points.ToString(), drawFont, drawBrush6, x + 25, y + 125, drawFormat);
                end = y+150;
                if (order != -1)
                {
                    mes.dc1.DrawImage(orderimage, x, y + 145, 20, 20);
                    mes.dc1.DrawString(order.ToString(), drawFont, drawBrush7, x + 25, y + 150, drawFormat);
                    end = y + 175;
                }

            }

            int c = end+15;

            for(int i = 0; i < number; i++)
            {
                mes.dc1.DrawImage(images[i], x, c, 20, 20);
                mes.dc1.DrawString(moves[i].ToString(), drawFont, drawBrush7, x + 25, c+5, drawFormat);
                c += 25;
            }

            drawBrush1.Dispose();
            drawBrush2.Dispose();
            drawBrush3.Dispose();
            drawBrush4.Dispose();
            drawBrush5.Dispose();
            drawBrush6.Dispose();
            drawBrush7.Dispose();

            drawFont.Dispose();
            drawFormat.Dispose();

            SolidBrush myBrush = new SolidBrush(color);
            mes.dc1.FillRectangle(myBrush, new Rectangle(x + 1, end, l, 10));
            myBrush.Dispose();
        }
    }
}
