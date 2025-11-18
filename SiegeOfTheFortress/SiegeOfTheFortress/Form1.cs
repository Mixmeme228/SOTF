using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TreeView;

namespace SiegeOfTheFortress
{
    public partial class Form1 : Form

    {
        public static event UpdateObject FormrectClick, FormmouseDown, FormmouseUp, FormmouseMove;
        public static event UpdateObject Formstartgame;
        public static event UpdateObject Formrectpaint;
        public static event UpdateObject FormMyTick;
        public static event UpdateObject MySizeChanged;

        public static event UpdateObject FormStopEvent;
        public static event UpdateObject FormContinueEvent;

        private int currentvariant;
        private bool game,stateofgame, gameorprepare;
        //Первое - ???, второе - игра (1) или этап выбора карт/уровней, третье - сама игра или подготовка к ней
        private Field Myfield;
        Graphics dc;
        private Timer t;
        public Form1()
        {
            InitializeComponent();
            dc = this.CreateGraphics();
            game = false;
            stateofgame = false;
            gameorprepare = false;
            Myfield = null;
            var mySize = new Size() { Height = 450, Width = 800 };
            MinimumSize = mySize;
            NewSoldier.BackgroundImageLayout= ImageLayout.Zoom;
            NewCannon.BackgroundImageLayout = ImageLayout.Zoom;
            NewWizard.BackgroundImageLayout = ImageLayout.Zoom;
            currentvariant = 0;
            Countofmoney.Text = "450";
            Countofmoves.Text = "40";
            t = new Timer();
            t.Enabled = false;
            t.Tick += T_Tick;
            Field.Fieldsearchdc += getMyDC;
            Field.FieldNextMove += NewCount;
            Field.FieldWorkAndDie += WorkandDie;
            Field.FieldGameOver += GameOver;
            Field.FieldChangeMoney += ChangeMoney;
            Field.FieldGetMoney += GetMoney;
            Field.FieldGetPoints += GetPoints;
            //Field.FieldStartAction += StartAction;
            //Field.FieldEndAction += EndAction;
        }

        public void getMyDC(object sender, MyMessage mes)
        {
            mes.dc = dc;
        }

        //public void StartAction(object sender, MyMessage mes)
        //{
        //FormBorderStyle = FormBorderStyle.FixedSingle;
        //MaximizeBox = false;
        //}

        //public void EndAction(object sender, MyMessage mes)
        //{
        //    FormBorderStyle = FormBorderStyle.Sizable;
        //    MaximizeBox = true;
        //}

        private void exitmyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void WorkandDie(object sender, MyMessage mes)
        {
            if (mes.Code != 99)
            {
                if (mes.Profile.Ally)
                {
                    int.TryParse(Countofenemypoints.Text, out int c);
                    c += mes.Profile.Value;
                    Countofenemypoints.Text = c.ToString();
                }
                else
                {
                    int.TryParse(Countofmypoints.Text, out int c);
                    c += mes.Profile.Value;
                    Countofmypoints.Text = c.ToString();
                }
            }
            else
            {
                Countofenemypoints.Text = mes.Annex.ToString();
                Countofmypoints.Text = mes.Answer.ToString();

            }
        }

        public void GameOver(object sender, MyMessage mes)
        {
            mes.Tpoint.X = ClientRectangle.Width;
            mes.Tpoint.Y = ClientRectangle.Height;
            t.Enabled = false;
        }

        public void ChangeMoney(object sender, MyMessage mes)
        {
            int.TryParse(Countofmoney.Text, out int c);
            c += mes.Profile.Value;
            Countofmoney.Text = c.ToString();
        }

        public void GetPoints(object sedner, MyMessage mes)
        {
            int.TryParse(Countofenemypoints.Text, out mes.Annex);
            int.TryParse(Countofmypoints.Text, out mes.Info);
        }

        public void GetMoney(object sender, MyMessage mes)
        {
            int.TryParse(Countofmoney.Text, out mes.Info);
        }

        public void NewCount(object sender, MyMessage mes)
        {
            Countofmoves.Text = mes.Data.ToString();
        }
        private void T_Tick(object sender, EventArgs e)
        {
            MyMessage mes = new MyMessage();
            mes.dc = dc;
            FormMyTick(this, mes);
        }

