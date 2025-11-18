using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;


namespace SiegeOfTheFortress
{
    [Serializable]
    public class Ball
    {
        private int x, y, starti, startj, endi, endj, w, l, mystate, tekp, dx, dy, factx, facty;
        [NonSerialized] private System.Windows.Forms.Timer MyT;
        private Bitmap[] MyPicture;
        private Character_impact ballvalue;
        public static event UpdateObject Ballattack;
        public static event UpdateObject DeleteMyself;

        public Ball(int x, int y, int starti, int startj, int w, int l, MyMessage mes)
        {
            MyPicture = new Bitmap[11];
            MyPicture[0] = new Bitmap(SiegeOfTheFortress.Properties.Resources.fire_ball1) ;
            MyPicture[1] = new Bitmap(SiegeOfTheFortress.Properties.Resources.fire_ball2);
            MyPicture[2] = new Bitmap(SiegeOfTheFortress.Properties.Resources.fire_ball3);
            MyPicture[3] = new Bitmap(SiegeOfTheFortress.Properties.Resources.fire_ball4);
            MyPicture[4] = new Bitmap(SiegeOfTheFortress.Properties.Resources.fire_ball5);
            MyPicture[5] = new Bitmap(SiegeOfTheFortress.Properties.Resources.fire_ball6);
            MyPicture[6] = new Bitmap(SiegeOfTheFortress.Properties.Resources.hit1);
            MyPicture[7] = new Bitmap(SiegeOfTheFortress.Properties.Resources.hit2);
            MyPicture[8] = new Bitmap(SiegeOfTheFortress.Properties.Resources.hit3);
            MyPicture[9] = new Bitmap(SiegeOfTheFortress.Properties.Resources.hit4);
            MyPicture[10] = new Bitmap(SiegeOfTheFortress.Properties.Resources.hit5);

            for (int ih = 0; ih < 11; ih++) MyPicture[ih].MakeTransparent(Color.White);

            MyT = new System.Windows.Forms.Timer();
            MyT.Enabled = false;
            MyT.Interval = 350;
            tekp = 0;
            mystate = 0;

            ballvalue = mes.Impact;

            this.x = x;
            this.y = y;
            factx = x;
            facty = y;
            this.starti = starti;
            this.startj = startj;

            endi = Field.Igoal;
            endj = Field.Jgoal;

            this.w = w;
            this.l = l;

            Cannon.ActivateBall += StartMoving;
            Cannon.CauseDeath += StopMoving;
            MyT.Tick += Yt_Tick;
            Field.FieldUpdateObject += UpdateObj;
            Field.Fieldstartgameprocess += ObjectContinue;
            Form1.FormStopEvent += ObjectStop;
            Form1.FormContinueEvent += ObjectContinue;
            Field.FieldrectMapmove += ObjectMouseMove;

        }

        public void ObjectMouseMove(object sender, MyMessage mes)
        {
            x += mes.X;
            y += mes.Y;
        }

        public void ReSignMyself()
        {
            MyT = new System.Windows.Forms.Timer();
            MyT.Enabled = false;
            MyT.Interval = 350;
            Cannon.ActivateBall += StartMoving;
            Field.Fieldstartgameprocess += ObjectContinue;
            Field.FieldrectMapmove += ObjectMouseMove;

            Cannon.CauseDeath += StopMoving;
            MyT.Tick += Yt_Tick;
            Field.FieldUpdateObject += UpdateObj;
            Form1.FormStopEvent += ObjectStop;
            Form1.FormContinueEvent += ObjectContinue;
        }

        public void StopMoving(object sender, MyMessage mes)
        {
            UnSignMyself();
            ObjectStop(sender, mes);
        }

        public void UnSignMyself()
        {
            Cannon.ActivateBall -= StartMoving;
            Cannon.CauseDeath -= StopMoving;
            Field.Fieldstartgameprocess -= ObjectContinue;
            Field.FieldrectMapmove -= ObjectMouseMove;

            MyT.Tick -= Yt_Tick;
            Field.FieldUpdateObject -= UpdateObj;
            Form1.FormStopEvent -= ObjectStop;
            Form1.FormContinueEvent -= ObjectContinue;
        }

        public void ObjectStop(object sender, MyMessage mes)
        {
            MyT.Enabled = false;
        }

        public void ObjectContinue(object sender, MyMessage mes)
        {
            MyT.Enabled = true;
        }
        public void StartMoving(object sender, MyMessage mes)
        {
            float koeff = 180.0f / 3.1416f;
            int ig = Field.Igoal;
            int jg = Field.Jgoal;
            float angle;
            if (ig >= starti && jg > startj)
                angle = (float)Math.Atan2(ig - starti, jg - startj) * koeff;
            else if (ig < starti && jg >= startj)
                angle = 270.0f+(float)Math.Atan2(jg - startj, starti - ig) * koeff;
            else if (ig <= starti && jg < startj)
                angle = 180.0f + (float)Math.Atan2(starti - ig, startj - jg) * koeff;
            else
                angle = 90.0f + (float)Math.Atan2(startj - jg, ig - starti) * koeff;

            float alpha = angle;

            while (alpha < 0) alpha += 360;

            float gamma = 90;
            float beta = 180 - angle - gamma;
            for (int ii = 0; ii < 6; ii++)
            {
                float c1 = MyPicture[ii].Height;
                float a1 = (float)(c1 * Math.Sin(alpha * Math.PI / 180));
                float b1 = (float)(c1 * Math.Sin(beta * Math.PI / 180));

                float c2 = MyPicture[ii].Width;
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
                    g.DrawImage(MyPicture[ii], new Point((width - MyPicture[ii].Width) / 2, (height - MyPicture[ii].Height) / 2)); //draw the image on the new bitmap
                }
                MyPicture[ii] = rotatedImage;
            }
            dx = (endj - startj) * w / 10;
            dy = (endi - starti) * l / 10;

            MyT.Enabled = true;
        }

        public void Yt_Tick(object sender, EventArgs e)
        {
            if (mystate == 0)
            {
                tekp = ++tekp%6;

            }
            else 
            {
                if (tekp < 6)
                    tekp = 6;
                else
                    tekp++;
                if (tekp == 10)
                {
                    MyMessage mes = new MyMessage();
                    DeleteMyself(this, mes);
                }
            }
        }

        public void UpdateObj(object sender, MyMessage mes)
        {
            if (mystate == 0)
            {
                if (x >= -Field.Leftdest + endj * w && x <= -Field.Leftdest + (endj + 1) * w &&
                    y >= -Field.Topdest + endi * l && y <= -Field.Topdest + (endi + 1) * l)
                //if (factx >= /*-Field.Leftdest + */endj * 40 && factx <= /*-Field.Leftdest + */(endj + 1) * 40 &&
                //    facty >= /*-Field.Topdest + */endi * 40 && facty <= /*-Field.Topdest +*/ (endi + 1) * 40)
                {
                    MyMessage newmes = new MyMessage();
                    newmes.Profile.I = Field.Igoal;
                    newmes.Profile.J = Field.Jgoal;
                    newmes.Impact = ballvalue;
                    mystate = 1;
                    if(Ballattack!=null)
                        Ballattack(this, newmes);
                }
                else
                {
                    x += dx;
                    y += dy;
                    factx += dx;
                    facty += dy;
                }
            }
            Show(sender, mes);
        }

        public void Show(object sender, MyMessage mes)
        {
            mes.dc1.DrawImage(MyPicture[tekp], x, y, w, l);

        }
    }
}
