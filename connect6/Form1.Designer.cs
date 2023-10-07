
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
            this.ReviewLastGame = new System.Windows.Forms.Button();
            this.ReviewPiece = new System.Windows.Forms.Timer(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnCvsP = new connect6.RJControls.RJButton();
            this.btnNewgame = new connect6.RJControls.RJButton();
            this.btnRedo = new connect6.RJControls.RJButton();
            this.btnUndo = new connect6.RJControls.RJButton();
            this.rjDropdownMenu1 = new connect6.RJControls.CustomControls.RJControls.RJDropdownMenu(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.rjDropdownMenu1.SuspendLayout();
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
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // btnCvsP
            // 
            this.btnCvsP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnCvsP.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnCvsP.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnCvsP.BorderRadius = 20;
            this.btnCvsP.BorderSize = 0;
            this.btnCvsP.FlatAppearance.BorderSize = 0;
            this.btnCvsP.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCvsP.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCvsP.ForeColor = System.Drawing.Color.White;
            this.btnCvsP.Location = new System.Drawing.Point(595, 2);
            this.btnCvsP.Name = "btnCvsP";
            this.btnCvsP.Size = new System.Drawing.Size(100, 44);
            this.btnCvsP.TabIndex = 12;
            this.btnCvsP.Text = "PvsC";
            this.btnCvsP.TextColor = System.Drawing.Color.White;
            this.btnCvsP.UseVisualStyleBackColor = false;
            // 
            // btnNewgame
            // 
            this.btnNewgame.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnNewgame.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnNewgame.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnNewgame.BorderRadius = 20;
            this.btnNewgame.BorderSize = 0;
            this.btnNewgame.FlatAppearance.BorderSize = 0;
            this.btnNewgame.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNewgame.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNewgame.ForeColor = System.Drawing.Color.White;
            this.btnNewgame.Location = new System.Drawing.Point(451, 2);
            this.btnNewgame.Name = "btnNewgame";
            this.btnNewgame.Size = new System.Drawing.Size(102, 44);
            this.btnNewgame.TabIndex = 11;
            this.btnNewgame.Text = "Restart";
            this.btnNewgame.TextColor = System.Drawing.Color.White;
            this.btnNewgame.UseVisualStyleBackColor = false;
            this.btnNewgame.Click += new System.EventHandler(this.btnNewgame_Click);
            // 
            // btnRedo
            // 
            this.btnRedo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnRedo.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnRedo.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnRedo.BorderRadius = 20;
            this.btnRedo.BorderSize = 0;
            this.btnRedo.FlatAppearance.BorderSize = 0;
            this.btnRedo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRedo.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRedo.ForeColor = System.Drawing.Color.White;
            this.btnRedo.Location = new System.Drawing.Point(331, 2);
            this.btnRedo.Name = "btnRedo";
            this.btnRedo.Size = new System.Drawing.Size(93, 44);
            this.btnRedo.TabIndex = 10;
            this.btnRedo.Text = "Redo";
            this.btnRedo.TextColor = System.Drawing.Color.White;
            this.btnRedo.UseVisualStyleBackColor = false;
            this.btnRedo.Click += new System.EventHandler(this.btnRedo_Click);
            // 
            // btnUndo
            // 
            this.btnUndo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnUndo.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnUndo.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnUndo.BorderRadius = 20;
            this.btnUndo.BorderSize = 0;
            this.btnUndo.FlatAppearance.BorderSize = 0;
            this.btnUndo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUndo.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUndo.ForeColor = System.Drawing.Color.White;
            this.btnUndo.Location = new System.Drawing.Point(221, 2);
            this.btnUndo.Name = "btnUndo";
            this.btnUndo.Size = new System.Drawing.Size(92, 44);
            this.btnUndo.TabIndex = 9;
            this.btnUndo.Text = "Undo";
            this.btnUndo.TextColor = System.Drawing.Color.White;
            this.btnUndo.UseVisualStyleBackColor = false;
            this.btnUndo.Click += new System.EventHandler(this.btnUndo_Click);
            // 
            // rjDropdownMenu1
            // 
            this.rjDropdownMenu1.IsMainMenu = false;
            this.rjDropdownMenu1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.rjDropdownMenu1.MenuItemHeight = 25;
            this.rjDropdownMenu1.MenuItemTextColor = System.Drawing.Color.Empty;
            this.rjDropdownMenu1.Name = "rjDropdownMenu1";
            this.rjDropdownMenu1.PrimaryColor = System.Drawing.Color.Empty;
            this.rjDropdownMenu1.Size = new System.Drawing.Size(181, 26);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuItem1.Text = "toolStripMenuItem1";
            // 
            // FrmGame
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.BackgroundImage = global::connect6.Properties.Resources.NewBoard;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1082, 749);
            this.Controls.Add(this.btnCvsP);
            this.Controls.Add(this.btnNewgame);
            this.Controls.Add(this.btnRedo);
            this.Controls.Add(this.btnUndo);
            this.Controls.Add(this.ReviewLastGame);
            this.Controls.Add(this.TotalTime);
            this.Controls.Add(this.pictureBox1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmGame";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PvsC";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.rjDropdownMenu1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer CountingDown;
        private System.Windows.Forms.Label TotalTime;
        private System.Windows.Forms.Timer Animation;
        private System.Windows.Forms.Button ReviewLastGame;
        private System.Windows.Forms.Timer ReviewPiece;
        private System.Windows.Forms.PictureBox pictureBox1;
        private RJControls.RJButton btnUndo;
        private RJControls.RJButton btnRedo;
        private RJControls.RJButton btnNewgame;
        private RJControls.RJButton btnCvsP;
        private RJControls.CustomControls.RJControls.RJDropdownMenu rjDropdownMenu1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
    }
}

