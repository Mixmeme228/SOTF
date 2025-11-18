using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiegeOfTheFortress
{
    [Serializable]
    public class Minifield
    {
        private float x, y, xe, ye, xk, yk, xek, yek, l, w;
        private Bitmap fon;
        private RectangleF /*destinationRect, sourceRect, */visibleRect;

        public Minifield(float x, float y, float xe, float ye, float xk, float yk, float xek, float yek, Bitmap fon)
        {
            Field.FieldShow += Show;
            Field.FieldUpdateObject += Show;
            Field.MyFieldSizeChanged += MinifieldSizeChange;
            Field.Fieldstartgameprocess += GOstartgame;
            //Field.FieldrectMinimapmove += MinifieldMouseMove;
            Field.FieldrectMapmove += MinifieldMouseMove;


            this.x = x;
            this.y = y;

            this.xe = xe;
            this.ye = ye;

            l = xe - x;
            w = ye - y;

            this.xek = xek;
            this.yek = yek;

            this.xk = xk;
            this.yk = yk;

            this.fon = fon;
            //destinationRect = new RectangleF(x, y, xe, ye);
            //sourceRect = new RectangleF(0, 0, 1.00f * fon.Width, 1.00f * fon.Height);
            visibleRect = new RectangleF(xk, yk, xek-xk, yek-yk);
        }

        public void MinifieldSizeChange(object sender, MyMessage mes)
        {
            x = mes.left;
            y = mes.top;
            xe = x + l;
            ye = y + w;
            xk = x+mes.X / 1698.0f * l;
            yk = y+mes.Y / 982.0f * w;
            xek = xk+mes.L*l;
            yek = yk+mes.W*w;
            //destinationRect = new RectangleF(x, y, xe, ye);
            visibleRect = new RectangleF(xk, yk, xek - xk, yek - yk);
        }

        public void MinifieldMouseMove(object sender, MyMessage mes)
        {
            xk = x + mes.left/1698.0f*l;
            yk = y + mes.top/982.0f*w;
            xek = xk + mes.L * l;
            yek = yk + mes.W * w;
            visibleRect = new RectangleF(xk, yk, xek - xk, yek - yk);

        }

        public void Show(object sender, MyMessage mes)
        {
            mes.dc1.DrawImage(fon, x, y, l, w);
            SolidBrush brush = new SolidBrush(Color.FromArgb(125, 169, 169, 169));
            mes.dc1.FillRectangle(brush, visibleRect);
            brush.Dispose();
        }
        public void DeleteMiniField(object sender, MyMessage mes)
        {
            Field.FieldShow -= Show;
            Field.MyFieldSizeChanged -= MinifieldSizeChange;
            Field.Fieldstartgameprocess -= GOstartgame;
            Field.FieldUpdateObject -= Show;
            //Field.FieldrectMinimapmove -= MinifieldMouseMove;
            Field.FieldrectMapmove -= MinifieldMouseMove;


        }

        public void GOstartgame(object sender, MyMessage mes)
        {
            Field.FieldShow -= Show;
            Field.FieldShow += Show;
            Field.FieldUpdateObject -= Show;
            Field.FieldUpdateObject += Show;
        }
    }
}