        private void Startmygame_Click(object sender, EventArgs e)
        {
            currentvariant = 0;
            gameorprepare = true;
            NewSoldier.Enabled = false;
            NewSoldier.Visible = false;
            NewCannon.Enabled = false;
            NewCannon.Visible = false;
            NewWizard.Enabled = false;
            NewWizard.Visible = false;
            Deletemyobject.Enabled = false;
            Deletemyobject.Visible = false;
            Startmygame.Enabled = false;
            Startmygame.Visible = false;
            pauseToolStripMenuItem.Enabled = true;
            saveToolStripMenuItem.Enabled = false;
            loadToolStripMenuItem.Enabled = false;
            restartmygameToolStripMenuItem.Enabled = true;
            MyMessage mes = new MyMessage();
            mes.dc = dc;
            t.Interval = 150;
            t.Enabled = true;
            Formstartgame(this, mes);
        }

        private void NewSoldier_Click(object sender, EventArgs e)
        {
            currentvariant = 1;
        }

        private void NewCannon_Click(object sender, EventArgs e)
        {
            currentvariant = 2;
        }

        private void NewWizard_Click(object sender, EventArgs e)
        {
            currentvariant = 3;
        }

        private void Deletemyobject_Click(object sender, EventArgs e)
        {
            currentvariant = 1000;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            MyMessage mes = new MyMessage();
            dc = this.CreateGraphics();
            mes.dc = dc;
            mes.right = Size.Width;
            mes.bottom = Size.Height;
            mes.clientright = ClientRectangle.Width;
            mes.clientbottom = ClientRectangle.Height;
            MySizeChanged(this, mes);
        }

        private void pauseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            continueToolStripMenuItem.Enabled = true;
            pauseToolStripMenuItem.Enabled = false;
            saveToolStripMenuItem.Enabled = true;
            loadToolStripMenuItem.Enabled = true;
            t.Enabled = false;
            MyMessage mes = new MyMessage();
            if(FormStopEvent!=null)
                FormStopEvent(this, mes);
        }

        private void continueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            continueToolStripMenuItem.Enabled = false;
            pauseToolStripMenuItem.Enabled = true;
            saveToolStripMenuItem.Enabled = false;
            loadToolStripMenuItem.Enabled = false;
            t.Enabled = true;
            MyMessage mes = new MyMessage();
            if (FormContinueEvent != null)
                FormContinueEvent(this, mes);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            MyMessage mes1 = new MyMessage();
            int.TryParse(Countofmoney.Text, out mes1.Annex);
            int.TryParse(Countofmypoints.Text, out mes1.Answer);
            int.TryParse(Countofenemypoints.Text, out mes1.Info);
            string filename = saveFileDialog1.FileName+".mfld";
            mes1.filename = filename;
            Myfield.BeforeOperation(mes1);

            FileStream FS = new FileStream(filename, FileMode.Create);
            BinaryFormatter BF = new BinaryFormatter();
            BF.Serialize(FS, Myfield);
            FS.Close();
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            Myfield.BeforeDeserialize();
            string filename = openFileDialog1.FileName;
            
            FileStream FS = File.OpenRead(filename);
            BinaryFormatter BF = new BinaryFormatter();

