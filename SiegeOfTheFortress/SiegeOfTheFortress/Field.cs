using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
//using static System.Net.Mime.MediaTypeNames;

namespace SiegeOfTheFortress
{
    public delegate void UpdateObject(object sender, MyMessage mes);

    [Serializable]
    public class Field
    {
        private int x, y, n, m, l,w,d, moves, money, mypoints, enemypoints, xe, ye, extramoves, allenemypoints, xek, yek, oldmousex, oldmousey, mousemove, leftsourceimage, topsourceimage;
        public static event UpdateObject FieldrectClick, FieldrectCardmove, FieldrectMapmove, /*FieldrectMinimapmove,*/ FieldMouseHover, FieldDontHover;
        public static event UpdateObject Fieldenemymove, FieldAskingforOrder, FieldGameOver, FieldChangeMoney;
        public static event UpdateObject Fieldattack, Fieldhook, FieldAddChar, FieldNextMove, FieldWorkAndDie;
        public static event UpdateObject FieldUpdateObject, Fieldsearchdc, Fieldquery, FieldShow, FieldDelete, Fieldapplyeffects;
        public static event UpdateObject Fieldstartgameprocess, FieldGetMoney, FieldGetPoints/*, FieldStartAction, FieldEndAction*/;
        public static event UpdateObject MyFieldSizeChanged;
        public static event UpdateObject FieldCreateProfile;
        private Cell[,] My_cell;
        private ListofGameObjects FirstGObject, ExtrafirstGObject;
        private ListofCharacters First_character, Extrafirst_character;
        private ListofCards First_card;
        private bool flag, game, gamermove, gamemove, booleye, boolnearenemy, boolnearally, gameorprepare, clickstate, mapmove;
        //[NonSerialized] private Form1 owner;
        private Witch Commander;
        //private int count;
        private int numberofcharacters, extranumberofcharacters;
        //private int currentvariant;
        [NonSerialized] private Graphics dcfield;
        private Bitmap fon, picture;
        private RectangleF destinationRect, sourceRect;
        private Minifield MyMiniField;
        [NonSerialized] private Aegrotat Certificate;

        static public int[] pj;
        static public int[] pi;
        static int stateofprocess;
        static int igoal;
        static int jgoal;
        static int xdelta;
        static int ydelta;
        static int tekindex;
        static int leftdest, topdest;

        static public int Leftdest
        {
            get
            {
                return leftdest;
            }
        }

        static public int Topdest
        {
            get
            {
                return topdest;
            }
        }

        static Field()
        {
            pj = new int[0];
            pi = new int[0];
        }
        static public int Tekindex
        {
            get
            {
                return tekindex;
            }
            set
            {
                tekindex = value;
            }
        }
        static public int Xdelta
        {
            get
            {
                return xdelta;
            }
            set
            {
                xdelta = value;
            }
        }
        static public int Ydelta
        {
            get
            {
                return ydelta;
            }
            set
            {
                ydelta = value;
            }
        }
        static public int Stateofprocess
        {
            get
            {
                return stateofprocess;
            }
            set
            {
                stateofprocess = value;
            }
        }
        static public int Igoal
        {
            get
            {
                return igoal;
            }
            set
            {
                igoal = value;
            }
        }
        static public int Jgoal
        {
            get
            {
                return jgoal;
            }
            set
            {
                jgoal = value;
            }
        }
        static public void AddMas(int[] pj, int[] pi, int n)
        {
            Field.pj = new int[n];
            Field.pi = new int[n];
            int i = 0;
            do
            {
                Field.pj[i] = pj[i];
                Field.pi[i] = pi[i];
                i++;
            } while (pj[i] != -1);
            Field.pj[i] = -1;
            Field.pi[i] = -1;
            
            tekindex = 0;
            int a = pj[1] - pj[0];
            xdelta = a >= 0 ? a == 0 ? 0 : 5 : -5;
            a = pi[1] - pi[0];
            ydelta = a >= 0 ? a == 0 ? 0 : 5 : -5;
        }

        public void FieldMouseUp(object sender, MyMessage mes)
        {
            if (mousemove!=0)

            {
                mousemove = 0;
            }
            
        }

        public void FieldMouseDown(object sender, MyMessage mes)
        {
            if (mousemove==0&&mes.but == MouseButtons.Middle)
            {
                if (mapmove&&mes.X >= xe && mes.Y <= ye)
                {
                    mousemove = 1;
                    oldmousey = mes.Y;
                }
                else if (mapmove&&mes.X<=xe && mes.Y <= ye)
                {
                    
                    mousemove = 2;
                    oldmousey = mes.Y;
                    oldmousex = mes.X;
                }
            }
        }

        public void FieldMouseMove(object sender, MyMessage mes)
        {

            if (mes.but == MouseButtons.Middle&&mousemove!=0)
            {
                if (FieldDontHover != null)
                {
                    FieldDontHover(this, mes);
                    Certificate = null;
                }
                if (mousemove == 1)
                {
                    int deltay = mes.Y - oldmousey;
                    oldmousey = mes.Y;
                    mes.Y = deltay;
                    mes.bottom = ye;
                    mes.Answer = 0;
                    mes.Annex = 0;
                    if (FieldrectCardmove != null)
                    {
                        FieldrectCardmove(this, mes);
                    }
                    SolidBrush myBrush = new SolidBrush(Color.White);

                    dcfield.FillRectangle(myBrush, new Rectangle(xe, 0, xek - xe, ye));

                }
                else if (mousemove == 2)
                {
                    int deltay = mes.Y - oldmousey;
                    int deltax = mes.X - oldmousex;
                    if (leftsourceimage - deltax <= 1698 - xe && leftsourceimage - deltax >= 0)
                        leftsourceimage -= deltax;
                    else
                    if (leftsourceimage - deltax <= 0)
                    {
                        deltax = leftsourceimage;
                        leftsourceimage = 0;
                    }
                    else if (leftsourceimage - deltax >= 1698 - xe)
                    {
                        deltax = leftsourceimage - 1698 + xe;
                        leftsourceimage = 1698 - xe;
                    }
                    if (topsourceimage - deltay <= 982 - ye && topsourceimage - deltay >= 0)
                        topsourceimage -= deltay;

                    else if (topsourceimage - deltay <= 0)
                    {
                        deltay = topsourceimage;
                        topsourceimage = 0;
                    }
                    else if (topsourceimage - deltay >= 982 - ye)
                    {
                        deltay = topsourceimage - 982 + ye;
                        topsourceimage = 982 - ye;
                    }
                    leftdest += deltax;
                    topdest += deltay;
                    oldmousey = mes.Y;
                    mes.Y = deltay;
                    oldmousex = mes.X;
                    mes.X = deltax;


                    sourceRect = new RectangleF(leftsourceimage / 1698.0f * fon.Width, topsourceimage / 982.0f * fon.Height, xe / 1698.0f * fon.Width, ye / 982.0f * fon.Height);

                    dcfield.Clear(Color.White);


                    mes.left = leftsourceimage;
                    mes.top = topsourceimage;


                    mes.L = xe / 1698.0f;
                    mes.W = ye / 982.0f;


                    if (FieldrectMapmove != null)
                    {
                        FieldrectMapmove(this, mes);
                    }
                    //if (FieldrectMinimapmove != null)
                    //{
                    //    FieldrectMinimapmove(this, mes);
                    //}
                }
                
                MyPaint(this, mes);
            }
            
        }

        public void FieldStop(object sender, MyMessage mes)
        {
            mapmove = true;
            clickstate = false;
        }

        public void FieldContinue(object sender, MyMessage mes)
        {
            if (gamermove)
            {
                mapmove = true;
                clickstate = true;
            }
            else
            {
                mapmove = false;
                clickstate = false;
            }
        }

