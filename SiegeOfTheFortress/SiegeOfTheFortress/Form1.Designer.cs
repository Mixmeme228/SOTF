namespace SiegeOfTheFortress
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.filemyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.restartmygameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pauseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.continueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitmyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.referenceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Startmygame = new System.Windows.Forms.Button();
            this.Deletemyobject = new System.Windows.Forms.Button();
            this.Moves = new System.Windows.Forms.Label();
            this.Countofmoves = new System.Windows.Forms.Label();
            this.MyPoints = new System.Windows.Forms.Label();
            this.Countofmypoints = new System.Windows.Forms.Label();
            this.EnemyPoints = new System.Windows.Forms.Label();
            this.Countofenemypoints = new System.Windows.Forms.Label();
            this.Money = new System.Windows.Forms.Label();
            this.Countofmoney = new System.Windows.Forms.Label();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.NewWizard = new System.Windows.Forms.Button();
            this.NewCannon = new System.Windows.Forms.Button();
            this.NewSoldier = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.filemyToolStripMenuItem,
            this.referenceToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1184, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // filemyToolStripMenuItem
            // 
            this.filemyToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.restartmygameToolStripMenuItem,
            this.pauseToolStripMenuItem,
            this.continueToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.loadToolStripMenuItem,
            this.exitmyToolStripMenuItem});
            this.filemyToolStripMenuItem.Name = "filemyToolStripMenuItem";
            this.filemyToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.filemyToolStripMenuItem.Text = "Файл";
            // 
            // restartmygameToolStripMenuItem
            // 
            this.restartmygameToolStripMenuItem.Enabled = false;
            this.restartmygameToolStripMenuItem.Name = "restartmygameToolStripMenuItem";
            this.restartmygameToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.restartmygameToolStripMenuItem.Text = "Начать сначала";
            this.restartmygameToolStripMenuItem.Click += new System.EventHandler(this.restartmygameToolStripMenuItem_Click);
            // 
            // pauseToolStripMenuItem
            // 
            this.pauseToolStripMenuItem.Enabled = false;
            this.pauseToolStripMenuItem.Name = "pauseToolStripMenuItem";
            this.pauseToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.pauseToolStripMenuItem.Text = "Пауза";
            this.pauseToolStripMenuItem.Click += new System.EventHandler(this.pauseToolStripMenuItem_Click);
            // 
            // continueToolStripMenuItem
            // 
            this.continueToolStripMenuItem.Enabled = false;
            this.continueToolStripMenuItem.Name = "continueToolStripMenuItem";
            this.continueToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.continueToolStripMenuItem.Text = "Продолжить";
            this.continueToolStripMenuItem.Click += new System.EventHandler(this.continueToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.saveToolStripMenuItem.Text = "Сохранить";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.loadToolStripMenuItem.Text = "Загрузить";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // exitmyToolStripMenuItem
            // 
            this.exitmyToolStripMenuItem.Name = "exitmyToolStripMenuItem";
            this.exitmyToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.exitmyToolStripMenuItem.Text = "Выход";
            this.exitmyToolStripMenuItem.Click += new System.EventHandler(this.exitmyToolStripMenuItem_Click);
            // 
            // referenceToolStripMenuItem
            // 
            this.referenceToolStripMenuItem.Name = "referenceToolStripMenuItem";
            this.referenceToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.referenceToolStripMenuItem.Text = "Справка";
            this.referenceToolStripMenuItem.Click += new System.EventHandler(this.referenceToolStripMenuItem_Click);
            // 
            // Startmygame
            // 
            this.Startmygame.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.Startmygame.Enabled = false;
            this.Startmygame.Location = new System.Drawing.Point(661, 267);
            this.Startmygame.Name = "Startmygame";
            this.Startmygame.Size = new System.Drawing.Size(53, 50);
            this.Startmygame.TabIndex = 1;
            this.Startmygame.Text = "Начать";
            this.Startmygame.UseVisualStyleBackColor = false;
            this.Startmygame.Visible = false;
            this.Startmygame.Click += new System.EventHandler(this.Startmygame_Click);
            // 
            // Deletemyobject
            // 
            this.Deletemyobject.BackColor = System.Drawing.Color.Red;
            this.Deletemyobject.Enabled = false;
            this.Deletemyobject.Location = new System.Drawing.Point(726, 267);
            this.Deletemyobject.Name = "Deletemyobject";
            this.Deletemyobject.Size = new System.Drawing.Size(62, 50);
            this.Deletemyobject.TabIndex = 2;
            this.Deletemyobject.Text = "Убрать";
            this.Deletemyobject.UseVisualStyleBackColor = false;
            this.Deletemyobject.Visible = false;
            this.Deletemyobject.Click += new System.EventHandler(this.Deletemyobject_Click);
            // 
            // Moves
            // 
            this.Moves.AutoSize = true;
            this.Moves.BackColor = System.Drawing.Color.Salmon;
            this.Moves.Enabled = false;
            this.Moves.Location = new System.Drawing.Point(1082, 24);
            this.Moves.Name = "Moves";
            this.Moves.Size = new System.Drawing.Size(34, 13);
            this.Moves.TabIndex = 6;
            this.Moves.Text = "Ходы";
            this.Moves.Visible = false;
            // 
            // Countofmoves
            // 
            this.Countofmoves.AutoSize = true;
            this.Countofmoves.BackColor = System.Drawing.Color.Salmon;
            this.Countofmoves.Enabled = false;
            this.Countofmoves.Location = new System.Drawing.Point(1137, 24);
            this.Countofmoves.Name = "Countofmoves";
            this.Countofmoves.Size = new System.Drawing.Size(35, 13);
            this.Countofmoves.TabIndex = 7;
            this.Countofmoves.Text = "label2";
            this.Countofmoves.Visible = false;
            // 
            // MyPoints
            // 
            this.MyPoints.AutoSize = true;
            this.MyPoints.Enabled = false;
            this.MyPoints.Location = new System.Drawing.Point(25, 394);
            this.MyPoints.Name = "MyPoints";
            this.MyPoints.Size = new System.Drawing.Size(60, 13);
            this.MyPoints.TabIndex = 8;
            this.MyPoints.Text = "Ваши очки";
            this.MyPoints.Visible = false;
            // 
            // Countofmypoints
            // 
            this.Countofmypoints.AutoSize = true;
            this.Countofmypoints.Enabled = false;
            this.Countofmypoints.Location = new System.Drawing.Point(101, 394);
            this.Countofmypoints.Name = "Countofmypoints";
            this.Countofmypoints.Size = new System.Drawing.Size(13, 13);
            this.Countofmypoints.TabIndex = 9;
            this.Countofmypoints.Text = "0";
            this.Countofmypoints.Visible = false;
            // 
            // EnemyPoints
            // 
            this.EnemyPoints.AutoSize = true;
            this.EnemyPoints.Enabled = false;
            this.EnemyPoints.Location = new System.Drawing.Point(159, 392);
            this.EnemyPoints.Name = "EnemyPoints";
            this.EnemyPoints.Size = new System.Drawing.Size(64, 13);
            this.EnemyPoints.TabIndex = 10;
            this.EnemyPoints.Text = "Очки врага";
            this.EnemyPoints.Visible = false;
            // 
            // Countofenemypoints
            // 
            this.Countofenemypoints.AutoSize = true;
            this.Countofenemypoints.Enabled = false;
            this.Countofenemypoints.Location = new System.Drawing.Point(261, 392);
            this.Countofenemypoints.Name = "Countofenemypoints";
            this.Countofenemypoints.Size = new System.Drawing.Size(13, 13);
            this.Countofenemypoints.TabIndex = 11;
            this.Countofenemypoints.Text = "0";
            this.Countofenemypoints.Visible = false;
            // 
            // Money
            // 
            this.Money.AutoSize = true;
            this.Money.Enabled = false;
            this.Money.Location = new System.Drawing.Point(374, 392);
            this.Money.Name = "Money";
            this.Money.Size = new System.Drawing.Size(47, 13);
            this.Money.TabIndex = 12;
            this.Money.Text = "Монеты";
            this.Money.Visible = false;
            // 
            // Countofmoney
            // 
            this.Countofmoney.AutoSize = true;
            this.Countofmoney.Enabled = false;
            this.Countofmoney.Location = new System.Drawing.Point(455, 392);
            this.Countofmoney.Name = "Countofmoney";
            this.Countofmoney.Size = new System.Drawing.Size(13, 13);
            this.Countofmoney.TabIndex = 13;
            this.Countofmoney.Text = "0";
            this.Countofmoney.Visible = false;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // NewWizard
            // 
            this.NewWizard.BackgroundImage = global::SiegeOfTheFortress.Properties.Resources.idle_wizard;
            this.NewWizard.Enabled = false;
            this.NewWizard.Location = new System.Drawing.Point(662, 202);
            this.NewWizard.Name = "NewWizard";
            this.NewWizard.Size = new System.Drawing.Size(52, 48);
            this.NewWizard.TabIndex = 5;
            this.NewWizard.UseVisualStyleBackColor = true;
            this.NewWizard.Visible = false;
            this.NewWizard.Click += new System.EventHandler(this.NewWizard_Click);
            // 
            // NewCannon
            // 
            this.NewCannon.BackgroundImage = global::SiegeOfTheFortress.Properties.Resources.cannon;
            this.NewCannon.Enabled = false;
            this.NewCannon.Location = new System.Drawing.Point(661, 139);
            this.NewCannon.Name = "NewCannon";
            this.NewCannon.Size = new System.Drawing.Size(53, 48);
            this.NewCannon.TabIndex = 4;
            this.NewCannon.UseVisualStyleBackColor = true;
            this.NewCannon.Visible = false;
            this.NewCannon.Click += new System.EventHandler(this.NewCannon_Click);
            // 
            // NewSoldier
            // 
            this.NewSoldier.BackgroundImage = global::SiegeOfTheFortress.Properties.Resources.idle_allysoldier;
            this.NewSoldier.Enabled = false;
            this.NewSoldier.Location = new System.Drawing.Point(661, 72);
            this.NewSoldier.Name = "NewSoldier";
            this.NewSoldier.Size = new System.Drawing.Size(53, 51);
            this.NewSoldier.TabIndex = 3;
            this.NewSoldier.UseVisualStyleBackColor = true;
            this.NewSoldier.Visible = false;
            this.NewSoldier.Click += new System.EventHandler(this.NewSoldier_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 691);
            this.Controls.Add(this.Countofmoney);
            this.Controls.Add(this.Money);
            this.Controls.Add(this.Countofenemypoints);
            this.Controls.Add(this.EnemyPoints);
            this.Controls.Add(this.Countofmypoints);
            this.Controls.Add(this.MyPoints);
            this.Controls.Add(this.Countofmoves);
            this.Controls.Add(this.Moves);
            this.Controls.Add(this.NewWizard);
            this.Controls.Add(this.NewCannon);
            this.Controls.Add(this.NewSoldier);
            this.Controls.Add(this.Deletemyobject);
            this.Controls.Add(this.Startmygame);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Siege of The Fortress";
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem filemyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitmyToolStripMenuItem;
        private System.Windows.Forms.Button Startmygame;
        private System.Windows.Forms.Button Deletemyobject;
        private System.Windows.Forms.Button NewSoldier;
        private System.Windows.Forms.Button NewCannon;
        private System.Windows.Forms.Button NewWizard;
        private System.Windows.Forms.Label Moves;
        private System.Windows.Forms.Label Countofmoves;
        private System.Windows.Forms.Label MyPoints;
        private System.Windows.Forms.Label Countofmypoints;
        private System.Windows.Forms.Label EnemyPoints;
        private System.Windows.Forms.Label Countofenemypoints;
        private System.Windows.Forms.Label Money;
        private System.Windows.Forms.Label Countofmoney;
        private System.Windows.Forms.ToolStripMenuItem pauseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem continueToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem restartmygameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem referenceToolStripMenuItem;
    }
}

