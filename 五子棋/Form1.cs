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
        /// Đối tượng quản lý trò chơi
        /// </summary>
        private GameManagement game = new GameManagement();

        /// <summary>
        /// Số lượng đối tượng trên biểu mẫu trong màn hình ban đầu
        /// </summary>
        private int ObjectCount = 4;

        /// <summary>
        /// Ghi lại chỉ số tồn tại của quân cờ hiện tại
        /// </summary>
        private int PieceIndex = 0;

        /// <summary>
        /// Thời gian bắn
        /// </summary>
        private string PlayerTime = "30";

        /// <summary>
        /// Bạn có tham gia trò chơi không?
        /// </summary>
        private bool IsOnGame = true;

        //Lưu lại mọi nước đi trong game để có thể xem lại sau
        private List<Piece> StoringForReviewGame = new List<Piece>();

        //Các bước để xem xét là gì?
        private int Step = 0;

        /// <summary>
        /// Khởi tạo màn hình
        /// </summary>
        public Form1()
        {
            InitializeComponent();

            //Chỉ số của quân cờ phải được cộng thêm số đối tượng khởi tạo hình thức từ đầu.
            PieceIndex = ObjectCount;

            //Đếm ngược mỗi 1 giây
            CountingDown.Tick += new EventHandler(Counting_Down);

            //Xem lại sau mỗi 0,5 giây
            ReviewPiece.Tick += new EventHandler(Review_Piece);
        }

        /// <summary>
        /// Tạo quân cờ khi nhấn chuột
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">落下座標</param>
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            //Nếu vị trí hiện tại của con trỏ có thể được di chuyển, tọa độ thực của việc di chuyển sẽ được trả về, nếu không sẽ trả về null.
            Piece piece = game.IsPlaceAPiece(e.X, e.Y);

            if (piece != null)
            {
                if (game.IsFirst)
                {
                    //Bước đầu tiên trong trò chơi
                    this.Controls.Add(piece);

                    //Lưu kỷ lục cờ vua
                    StoringForReviewGame.Add(piece);

                    CountingDown.Start();
                }
                else if (game.IsRedHint)
                {
                    //Quân đầu tiên trong hai quân ở bất kỳ vòng nào (quân nhắc màu đỏ)
                    this.Controls.Add(piece);

                    PieceIndex++;
                }
                else
                {
                    //Đầu tiên hãy xóa dấu nhắc màu đỏ ở bước trước
                    this.Controls.RemoveAt(PieceIndex);

                    //Sau đó thay quân cờ bằng quân màu ban đầu
                    this.Controls.Add(game.TempStorePiece);

                    
                    this.Controls.Add(piece);

                    // Lưu kỷ lục cờ vua
                    StoringForReviewGame.Add(game.TempStorePiece);
                    StoringForReviewGame.Add(piece);

                    PieceIndex++;

                    CountingDown.Stop();
                    TotalTime.Text = PlayerTime;
                    CountingDown.Start();
                }

                // Đánh giá xem có chiến thắng không
                if (game.Winner == PieceType.BLACK)
                {
                    CountingDown.Stop();

                    // Nếu dấu nhắc màu đỏ mang lại chiến thắng, thông báo chiến thắng sẽ được thông báo sau khi dấu nhắc màu đỏ biến mất.
                    if (!game.IsRedHint)
                    {
                        //Số quân cờ đen
                        int blackToalPieces = (PieceIndex / 2) + 1;

                        if (blackToalPieces <= 20)
                        {
                            MessageBox.Show("Tiêu diệt ngay lập tức của Đen\n\"Trắng bị giết trong vòng 20 nước đi\"", "Đen thắng", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else if (blackToalPieces > 20 && blackToalPieces <= 30)
                        {
                            MessageBox.Show("Đen giết \n\"Trắng chết trong vòng 30 nước đi\"", "Đen thắng", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Đen nhỉnh hơn một chút \n\"Trắng sống sót sau 30 nước đi\"", "Đen thắng", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        //Trò chơi kết thúc
                        IsOnGame = false;
                    }
                }
                else if (game.Winner == PieceType.WHITE)
                {
                    CountingDown.Stop();

                    //Số quân cờ trắng
                    int whiteToalPieces = (PieceIndex / 2) + 1;

                    if (!game.IsRedHint)
                    {
                        if (whiteToalPieces <= 20)
                        {
                            MessageBox.Show("Giết ngay lập tức\n\"Đen bị giết trong vòng 20 nước đi\"", "Trắng thắng", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else if (whiteToalPieces > 20 && whiteToalPieces <= 30)
                        {
                            MessageBox.Show("Trắng giết \n\"Đen chết trong vòng 30 nước đi\"", "Trắng thắng", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Trắng nhỉnh hơn một chút \n\"Đen sống sót sau 30 nước đi\"", "Trắng thắng", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        //Trò chơi kết thúc
                        IsOnGame = false;
                    }
                }

                if (IsOnGame)
                {
                    //Xác định ai sẽ thay thế ở vòng tiếp theo
                    game.ChangeWhoRule();
                }
            }
        }

        /// <summary>
        /// đếm ngược
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Counting_Down(object sender, EventArgs e)
        {
            if (TotalTime.Text == "0")
            {
                CountingDown.Stop();

                //Xác định bên nào đã hết thời gian chờ
                if (game.CurrentPlayer == PieceType.BLACK)
                {
                    MessageBox.Show("Đen đã hết thời gian"," Trắng thắng", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (game.CurrentPlayer == PieceType.WHITE)
                {
                    MessageBox.Show("Trắng đã hết thời gian", "Đen thắng", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                //Trò chơi kết thúc
                IsOnGame = false;
            }
            else if (TotalTime.Text != "0")
            {
                TotalTime.Text = Convert.ToString(Convert.ToInt32(TotalTime.Text) - 1);
            }
        }

        /// <summary>
        /// Chơi từng nước đi từ ván trước
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
        /// Xóa các quân cờ khỏi bàn cờ và tạo một ván cờ mới
        /// </summary>
        private void UpdatingGame()
        {
            CountingDown.Stop();
            ReviewPiece.Stop();

            // Xóa quân cờ và bắt đầu lại (đếm 0 là đối tượng đếm ngược, đếm 1 là đối tượng nhãn, đếm 2 và 3 là đối tượng nút)
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
        ///Biến con trỏ thành biểu tượng ngón tay khi di chuyển đến giao lộ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">Tọa độ hiện tại của con trỏ</param>
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            //textBox1.Text = $"x:{e.X}y:{e.Y}";

            //Xác định có chuyển đổi sang con trỏ ngón tay hay không
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
        /// Bắt đầu một trò chơi mới
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RestartGame_Click(object sender, EventArgs e)
        {
            if(StoringForReviewGame.Count != 0)
            {
                //Bảng cập nhật
                UpdatingGame();

                //Xóa vùng chứa hồ sơ chơi trò chơi
                StoringForReviewGame.Clear();

                //Trò chơi đang diễn ra
                IsOnGame = true;
            }
            else
            {
                MessageBox.Show("Đã là ván cờ mới nhất", "Tin nhắn", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }            
        }

        /// <summary>
        /// Bắt đầu xem lại phát lại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReviewLastGame_Click(object sender, EventArgs e)
        {
            //Bạn có thể xem đánh giá sau khi trò chơi kết thúc
            if (!IsOnGame)
            {
                UpdatingGame();
                ReviewPiece.Start();
            }
            else
            {
                MessageBox.Show("Trong trò chơi, không thể tiếp tục!", "Thông báo cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void TotalTime_Click(object sender, EventArgs e)
        {

        }
    }
}
