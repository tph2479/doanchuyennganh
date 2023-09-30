
namespace connect6
{
    partial class FrmGame
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmGame));
            this.CountingDown = new System.Windows.Forms.Timer(this.components);
            this.TotalTime = new System.Windows.Forms.Label();
            this.Animation = new System.Windows.Forms.Timer(this.components);
            this.RestartGame = new System.Windows.Forms.Button();
            this.ReviewLastGame = new System.Windows.Forms.Button();
            this.ReviewPiece = new System.Windows.Forms.Timer(this.components);
            this.buttonUndo = new Guna.UI2.WinForms.Guna2CircleButton();
            this.buttonRedo = new Guna.UI2.WinForms.Guna2CircleButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // CountingDown
            // 
            this.CountingDown.Interval = 1000;
            // 
            // TotalTime
            // 
            this.TotalTime.AutoSize = true;
            this.TotalTime.BackColor = System.Drawing.Color.Yellow;
            this.TotalTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.TotalTime.ForeColor = System.Drawing.Color.Red;
            this.TotalTime.Location = new System.Drawing.Point(532, 67);
            this.TotalTime.Name = "TotalTime";
            this.TotalTime.Size = new System.Drawing.Size(21, 15);
            this.TotalTime.TabIndex = 1;
            this.TotalTime.Text = "30";
            this.TotalTime.Click += new System.EventHandler(this.TotalTime_Click);
            // 
            // RestartGame
            // 
            this.RestartGame.BackColor = System.Drawing.Color.Black;
            this.RestartGame.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.RestartGame.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gold;
            this.RestartGame.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RestartGame.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.RestartGame.ForeColor = System.Drawing.Color.Red;
            this.RestartGame.Location = new System.Drawing.Point(584, 813);
            this.RestartGame.Name = "RestartGame";
            this.RestartGame.Size = new System.Drawing.Size(150, 50);
            this.RestartGame.TabIndex = 2;
            this.RestartGame.Text = "Restart";
            this.RestartGame.UseVisualStyleBackColor = false;
            this.RestartGame.Click += new System.EventHandler(this.RestartGame_Click);
            // 
            // ReviewLastGame
            // 
            this.ReviewLastGame.BackColor = System.Drawing.Color.Black;
            this.ReviewLastGame.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.ReviewLastGame.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gold;
            this.ReviewLastGame.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ReviewLastGame.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ReviewLastGame.ForeColor = System.Drawing.Color.Red;
            this.ReviewLastGame.Location = new System.Drawing.Point(360, 813);
            this.ReviewLastGame.Name = "ReviewLastGame";
            this.ReviewLastGame.Size = new System.Drawing.Size(150, 50);
            this.ReviewLastGame.TabIndex = 2;
            this.ReviewLastGame.Text = "Review";
            this.ReviewLastGame.UseVisualStyleBackColor = false;
            this.ReviewLastGame.Click += new System.EventHandler(this.ReviewLastGame_Click);
            // 
            // ReviewPiece
            // 
            this.ReviewPiece.Interval = 1000;
            // 
            // buttonUndo
            // 
            this.buttonUndo.BackColor = System.Drawing.Color.DimGray;
            this.buttonUndo.BackgroundImage = global::connect6.Properties.Resources.BeautifulScene;
            this.buttonUndo.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.buttonUndo.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.buttonUndo.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.buttonUndo.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.buttonUndo.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.buttonUndo.Font = new System.Drawing.Font("Sitka Text", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonUndo.ForeColor = System.Drawing.Color.White;
            this.buttonUndo.Location = new System.Drawing.Point(3, 2);
            this.buttonUndo.Name = "buttonUndo";
            this.buttonUndo.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.buttonUndo.Size = new System.Drawing.Size(133, 44);
            this.buttonUndo.TabIndex = 5;
            this.buttonUndo.Text = "Undo";
            this.buttonUndo.Click += new System.EventHandler(this.guna2CircleButton1_Click);
            // 
            // buttonRedo
            // 
            this.buttonRedo.BackgroundImage = global::connect6.Properties.Resources.BeautifulScene;
            this.buttonRedo.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.buttonRedo.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.buttonRedo.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.buttonRedo.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.buttonRedo.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.buttonRedo.Font = new System.Drawing.Font("Sitka Text", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonRedo.ForeColor = System.Drawing.Color.White;
            this.buttonRedo.Location = new System.Drawing.Point(133, 2);
            this.buttonRedo.Name = "buttonRedo";
            this.buttonRedo.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.buttonRedo.Size = new System.Drawing.Size(122, 44);
            this.buttonRedo.TabIndex = 6;
            this.buttonRedo.Text = "Redo";
            this.buttonRedo.Click += new System.EventHandler(this.guna2CircleButton2_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(200, 62);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(688, 26);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // FrmGame
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.BackgroundImage = global::connect6.Properties.Resources.NewBoard;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1078, 749);
            this.Controls.Add(this.buttonRedo);
            this.Controls.Add(this.buttonUndo);
            this.Controls.Add(this.ReviewLastGame);
            this.Controls.Add(this.RestartGame);
            this.Controls.Add(this.TotalTime);
            this.Controls.Add(this.pictureBox1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmGame";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Connect 6";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer CountingDown;
        private System.Windows.Forms.Label TotalTime;
        private System.Windows.Forms.Timer Animation;
        private System.Windows.Forms.Button RestartGame;
        private System.Windows.Forms.Button ReviewLastGame;
        private System.Windows.Forms.Timer ReviewPiece;
        private Guna.UI2.WinForms.Guna2CircleButton buttonUndo;
        private Guna.UI2.WinForms.Guna2CircleButton buttonRedo;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

