
namespace 五子棋
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.CountingDown = new System.Windows.Forms.Timer(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.TotalTime = new System.Windows.Forms.Label();
            this.Animation = new System.Windows.Forms.Timer(this.components);
            this.RestartGame = new System.Windows.Forms.Button();
            this.ReviewLastGame = new System.Windows.Forms.Button();
            this.ReviewPiece = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // CountingDown
            // 
            this.CountingDown.Interval = 1000;
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
            // TotalTime
            // 
            this.TotalTime.AutoSize = true;
            this.TotalTime.BackColor = System.Drawing.Color.Yellow;
            this.TotalTime.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.TotalTime.ForeColor = System.Drawing.Color.Red;
            this.TotalTime.Location = new System.Drawing.Point(532, 67);
            this.TotalTime.Name = "TotalTime";
            this.TotalTime.Size = new System.Drawing.Size(24, 18);
            this.TotalTime.TabIndex = 1;
            this.TotalTime.Text = "30";
            // 
            // RestartGame
            // 
            this.RestartGame.BackColor = System.Drawing.Color.Black;
            this.RestartGame.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.RestartGame.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gold;
            this.RestartGame.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RestartGame.Font = new System.Drawing.Font("標楷體", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.RestartGame.ForeColor = System.Drawing.Color.Red;
            this.RestartGame.Location = new System.Drawing.Point(584, 813);
            this.RestartGame.Name = "RestartGame";
            this.RestartGame.Size = new System.Drawing.Size(150, 50);
            this.RestartGame.TabIndex = 2;
            this.RestartGame.Text = "重新遊戲";
            this.RestartGame.UseVisualStyleBackColor = false;
            this.RestartGame.Click += new System.EventHandler(this.RestartGame_Click);
            // 
            // ReviewLastGame
            // 
            this.ReviewLastGame.BackColor = System.Drawing.Color.Black;
            this.ReviewLastGame.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.ReviewLastGame.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gold;
            this.ReviewLastGame.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ReviewLastGame.Font = new System.Drawing.Font("標楷體", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ReviewLastGame.ForeColor = System.Drawing.Color.Red;
            this.ReviewLastGame.Location = new System.Drawing.Point(360, 813);
            this.ReviewLastGame.Name = "ReviewLastGame";
            this.ReviewLastGame.Size = new System.Drawing.Size(150, 50);
            this.ReviewLastGame.TabIndex = 2;
            this.ReviewLastGame.Text = "觀看復盤";
            this.ReviewLastGame.UseVisualStyleBackColor = false;
            this.ReviewLastGame.Click += new System.EventHandler(this.ReviewLastGame_Click);
            // 
            // ReviewPiece
            // 
            this.ReviewPiece.Interval = 1000;
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.BackgroundImage = global::五子棋.Properties.Resources.NewBoard;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1078, 894);
            this.Controls.Add(this.ReviewLastGame);
            this.Controls.Add(this.RestartGame);
            this.Controls.Add(this.TotalTime);
            this.Controls.Add(this.pictureBox1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "六道茶棧";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer CountingDown;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label TotalTime;
        private System.Windows.Forms.Timer Animation;
        private System.Windows.Forms.Button RestartGame;
        private System.Windows.Forms.Button ReviewLastGame;
        private System.Windows.Forms.Timer ReviewPiece;
    }
}

