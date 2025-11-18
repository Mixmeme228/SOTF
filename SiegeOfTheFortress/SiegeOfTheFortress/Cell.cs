using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace SiegeOfTheFortress
{
    [Serializable]
    public class Cell
    {
        private int x, y, i, j, l, w, d, metka, value, left, top, right, bottom, factx, facty;
        Color color;
        private GameObject internalobj;
        
        public Cell(int x, int y, int i, int j, int l, int w, Color color, int d)
        {
            value = -2;
            factx = x;
            facty = y;
            this.x = x;
            this.y = y;
            this.i = i;
            this.j = j;
            this.l = l;
            this.w = w;
            this.color = color;
            this.d = d;
            internalobj = null;
            left = x - d / 2;
            right = x + d / 2;
            top = y - d / 2;
            bottom = y + d / 2;
            metka = 0;
            Field.FieldrectClick += MouseClick;
            Field.FieldDelete += DeleteChar;
            Field.FieldAddChar += AddChar;
            Field.FieldUpdateObject += Show;
            Field.FieldrectMapmove += CellMouseMove;
            Field.Fieldstartgameprocess += StartGame;
        }

        public void StartGame(object sender, MyMessage mes)
        {
            if (mes.Code == 1)
                metka = 0;
        }

        public void CellMouseMove(object sender, MyMessage mes)
        {
            x += mes.X;
            y += mes.Y;
            left = x - d / 2;
            right = x + d / 2;
            top = y - d / 2;
            bottom = y + d / 2;
        }
        public void AddChar(object sender, MyMessage mes)
        {
            if (mes.Profile.I == i && mes.Profile.J == j)
            {
                internalobj = mes.Myobject;
                value = mes.Annex;
            }
        }

        public void DeleteChar(object sender, MyMessage mes)
        {
            if(mes.Profile.I==i&&mes.Profile.J==j)
            {
                internalobj = null;
                value = -2;
            }
        }
        public int Value { get { return value; } set { this.value = value; } }
        public int Metka { get { return metka; } }
        public GameObject InternalObj { get { return internalobj; } set { internalobj = value; } }
        public void LeeAlgorithm(MyMessage mes)
        {
            value = mes.Annex;
            if (mes.Profile.Ally && mes.Answer != 0)
                metka = 1;
            else
                metka = 0;
            color = mes.color;
            mes.Myobject = internalobj;
        }
        
        public void ReturnPast(MyMessage mes)
        {
            if (value > 0)
                value = -2;
            if (value == 0)
                if (mes.Profile.Ally)
                    value = -3;
                else
                    value = -1;
            color = Color.White;
            metka = 0;
        }

        public void MouseClick(object sender, MyMessage mes)
        {
            if (mes.X >= left && mes.X <= right && mes.Y >= top && mes.Y <= bottom)
            {
                //if (mes.but == MouseButtons.Right)
                //{
                //    color = Color.FromArgb(80, 0, 255, 0);
                //    metka = 1;
                //}
                //else
                //{
                if (mes.Code == 1)
                {
                    //mes.Code = 6;
                    if (metka == 1)
                        mes.State = true;
                    if (internalobj == null)
                    {
                        mes.Profile.I = i;
                        mes.Profile.J = j;
                        mes.Myobject = internalobj;
                        mes.X = j;
                        mes.Y = i;
                    }
                    else
                    {
                        mes.Profile.Ally = false;
                        mes.Myobject = internalobj;
                        internalobj.Createprofile(this, mes);
                        mes.Profile.I = i;
                        mes.Profile.J = j;
                        mes.Myobject = internalobj;
                        mes.X = j;
                        mes.Y = i;
                    }
                }
                else
                {
                    mes.X = j;
                    mes.Y = i;
                }
            }
        }
        public void Show(object sender, MyMessage mes)
        {
            if (left<=mes.right&&right>=mes.left&&top<=mes.bottom&&bottom>=mes.top)
            {
                Pen hPen = new Pen(Brushes.Black);
                hPen.Width = 0.5F;
                mes.dc1.DrawLine(hPen, left, top, right - 1, top);
                mes.dc1.DrawLine(hPen, right - 1, top, right - 1, bottom - 1);
                mes.dc1.DrawLine(hPen, right - 1, bottom - 1, left, bottom - 1);
                mes.dc1.DrawLine(hPen, left, bottom - 1, left, top);
                hPen.Dispose();
                if (metka != 0)
                {
                    SolidBrush brush = new SolidBrush(color);
                    mes.dc1.FillRectangle(brush, left, top, right - left, bottom - top);
                    brush.Dispose();
                }
            }
            
        }
    }
}