            Myfield = (Field)BF.Deserialize(FS);
            MyMessage mes = new MyMessage();
            mes.filename = filename;
            //mes.exampleForm = this;
            Myfield.DeserializeField(mes);
            if (mes.Profile.Ally)
            {
                currentvariant = 0;
                gameorprepare = true;
                NewSoldier.Enabled = false;
                NewSoldier.Visible = false;
                NewCannon.Enabled = false;
                NewCannon.Visible = false;
                NewWizard.Enabled = false;
                NewWizard.Visible = false;
                Deletemyobject.Enabled = false;
                Deletemyobject.Visible = false;
                Startmygame.Enabled = false;
                Startmygame.Visible = false;
                pauseToolStripMenuItem.Enabled = true;
                continueToolStripMenuItem.Enabled = false;
                loadToolStripMenuItem.Enabled = false;
                saveToolStripMenuItem.Enabled = false;
                restartmygameToolStripMenuItem.Enabled = true;
                t.Interval = 150;
                t.Enabled = true;
            }
            else
            {
                if (gameorprepare)

                {
                    currentvariant = 0;
                    gameorprepare = false;
                    NewSoldier.Enabled = true;
                    NewSoldier.Visible = true;
                    NewCannon.Enabled = true;
                    NewCannon.Visible = true;
                    NewWizard.Enabled = true;
                    NewWizard.Visible = true;
                    Deletemyobject.Enabled = true;
                    Deletemyobject.Visible = true;
                    Startmygame.Enabled = true;
                    Startmygame.Visible = true;
                    pauseToolStripMenuItem.Enabled = false;
                    continueToolStripMenuItem.Enabled = false;
                    loadToolStripMenuItem.Enabled = true;
                    saveToolStripMenuItem.Enabled = true;
                    restartmygameToolStripMenuItem.Enabled = false;
                }
                Invalidate();
            }
            Countofenemypoints.Text = mes.Info.ToString();
            Countofmypoints.Text = mes.Answer.ToString();
            Countofmoney.Text = mes.Annex.ToString();
            Countofmoves.Text = mes.Data.ToString();
            FS.Close();
        }

        private void restartmygameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            t.Enabled = true;
            Countofenemypoints.Text = "0";
            Countofmypoints.Text = "0";
            Myfield.Reverso();
        }

        

        private void referenceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormInstruction insForm = new FormInstruction();
            insForm.Show();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
                MyMessage mes = new MyMessage();
                mes.dc = dc;
                mes.X = e.X;
                mes.Y = e.Y;
                mes.but = e.Button;
                if (FormmouseDown != null)
                    FormmouseDown(sender, mes);
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            MyMessage mes = new MyMessage();
            mes.dc = dc;
            mes.X = e.X;
            mes.Y = e.Y;
            mes.but = e.Button;
            if (FormmouseUp != null)
                FormmouseUp(sender, mes);

        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            MyMessage mes = new MyMessage();
            mes.dc = dc;
            mes.X = e.X;
            mes.Y = e.Y;
            mes.but = e.Button;
            if (FormmouseMove != null)
                FormmouseMove(sender, mes);

        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
                MyMessage mes = new MyMessage();
                mes.dc = dc;
                mes.X = e.X;
                mes.Y = e.Y;
                mes.but = e.Button;
                mes.Data = currentvariant;
                if (FormrectClick != null)
                    FormrectClick(sender, mes);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (!stateofgame)
            {
                MyMessage mes = new MyMessage();
                stateofgame = true;
                game = true;
                gameorprepare = false;

                string name_of_map = "TESTOne.mfld";

                mes.X = ClientRectangle.Width;
                mes.Y = ClientRectangle.Height;
                mes.clientbottom = ClientRectangle.Height;

                //Myfield = new Field(dc, mes, 0, 23, 960, 663, ClientRectangle.Width, ClientRectangle.Height, name_of_map, count, true, true, false, this);


                FileStream FS = File.OpenRead(name_of_map);
                BinaryFormatter BF = new BinaryFormatter();

                Myfield = (Field)BF.Deserialize(FS);
                MyMessage mes1 = new MyMessage();
                mes1.filename = name_of_map;
                //mes1.exampleForm = this;
                Myfield.DeserializeField(mes1);
                Countofenemypoints.Text = mes1.Info.ToString();
                Countofmypoints.Text = mes1.Answer.ToString();
                Countofmoney.Text = mes1.Annex.ToString();
                Countofmoves.Text = mes1.Data.ToString();
                FS.Close();

                if (stateofgame)
                {
                    mes.dc = e.Graphics;
                    Formrectpaint(this, mes);
                }
                Point myLoc = new Point(ClientRectangle.Width-55, 30);

                Moves.Enabled = true;
                Moves.Location = myLoc;
                Moves.Visible = true;

                myLoc.X = ClientRectangle.Width-20; myLoc.Y = 30;

                Countofmoves.Enabled = true;
                Countofmoves.Location = myLoc;
                Countofmoves.Visible = true;

                myLoc.X = ClientRectangle.Width * 7 / 12; myLoc.Y = ClientRectangle.Height-15;

                Money.Enabled = true;
                Money.Location = myLoc;
                Money.Visible = true;

                myLoc.X = ClientRectangle.Width * 8 / 12; myLoc.Y = ClientRectangle.Height-15;

                Countofmoney.Enabled = true;
                Countofmoney.Location = myLoc;
                Countofmoney.Visible = true;

                myLoc.X = ClientRectangle.Width * 3 / 12; myLoc.Y = ClientRectangle.Height-15;

                EnemyPoints.Enabled = true;
                EnemyPoints.Location = myLoc;
                EnemyPoints.Visible = true;

                myLoc.X = ClientRectangle.Width * 4 / 12; myLoc.Y = ClientRectangle.Height-15;

                Countofenemypoints.Enabled = true;
                Countofenemypoints.Location = myLoc;
                Countofenemypoints.Visible = true;

                myLoc.X = ClientRectangle.Width * 1 / 12; myLoc.Y = ClientRectangle.Height-15;

                MyPoints.Enabled = true;
                MyPoints.Location = myLoc;
                MyPoints.Visible = true;

                myLoc.X = ClientRectangle.Width * 2 / 12; myLoc.Y = ClientRectangle.Height-15;

                Countofmypoints.Enabled = true;
                Countofmypoints.Location = myLoc;
                Countofmypoints.Visible = true;

                myLoc.X = ClientRectangle.Width * 10 / 12; myLoc.Y= ClientRectangle.Height * 2 / 10;

                NewSoldier.Enabled = true;
                NewSoldier.Location = myLoc;
                NewSoldier.Visible = true;

                myLoc.X = ClientRectangle.Width * 10 / 12; myLoc.Y = ClientRectangle.Height * 3 / 10;

                NewCannon.Enabled = true;
                NewCannon.Location = myLoc;
                NewCannon.Visible = true;

                myLoc.X = ClientRectangle.Width * 10 / 12; myLoc.Y = ClientRectangle.Height * 4 / 10;

                NewWizard.Enabled = true;
                NewWizard.Location = myLoc;
                NewWizard.Visible = true;

                myLoc.X = ClientRectangle.Width * 10 / 12; myLoc.Y = ClientRectangle.Height * 5 / 10;

                Startmygame.Enabled = true;
                Startmygame.Location = myLoc;
                Startmygame.Visible = true;

                myLoc.X = ClientRectangle.Width * 11 / 12; myLoc.Y = ClientRectangle.Height * 5 / 10;

                Deletemyobject.Enabled = true;
                Deletemyobject.Location = myLoc;
                Deletemyobject.Visible = true;

            }
            else
            {
                if (stateofgame)
                {
                    MyMessage mes = new MyMessage();
                    mes.dc = e.Graphics;
                    Formrectpaint(this, mes);
                }
            }
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            if (stateofgame)
            {
                Point myLoc = new Point(ClientRectangle.Width-55, 30);


                Moves.Location = myLoc;


                myLoc.X = ClientRectangle.Width-20; myLoc.Y = 30;


                Countofmoves.Location = myLoc;


                myLoc.X = ClientRectangle.Width * 7 / 12; myLoc.Y = ClientRectangle.Height-15;


                Money.Location = myLoc;


                myLoc.X = ClientRectangle.Width * 8 / 12; myLoc.Y = ClientRectangle.Height-15;


                Countofmoney.Location = myLoc;


                myLoc.X = ClientRectangle.Width * 3 / 12; myLoc.Y = ClientRectangle.Height-15;


                EnemyPoints.Location = myLoc;


                myLoc.X = ClientRectangle.Width * 4 / 12; myLoc.Y = ClientRectangle.Height-15;


                Countofenemypoints.Location = myLoc;


                myLoc.X = ClientRectangle.Width * 1 / 12; myLoc.Y = ClientRectangle.Height-15;


                MyPoints.Location = myLoc;


                myLoc.X = ClientRectangle.Width * 2 / 12; myLoc.Y = ClientRectangle.Height-15;


                Countofmypoints.Location = myLoc;
                if (!gameorprepare)
                {
                    Point myLoc1 = new Point(ClientRectangle.Width - 180, ClientRectangle.Height * 2 / 10);


                    NewSoldier.Location = myLoc1;


                    myLoc1.X = ClientRectangle.Width - 180; myLoc1.Y = ClientRectangle.Height * 3 / 10;


                    NewCannon.Location = myLoc1;


                    myLoc1.X = ClientRectangle.Width - 180; myLoc1.Y = ClientRectangle.Height * 4 / 10;


                    NewWizard.Location = myLoc1;


                    myLoc1.X = ClientRectangle.Width - 180; myLoc1.Y = ClientRectangle.Height * 5 / 10;


                    Startmygame.Location = myLoc1;


                    myLoc1.X = ClientRectangle.Width - 100; myLoc1.Y = ClientRectangle.Height * 5 / 10;


                    Deletemyobject.Location = myLoc1;
                }
                
            }
            
        }
        
    }
}
