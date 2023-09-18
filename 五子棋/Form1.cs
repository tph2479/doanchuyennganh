using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace 五子棋
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// 遊戲管理物件
        /// </summary>
        private GameManagement game = new GameManagement();

        /// <summary>
        /// 初始畫面時form上的物件個數
        /// </summary>
        private int ObjectCount = 4;

        /// <summary>
        /// 紀錄當前棋子存在第幾個index
        /// </summary>
        private int PieceIndex = 0;

        /// <summary>
        /// 出手時間
        /// </summary>
        private string PlayerTime = "30";

        /// <summary>
        /// 是否正在遊戲中
        /// </summary>
        private bool IsOnGame = true;

        //儲存遊戲中的每一步棋以讓之後可以觀看復盤
        private List<Piece> StoringForReviewGame = new List<Piece>();
        
        //復盤第幾步
        private int Step = 0;

        /// <summary>
        /// 畫面初始化
        /// </summary>
        public Form1()
        {
            InitializeComponent();

            //棋子的index要從先加上form初始化物件個數
            PieceIndex = ObjectCount;

            //每1秒計時倒數
            CountingDown.Tick += new EventHandler(Counting_Down);

            //每0.5秒復盤一子
            ReviewPiece.Tick += new EventHandler(Review_Piece);
        }

        /// <summary>
        /// 滑鼠按下時產生棋子
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">落下座標</param>
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            //游標目前位置若可以落子則回傳落子的真實座標，否則回傳null
            Piece piece = game.IsPlaceAPiece(e.X, e.Y);

            if (piece != null)
            {
                if (game.IsFirst)
                {
                    //開局第一步
                    this.Controls.Add(piece);

                    //儲存下棋紀錄
                    StoringForReviewGame.Add(piece);

                    CountingDown.Start();
                }
                else if (game.IsRedHint)
                {
                    //任一回合兩顆棋子中的第一顆(紅色提示子)
                    this.Controls.Add(piece);

                    PieceIndex++;
                }
                else
                {
                    //先將前一步紅色提示子刪除
                    this.Controls.RemoveAt(PieceIndex);

                    //再將棋替換為原先顏色的棋子
                    this.Controls.Add(game.TempStorePiece);

                    //第二顆子
                    this.Controls.Add(piece);

                    //儲存下棋紀錄
                    StoringForReviewGame.Add(game.TempStorePiece);
                    StoringForReviewGame.Add(piece);

                    PieceIndex++;

                    CountingDown.Stop();
                    TotalTime.Text = PlayerTime;
                    CountingDown.Start();
                }

                //判斷是否勝利
                if (game.Winner == PieceType.BLACK)
                {
                    CountingDown.Stop();

                    //若紅色提示子造成獲勝，需等待紅色提示子不見時才會宣布獲勝訊息
                    if (!game.IsRedHint)
                    {
                        //黑方棋子種數
                        int blackToalPieces = (PieceIndex / 2) + 1;

                        if (blackToalPieces <= 20)
                        {
                            MessageBox.Show("黑方瞬殺 \n\"20步內白方陣亡\"", "黑方勝利", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else if (blackToalPieces > 20 && blackToalPieces <= 30)
                        {
                            MessageBox.Show("黑方擊殺 \n\"30步內白方陣亡\"", "黑方勝利", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("黑方略勝一籌 \n\"白方撐過30步\"", "黑方勝利", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        //遊戲結束
                        IsOnGame = false;
                    }
                }
                else if (game.Winner == PieceType.WHITE)
                {
                    CountingDown.Stop();

                    //白方棋子種數
                    int whiteToalPieces = (PieceIndex / 2) + 1;

                    if (!game.IsRedHint)
                    {
                        if (whiteToalPieces <= 20)
                        {
                            MessageBox.Show("白方瞬殺 \n\"20步內黑方陣亡\"", "白方勝利", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else if (whiteToalPieces > 20 && whiteToalPieces <= 30)
                        {
                            MessageBox.Show("白方擊殺 \n\"30步內黑方陣亡\"", "白方勝利", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("白方略勝一籌 \n\"黑方撐過30步\"", "白方勝利", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        //遊戲結束
                        IsOnGame = false;
                    }
                }

                if (IsOnGame)
                {
                    //判斷下一輪要換誰下
                    game.ChangeWhoRule();
                }
            }
        }

        /// <summary>
        /// 倒數計時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Counting_Down(object sender, EventArgs e)
        {
            if (TotalTime.Text == "0")
            {
                CountingDown.Stop();

                //判斷哪一方超時
                if (game.CurrentPlayer == PieceType.BLACK)
                {
                    MessageBox.Show("黑方超時", "白方勝利", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (game.CurrentPlayer == PieceType.WHITE)
                {
                    MessageBox.Show("白方超時", "黑方勝利", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                //遊戲結束
                IsOnGame = false;
            }
            else if (TotalTime.Text != "0")
            {
                TotalTime.Text = Convert.ToString(Convert.ToInt32(TotalTime.Text) - 1);
            }
        }

        /// <summary>
        /// 播放上一盤的每步棋子
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Review_Piece(object sender, EventArgs e)
        {
            if (Step == 0)
            {
                this.Controls.Add(StoringForReviewGame[Step]);
                Step++;
            }
            else if (Step < StoringForReviewGame.Count)
            {
                this.Controls.Add(StoringForReviewGame[Step]);
                this.Controls.Add(StoringForReviewGame[Step+1]);
                Step+=2;
                PieceIndex += 2;
            }
            else
            {
                ReviewPiece.Stop();
            }
        }

        /// <summary>
        /// 刪除棋盤棋子並重新產一盤新棋局
        /// </summary>
        private void UpdatingGame()
        {
            CountingDown.Stop();
            ReviewPiece.Stop();

            //清空棋子重新開始(count 0 是倒數計時物件 count 1 是label物件 count 2 3是button物件)
            int count = ObjectCount;
            while (count <= PieceIndex)
            {
                this.Controls.RemoveAt(4);
                count++;
            }

            game = new GameManagement();
            TotalTime.Text = PlayerTime;
            PieceIndex = ObjectCount;
            Step = 0;
        }

        /// <summary>
        /// 當游標移動到交叉點時將游標轉為手指符號
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">游標目前座標</param>
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            //textBox1.Text = $"x:{e.X}y:{e.Y}";

            //判斷是否轉為手指游標
            if (game.CanBePlace(e.X, e.Y))
            {
                this.Cursor = Cursors.Hand;
            }
            else
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// 開啟新的一局
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RestartGame_Click(object sender, EventArgs e)
        {
            if(StoringForReviewGame.Count != 0)
            {
                //更新棋盤
                UpdatingGame();

                //清空儲存遊戲下棋紀錄的容器
                StoringForReviewGame.Clear();

                //遊戲進行中
                IsOnGame = true;
            }
            else
            {
                MessageBox.Show("已為最新棋局", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }            
        }

        /// <summary>
        /// 開始重播復盤
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReviewLastGame_Click(object sender, EventArgs e)
        {
            //遊戲結束才能觀看復盤
            if (!IsOnGame)
            {
                UpdatingGame();
                ReviewPiece.Start();
            }
            else
            {
                MessageBox.Show("正在遊戲中，無法復盤!", "警告訊息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
