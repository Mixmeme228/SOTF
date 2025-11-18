using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SiegeOfTheFortress
{
    public class FormInstruction:Form
    {
        private MenuStrip menuStrip1;
        private ToolStripMenuItem aboutPlotToolStripMenuItem;
        private ToolStripMenuItem charactersToolStripMenuItem;
        private ToolStripMenuItem soldierToolStripMenuItem;
        private ToolStripMenuItem wizardToolStripMenuItem;
        private ToolStripMenuItem cannonToolStripMenuItem;
        private ToolStripMenuItem buildingsToolStripMenuItem;
        private ToolStripMenuItem artifactToolStripMenuItem;
        private ToolStripMenuItem wallsToolStripMenuItem;
        private ToolStripMenuItem rulesToolStripMenuItem;
        private ToolStripMenuItem goalToolStripMenuItem;
        private ToolStripMenuItem selectCharsToolStripMenuItem;
        private ToolStripMenuItem moveOrderToolStripMenuItem;
        private ToolStripMenuItem savingGameToolStripMenuItem;
        private ToolStripMenuItem restartGameToolStripMenuItem;
        private ToolStripMenuItem actionsToolStripMenuItem;
        private System.Windows.Forms.Button AboutPlot;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Button ArtifactButton;
        private System.Windows.Forms.Button CannonButton;
        private System.Windows.Forms.Button WizardButton;
        private System.Windows.Forms.Button SoldierButton;
        private RichTextBox richTextBox1;
        private System.Windows.Forms.Button RestartGameButton;
        private System.Windows.Forms.Button SaveGameButton;
        private System.Windows.Forms.Button ActionsButton;
        private System.Windows.Forms.Button OrderButton;
        private System.Windows.Forms.Button SelectionButton;
        private System.Windows.Forms.Button GoalButton;
        private System.Windows.Forms.Button WallButton;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private PictureBox pictureBox3;
        private PictureBox pictureBox4;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Panel panel1;
        //private System.ComponentModel.IContainer components;

        public FormInstruction()
        {
            InitializeComponent();
            //richTextBox1.SelectionBullet = true;
            richTextBox1.SelectionAlignment = HorizontalAlignment.Left;
            richTextBox1.SelectionIndent = 8;
            richTextBox1.SelectionHangingIndent = 3;
            richTextBox1.SelectionRightIndent = 12;
            richTextBox1.Text = "Добро пожаловать в справку об игре Siege of The Fortress! Здесь можно найти информацию о сюжете игры.\n " +
                "Siege of the Fortress - это пошаговая стратегия, в которой нужно управлять своими персонажами, чтобы штурмовать блокпост коварного Колдуна " +
                "и в итоге разрушить Таинственный Артефакт, грозящий уничтожением всего живого! Далее представлен сценарий игры.\n " +
                "Шаг 1: Начало игры\n " +
                "Игрок выбирает своих персонаей в зависимости от своих предпочтений и тактической обстановки на поле боя, размещая своих " +
                "бойцов по клеткам карты. Дополнительную информацию можно найти в пункте справки \"Выбор персонажей\".\n " +
                "Шаг 2: Сражение\n " +
                "Игрок выбирает для каждого собственного персонажа в соответствии со сформированным порядком ходов некоторое действие, которое " +
                "тот совершает следом. Дополнительную информацию можно найти в разделах \"Цель игры\", \"Порядок ходов\" и \"Действия\".\n " +
                "Шаг 3: Победа или Поражение\n " +
                "Если Игрок успеет разрушить Артефакт, то он победит! А если же его персонажи будут повержены или число доступных ходов иссякнет, то игрок проиграет... " +
                "Подробнее см. в разделе \"Цель игры\". ";
            label1.Text = "";
            label2.Text = "";
            label3.Text = "";
            label4.Text = "";
            toolStripStatusLabel1.Text = "О сюжете";
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox4.SizeMode = PictureBoxSizeMode.Zoom;
            

        }


        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.aboutPlotToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.charactersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.soldierToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wizardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cannonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buildingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.artifactToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wallsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rulesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.goalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectCharsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moveOrderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.actionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.savingGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.restartGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RestartGameButton = new System.Windows.Forms.Button();
            this.SaveGameButton = new System.Windows.Forms.Button();
            this.ActionsButton = new System.Windows.Forms.Button();
            this.OrderButton = new System.Windows.Forms.Button();
            this.SelectionButton = new System.Windows.Forms.Button();
            this.GoalButton = new System.Windows.Forms.Button();
            this.WallButton = new System.Windows.Forms.Button();
            this.ArtifactButton = new System.Windows.Forms.Button();
            this.CannonButton = new System.Windows.Forms.Button();
            this.WizardButton = new System.Windows.Forms.Button();
            this.SoldierButton = new System.Windows.Forms.Button();
            this.AboutPlot = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutPlotToolStripMenuItem,
            this.charactersToolStripMenuItem,
            this.buildingsToolStripMenuItem,
            this.rulesToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(935, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // aboutPlotToolStripMenuItem
            // 
            this.aboutPlotToolStripMenuItem.Name = "aboutPlotToolStripMenuItem";
            this.aboutPlotToolStripMenuItem.Size = new System.Drawing.Size(73, 20);
            this.aboutPlotToolStripMenuItem.Text = "О сюжете";
            this.aboutPlotToolStripMenuItem.Click += new System.EventHandler(this.aboutPlotToolStripMenuItem_Click);
            // 
            // charactersToolStripMenuItem
            // 
            this.charactersToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.soldierToolStripMenuItem,
            this.wizardToolStripMenuItem,
            this.cannonToolStripMenuItem});
            this.charactersToolStripMenuItem.Name = "charactersToolStripMenuItem";
            this.charactersToolStripMenuItem.Size = new System.Drawing.Size(83, 20);
            this.charactersToolStripMenuItem.Text = "Персонажи";
            // 
            // soldierToolStripMenuItem
            // 
            this.soldierToolStripMenuItem.Name = "soldierToolStripMenuItem";
            this.soldierToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.soldierToolStripMenuItem.Text = "Пехотинец/Скелет";
            this.soldierToolStripMenuItem.Click += new System.EventHandler(this.soldierToolStripMenuItem_Click);
            // 
            // wizardToolStripMenuItem
            // 
            this.wizardToolStripMenuItem.Name = "wizardToolStripMenuItem";
            this.wizardToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.wizardToolStripMenuItem.Text = "Маг/Ведьмак";
            this.wizardToolStripMenuItem.Click += new System.EventHandler(this.wizardToolStripMenuItem_Click);
            // 
            // cannonToolStripMenuItem
            // 
            this.cannonToolStripMenuItem.Name = "cannonToolStripMenuItem";
            this.cannonToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.cannonToolStripMenuItem.Text = "Пушка";
            this.cannonToolStripMenuItem.Click += new System.EventHandler(this.cannonToolStripMenuItem_Click);
            // 
            // buildingsToolStripMenuItem
            // 
            this.buildingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.artifactToolStripMenuItem,
            this.wallsToolStripMenuItem});
            this.buildingsToolStripMenuItem.Name = "buildingsToolStripMenuItem";
            this.buildingsToolStripMenuItem.Size = new System.Drawing.Size(72, 20);
            this.buildingsToolStripMenuItem.Text = "Строения";
            // 
            // artifactToolStripMenuItem
            // 
            this.artifactToolStripMenuItem.Name = "artifactToolStripMenuItem";
            this.artifactToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.artifactToolStripMenuItem.Text = "Артефакт";
            this.artifactToolStripMenuItem.Click += new System.EventHandler(this.artifactToolStripMenuItem_Click);
            // 
            // wallsToolStripMenuItem
            // 
            this.wallsToolStripMenuItem.Name = "wallsToolStripMenuItem";
            this.wallsToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.wallsToolStripMenuItem.Text = "Стены";
            this.wallsToolStripMenuItem.Click += new System.EventHandler(this.wallsToolStripMenuItem_Click);
            // 
            // rulesToolStripMenuItem
            // 
            this.rulesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.goalToolStripMenuItem,
            this.selectCharsToolStripMenuItem,
            this.moveOrderToolStripMenuItem,
            this.actionsToolStripMenuItem,
            this.savingGameToolStripMenuItem,
            this.restartGameToolStripMenuItem});
            this.rulesToolStripMenuItem.Name = "rulesToolStripMenuItem";
            this.rulesToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            this.rulesToolStripMenuItem.Text = "Правила";
            // 
            // goalToolStripMenuItem
            // 
            this.goalToolStripMenuItem.Name = "goalToolStripMenuItem";
            this.goalToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.goalToolStripMenuItem.Text = "Цель игры";
            this.goalToolStripMenuItem.Click += new System.EventHandler(this.goalToolStripMenuItem_Click);
            // 
            // selectCharsToolStripMenuItem
            // 
            this.selectCharsToolStripMenuItem.Name = "selectCharsToolStripMenuItem";
            this.selectCharsToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.selectCharsToolStripMenuItem.Text = "Выбор персонажей";
            this.selectCharsToolStripMenuItem.Click += new System.EventHandler(this.selectCharsToolStripMenuItem_Click);
            // 
            // moveOrderToolStripMenuItem
            // 
            this.moveOrderToolStripMenuItem.Name = "moveOrderToolStripMenuItem";
            this.moveOrderToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.moveOrderToolStripMenuItem.Text = "Порядок ходов";
            this.moveOrderToolStripMenuItem.Click += new System.EventHandler(this.moveOrderToolStripMenuItem_Click);
            // 
            // actionsToolStripMenuItem
            // 
            this.actionsToolStripMenuItem.Name = "actionsToolStripMenuItem";
            this.actionsToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.actionsToolStripMenuItem.Text = "Действия";
            this.actionsToolStripMenuItem.Click += new System.EventHandler(this.actionoptionsToolStripMenuItem_Click);
            // 
            // savingGameToolStripMenuItem
            // 
            this.savingGameToolStripMenuItem.Name = "savingGameToolStripMenuItem";
            this.savingGameToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.savingGameToolStripMenuItem.Text = "Сохранение игры";
            this.savingGameToolStripMenuItem.Click += new System.EventHandler(this.savingGameToolStripMenuItem_Click);
            // 
            // restartGameToolStripMenuItem
            // 
            this.restartGameToolStripMenuItem.Name = "restartGameToolStripMenuItem";
            this.restartGameToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.restartGameToolStripMenuItem.Text = "Начать сначала";
            this.restartGameToolStripMenuItem.Click += new System.EventHandler(this.restartGameToolStripMenuItem_Click);
            // 
            // RestartGameButton
            // 
            this.RestartGameButton.BackColor = System.Drawing.Color.LightSeaGreen;
            this.RestartGameButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.RestartGameButton.Location = new System.Drawing.Point(0, 327);
            this.RestartGameButton.Name = "RestartGameButton";
            this.RestartGameButton.Size = new System.Drawing.Size(120, 23);
            this.RestartGameButton.TabIndex = 13;
            this.RestartGameButton.Text = "Начать сначала";
            this.RestartGameButton.UseVisualStyleBackColor = false;
            this.RestartGameButton.Click += new System.EventHandler(this.restartGameToolStripMenuItem_Click);
            // 
            // SaveGameButton
            // 
            this.SaveGameButton.BackColor = System.Drawing.Color.LightSeaGreen;
            this.SaveGameButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.SaveGameButton.Location = new System.Drawing.Point(0, 298);
            this.SaveGameButton.Name = "SaveGameButton";
            this.SaveGameButton.Size = new System.Drawing.Size(120, 23);
            this.SaveGameButton.TabIndex = 12;
            this.SaveGameButton.Text = "Сохранение игры";
            this.SaveGameButton.UseVisualStyleBackColor = false;
            this.SaveGameButton.Click += new System.EventHandler(this.savingGameToolStripMenuItem_Click);
            // 
            // ActionsButton
            // 
            this.ActionsButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.ActionsButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ActionsButton.Location = new System.Drawing.Point(0, 269);
            this.ActionsButton.Name = "ActionsButton";
            this.ActionsButton.Size = new System.Drawing.Size(120, 23);
            this.ActionsButton.TabIndex = 11;
            this.ActionsButton.Text = "Действия";
            this.ActionsButton.UseVisualStyleBackColor = false;
            this.ActionsButton.Click += new System.EventHandler(this.actionoptionsToolStripMenuItem_Click);
            // 
            // OrderButton
            // 
            this.OrderButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.OrderButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.OrderButton.Location = new System.Drawing.Point(0, 240);
            this.OrderButton.Name = "OrderButton";
            this.OrderButton.Size = new System.Drawing.Size(120, 23);
            this.OrderButton.TabIndex = 10;
            this.OrderButton.Text = "Порядок ходов";
            this.OrderButton.UseVisualStyleBackColor = false;
            this.OrderButton.Click += new System.EventHandler(this.moveOrderToolStripMenuItem_Click);
            // 
            // SelectionButton
            // 
            this.SelectionButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.SelectionButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.SelectionButton.Location = new System.Drawing.Point(0, 211);
            this.SelectionButton.Name = "SelectionButton";
            this.SelectionButton.Size = new System.Drawing.Size(120, 23);
            this.SelectionButton.TabIndex = 9;
            this.SelectionButton.Text = "Выбор персонажей";
            this.SelectionButton.UseVisualStyleBackColor = false;
            this.SelectionButton.Click += new System.EventHandler(this.selectCharsToolStripMenuItem_Click);
            // 
            // GoalButton
            // 
            this.GoalButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.GoalButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.GoalButton.Location = new System.Drawing.Point(0, 182);
            this.GoalButton.Name = "GoalButton";
            this.GoalButton.Size = new System.Drawing.Size(120, 23);
            this.GoalButton.TabIndex = 8;
            this.GoalButton.Text = "Цель игры";
            this.GoalButton.UseVisualStyleBackColor = false;
            this.GoalButton.Click += new System.EventHandler(this.goalToolStripMenuItem_Click);
            // 
            // WallButton
            // 
            this.WallButton.BackColor = System.Drawing.Color.Red;
            this.WallButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.WallButton.Location = new System.Drawing.Point(0, 153);
            this.WallButton.Name = "WallButton";
            this.WallButton.Size = new System.Drawing.Size(120, 23);
            this.WallButton.TabIndex = 7;
            this.WallButton.Text = "Стена";
            this.WallButton.UseVisualStyleBackColor = false;
            this.WallButton.Click += new System.EventHandler(this.wallsToolStripMenuItem_Click);
            // 
            // ArtifactButton
            // 
            this.ArtifactButton.BackColor = System.Drawing.Color.Red;
            this.ArtifactButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ArtifactButton.Location = new System.Drawing.Point(0, 124);
            this.ArtifactButton.Name = "ArtifactButton";
            this.ArtifactButton.Size = new System.Drawing.Size(120, 23);
            this.ArtifactButton.TabIndex = 6;
            this.ArtifactButton.Text = "Артефакт";
            this.ArtifactButton.UseVisualStyleBackColor = false;
            this.ArtifactButton.Click += new System.EventHandler(this.artifactToolStripMenuItem_Click);
            // 
            // CannonButton
            // 
            this.CannonButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.CannonButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.CannonButton.Location = new System.Drawing.Point(0, 95);
            this.CannonButton.Name = "CannonButton";
            this.CannonButton.Size = new System.Drawing.Size(120, 23);
            this.CannonButton.TabIndex = 5;
            this.CannonButton.Text = "Пушка";
            this.CannonButton.UseVisualStyleBackColor = false;
            this.CannonButton.Click += new System.EventHandler(this.cannonToolStripMenuItem_Click);
            // 
            // WizardButton
            // 
            this.WizardButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.WizardButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.WizardButton.Location = new System.Drawing.Point(0, 66);
            this.WizardButton.Name = "WizardButton";
            this.WizardButton.Size = new System.Drawing.Size(120, 23);
            this.WizardButton.TabIndex = 4;
            this.WizardButton.Text = "Маг";
            this.WizardButton.UseVisualStyleBackColor = false;
            this.WizardButton.Click += new System.EventHandler(this.wizardToolStripMenuItem_Click);
            // 
            // SoldierButton
            // 
            this.SoldierButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.SoldierButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.SoldierButton.Location = new System.Drawing.Point(0, 37);
            this.SoldierButton.Name = "SoldierButton";
            this.SoldierButton.Size = new System.Drawing.Size(120, 23);
            this.SoldierButton.TabIndex = 3;
            this.SoldierButton.Text = "Пехотинец";
            this.SoldierButton.UseVisualStyleBackColor = false;
            this.SoldierButton.Click += new System.EventHandler(this.soldierToolStripMenuItem_Click);
            // 
            // AboutPlot
            // 
            this.AboutPlot.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.AboutPlot.ForeColor = System.Drawing.SystemColors.ControlText;
            this.AboutPlot.Location = new System.Drawing.Point(0, 8);
            this.AboutPlot.Name = "AboutPlot";
            this.AboutPlot.Size = new System.Drawing.Size(120, 23);
            this.AboutPlot.TabIndex = 0;
            this.AboutPlot.Text = "О сюжете";
            this.AboutPlot.UseVisualStyleBackColor = false;
            this.AboutPlot.Click += new System.EventHandler(this.aboutPlotToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 520);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(935, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // richTextBox1
            // 
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.richTextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.richTextBox1.Location = new System.Drawing.Point(146, 27);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(636, 490);
            this.richTextBox1.TabIndex = 3;
            this.richTextBox1.Text = "";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(0, 178);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(126, 123);
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(0, 323);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(126, 122);
            this.pictureBox2.TabIndex = 5;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Location = new System.Drawing.Point(788, 28);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(137, 128);
            this.pictureBox3.TabIndex = 6;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Location = new System.Drawing.Point(788, 191);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(137, 123);
            this.pictureBox4.TabIndex = 7;
            this.pictureBox4.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 304);
            this.label1.MaximumSize = new System.Drawing.Size(120, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "label1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 448);
            this.label2.MaximumSize = new System.Drawing.Size(120, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "label2";
            // 
            // label3
            // 
            this.label3.AutoEllipsis = true;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(788, 161);
            this.label3.MaximumSize = new System.Drawing.Size(120, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "label3";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(788, 323);
            this.label4.MaximumSize = new System.Drawing.Size(120, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "label4";
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.RestartGameButton);
            this.panel1.Controls.Add(this.AboutPlot);
            this.panel1.Controls.Add(this.SaveGameButton);
            this.panel1.Controls.Add(this.SoldierButton);
            this.panel1.Controls.Add(this.ActionsButton);
            this.panel1.Controls.Add(this.WizardButton);
            this.panel1.Controls.Add(this.OrderButton);
            this.panel1.Controls.Add(this.CannonButton);
            this.panel1.Controls.Add(this.SelectionButton);
            this.panel1.Controls.Add(this.ArtifactButton);
            this.panel1.Controls.Add(this.GoalButton);
            this.panel1.Controls.Add(this.WallButton);
            this.panel1.Location = new System.Drawing.Point(0, 27);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(140, 145);
            this.panel1.TabIndex = 12;
            // 
            // FormInstruction
            // 
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(935, 542);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(951, 581);
            this.Name = "FormInstruction";
            this.Text = "Справка об игре";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void aboutPlotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "Добро пожаловать в справку об игре Siege of The Fortress! Здесь можно найти информацию о сюжете игры.\n " +
                "Siege of the Fortress - это пошаговая стратегия, в которой нужно управлять своими персонажами, чтобы штурмовать блокпост коварного Колдуна " +
                "и в итоге разрушить Таинственный Артефакт, грозящий уничтожением всего живого! Далее представлен сценарий игры.\n " +
                "Шаг 1: Начало игры\n " +
                "Игрок выбирает своих персонаей в зависимости от своих предпочтений и тактической обстановки на поле боя, размещая своих " +
                "бойцов по клеткам карты. Дополнительную информацию можно найти в пункте справки \"Выбор персонажей\".\n " +
                "Шаг 2: Сражение\n " +
                "Игрок выбирает для каждого собственного персонажа в соответствии со сформированным порядком ходов некоторое действие, которое " +
                "тот совершает следом. Дополнительную информацию можно найти в разделах \"Цель игры\", \"Порядок ходов\" и \"Действия\".\n " +
                "Шаг 3: Победа или Поражение\n " +
                "Если Игрок успеет разрушить Артефакт, то он победит! А если же его персонажи будут повержены или число доступных ходов иссякнет, то игрок проиграет... " +
                "Подробнее см. в разделе \"Цель игры\". ";

            label1.Text = "";
            label2.Text = "";
            label3.Text = "";
            label4.Text = "";
            toolStripStatusLabel1.Text = "О сюжете";
            pictureBox1.Image = null/*new Bitmap(pictureBox4.Size.Width, pictureBox4.Size.Height)*/;
            pictureBox2.Image = null/*new Bitmap(pictureBox4.Size.Width, pictureBox4.Size.Height)*/;
            pictureBox3.Image = null/*new Bitmap(pictureBox4.Size.Width, pictureBox4.Size.Height)*/;
            pictureBox4.Image = null/*new Bitmap(pictureBox4.Size.Width, pictureBox4.Size.Height)*/;

        }

        private void wizardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "Маг - это персонаж вашего и вражеского войска высокого ранга. " +
                "Он способен атаковать цели на расстоянии, невзирая на существующие препятствия. За один ход может либо " +
                "остаться на прежнем месте, либо атаковать цель, либо перейти на новое место.\n";

            richTextBox1.AppendText("\n");


            richTextBox1.SelectionColor = Color.Violet;
            richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
            richTextBox1.AppendText("Стоимость размещения на карте: 200 монет.\n");
            richTextBox1.SelectionColor = richTextBox1.ForeColor;

            richTextBox1.SelectionColor = Color.Green;
            richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
            richTextBox1.AppendText("Здоровье: 200 единиц.\n");
            richTextBox1.SelectionColor = richTextBox1.ForeColor;

            richTextBox1.SelectionColor = Color.Red;
            richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
            richTextBox1.AppendText("Сила атаки: 50 единиц.\n");
            richTextBox1.SelectionColor = richTextBox1.ForeColor;

            richTextBox1.SelectionColor = Color.Orange;
            richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
            richTextBox1.AppendText("Скорость: 122 единицы.\n");
            richTextBox1.SelectionColor = richTextBox1.ForeColor;

            richTextBox1.SelectionColor = Color.HotPink;
            richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
            richTextBox1.AppendText("Дальность передвижения: 3 клетки.\n");
            richTextBox1.SelectionColor = richTextBox1.ForeColor;

            richTextBox1.SelectionColor = Color.Silver;
            richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
            richTextBox1.AppendText("Радиус атаки: 4 клетки.\n");
            richTextBox1.SelectionColor = richTextBox1.ForeColor;

            richTextBox1.SelectionColor = Color.Blue;
            richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
            richTextBox1.AppendText("Очки за уничтожение: 200 единиц.\n");
            richTextBox1.SelectionColor = richTextBox1.ForeColor;

            richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
            richTextBox1.AppendText("Особый эффект:Смерч. При нанесении удара по вражескому персонажу накладывает на цель негативный эффект: " +
                "сила атаки цели снижается на 20 единиц (или до нуля, если сила атаки цели до применения эффекта меньше 20 единиц) " +
                "в течение 3 ходов.\n");
            richTextBox1.SelectionColor = richTextBox1.ForeColor;

            label1.Text = "Маг";
            pictureBox1.Image = new Bitmap(SiegeOfTheFortress.Properties.Resources.idle_wizard);
            label2.Text = "Ведьмак";
            pictureBox2.Image = new Bitmap(SiegeOfTheFortress.Properties.Resources.idle_enemywizard);
            label3.Text = "Эффект Смерч";
            pictureBox3.Image = new Bitmap(SiegeOfTheFortress.Properties.Resources.Swirlwind);
            label4.Text = "";
            pictureBox4.Image = null/*new Bitmap(pictureBox4.Size.Width, pictureBox4.Size.Height)*/;
            toolStripStatusLabel1.Text = "Маг/Ведьмак";
        }

        private void cannonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "Пушка - это персонаж вашего и вражеского войска высокого ранга. " +
                "Она способна наносить удары по целям на расстоянии, но только по прямой. За одно действие способна либо пропустить ход, " +
                "либо атаковать цель, либо перейти на новое место.\n";

            richTextBox1.AppendText("\n");

            richTextBox1.SelectionColor = Color.Violet;
            richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
            richTextBox1.AppendText("Стоимость размещения на карте: 150 монет.\n");
            richTextBox1.SelectionColor = richTextBox1.ForeColor;

            richTextBox1.SelectionColor = Color.Green;
            richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
            richTextBox1.AppendText("Здоровье: 240 единиц.\n");
            richTextBox1.SelectionColor = richTextBox1.ForeColor;

            richTextBox1.SelectionColor = Color.Red;
            richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
            richTextBox1.AppendText("Сила атаки: 80 единиц.\n");
            richTextBox1.SelectionColor = richTextBox1.ForeColor;

            richTextBox1.SelectionColor = Color.Orange;
            richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
            richTextBox1.AppendText("Скорость: 100 единиц.\n");
            richTextBox1.SelectionColor = richTextBox1.ForeColor;

            richTextBox1.SelectionColor = Color.HotPink;
            richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
            richTextBox1.AppendText("Дальность передвижения: 1 клетка.\n");
            richTextBox1.SelectionColor = richTextBox1.ForeColor;

            richTextBox1.SelectionColor = Color.Silver;
            richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
            richTextBox1.AppendText("Радиус атаки: 5 клеток.\n");
            richTextBox1.SelectionColor = richTextBox1.ForeColor;

            richTextBox1.SelectionColor = Color.Blue;
            richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
            richTextBox1.AppendText("Очки за уничтожение: 150 единиц.\n");
            richTextBox1.SelectionColor = richTextBox1.ForeColor;

            richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
            richTextBox1.AppendText("Бонус: наносит удвоенный урон по фортификациям.\n");
            richTextBox1.SelectionColor = richTextBox1.ForeColor;

            richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
            richTextBox1.AppendText("Особый эффект: Кровотечение. При нанесении удара по вражескому персонажу накладывает на цель негативный эффект: " +
                "цель в течение 3 ходов в начале каждого своего хода будет терять 40 единиц здоровья.\n");
            richTextBox1.SelectionColor = richTextBox1.ForeColor;

            label1.Text = "Пушка игрока";
            pictureBox1.Image = new Bitmap(SiegeOfTheFortress.Properties.Resources.cannon);
            label2.Text = "Пушка Колдуна";
            pictureBox2.Image = new Bitmap(SiegeOfTheFortress.Properties.Resources.cannonenemy);
            label3.Text = "Эффект Кровотечение";
            pictureBox3.Image = new Bitmap(SiegeOfTheFortress.Properties.Resources.Bleeding);
            label4.Text = "";
            pictureBox4.Image = null/*new Bitmap(pictureBox4.Size.Width, pictureBox4.Size.Height)*/;

            toolStripStatusLabel1.Text = "Пушка";
        }

        private void artifactToolStripMenuItem_Click(object sender, EventArgs e)
        {
            

            richTextBox1.Text = "";
            richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Regular);
            richTextBox1.AppendText("Артефакт - это особый механизм, разработанный по личному указанию Колдуна! Это ядро всей крепости, " + 
                "его сохранению посвящена организация обороны крепости. Уничтожение артефакта - это цель данной игры. Его нужно несколько раз атаковать, " +
                "чтобы уничтожить.\n");

            richTextBox1.AppendText("\n");


            richTextBox1.SelectionColor = Color.Green;
            richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
            richTextBox1.AppendText("Здоровье: 200 единиц для тестовой карты и 400 единиц для всех остальных.\n");
            richTextBox1.SelectionColor = richTextBox1.ForeColor;

            richTextBox1.SelectionColor = Color.Blue;
            richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
            richTextBox1.AppendText("Очки за уничтожение: 1000 единиц.");
            richTextBox1.SelectionColor = richTextBox1.ForeColor;


            label1.Text = "Артефакт";
            pictureBox1.Image = new Bitmap(SiegeOfTheFortress.Properties.Resources.artifact);
            label2.Text = "";
            pictureBox2.Image = null;
            label3.Text = "";
            pictureBox3.Image = null;
            label4.Text = "";
            pictureBox4.Image = null/*new Bitmap(pictureBox4.Size.Width, pictureBox4.Size.Height)*/;

            toolStripStatusLabel1.Text = "Артефакт";
        }

        private void wallsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            

            richTextBox1.Text = "";
            richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Regular);
            richTextBox1.AppendText("Стена - это фортификационное сооружение, препятствующее движению и проведению дальних атак по прямой траектории." +
                " Её необходимо несколько раз атаковать, чтобы уничтожить.\n");

            richTextBox1.AppendText("\n");

            richTextBox1.SelectionColor = Color.Green;
            richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
            richTextBox1.AppendText("Здоровье: 100 единиц для тестовой карты и 200 - для всех остальных.\n");
            richTextBox1.SelectionColor = richTextBox1.ForeColor;

            richTextBox1.SelectionColor = Color.Blue;
            richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
            richTextBox1.AppendText("Очки за уничтожение: 30 единиц.");
            richTextBox1.SelectionColor = richTextBox1.ForeColor;

            label1.Text = "Угол крепостной стены";
            pictureBox1.Image = new Bitmap(SiegeOfTheFortress.Properties.Resources.wall);
            label2.Text = "Стена";
            pictureBox2.Image = new Bitmap(SiegeOfTheFortress.Properties.Resources.wall1);
            label3.Text = "";
            pictureBox3.Image = null;
            label4.Text = "";
            pictureBox4.Image = null/*new Bitmap(pictureBox4.Size.Width, pictureBox4.Size.Height)*/;

            toolStripStatusLabel1.Text = "Стена";
        }

        private void goalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "Цель игры - уничтожить охраняемый противниками артефакт. Игроку для этого предоставляется " +
                "некоторый капитал, на который он может нанять войско, и количество ходов, в течение которых игрок должен достичь " +
                "поставленную цель. Если в процессе сражения все персонажи игрока будут повержены или количество ходов иссякнет, то " +
                "игрок проиграет. Если цель будет достигнута, то игрок выиграет. В конце игры вне зависимости от исхода битвы, " +
                "игроку будут начислены баллы, которые можно получить за общее количество уничтоженных объектов и поверженных персонажей противника, " +
                "а также за оставшееся количество ходов. На количество баллов также влияет общий успех командованием: чем выше число поверженных " +
                "ваших персонажей, тем меньше будет ваш балл по итогу.";

            label3.Text = "Цель игры - уничтожить артефакт";
            pictureBox3.Image = new Bitmap(SiegeOfTheFortress.Properties.Resources.picture1);
            label4.Text = "Число ходов";
            pictureBox4.Image = new Bitmap(SiegeOfTheFortress.Properties.Resources.picture2);
            label1.Text = "";
            pictureBox1.Image = null;
            label2.Text = "";
            pictureBox2.Image = null/*new Bitmap(pictureBox4.Size.Width, pictureBox4.Size.Height)*/;

            toolStripStatusLabel1.Text = "Цель игры";
        }

        private void selectCharsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "Первый этап игры - это выбор персонажей. Некоторые персонажи уже расставлены на поле, других предстоит выбрать и добавить самостоятельно. " +
                "Всех расставленных персонажей можно убрать с поля, при этом вы получите их полную стоимость обратно. Расставлять персонажей можно только по краям карты сверху, слева и снизу. " +
                "После расстановки можно будет начать игру. " +
                "У игрока есть ограничение на количество персонажей в отряде, заключающееся в количестве имеющихся монет. Узнать, " +
                "сколько стоит размещение каждого из персонажей, можно на странице справки соответствующего персонажа. ";

            richTextBox1.AppendText("\n");

            richTextBox1.SelectionColor = Color.Red;
            richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
            richTextBox1.AppendText("Внимание! Внимание! Внимание!\n" +
                "Характеристики уже размещенных на первой карте под названием ТЕСТ персонажей, в том числе и персонажей противника, касательно их скорости могут отличаться от " +
                "указанных в справке в целях демонстрации игровых возможностей. Характеристики персонажей, размещаемых игроком " +
                "на поле, полностью соответствуют их заявленным характеристикам.\n" +
                "Кроме того, обратите внимание на то, что некоторые карты по сюжету могут иметь уже размещенных персонажей игрока " +
                "в некоторых частях карты, которых тем не менее игрок может убрать. Однако игрок не сможет расставить каки-либо персонажей внитри карты, а не на ее границе, " +
                "что стоит учитывать при планировании стратегии сражения.");
            richTextBox1.SelectionColor = richTextBox1.ForeColor;

            label1.Text = "Выбор персонажей";
            pictureBox1.Image = new Bitmap(SiegeOfTheFortress.Properties.Resources.picture3);
            label2.Text = "Кнопка Продолжить";
            pictureBox2.Image = new Bitmap(SiegeOfTheFortress.Properties.Resources.picture4);
            label3.Text = "Кнопка Убрать";
            pictureBox3.Image = new Bitmap(SiegeOfTheFortress.Properties.Resources.picture5);

            label4.Text = "Доступный капитал";
            pictureBox4.Image = new Bitmap(SiegeOfTheFortress.Properties.Resources.picture6);


            toolStripStatusLabel1.Text = "Выбор персонажей";
        }

        private void moveOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "Порядок совершения персонажами ходов определяется их скоростью. Так, у кого будет самая " +
                "высокая скорость, тот будет ходить чаще. При наступлении очереди персонажа игрока " +
                "будут определены все клетки, в отношении которых персонаж сможет предпринять действия: " +
                "передвинуться, атаковать цель, размещенную в данной клетке или наложить эффект на союзника. Доступные " +
                "для передвижения клетки будут отмечены зеленым цветом, клетки, в которых находятся достижимые вражеские цели, закрашены красным, " +
                "а клетки с союзниками, на которых можно воздействовать, отмечены синим.\n" +
                "Важно уточнить, что, пока ваши персонажи не окажутся в зоне видимости соперников, те не станут совершать никаких " +
                "действий и будут просто пропускать свой ход. Однако они могут начать передвигаться и атаковать, если вы разрушите хотя бы " +
                "один фрагмент стены, окружающей Артефакт, либо атакуете его непосредственно. Кроме того, противники будут атаковать и преследовать " +
                "ваших обнаруженных персонажей до тех пор, пока они не будут повержены, либо не будут повержены все противники.\n " +
                "Очередь совершения персонажами действия редставлена в виде карточек справа во время игры.";

            label1.Text = "";
            pictureBox1.Image = null;
            label2.Text = "";
            pictureBox2.Image = null;
            label3.Text = "";
            pictureBox3.Image = null;
            label4.Text = "";
            pictureBox4.Image = null;

            toolStripStatusLabel1.Text = "Порядок персонажей";
        }

        private void savingGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "Чтобы временно прервать игровой процесс, доступна функция сохранения игры. Сохранить " +
                "можно игру как на этапе выбора персонажей, так и собственно в течение сражения. " +
                "Во время сражения перед сохранением следует сначала поставить игру на паузу. Кроме того, для сохранения " +
                "игры нужно выбрать папку, в которой будут сохранены несколько файлов. Мы не рекомендуем вам удалять или каким бы то ни было " +
                "образом изменять эти файлы или их местоположение, если планируете возобновить прерванный игровой процесс.";

            label1.Text = "";
            pictureBox1.Image = null;
            label2.Text = "";
            pictureBox2.Image = null;
            label3.Text = "";
            pictureBox3.Image = null;
            label4.Text = "";
            pictureBox4.Image = null/*new Bitmap(pictureBox4.Size.Width, pictureBox4.Size.Height)*/;

            toolStripStatusLabel1.Text = "Сохранение игры";
        }

        private void restartGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "Загрузить ранее сохраненный игровой процесс можно как на этапе выбора персонажей для игры, так и на этапе " +
                "собственно сражения. Стоит отметить, что в таком случае будет продолжен оригинальный " +
                "(ранее сохраненный и уже возобновленный) игровой процесс, а прерванный сохранен не будет. Для возобновления " +
                "прерванного, но сохраненного игрового процесса нужно сначала поставить игру на паузу (если " +
                "игрок находится в стадии сражения), а после выбрать сохраненный файл. ";

            label1.Text = "";
            pictureBox1.Image = null;
            label2.Text = "";
            pictureBox2.Image = null;
            label3.Text = "";
            pictureBox3.Image = null;
            label4.Text = "";
            pictureBox4.Image = null/*new Bitmap(pictureBox4.Size.Width, pictureBox4.Size.Height)*/;

            toolStripStatusLabel1.Text = "Начать сначала";
        }

        private void soldierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "Пехотинец - это базовый персонаж вашего и вражеского войска. Это боец ближнего боя, который может только атаковать " +
                "или передвигаться по карте. За одно действие способен либо переместиться на другую точку карты, либо остаться на месте, либо атаковать " +
                "вражескую цель около себя. Если эта цель расположена в клетке, которая примыкает к области, доступной для передвижения, " +
                "либо находится в данной области, то пехотинец способен атаковать и её, при этом он будет вынужден переместиться, " +
                "а координаты его нового местоположения будут определены автоматически.\n";

            richTextBox1.AppendText("\n");


            richTextBox1.SelectionColor = Color.Violet;
            richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
            richTextBox1.AppendText("Стоимость размещения на карте: 100 монет.\n");
            richTextBox1.SelectionColor = richTextBox1.ForeColor;

            richTextBox1.SelectionColor = Color.Green;
            richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
            richTextBox1.AppendText("Здоровье: 120 единиц.\n");
            richTextBox1.SelectionColor = richTextBox1.ForeColor;

            richTextBox1.SelectionColor = Color.Red;
            richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
            richTextBox1.AppendText("Сила атаки: 40 единиц.\n");
            richTextBox1.SelectionColor = richTextBox1.ForeColor;

            richTextBox1.SelectionColor = Color.Orange;
            richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
            richTextBox1.AppendText("Скорость: 100 единиц.\n");
            richTextBox1.SelectionColor = richTextBox1.ForeColor;

            richTextBox1.SelectionColor = Color.HotPink;
            richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
            richTextBox1.AppendText("Дальность передвижения: 2 клетки.\n");
            richTextBox1.SelectionColor = richTextBox1.ForeColor;

            richTextBox1.SelectionColor = Color.Blue;
            richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
            richTextBox1.AppendText("Очки за уничтожение: 100 единиц.");
            richTextBox1.SelectionColor = richTextBox1.ForeColor;

            label1.Text = "Пехотинец игрока";
            pictureBox1.Image = new Bitmap(SiegeOfTheFortress.Properties.Resources.idle_allysoldier);
            label2.Text = "Скелет";
            pictureBox2.Image= new Bitmap(SiegeOfTheFortress.Properties.Resources.idle_enemysoldier);
            label3.Text = "";
            pictureBox3.Image = null;
            label4.Text = "";
            pictureBox4.Image = null/*new Bitmap(pictureBox4.Size.Width, pictureBox4.Size.Height)*/;

            toolStripStatusLabel1.Text = "Пехотинец/Скелет";
        }

        private void actionoptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "Для игрока, помимо совершения действий по передвижению, атаке или оказанию помощи союзникам, " +
                " осуществляемых кликом левой клавиши мыши, доступны действия по перемещению карты, в случае, если она не помещается в пространстве отображения " +
                "(посредством зажатия колесика мыши и ее перемещения), а также проркутка карточек (аналогичным способом). " +
                "Нажатием правой кнопки мыши по любому объекту на поле можно узнать характеристики данного объекта. Нажатием любой кнопки мыши за пределами " +
                "этого объекта данную справку можно убрать.\n" +
                "Значок сердца в минисправке соответствует здоровью, меч - силе атаки, молния - скорости, сапог - дальности передвижения, стрела - " +
                "дальности атаки (для персонажей дальнего боя), алмаз - очкам, а пьедестал - номеру карточки в списке карточек, соответствующей данному персонажу. " +
                "Под цветной линией расположены наложенные на данного персонажа эффекты с указанием количества ходов, в течение которых эти эффекты будут действовать. " +
                "Об этих эффектах можно узнать в разделах персонажей. ";

            label1.Text = "Минисправка";
            pictureBox1.Image = new Bitmap(SiegeOfTheFortress.Properties.Resources.Aegrotat);
            label2.Text = "";
            pictureBox2.Image = null;
            label3.Text = "";
            pictureBox3.Image = null;
            label4.Text = "";
            pictureBox4.Image = null;

            toolStripStatusLabel1.Text = "Действия";
        }
    }
}