        public Field(int ncells, int mcells, Graphics dc, MyMessage mes, int x, int y, int xe, int ye, int xek, int yek, /*string name_of_map, int count,*/ bool flag, bool game/*, bool warorpeace, Form1 owner*/) {
            fon = new Bitmap(SiegeOfTheFortress.Properties.Resources.earthtexture);
            MyMiniField = new Minifield(xe+2, yek - 150, xek, yek, xe, mes.clientbottom-150, xe + xe / 1698.0f * (xek - xe), mes.clientbottom - 150 + ye / 982.0f * 150, fon);
            clickstate = false;
            picture = new Bitmap(mes.X, mes.Y);
            dcfield = Graphics.FromImage(picture);
            leftsourceimage = 0;
            topsourceimage = 0;
            Form1.FormmouseUp += FieldMouseUp;
            Form1.FormmouseDown += FieldMouseDown;
            Form1.FormmouseMove += FieldMouseMove;
            mousemove = 0;
            mapmove = true;
            clickstate = true;
            Form1.FormStopEvent += FieldStop;
            Form1.FormContinueEvent += FieldContinue;

            Form1.FormrectClick += MouseClick;
            Form1.Formstartgame += Fieldstartgame;
            Form1.Formrectpaint += MyPaint;
            Form1.MySizeChanged += FieldSizeChanged;
            GameObject.Charhook += Hookisover;
            Artifact.ArtifactisAttacked += StateofWar;
            Fieldhook += Hookisover;
            this.x = x;
            this.y = y;
            this.xe = xe + 1;
            this.ye = ye + 1;
            this.xek = xek;
            this.yek = yek;
            destinationRect = new RectangleF(0, 0, xe, ye);
            sourceRect = new RectangleF(0, 0, xe / 1698.0f * fon.Width, ye / 982.0f * fon.Height);
            gameorprepare = false;
            booleye = false;
            boolnearenemy = false;
            boolnearally = false;
            //currentvariant = 0;
            //n = 16;
            n = ncells;
            //m = 24;
            m = mcells;
            moves = 40;
            extramoves = moves;
            this.flag = flag;
            this.game = game;
            gamermove = false;
            //this.owner = owner;
            gamemove = false;
            numberofcharacters = 0;
            Commander = new Witch(false);
            l = 40;
            w = 40;
            d = 38;
            int start_x = x + w / 2;
            int start_y = y + l / 2;
            My_cell = new Cell[n, m];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    My_cell[i, j] = new Cell(start_x, start_y, i, j, l, w, Color.White, d);
                    start_x += w;
                }
                start_y += l;
                start_x = x + w / 2;
            }
            Form1.FormMyTick += t_Tick;
            FirstGObject = null;
            ExtrafirstGObject = null;
            Extrafirst_character = null;
            numberofcharacters = 7;
            extranumberofcharacters = numberofcharacters;
            allenemypoints = 2100;
        }

        public void BeforeOperation(MyMessage mes)
        {
            money = mes.Annex;
            mypoints = mes.Answer;
            enemypoints = mes.Info;

            string text = mes.filename;
            text = text.Substring(0, text.Length - 5);
            Directory.CreateDirectory(text);

            string filename1 = text+"/staticfield1.msfld";
            FileStream FS1 = new FileStream(filename1, FileMode.Create);
            string filename2 = text+"/staticfield2.msfld";
            FileStream FS2 = new FileStream(filename2, FileMode.Create);
            string filename3 = text+"/staticfield3.msfld";
            FileStream FS3 = new FileStream(filename3, FileMode.Create);
            string filename4 = text+"/staticfield4.msfld";
            FileStream FS4 = new FileStream(filename4, FileMode.Create);
            string filename5 = text+"/staticfield5.msfld";
            FileStream FS5 = new FileStream(filename5, FileMode.Create);
            string filename6 = text+"/staticfield6.msfld";
            FileStream FS6 = new FileStream(filename6, FileMode.Create);
            string filename7 = text+"/staticfield7.msfld";
            FileStream FS7 = new FileStream(filename7, FileMode.Create);
            string filename8 = text+"/staticfield8.msfld";
            FileStream FS8 = new FileStream(filename8, FileMode.Create);
            BinaryFormatter BF = new BinaryFormatter();
            string filename9 = text + "/staticfield9.msfld";
            FileStream FS9 = new FileStream(filename9, FileMode.Create);
            string filename10 = text + "/staticfield10.msfld";
            FileStream FS10 = new FileStream(filename10, FileMode.Create);
            string filename11 = text + "/staticfield11.msfld";
            FileStream FS11 = new FileStream(filename11, FileMode.Create);

            BF.Serialize(FS1, igoal);
            BF.Serialize(FS2, jgoal);

            BF.Serialize(FS3, xdelta);
            BF.Serialize(FS4, ydelta);

            BF.Serialize(FS5, tekindex);
            BF.Serialize(FS6, stateofprocess);

            BF.Serialize(FS7, pj);
            BF.Serialize(FS8, pi);

            BF.Serialize(FS9, leftdest);
            BF.Serialize(FS10, topdest);

            BF.Serialize(FS11, First_character.obj.Numberally);


            FS1.Close();
            FS2.Close();
            FS3.Close();
            FS4.Close();
            FS5.Close();
            FS6.Close();
            FS7.Close();
            FS8.Close();
            FS9.Close();
            FS10.Close();
            FS11.Close();

        }

        public void BeforeDeserialize()
        {
            
            Form1.FormrectClick -= MouseClick;
            Form1.Formstartgame -= Fieldstartgame;
            Form1.Formrectpaint -= MyPaint;
            Form1.MySizeChanged -= FieldSizeChanged;
            GameObject.Charhook -= Hookisover;
            Form1.FormmouseUp -= FieldMouseUp;
            Form1.FormmouseDown -= FieldMouseDown;
            Form1.FormmouseMove -= FieldMouseMove;
            Artifact.ArtifactisAttacked -= StateofWar;
            Fieldhook -= Hookisover;
            Form1.FormMyTick -= t_Tick;
            Fieldenemymove -= Commander.WitchAnalize;

            Field.FieldShow -= MyMiniField.Show;
            Field.MyFieldSizeChanged -= MyMiniField.MinifieldSizeChange;
            Field.Fieldstartgameprocess -= MyMiniField.GOstartgame;
            Field.FieldUpdateObject -= MyMiniField.Show;
            //Field.FieldrectMinimapmove -= MyMiniField.MinifieldMouseMove;
            Field.FieldrectMapmove -= MyMiniField.MinifieldMouseMove;

            
            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                {
                    Field.FieldrectClick -= My_cell[i, j].MouseClick;
                    Field.FieldDelete -= My_cell[i, j].DeleteChar;
                    Field.FieldAddChar -= My_cell[i, j].AddChar;
                    Field.FieldUpdateObject -= My_cell[i,j].Show;
                    Field.FieldrectMapmove -= My_cell[i,j].CellMouseMove;
                    Field.Fieldstartgameprocess -= My_cell[i, j].StartGame;
                    My_cell[i, j] = null;
                }

            ListofCards fcard = First_card;
            while (fcard != null)
            {
                Field.Fieldapplyeffects -= fcard.obj.Endmove;
                Field.FieldDelete -= fcard.obj.DeleteMyCard;
                Field.MyFieldSizeChanged -= fcard.obj.CardSizeChanged;
                Field.FieldShow -= fcard.obj.Show;
                Field.FieldrectCardmove -= fcard.obj.Cardmove;

                Field.FieldAskingforOrder -= fcard.obj.AnsweringonOrder;
                Field.FieldUpdateObject -= fcard.obj.Show;
                fcard = fcard.next;
            }

            ListofGameObjects fobj = FirstGObject, fobjextra = ExtrafirstGObject;
            while (fobj != null)
            {
                fobj.obj.UnSignMySelf();
                fobj = fobj.next;
                fobjextra.obj.UnSignMySelf();
                fobjextra = fobjextra.next;
            }

            ListofCharacters fchar = First_character, fcharextra=Extrafirst_character;
            while (fchar != null)
            {
                fchar.obj.UnSignMySelf();
                fchar = fchar.next;
                fcharextra.obj.UnSignMySelf();
                fcharextra = fcharextra.next;
            }
            Commander = null;
            Certificate = null;
            MyMiniField = null;
        }

        public void DeserializeField(MyMessage mes)
        {
            string text = mes.filename;
            text = text.Substring(0, text.Length - 5);
            string filename1 = text+"/staticfield1.msfld";
            FileStream FS1 = File.OpenRead(filename1);
            string filename2 = text+"/staticfield2.msfld";
            FileStream FS2 = File.OpenRead(filename2);

            string filename3 = text+"/staticfield3.msfld";
            FileStream FS3 = File.OpenRead(filename3);

            string filename4 = text+"/staticfield4.msfld";
            FileStream FS4 = File.OpenRead(filename4);

            string filename5 = text+"/staticfield5.msfld";
            FileStream FS5 = File.OpenRead(filename5);

            string filename6 = text+"/staticfield6.msfld";
            FileStream FS6 = File.OpenRead(filename6);

            string filename7 = text+"/staticfield7.msfld";
            FileStream FS7 = File.OpenRead(filename7);

            string filename8 = text+"/staticfield8.msfld";
            FileStream FS8 = File.OpenRead(filename8);

            string filename9 = text + "/staticfield9.msfld";
            FileStream FS9 = File.OpenRead(filename9);

            string filename10 = text + "/staticfield10.msfld";
            FileStream FS10 = File.OpenRead(filename10);

            string filename11 = text + "/staticfield11.msfld";
            FileStream FS11 = File.OpenRead(filename11);

            BinaryFormatter BF = new BinaryFormatter();

            igoal = (int)BF.Deserialize(FS1);
            jgoal = (int)BF.Deserialize(FS2);
            xdelta = (int)BF.Deserialize(FS3);
            ydelta = (int)BF.Deserialize(FS4);
            tekindex = (int)BF.Deserialize(FS5);
            stateofprocess = (int)BF.Deserialize(FS6);
            pj = (int[])BF.Deserialize(FS7);
            pi = (int[])BF.Deserialize(FS8);
            leftdest = (int)BF.Deserialize(FS9);
            topdest = (int)BF.Deserialize(FS10);
            First_character.obj.Numberally = (int)BF.Deserialize(FS11);

            FS1.Close();
            FS2.Close();
            FS3.Close();
            FS4.Close();
            FS5.Close();
            FS6.Close();
            FS7.Close();
            FS8.Close();
            FS9.Close();
            FS10.Close();

            dcfield = Graphics.FromImage(picture);
            //owner = mes.exampleForm;
            Form1.FormrectClick += MouseClick;
            Form1.Formstartgame += Fieldstartgame;
            Form1.Formrectpaint += MyPaint;
            Form1.MySizeChanged += FieldSizeChanged;
            Form1.FormmouseUp += FieldMouseUp;
            Form1.FormmouseDown += FieldMouseDown;
            Form1.FormmouseMove += FieldMouseMove;
            Field.FieldShow += MyMiniField.Show;
            Field.MyFieldSizeChanged += MyMiniField.MinifieldSizeChange;
            Field.Fieldstartgameprocess += MyMiniField.GOstartgame;
            Field.FieldUpdateObject += MyMiniField.Show;
            //Field.FieldrectMinimapmove += MyMiniField.MinifieldMouseMove;
            Field.FieldrectMapmove += MyMiniField.MinifieldMouseMove;


            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                {
                    Field.FieldrectClick += My_cell[i, j].MouseClick;
                    Field.FieldDelete += My_cell[i, j].DeleteChar;
                    Field.FieldAddChar += My_cell[i, j].AddChar;
                    Field.FieldUpdateObject += My_cell[i,j].Show;
                    Field.FieldrectMapmove += My_cell[i,j].CellMouseMove;
                    Field.Fieldstartgameprocess += My_cell[i,j].StartGame;

                }

            ListofCards fcard = First_card;
            while (fcard != null)
            {
                Field.Fieldapplyeffects += fcard.obj.Endmove;
                Field.FieldDelete += fcard.obj.DeleteMyCard;
                Field.MyFieldSizeChanged += fcard.obj.CardSizeChanged;
                Field.FieldShow += fcard.obj.Show;
                Field.FieldrectCardmove += fcard.obj.Cardmove;

                Field.FieldAskingforOrder += fcard.obj.AnsweringonOrder;
                Field.FieldUpdateObject -= fcard.obj.Show;
                fcard = fcard.next;
            }

            ListofGameObjects fobj = FirstGObject, fobjextra=ExtrafirstGObject;
            while (fobj != null)
            {
                fobj.obj.ReSignMySelf();
                fobj = fobj.next;
                fobjextra.obj.ReSignMySelf();
                fobjextra = fobjextra.next;
            }

            ListofCharacters fchar = First_character, fcharextra=Extrafirst_character;
            while (fchar != null)
            {
                fchar.obj.ReSignMySelf();
                fchar = fchar.next;
                fcharextra.obj.ReSignMySelf();
                fcharextra = fcharextra.next;
            }

            GameObject.Charhook += Hookisover;
            Artifact.ArtifactisAttacked += StateofWar;
            Fieldhook += Hookisover;
            Form1.FormMyTick += t_Tick;
            Fieldenemymove += Commander.WitchAnalize;
            mes.Code = 99;
            mes.Annex = enemypoints;
            mes.Answer = mypoints;
            FieldWorkAndDie(this, mes);
            if (gameorprepare)
            {
                MyMessage mes1 = new MyMessage();
                mes1.Code = 0;
                Fieldstartgameprocess(this, mes1);
            }
            mes.Profile.Ally = gameorprepare;
            mes.Annex = money;
            mes.Answer = mypoints;
            mes.Info = enemypoints;
            mes.Data = moves;
        }
        public void FieldSizeChanged(object sender, MyMessage mes)
        {
            xe = mes.right - 238;
            ye = mes.bottom - 66;
            destinationRect = new RectangleF(0, 0, xe, ye);
            int oldleft = leftsourceimage;
            int oldtop = topsourceimage;
            int deltax = 0, deltay = 0;
            if (1698 - leftsourceimage < xe)
            {
                leftsourceimage = 1698 - xe;
                oldleft -= leftsourceimage;
                deltax = oldleft;
            }
            if (982 - topsourceimage < ye)
            {
                topsourceimage = 982 - ye;
                oldtop -= topsourceimage;
                deltay = oldtop;
            }
            if (oldleft != leftsourceimage || oldtop != topsourceimage)
            {
                leftdest -= deltax;
                topdest -= deltay;
                mes.X = deltax;
                mes.Y = deltay;
                if (FieldrectMapmove != null)
                {
                    FieldrectMapmove(this, mes);
                }
            }
            sourceRect = new RectangleF(leftsourceimage / 1698.0f * fon.Width, topsourceimage / 982.0f * fon.Height, xe / 1698.0f * fon.Width, ye / 982.0f * fon.Height);
            if (mes.right * mes.bottom != 0)
            {
                picture = new Bitmap(mes.right, mes.bottom);
                xek = mes.right;
                yek = mes.bottom;
                dcfield = Graphics.FromImage(picture);
                mes.right -= 20;
                dcfield.Clear(Color.White);
                mes.left = xe;
                mes.top = mes.clientbottom - 150;
                mes.X = leftsourceimage;//!!!
                mes.Y = topsourceimage;//!!!
                mes.L = xe / 1698.0f;
                mes.W = ye / 982.0f;
                if (MyFieldSizeChanged != null) 
                    MyFieldSizeChanged(this, mes);
                MyPaint(sender, mes);
            }
        }
        public void Hookisover(object sender, MyMessage mes)
        {
            Fieldsearchdc(this, mes);
            if (gamemove)
            {
                moves--;
                mes.Annex = moves;
                //Fieldnewvalueofmoves(this, mes);
            }
            else
                Returnpast(mes);
            Changelistofchar(mes.dc, mes);
            mes.Data = moves;
            FieldNextMove(this, mes);
            if (game)
            {
                Hidefieldofcards(mes);
                mes.Code = 0;
                MyPaint(mes.dc, mes);
                mes.Myobject = First_card.obj.person;
                FieldCreateProfile(this, mes);
                gamemove = mes.Profile.Ally;
                if (mes.Profile.Ally)
                {
                    Newmove(mes);
                    //FieldEndAction(this, mes);
                    clickstate = true;
                    mapmove = true;
                    gamermove = true;
                }
                else
                {

                    Newmove(mes);
                    Enemyturn(this, mes);

                }
            }
        }
        public void t_Tick(object sender, MyMessage mes)
        {
            Rectangle recfon = new Rectangle(0, 0, xe, ye);
            dcfield.DrawImage(fon, destinationRect, sourceRect, GraphicsUnit.Pixel);

            mes.Code = 0;
            mes.dc1 = dcfield;
            mes.top = 23;
            mes.bottom = ye;
            mes.left = 0;
            mes.right = xe;

            

            if (FieldUpdateObject!=null)
                FieldUpdateObject(this, mes);

            SolidBrush myBrush = new SolidBrush(Color.White);

            dcfield.FillRectangle(myBrush, new Rectangle(xe, 0, xek - xe, ye));

            dcfield.FillRectangle(myBrush, new Rectangle(0, ye, xe, yek));

            if (FieldShow != null)
                FieldShow(this, mes);
            mes.dc.DrawImage(picture, 0, 0);
            if (!game && gameorprepare)
                Printtext(mes.dc, mes);
            
        }

        public void Fieldstartgame(object sender, MyMessage mes)
        {
            extranumberofcharacters = numberofcharacters;
            gameorprepare = true;
            Create_cards(mes);
            mes.dc1 = dcfield;
            if (FieldShow != null)
                FieldShow(this, mes);
            mes.Code = 1;
            Fieldstartgameprocess(this, mes);

            Newmove(mes);
            if (!gamemove)
            {
                //FieldStartAction(this, mes);
                clickstate = false;
                mapmove = false;
                gamermove = false;
                Enemyturn(this, mes);
            }

            else

            {
                clickstate = true;
                mapmove = true;
                gamermove = true;
            }

        }

        public void Enemyturn(object sender, MyMessage mes)
        {
            int[] dx = new int[8] { 1, 1, 0, -1, -1, -1, 0, 1 };
            int[] dy = new int[8] { 0, 1, 1, 1, 0, -1, -1, -1 };
            mes.Myobject = First_card.obj.person;
            FieldCreateProfile(this, mes);
            int atd = mes.Profile.R;
            int bdist = mes.Annex;
            int ddist = mes.Profile.Dist;
            int type = mes.Info;
            int teki = mes.Profile.I;
            int tekj = mes.Profile.J;
            int endi = teki, endj = tekj;
            bool enemyissuccessful = true;
            if (boolnearenemy)
                mes.Annex = 1;
            else
                mes.Annex = 0;
            if (boolnearally)
                mes.Info = 1;
            else
                mes.Info = 0;
            mes.Answer = type;
            Fieldenemymove(this, mes);
            //Получить от колдуна информацию о том, что и как делать
            if (mes.Answer == 1)
            {
                //Идти по списку и искать по глазам тех, кто самый близкий (олучать координаты клеточные и высчитывать, потом запоминать). Либо же не просто ближайшего, а еще и по здоровью/силе атаки, но это уже потом
                //Провести атаку, как обычно (т.е. как для союзника)
                //Выбрать соответствующий метод и его реализовать
                if (booleye)
                {
                    ListofCharacters curr = First_character;
                    int f = 1;
                    int neari = -1000, nearj = -1000;
                    int ke;
                    while (f != 0 && curr != null)
                    {
                        if (curr.eye)
                        {
                            mes.Myobject = curr.obj;
                            FieldCreateProfile(this, mes);
                            if ((teki - mes.Profile.I) * (teki - mes.Profile.I) + (tekj - mes.Profile.J) * (tekj - mes.Profile.J) < (teki - neari) * (teki - neari) + (tekj - nearj) * (tekj - nearj))
                            {
                                neari = mes.Profile.I;
                                nearj = mes.Profile.J;
                                if (curr.near_c)
                                    ke = 1;
                                else
                                    ke = 0;
                            }
                        }
                        curr = curr.next;
                    }
                    int floatleni = -1, floatlenj = -1;
                    if (bdist == 0)
                    {
                        int minlen = n * m;
                        for (int k = 0; k < 8; k++)
                        {
                            int iy = neari + dy[k], ix = nearj + dx[k];
                            if (iy >= 0 && iy < n && ix >= 0 && ix < m)
                            {

                                mes.Annex = My_cell[iy, ix].Value;
                                mes.Info = My_cell[iy, ix].Metka;
                                if (mes.Annex >= 0 && mes.Annex < minlen)
                                {
                                    floatleni = iy;
                                    floatlenj = ix;
                                    minlen = mes.Annex;
                                }
                            }
                        }
                        if (floatleni >= 0)
                        {
                            enemyissuccessful = false;
                            Pathfinding_process(mes.dc, mes, minlen, floatleni, floatlenj, minlen, ref endi, ref endj, neari, nearj, ddist);
                            if (neari - 1 <= endi && endi <= neari + 1 && nearj - 1 <= endj && endj <= nearj + 1)
                            {

                                Field.Stateofprocess = 1;

                                Field.Igoal = neari;
                                Field.Jgoal = nearj;

                            }
                            else


                                Field.Stateofprocess = 0;


                            Returnpast(mes);
                            mes.Character = First_card.obj.person;
                            Fieldattack(this, mes);
                        }
                    }
                    else
                    {
                        int ifd;
                        if (mes.Annex == -4)
                            ifd = 1;
                        else
                            ifd = 0;
                        if (neari >= endi - atd && neari <= endi + atd && nearj >= endj - atd && nearj <= endj + atd)
                        {
                            enemyissuccessful = false;
                            Field.Stateofprocess = 2;
                            Field.Igoal = neari;
                            Field.Jgoal = nearj;
                            Returnpast(mes);
                            mes.Character = First_card.obj.person;
                            mes.Info = ifd;
                            Fieldattack(this, mes);
                        }
                        else
                        {
                            int minlen = n * m;
                            for (int k = 0; k < 8; k++)
                            {
                                int iy = neari + dy[k], ix = nearj + dx[k];
                                if (iy >= 0 && iy < n && ix >= 0 && ix < m)
                                {

                                    mes.Annex = My_cell[iy, ix].Value;
                                    mes.Info = My_cell[iy, ix].Metka;
                                    if (mes.Annex >= 0 && mes.Annex < minlen)
                                    {
                                        floatleni = iy;
                                        floatlenj = ix;
                                        minlen = mes.Annex;
                                    }
                                }
                            }
                            if (floatleni >= 0)
                            {
                                enemyissuccessful = false;
                                Pathfinding_process(mes.dc, mes, minlen, floatleni, floatlenj, minlen, ref endi, ref endj, neari, nearj, ddist);
                                Field.Stateofprocess = 0;
                                Returnpast(mes);
                                mes.Character = First_card.obj.person;
                                mes.Info = ifd;
                                Fieldattack(this, mes);
                            }
                        }

                    }
                }
                else
                {

                    if(Fieldquery!=null)
                        Fieldquery(this, mes);
                    int neari = mes.Profile.I;
                    int nearj = mes.Profile.J;
                    int floatleni = -1, floatlenj = -1;
                    if (!(bdist == 0 && neari >= endi - ddist - 1 && neari <= endi + ddist + 1 && nearj >= endj - ddist - 1 && nearj <= endj + ddist + 1 || !(bdist == 0) && neari >= endi - atd && neari <= endi + atd && nearj >= endj - atd && nearj <= endj + atd))
                    {
                        int minlen = n * m;
                        for (int k = 0; k < 8; k++)
                        {
                            int iy = neari + dy[k], ix = nearj + dx[k];
                            if (iy >= 0 && iy < n && ix >= 0 && ix < m)
                            {
                                mes.Annex = My_cell[iy, ix].Value;
                                mes.Info = My_cell[iy, ix].Metka;
                                if (mes.Annex >= 0 && mes.Annex < minlen)
                                {
                                    floatleni = iy;
                                    floatlenj = ix;
                                    minlen = mes.Annex;
                                }
                            }
                        }
                        if (floatleni >= 0)
                        {
                            Pathfinding_process(mes.dc, mes, minlen, floatleni, floatlenj, minlen, ref endi, ref endj, neari, nearj, ddist);
                            
                            Field.Stateofprocess = 0;
                            Returnpast(mes);
                            mes.Character = First_card.obj.person;
                            Fieldattack(this, mes);
                            enemyissuccessful = false;
                        }
                    }
                }
            }
            else if (mes.Answer == 2)
            {
                ListofCharacters curr = First_character;
                int f = 1;
                int neari = -1000, nearj = -1000;
                int ke;
                while (f != 0 && curr != null)
                {
                    if (curr.near_h)
                    {
                        mes.Myobject = curr.obj;
                        FieldCreateProfile(this, mes);
                        if ((teki - mes.Profile.I) * (teki - mes.Profile.J) + (tekj - mes.Profile.J) * (tekj - mes.Profile.J) < (teki - neari) * (teki - neari) + (tekj - nearj) * (tekj - nearj))
                        {
                            neari = mes.Profile.I;
                            nearj = mes.Profile.J;
                            if (curr.near_c)
                                ke = 1;
                            else
                                ke = 0;
                        }
                    }
                    curr = curr.next;
                }
                int floatleni = -1, floatlenj = -1;
                if (bdist == 0)
                {
                    int minlen = n * m;
                    for (int k = 0; k < 8; k++)
                    {
                        int iy = neari + dy[k], ix = nearj + dx[k];
                        if (iy >= 0 && iy < n && ix >= 0 && ix < m)
                        {
                            mes.Annex = My_cell[iy, ix].Value;
                            mes.Info = My_cell[iy, ix].Metka;
                            if (mes.Annex >= 0 && mes.Annex < minlen)
                            {
                                floatleni = iy;
                                floatlenj = ix;
                                minlen = mes.Annex;
                            }
                        }
                    }
                    if (floatleni >= 0)
                    {

                        Pathfinding_process(mes.dc, mes, minlen, floatleni, floatlenj, minlen, ref endi, ref endj, neari, nearj, ddist);
                        if (neari - 1 <= endi && endi <= neari + 1 && nearj - 1 <= endj && endj <= nearj + 1)
                        {

                            Field.Stateofprocess = 3;
                            Field.Igoal = neari;
                            Field.Jgoal = nearj;

                        }
                        else

                            Field.Stateofprocess = 0;
                        enemyissuccessful = false;
                        Returnpast(mes);
                        mes.Character = First_card.obj.person;
                        Fieldattack(this, mes);
                    }
                }
                else
                {
                    int ifd;
                    if (mes.Annex == -4)
                        ifd = 1;
                    else
                        ifd = 0;
                    if (neari >= endi - atd && neari <= endi + atd && nearj >= endj - atd && nearj <= endj + atd)
                    {
                        Field.Stateofprocess = 4;
                        Field.Igoal = neari;
                        Field.Jgoal = nearj;
                        enemyissuccessful = false;
                        Returnpast(mes);
                        mes.Character = First_card.obj.person;
                        mes.Info = ifd;
                        Fieldattack(this, mes);
                    }
                    else
                    {
                        int minlen = n * m;
                        for (int k = 0; k < 8; k++)
                        {
                            int iy = neari + dy[k], ix = nearj + dx[k];
                            if (iy >= 0 && iy < n && ix >= 0 && ix < m)
                            {
                                mes.Annex = My_cell[iy, ix].Value;
                                mes.Info = My_cell[iy, ix].Metka;
                                if (mes.Annex >= 0 && mes.Annex < minlen)
                                {
                                    floatleni = iy;
                                    floatlenj = ix;
                                    minlen = mes.Annex;
                                }
                            }
                        }
                        if (floatleni >= 0)
                        {
                            Field.Stateofprocess = 0;
                            enemyissuccessful = false;
                            Returnpast(mes);
                            mes.Character = First_card.obj.person;
                            mes.Info = ifd;
                            Fieldattack(this, mes);
                        }
                    }

                }
            }
            if (enemyissuccessful)

                Hookisover(this, mes);

        }
        public void MouseClick(object sender, MyMessage mes)
        {
            if (mes.but == MouseButtons.Left)
            {
                if (FieldDontHover != null)
                {
                    FieldDontHover(this, mes);
                    Certificate = null;
                    MyPaint(this, mes);
                }
                if (!gameorprepare&&mes.Data>0)
                {
                    int currentvariant;
                    mes.Code = 1;
                    mes.State = false;
                    if (FieldrectClick != null)
                        FieldrectClick(sender, mes);
                    currentvariant = mes.Data;
                    if (mes.State)
                    {

                        if (currentvariant != 1000 && mes.Myobject == null)
                        {
                            int flag = 1;
                            FieldGetMoney(this, mes);
                            switch (currentvariant)
                            {
                                case 1:
                                    {
                                        if (mes.Info - 100 < 0)
                                            flag = 0;
                                        break;
                                    }
                                case 2:
                                    {
                                        if (mes.Info - 150 < 0)
                                            flag = 0;
                                        break;
                                    }
                                case 3:
                                    {
                                        if (mes.Info - 200 < 0)
                                            flag = 0;
                                        break;
                                    }
                            }
                            if (flag != 0)
                            {
                                numberofcharacters++;
                                ListofCharacters curr = First_character, last = null, extracurr = Extrafirst_character, extralast = null;
                                while (curr != null)
                                {
                                    last = curr;
                                    curr = curr.next;
                                    extralast = extracurr;
                                    extracurr = extracurr.next;
                                }
                                int w = 40;
                                int l = 40;
                                switch (currentvariant)
                                {
                                    case 1:
                                        {
                                            curr = new ListofCharacters(new Soldier(w * mes.X - leftsourceimage, y + l * mes.Y - topsourceimage, mes.Y, mes.X, 120, 100, 0, 0, Color.FromArgb(218, 112, 214)/*RGB(218, 112, 214)*/, 100, 40, 2, true, TypeofCharacter.SimpleSoldier, true), null, false, false, false);
                                            
                                            extracurr = new ListofCharacters(curr.obj.Clone2(), null, false, false, false);
                                            mes.Profile.Value = -100;
                                            break;
                                        }
                                    case 2:
                                        {
                                            curr = new ListofCharacters(new Cannon(120, 80, 1, Color.FromArgb(91, 58, 41)/*RGB(91, 58, 41)*/, w * mes.X - leftsourceimage, y + l * mes.Y - topsourceimage, mes.Y, mes.X, 240, 150, 0, true, 5, TypeofCharacter.SimpleCannon, true), null, false, false, false);
                                            
                                            extracurr = new ListofCharacters(curr.obj.Clone2(), null, false, false, false);
                                            mes.Profile.Value = -150;
                                            break;
                                        }
                                    case 3:
                                        {
                                            curr = new ListofCharacters(new Wizard(w * mes.X - leftsourceimage, y + l * mes.Y - topsourceimage, mes.Y, mes.X, 200, 200, 0, 4, Color.FromArgb(218, 112, 214)/*RGB(218, 112, 214)*/, 122, 50, 3, true, TypeofCharacter.SimpleWizard, true), null, false, false, false);
                                            
                                            extracurr = new ListofCharacters(curr.obj.Clone2(), null, false, false, false);
                                            mes.Profile.Value = -200;
                                            break;
                                        }
                                }
                                last.next = curr;
                                curr.next = null;
                                extralast.next = extracurr;
                                extracurr.next = null;
                                My_cell[mes.Y, mes.X].Value = -3;
                                My_cell[mes.Y, mes.X].InternalObj = curr.obj;
                                FieldChangeMoney(this, mes);

                            }
                        }
                        else if (mes.Myobject != null && currentvariant == 1000 && mes.Profile.Ally)
                        {
                            mes.Del = 1;
                            numberofcharacters--;
                            FieldCreateProfile(this, mes);
                            FieldChangeMoney(this, mes);
                            ListofCharacters curr = First_character, last = null, extracurr = Extrafirst_character, extralast = null;
                            mes.Character = (Character)mes.Myobject;
                            FieldDelete(this, mes);
                            //и еще как-то надо будет удалить его копию
                            int d = 1;
                            while (d != 0 && curr != null)
                            {
                                if (curr.obj == mes.Myobject)
                                    d = 0;
                                else
                                {
                                    last = curr;
                                    curr = curr.next;
                                    extralast = extracurr;
                                    extracurr = extracurr.next;
                                }
                            }
                            mes.Character = extracurr.obj;
                            FieldDelete(this, mes);
                            if (last == null)
                            {
                                curr.obj = null;
                                First_character = curr.next;
                                extracurr.obj = null;
                                Extrafirst_character = extracurr.next;
                            }
                            else
                            {
                                last.next = curr.next;
                                curr.obj = null;
                                extralast.next = extracurr.next;
                                extracurr.obj = null;
                            }
                            mes.Del = 0;
                        }
                        mes.Answer = 0;
                        MyPaint(mes.dc, mes);
                    }
                }
                else
                {
                    if (game && gamermove && mes.X <= xe && mes.Y <= ye)
                    {
                        //FieldStartAction(this, mes);
                        //if (mes.but == MouseButtons.Left)
                        //{
                            int indexi=n+1, indexj=m+1;
                            mes.Code = 2;
                            if (FieldrectClick != null)
                                FieldrectClick(this, mes);
                            indexi = mes.Y;
                            indexj = mes.X;
                        
                        if (indexi <= n && indexj <= m)
                            {
                            mes.Myobject = First_card.obj.person;
                            FieldCreateProfile(this, mes);
                            int bdist = mes.Annex;
                            int ddist = mes.Profile.Dist;
                            mes.Annex = My_cell[indexi, indexj].Value;
                                mes.Info = My_cell[indexi, indexj].Metka;
                                int[] dx = new int[8] { 1, 1, 0, -1, -1, -1, 0, 1 };
                                int[] dy = new int[8] { 0, 1, 1, 1, 0, -1, -1, -1 };
                                if (mes.Info != 0)
                                {
                                    if (mes.Annex > 0)
                                    {
                                        int endi = 0, endj = 0;
                                        Pathfinding_process(mes.dc, mes, mes.Annex, indexi, indexj, mes.Annex, ref endi, ref endj, indexi, indexj, ddist);
                                        Returnpast(mes);
                                        mes.Character = First_card.obj.person;
                                        Field.Stateofprocess = 0;
                                        Field.leftdest = leftsourceimage;
                                        Field.topdest = topsourceimage;
                                        clickstate = false;
                                        mapmove = false;
                                        gamermove = false;
                                        Fieldattack(this, mes);
                                    }
                                    else if (mes.Annex == 0)
                                    {
                                        Returnpast(mes);
                                        clickstate = false;
                                        mapmove = false;
                                        gamermove = false;
                                        Fieldhook(this, mes);
                                    }
                                    else
                                    {
                                        if (mes.Annex == -1 || mes.Annex == -4)
                                        {

                                            if (bdist == 0)
                                            {
                                                int floatleni = 0, floatlenj = 0;
                                                int minlen = n * m;
                                                int AValue;
                                                for (int k = 0; k < 8; k++)
                                                {
                                                    int iy = indexi + dy[k], ix = indexj + dx[k];
                                                    if (iy >= 0 && iy < n && ix >= 0 && ix < m)
                                                    {
                                                        AValue = My_cell[iy, ix].Value;
                                                        if (AValue >= 0 && AValue < minlen)
                                                        {
                                                            floatleni = iy;
                                                            floatlenj = ix;
                                                            minlen = AValue;
                                                        }
                                                    }
                                                }
                                                int endi = 0, endj = 0;
                                                Pathfinding_process(mes.dc, mes, minlen, floatleni, floatlenj, minlen, ref endi, ref endj, indexi, indexj, ddist);
                                                Returnpast(mes);
                                                Field.Stateofprocess = 1;
                                                Field.leftdest = leftsourceimage;
                                                Field.topdest = topsourceimage;
                                                mes.Character = First_card.obj.person;
                                                mes.Profile.I = indexi;
                                                mes.Profile.J = indexj;
                                                clickstate = false;
                                                mapmove = false;
                                                gamermove = false;
                                                Fieldattack(this, mes);
                                            }
                                            else
                                            {
                                                Returnpast(mes);
                                                if (mes.Annex == -4)
                                                    mes.Info = 1;
                                                else
                                                    mes.Info = 0;
                                                Field.Stateofprocess = 2;
                                                Field.Igoal = indexi;
                                                Field.Jgoal = indexj;
                                                Field.leftdest = leftsourceimage;
                                                Field.topdest = topsourceimage;
                                                mes.Character = First_card.obj.person;
                                                clickstate = false;
                                                mapmove = false;
                                                gamermove = false;
                                                Fieldattack(this, mes);
                                            }
                                        }
                                        else if (mes.Annex == -3)
                                        {
                                            if (bdist == 0)
                                            {
                                                int floatleni = 0, floatlenj = 0;
                                                int minlen = n * m;
                                                int AValue;
                                                for (int k = 0; k < 8; k++)
                                                {
                                                    int iy = indexi + dy[k], ix = indexj + dx[k];
                                                    if (iy >= 0 && iy < n && ix >= 0 && ix < m)
                                                    {
                                                        AValue = My_cell[iy, ix].Value;
                                                        if (AValue >= 0 && AValue < minlen)
                                                        {
                                                            floatleni = iy;
                                                            floatlenj = ix;
                                                            minlen = AValue;
                                                        }
                                                    }
                                                }
                                                int endi = 0, endj = 0;
                                                Pathfinding_process(mes.dc, mes, minlen, floatleni, floatlenj, minlen, ref endi, ref endj, indexi, indexj, ddist);
                                                Returnpast(mes);
                                                Field.Stateofprocess = 3;
                                                mes.Character = First_card.obj.person;
                                                clickstate = false;
                                                mapmove = false;
                                                gamermove = false;
                                                Fieldattack(this, mes);
                                            }
                                            else
                                            {
                                                Returnpast(mes);
                                                Field.Stateofprocess = 4;
                                                mes.Character = First_card.obj.person;
                                                mes.Profile.I = indexi;
                                                mes.Profile.J = indexj;
                                                Field.leftdest = leftsourceimage;
                                                Field.topdest = topsourceimage;
                                                clickstate = false;
                                                mapmove = false;
                                                gamermove = false;
                                                Fieldattack(this, mes);
                                            }
                                        }
                                    }
                                }
                            }
                        //}
                    }
                }
            }
            else if (mes.but == MouseButtons.Right)
            {
                //if (FieldrectClick != null)
                //    FieldrectClick(this, mes);


                mes.Code = 0;
                if (FieldMouseHover != null)
                    FieldMouseHover(this, mes);
                if (mes.Code == 1)
                {
                    int number = mes.Del;
                    mes.Data = -1;
                    if (FieldAskingforOrder != null)
                        FieldAskingforOrder(this, mes);
                    int order = mes.Data;
                    string namechar = "";
                    Color colorchar;
                    if (mes.Profile.Ally)
                        colorchar = Color.Blue;
                    else
                        colorchar = Color.Red;
                    switch (mes.Profile.Extra_info)
                    {
                        case TypeofCharacter.SimpleSoldier:
                            if (mes.Profile.Ally)
                                namechar = "Пехотинец";
                            else
                                namechar = "Скелет";
                            break;
                        case TypeofCharacter.SimpleWizard:
                            if (mes.Profile.Ally)
                                namechar = "Маг";
                            else
                                namechar = "Ведьмак";
                            break;
                        case TypeofCharacter.SimpleCannon:
                            if (mes.Profile.Ally)
                                namechar = "Союзная пушка";
                            else
                                namechar = "Вражеская пушка";
                            break;
                        case TypeofCharacter.SimpleWall:
                            namechar = "Стена";
                            break;
                        case TypeofCharacter.SimpleArtifact:
                            namechar = "Артефакт";
                            break;
                    }
                    if (FieldDontHover != null)
                        FieldDontHover(this, mes);
                    int dlina = 210;
                    if (mes.Annex == 0)
                        dlina -= 25;
                    if (order == -1)
                        dlina -= 25;
                    Certificate = new Aegrotat(mes.X, mes.Y, 120, dlina + number * 25, mes.Profile.V, mes.Profile.Health, mes.Profile.Damage, mes.Profile.Dist, mes.Profile.Value, mes.Profile.R, mes.Annex, order, number, mes.moves, mes.effs, namechar, colorchar);

                }
                else
                {
                    if (FieldDontHover != null)
                        FieldDontHover(this, mes);
                    Certificate = null;
                }
                MyPaint(this, mes);
            }
        }

        public void StateofWar(object sender, MyMessage mes)
        {
            ListofCharacters curr = First_character;
            int j = 1;
            mes.Myobject = First_card.obj.person;
            FieldCreateProfile(this, mes);
            while (j != 0 && curr != null)
            {
                if (curr.obj == mes.Character)
                {
                    j = 0;
                    booleye = true;
                    curr.eye = true;
                };
                curr = curr.next;
            }
        }

        public void Checkwin(Graphics dc, MyMessage mes)
        {
            MyPaint(dc, mes);
            mes.Annex = First_character.obj.Numberally;
            if (mes.Annex == 0 || moves <= 0)
            {
                game = false;
                flag = false;
            }
            else
            {
                mes.Myobject = FirstGObject.obj;
                FieldCreateProfile(this, mes);
                if (mes.Profile.Health <= 0)
                {
                    mes.Character = null;
                    FieldDelete(this, mes);

                    mes.Profile.Value = 1000;
                    mes.Profile.Ally = false;
                    FieldWorkAndDie(this, mes);
                    game = false;
                    flag = true;
                }
                else
                {
                    ListofGameObjects curr = FirstGObject.next, last = FirstGObject;
                    while (curr != null)
                    {
                        mes.Myobject = curr.obj;
                        FieldCreateProfile(this, mes);
                        if (mes.Profile.Health <= 0)
                        {
                            mes.Character = null;
                            mes.Code = -1;
                            FieldDelete(this, mes);
                            last.next = curr.next;
                            FieldWorkAndDie(this, mes);
                            curr = last.next;
                        }
                        else
                        {
                            last = curr;
                            curr = curr.next;
                        }
                    }
                }
            }
            if (!game)
                Printtext(dc, mes);
        }

        public void Returnpast(MyMessage mes)
        {
            mes.Profile.Ally = gamemove;
            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                    My_cell[i, j].ReturnPast(mes);//Тоже можно через событие
        }
        public void Pathfinding_process(Graphics dc, MyMessage mes, int d, int floatleni, int floatlenj, int minlen, ref int endi, ref int endj, int neari, int nearj, int bdist)
        {
            Field.Igoal = neari;
            Field.Jgoal = nearj;
            int[] pj = new int[m * n];
            int[] pi = new int[m * n];
            for (int i = 0; i < m * n; i++)
            {
                pj[i] = -1;
                pi[i] = -1;
            }
            int[] dx = new int[8] { 1, 0, -1, 0, 1, -1, -1, 1 };
            int[] dy = new int[8] { 0, 1, 0, -1, 1, 1, -1, -1 };
            while (d > 0)
            {
                pj[d] = floatlenj;
                pi[d] = floatleni;
                d--;
                for (int k = 0; k < 8; k++)
                {
                    int iy = floatleni + dy[k], ix = floatlenj + dx[k];
                    if (iy >= 0 && iy < n && ix >= 0 && ix < m)
                    {
                        mes.Annex = My_cell[iy, ix].Value;
                        mes.Info = My_cell[iy, ix].Metka;
                        if (mes.Annex == d)
                        {
                            floatlenj += dx[k];
                            floatleni += dy[k];
                            break;
                        }
                    }
                }
            }
            mes.Myobject = First_card.obj.person;
            FieldCreateProfile(this, mes);
            pj[0] = mes.Profile.J;
            pi[0] = mes.Profile.I;


            int maxdist = mes.Profile.Dist;
            Character chelovek = mes.Character;
            My_cell[pi[0], pj[0]].InternalObj = null;
            My_cell[pi[0], pj[0]].Value = -2;
            int h = 1;
            while (pi[h] != -1 && Math.Abs(pi[h] - pi[0]) <= bdist && Math.Abs(pj[h] - pj[0]) <= bdist)
                h++;
            pi[h] = -1;
            pj[h] = -1;
            Field.leftdest = leftsourceimage;
            Field.topdest = topsourceimage;
            Field.AddMas(pj, pi, n * m);
            if (mes.Profile.Ally)
                mes.Annex = -3;
            else
                mes.Annex = -1;
            My_cell[pi[h - 1], pj[h - 1]].InternalObj = chelovek;
            My_cell[pi[h - 1], pj[h - 1]].Value = mes.Annex;
            endi = pi[h - 1];
            endj = pj[h - 1];
            chelovek.I = pi[h - 1];
            chelovek.J = pj[h - 1];
        }
        public void MyPaint(object sender, MyMessage mes)
        {
            Rectangle recfon = new Rectangle(0, 0, xe, ye);
            dcfield.DrawImage(fon, destinationRect, sourceRect, GraphicsUnit.Pixel);
            mes.Code = 0;
            mes.dc1 = dcfield;

            Pen hPen = new Pen(Brushes.Black);
            hPen.Width = 0.8F;

            dcfield.DrawLine(hPen, xe, 0, xe, ye);
            dcfield.DrawLine(hPen, xe, ye, 0, ye);
            hPen.Dispose();
            mes.top = 23;
            mes.bottom = ye;
            mes.left = 0;
            mes.right = xe;
            if (FieldUpdateObject != null)
                FieldUpdateObject(this, mes);

            SolidBrush myBrush = new SolidBrush(Color.White);

            dcfield.FillRectangle(myBrush, new Rectangle(xe, 0, xek - xe, ye));

            dcfield.FillRectangle(myBrush, new Rectangle(0, ye, xe, yek));

            if (FieldShow!=null)
                FieldShow(this, mes);
            

            if (gameorprepare && !game)
                Printtext(mes.dc, mes);
            
            mes.dc.DrawImage(picture, 0, 0);
        }
        
        public void Create_cards(MyMessage mes)
        {
            
            First_card = null;
            ListofCards curr, last, newel;
            last = null;
            ListofCharacters tek;
            tek = First_character;
            for (int i = 0; i < numberofcharacters; i++)
            {
                mes.Myobject = tek.obj;
                FieldCreateProfile(this, mes);
                Color mycolor;
                if (mes.Profile.Ally)
                    mycolor = Color.FromArgb(0, 0, 255);
                else
                    mycolor = Color.FromArgb(255, 0, 0);
                
                newel = new ListofCards(new Card(200, 60, mycolor, mes.Profile.Health, mes.Profile.Damage, mes.Profile.Period, mes.Profile.Ally, tek.obj), null);
                tek = tek.next;
                if (First_card==null)
                
                    First_card = newel;
                
                else
                {
                    curr = First_card;
                    last = null;
                    int currperiod;
                    int newelperiod;
                    currperiod = curr.obj.Period;
                    newelperiod = newel.obj.Period;
                    while (curr!=null && currperiod < newelperiod)
                    {
                        last = curr;
                        curr = curr.next;
                        if (curr!=null)
                        
                            currperiod = curr.obj.Period;
                        
                    }
                    if (last==null)
                        First_card = newel;
                    else
                        last.next = newel;
                    newel.next = curr;
                }
            }
            int start_period = First_card.obj.Period;
            First_card.obj.Difference = 0;
            curr = First_card;
            for (int i = 0; i < numberofcharacters; i++)
            {
                curr.obj.Difference = curr.obj.Period - start_period;
                curr.obj.Index = i;
                curr.obj.X = xek - 20;
                curr.obj.Y = i;
                curr = curr.next;
            }
        }
        public void Delete_card(Character corpse)
        {
            ListofCharacters curr, last;
            curr = First_character;
            last = null;
            while (curr.obj != corpse)
            {
                last = curr;
                curr = curr.next;
            }
            last.next = curr.next;
        }
        
        public void Newmove(MyMessage mes)
        {
            mes.Myobject = First_card.obj.person;
            FieldCreateProfile(this, mes);
            gamemove = mes.Profile.Ally;
            int dist = mes.Profile.Dist;
            int bdist = mes.Annex;
            int r = mes.Profile.R;
            int teki = mes.Profile.I;
            int tekj = mes.Profile.J;
            int type = mes.Info;
            int type1 = mes.Answer;
            int[] dx = new int[8]{ 1, 0, -1, 0, 1, -1, -1, 1 };
            int[] dy = new int[8]{ 0, 1, 0, -1, 1, 1, -1, -1 };
            int d = 0;
            mes.Annex = 0;
            mes.Answer = 1;
            mes.color = Color.FromArgb(80, 0, 255, 0);
            My_cell[teki, tekj].LeeAlgorithm(mes);
            bool stop;
            do
            {
                stop = true;
                for (int y = 0; y < n; y++)
                    for (int x = 0; x < m; x++)
                    {
                        mes.Annex = My_cell[y, x].Value;
                        mes.Info = My_cell[y, x].Metka;
                        if (mes.Annex == d)
                        {
                            for (int k = 0; k < 8; k++)
                            {
                                int iy = y + dy[k], ix = x + dx[k];

                                if (iy >= 0 && iy < n  && ix >= 0 && ix < m )
                                {
                                    mes.Annex = My_cell[iy, ix].Value;
                                    mes.Info = My_cell[iy, ix].Metka;
                                    if (mes.Annex == -2)
                                    {
                                        stop = false;
                                        mes.Annex = d + 1;
                                        if (dist >= d + 1)
                                            mes.Answer = 1;
                                        else
                                            mes.Answer = 0;
                                        mes.color = Color.FromArgb(80,0, 255, 0);
                                        mes.Profile.Ally = gamemove;
                                        My_cell[iy, ix].LeeAlgorithm(mes);
                                    }
                                }
                            }
                        }
                    }
                d++;
            } while (!stop);
            mes.Code = 23;
            if (bdist==0)
            {
                if (gamemove)
                {
                    Attacktraj(mes, -1, Color.FromArgb(80,255,0,0), teki, tekj, dist + 1);
                    Attacktraj(mes, -4, Color.FromArgb(80,255,0,0), teki, tekj, dist + 1);
                    if (type==0)
                        Attacktraj(mes, -3, Color.FromArgb(80,0,0,255), teki, tekj, dist + 1);
                } 
                else
                {
                    Attacktraj(mes, -3, Color.FromArgb(80,255,0,0), teki, tekj, dist + 1);//В таком случае он должен вернуть информацию о том, есть ли такие вообще и сразу передать колдуну (или начать формировать структуру)
                    if (type==0)
                        Attacktraj(mes, -1, Color.FromArgb(80,0,0,255), teki, tekj, dist + 1);//Аналогично
                }
            }
            else
                //Обойти все в радиусе, и если противник, то попытаться построить прямой отрезок. Если на пути отрезка препятствие, то стрелять не может => не можем и попасть => красить не надо. Если выхода не последовало, то красим.
                if (type1==0)
            {
                if (gamemove)
                {
                    mes.Code = 23;
                    Linetraj(mes, -1, Color.FromArgb(80,255,0,0), teki, tekj, r);
                    Linetraj(mes, -4, Color.FromArgb(80,255,0,0), teki, tekj, r);
                    if (type==0)
                        Linetraj(mes, -3, Color.FromArgb(80,0,0,255), teki, tekj, r);
                }
                else
                {
                    mes.Code = 23;
                    Linetraj(mes, -3, Color.FromArgb(80,255,0,0), teki, tekj, r);
                    if (type==0)
                        Linetraj(mes, -1, Color.FromArgb(80,0,0,255), teki, tekj, r);
                }
            }
            else
            {
                if (gamemove)
                {
                    mes.Code = 23;
                    Partraj(mes, -1, Color.FromArgb(80,255,0,0), teki, tekj, r);
                    Partraj(mes, -4, Color.FromArgb(80,255,0,0), teki, tekj, r);
                    if (type==0)
                        Partraj(mes, -3, Color.FromArgb(180,0,0,255), teki, tekj, r);
                }
                else
                {
                    mes.Code = 23;
                    Partraj(mes, -3, Color.FromArgb(80,255,0,0), teki, tekj, r);
                    if (type==0)
                        Partraj(mes, -1, Color.FromArgb(80,0,0,255), teki, tekj, r);
                }
            }
        }
        public void Partraj(MyMessage mes, int symbol, Color color, int teki, int tekj, int r)
        {
            for (int i = Math.Max(0, teki - r); i <= Math.Min(n - 1, teki + r); i++)
            {
                for (int j = Math.Max(0, tekj - r); j <= Math.Min(m - 1, tekj + r); j++)
                {
                    mes.Annex = My_cell[i, j].Value;
                    mes.Info = My_cell[i, j].Metka;
                    if (mes.Annex == symbol)
                    {
                        mes.color = color;
                        mes.Annex = symbol;
                        mes.Answer = 1;
                        mes.Profile.Ally = gamemove;
                        My_cell[i, j].LeeAlgorithm(mes);
                        if (!gamemove)
                        {
                            ListofCharacters curr = First_character;
                            int jk = 1;
                            while (jk != 0 && curr != null)
                            {
                                if (curr.obj == mes.Myobject)
                                {
                                    jk = 0;
                                    if (symbol == -3)
                                    {
                                        boolnearenemy = true;
                                        booleye = true;
                                        curr.eye = true;
                                    }
                                    else if (symbol == -1)
                                    {
                                        First_card.obj.GetEffStruct(mes);
                                        
                                        curr.obj.CheckEffects(mes);
                                        if (mes.Answer != 0 && mes.Annex != 0)
                                        {
                                            boolnearally = true;
                                            curr.near_h = true;
                                        }
                                    }
                                    curr.near_c = true;
                                }
                                curr = curr.next;
                            }
                        }
                        mes.Code = 23;
                    }
                }
            }
        }
        public void Linetraj(MyMessage mes, int symbol, Color color, int teki, int tekj, int r)
        {
            for (int i = Math.Max(0, teki - r); i <= Math.Min(n - 1, teki + r); i++)
            {
                for (int j = Math.Max(0, tekj - r); j <= Math.Min(m - 1, tekj + r); j++)
                {
                    mes.Annex = My_cell[i, j].Value;
                    mes.Info = My_cell[i, j].Metka;
                    if (mes.Annex == symbol)
                    {
                        bool flag = true;
                        int starti = mes.Profile.I;
                        int startj = mes.Profile.J;
                        int deltax = Math.Abs(startj - j);
                        int deltay = Math.Abs(starti - i);
                        int signx = startj < j ? 1 : -1;
                        int signy = starti < i ? 1 : -1;
                        int error = deltax - deltay;
                        while (flag && (startj != j || starti != i))
                        {
                            mes.Annex = My_cell[starti, startj].Value;
                            mes.Info = My_cell[starti, startj].Metka;
                            if (mes.Annex != -1 && mes.Annex != -3 && mes.Annex != -4)
                            {
                                int error2 = error * 2;
                                if (error2 > -deltay)
                                {
                                    error -= deltay;
                                    startj += signx;
                                }
                                if (error2 < deltax)
                                {
                                    error += deltax;
                                    starti += signy;
                                }
                            }
                            else
                                flag = false;
                        }
                        if (flag)
                        {
                            mes.color = color;
                            mes.Answer = 1;
                            mes.Annex = symbol;
                            mes.Profile.Ally = gamemove;
                            My_cell[i, j].LeeAlgorithm(mes);
                            if (!gamemove)
                            {
                                ListofCharacters curr = First_character;
                                int jk = 1;
                                while (jk != 0 && curr != null)
                                {
                                    if (curr.obj == mes.Myobject)
                                    {
                                        jk = 0;
                                        if (symbol == -3)
                                        {
                                            boolnearenemy = true;
                                            booleye = true;
                                            curr.eye = true;
                                        }
                                        else if (symbol == -1)
                                        {
                                            First_card.obj.GetEffStruct(mes);

                                            curr.obj.CheckEffects(mes);
                                            if (mes.Answer != 0 && mes.Annex != 0)
                                            {
                                                boolnearally = true;
                                                curr.near_h = true;
                                            }

                                        }
                                        curr.near_c = true;
                                    }
                                    curr = curr.next;
                                }
                            }
                            mes.Code = 23;
                        }
                    }
                }
            }
        }
        public void Attacktraj(MyMessage mes, int symbol, Color color, int teki, int tekj, int dist)
        {
            int[] dx = new int[8] { 1, 1, 0, -1, -1, -1, 0, 1 };
            int[] dy = new int[8] { 0, 1, 1, 1, 0, -1, -1, -1 };
            for (int i = Math.Max(0, teki - dist); i <= Math.Min(n - 1, teki + dist); i++)
            {
                for (int j = Math.Max(0, tekj - dist); j <= Math.Min(m - 1, tekj + dist); j++)
                {
                    mes.Annex = My_cell[i, j].Value;
                    mes.Info = My_cell[i, j].Metka;
                    if (mes.Annex == symbol)
                    {
                        bool flag = true;
                        for (int k = 0; k < 8 && flag; k++)
                        {
                            int iy = i + dy[k], ix = j + dx[k];
                            if (iy >= 0 && iy < n && ix >= 0 && ix < m)
                            {
                                mes.Annex = My_cell[iy, ix].Value;
                                mes.Info = My_cell[iy, ix].Metka;
                                if (mes.Annex >= 0 && mes.Annex <= dist - 1)
                                {
                                    flag = false;
                                    mes.Annex = symbol;
                                    mes.Answer = 1;
                                    mes.color = color;
                                    mes.Profile.Ally = gamemove;
                                    My_cell[i, j].LeeAlgorithm(mes);
                                    if (!gamemove)
                                    {
                                        ListofCharacters curr = First_character;
                                        int jk = 1;
                                        while (jk != 0 && curr != null)
                                        {
                                            if (curr.obj == mes.Myobject)
                                            {
                                                jk = 0;
                                                if (symbol == -3)
                                                {
                                                    boolnearenemy = true;
                                                    booleye = true;
                                                    curr.eye = true;
                                                }
                                                else if (symbol == -1)
                                                {
                                                    First_card.obj.GetEffStruct(mes);

                                                    curr.obj.CheckEffects(mes);
                                                    if (mes.Answer != 0 && mes.Annex != 0)
                                                    {
                                                        boolnearally = true;
                                                        curr.near_h = true;
                                                    }

                                                }
                                                curr.near_c = true;
                                            }
                                            curr = curr.next;
                                        }
                                    }
                                    mes.Code = 23;
                                }
                            }
                        }
                    }
                }
            }
        }
        public void Changelistofchar(Graphics dc, MyMessage mes)
        {
            //Изменить значение цикла для первого, узнать скорость, умножить на значение цикла
            //Достучаться до всех и узнать их новые состояния. Если живы, то хорошо. Если нет, то удалить как персонажа, так и его карту - это реализовать потом
            //По значению периода рассортировать карточки, определить самого первого персонажа
            //Нарисовать поле карт и сами карточки
            booleye = false;
            boolnearenemy = false;
            boolnearally = false;
            ListofCards curr = First_card, last = null, tek, pred;
            mes.Character = curr.obj.person;
            Fieldapplyeffects(this, mes);
            while (curr!=null)
            {
                mes.Myobject = curr.obj.person;
                FieldCreateProfile(this, mes);
                curr.obj.UpdateCard(mes);
                if (mes.Profile.Health <= 0)
                {
                    
                    
                    curr.obj = null;
                    numberofcharacters--;
                    if (last!=null)
                    {
                        last.next = curr.next;
                        curr = last.next;
                    }
                    else
                    {
                        First_card = First_card.next;
                        curr = First_card;
                    }
                    mes.Myobject = mes.Character;
                    FieldCreateProfile(this, mes);
                    mes.Code = -1;
                    FieldDelete(this, mes);
                    FieldWorkAndDie(this, mes);
                    ListofCharacters currch = First_character, lastch = null;
                    int p = 1;
                    while (p!=0 && currch!=null)
                    {
                        if (currch.obj == mes.Character)
                        {
                            currch.obj = null;
                            if (lastch!=null)
                                lastch.next = currch.next;
                            else
                                First_character = First_character.next;
                            p = 0;
                        }
                        else
                        {
                            lastch = currch;
                            currch = currch.next;
                        }
                    }
                }
                else
                {
                    last = curr;
                    curr = curr.next;
                }
                mes.Code = 16;
            }
            Checkwin(dc, mes);
            booleye = false;
            ListofCharacters currr = First_character;
            while (currr!=null)
            {
                currr.near_c = false;
                currr.near_h = false;
                if (currr.eye)
                    booleye = true;
                currr = currr.next;
            }
            //Ели в живых не останется никого, то и моих тоже, а значит, я сюда не попаду
            if (game)
            {
                curr = First_card.next;
                pred = First_card;
                int currperiod;
                int newelperiod;
                while (curr!=null)
                {
                    
                    currperiod = pred.obj.Period;
                    newelperiod = curr.obj.Period;
                    if (currperiod > newelperiod)
                    {
                        pred.next = curr.next;
                        tek = First_card;
                        last = null;
                        currperiod = tek.obj.Period;
                        while (tek!=null && currperiod < newelperiod)
                        {
                            last = tek;
                            tek = tek.next;
                            if (tek!=null)
                            {
                                currperiod = tek.obj.Period;
                            }
                        }
                        if (last==null)
                            First_card = curr;
                        else
                            last.next = curr;
                        curr.next = tek;
                        curr = pred.next;
                    }
                    else
                    {
                        pred = curr;
                        curr = curr.next;
                    }
                }
                
                int start_period = First_card.obj.Period;
                First_card.obj.Difference = 0;
                curr = First_card;
                for (int i = 0; i < numberofcharacters; i++)
                {
                    
                    curr.obj.Difference = curr.obj.Period - start_period;
                    curr.obj.Index = i;
                    curr.obj.X = xek - 20;
                    curr.obj.Y = i;
                    curr = curr.next;
                }
            }
        }
        public void Printtext(Graphics dc, MyMessage mes)
        {



            Font drawFont = new Font("Arial", 20);
            SolidBrush drawBrush;
            string otvet;
            StringFormat drawFormat = new StringFormat();
            

            int resultpoints;
            //mes.Code = 31;
            //owner.Dispatch(mes.dc, mes);
            FieldGetPoints(this, mes);
            int gamem = gamemove ? 1 : 0;
            resultpoints = (mes.Info - mes.Annex) * (int)(1 + (float)mes.Info / allenemypoints) + 15 * (moves + 1) * gamem;

            FieldGameOver(this, mes);
            if (flag)
            {
                drawBrush = new SolidBrush(Color.Blue);
                otvet = "Победа!";
            }
            else
            {
                drawBrush = new SolidBrush(Color.Red);
                otvet = "Поражение!";
            }
            mes.dc1.DrawString(otvet, drawFont, drawBrush, mes.Tpoint.X/2, 40, drawFormat);
            if (resultpoints < 0)
                resultpoints = 0;
            mes.dc1.DrawString(resultpoints.ToString(), drawFont, drawBrush, mes.Tpoint.X / 2, 70, drawFormat);
            mes.dc.DrawImage(picture, 0, 0);
        }
        public void Hidefieldofcards(MyMessage mes)
        {
            SolidBrush myBrush1 = new SolidBrush(Color.White);
            mes.dc1.FillRectangle(myBrush1, new Rectangle(xe + 1, y + 25, xek-xe-1, yek-y-25));
            myBrush1.Dispose();

        }
        public void Reverso()
        {
            if (gameorprepare)
            {
                MyMessage mes = new MyMessage();
                int w = xe / m, l = ye / n;
                Returnpast(mes);
                numberofcharacters = extranumberofcharacters;
                moves = extramoves;
                ListofCards currc = First_card, lastc = null;
                mes.Code = 1;
                mes.Profile.I = -1;
                mes.Profile.J = -1;
                while (currc!=null)
                {
                    lastc = currc;
                    mes.Myobject = currc.obj.person;
                    FieldCreateProfile(this, mes);
                    mes.Code = -1;
                    currc = currc.next;
                    FieldDelete(this, mes);
                    lastc = null;
                }
                First_card = null;
                ListofCharacters curr = First_character, last = null;
                mes.Del = 1;
                mes.Code = 6;
                while (curr != null)
                {
                    last = curr;
                    curr = curr.next;
                    last = null;
                }
                mes.Del = 0;
                First_character = null;
                mes.Code = 11;
                ListofGameObjects tek = FirstGObject, pred = null;
                while (tek!=null)
                {
                    mes.Myobject = tek.obj;
                    FieldCreateProfile(this, mes);
                    mes.Code = -1;
                    pred = tek;
                    tek = tek.next;
                    FieldDelete(this, mes);
                    pred = null;
                }
                ListofCharacters extracurr = Extrafirst_character, extralast = null;
                First_character = null;
                mes.Code = 6;
                mes.W = w;
                mes.L = l;
                while (extracurr!=null)
                {
                    mes.Myobject = extracurr.obj;
                    curr = new ListofCharacters(extracurr.obj.Clone2(),null,false,false,false);
                    //Добавить в криэйтпрофайл то, какой это тип персонажа. По нему и остальным данным создать объект нужного класса. Потом сделать то же самое, только для объектов, а не персонажей
                    //Потом вернуть информацию об очках персонажа и компьютера (изначально 0)
                    //Потом вернуть информацию о начальном количестве ходов и монет
                    //Потом вызвать функцию определения самого первого хода (по факту, это новое событие), т.е. еще и карточки создать
                    mes.Myobject = curr.obj;
                    FieldCreateProfile(this, mes);
                    if (mes.Profile.Ally)
                        mes.Annex = -3;
                    else
                        mes.Annex = -1;
                    FieldAddChar(this, mes);
                    if (First_character!=null)
                        last.next = curr;
                    else
                        First_character = curr;
                    last = curr;
                    extracurr = extracurr.next;
                    mes.Code = 6;
                }
                ListofGameObjects extratek = ExtrafirstGObject, extrapred = null;
                FirstGObject = null;
                mes.Code = 6;
                mes.W = w;
                mes.L = l;
                //mes.Pole = this;
                while (extratek!=null)
                {
                    tek = new ListofGameObjects(extratek.obj.Clone1(),null);
                    //Добавить в криэйтпрофайл то, какой это тип персонажа. По нему и остальным данным создать объект нужного класса. Потом сделать то же самое, только для объектов, а не персонажей
                    //Потом вернуть информацию об очках персонажа и компьютера (изначально 0)
                    //Потом вернуть информацию о начальном количестве ходов и монет
                    //Потом вызвать функцию определения самого первого хода (по факту, это новое событие), т.е. еще и карточки создать
                    //+Сообщить колдуну, что все сначала
                    //+Обнулить все булины и т.д.
                    mes.Myobject = tek.obj;
                    FieldCreateProfile(this, mes);
                    mes.Annex = -4;
                    FieldAddChar(this, mes);
                    if (FirstGObject!=null)
                        pred.next = tek;
                    else
                        FirstGObject = tek;
                    pred = tek;
                    extratek = extratek.next;
                    mes.Code = 6;
                }
                moves = extramoves;
                mes.Data = moves;
                mes.Annex = 0;
                mes.Answer = 0;
                mes.Profile.Ally = false;
                FieldNextMove(this, mes);
                game = true;
                Fieldstartgame(this, mes);
            }
        }
    }
}
