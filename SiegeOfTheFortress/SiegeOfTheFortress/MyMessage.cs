using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace SiegeOfTheFortress
{
    public class MyMessage: EventArgs
    {
        public int Code;
        public int Annex;
        public int Info;
        public bool State;
        public int Answer;
        public int Del;
        public Character_impact Impact;
        public int X;
        public int Y;
        public Point Tpoint;
        public Color color;
        public int left;
        public int top;
        public int right;
        public int bottom;
        public int clientleft;
        public int clienttop;
        public int clientright;
        public int clientbottom;
        public float W;
        public float L;
        public int Data;
        public GameObject Myobject;
        public Character Character;
        public Charinfo Profile;
        //public Field Pole;
        //public PaintEventArgs e;
        public Graphics dc;
        public MouseButtons but;
        public Rectangle rect;
        public Graphics dc1;
        //public Form1 exampleForm;
        public string filename;
        public TypeofCharacterEffects[] effs;
        public int[] moves;

        public MyMessage() : base() { }
    }
}
