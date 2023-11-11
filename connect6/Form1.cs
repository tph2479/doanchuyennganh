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

namespace connect6
{
    public partial class FrmGame : Form
    {
        /// <summary>
        /// Đối tượng quản lý trò chơi
        /// </summary>
        private GameManagement game = new GameManagement();
        private Board board = new Board();
        private ComputerPlayer computerPlayer;

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
        //Lưu lại các nước đi sau khi undo
        private List<Piece> RedoMoves = new List<Piece>();
       
        //Các bước để xem xét là gì?
        private int Step = 0;

        /// <summary>
        /// Khởi tạo màn hình
        /// </summary>
        public FrmGame()
        {
            InitializeComponent();

            //Chỉ số của quân cờ phải được cộng thêm số đối tượng khởi tạo hình thức từ đầu.
            PieceIndex = ObjectCount;
            computerPlayer = new ComputerPlayer(game, board);

            //Đếm ngược mỗi 1 giây
            CountingDown.Tick += new EventHandler(Counting_Down);

            //Xem lại sau mỗi 0,5 giây
            ReviewPiece.Tick += new EventHandler(Review_Piece);
           
        }
     
            /// <summary>
            /// Tạo quân cờ khi nhấn chuột
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e">Tạo độ</param>
            private void Form1_MouseDown(object sender, MouseEventArgs e)
            {
                if (IsOnGame == false)
                {
                    return;
                }

                //Nếu vị trí hiện tại của con trỏ có thể được di chuyển, tọa độ thực của việc di chuyển sẽ được trả về, nếu không sẽ trả về null.
                Piece piece = game.IsPlaceAPiece(e.X, e.Y);
           
                if (piece != null)
                {
                this.Controls.Add(pictureBox1);
                StoringForReviewGame.Add(piece);
                //this.Controls.RemoveAt(PieceIndex);
                PieceIndex++;

                    if (game.IsFirst)
                    {
                        //Bước đầu tiên trong trò chơi
                        this.Controls.Add(piece);

                        CountingDown.Start();
                        // this.Controls.Add(TotalTime);
                        // this.Controls.RemoveAt(PieceIndex);  // Khải: Chỗ Này là để xóa Dấu nhắc đỏ ở nước vừa đi
                    }
                    else
                    {
                        this.Controls.Add(piece);
                        
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
                    game.ChangeWhoRule();

                    //if (game.CurrentPlayer == game.PC)
                    //{

                    //    Console.WriteLine("Máy đánh");
                    //    // tới lượt máy đánh 2 lần liên tiếp
                    //    // tạo 2 bước đi tốt nhất liền kề nhau

                    //    //Piece p1 = game.IsPlaceAPiece(e.X, e.Y);
                    //    //Piece p2 = game.IsPlaceAPiece(e.X, e.Y);

                    //    Piece p1 = null;

                    //    //while (p1 == null)
                    //    //{

                    //    Point nodePos = game.Board.FindTheCloseNode(new Random().Next(1, 19), new Random().Next(1, 19));
                    //    p1 = game.IsPlaceAPiece(nodePos.X, nodePos.Y);
                    //    Console.WriteLine("lặp lại lần nữa ");
                    //    this.Controls.Add(p1);
                        
                    //    //}
                        
                    //    //if (game.IsFirst == false)
                    //    //{
                    //    //    game.ChangeWhoRule();
                    //    //    Piece p2 = game.IsPlaceAPiece(new Random().Next(1, 19), new Random().Next(1, 19));
                    //    //    while (p2 == null)
                    //    //        p1 = game.IsPlaceAPiece(new Random().Next(1, 19), new Random().Next(1, 19));
                    //    //    game.ChangeWhoRule();
                    //    //}
                    //}

                    if (game.IsFirst == true)
                        game.IsFirst = false;
                }

                
            }
         
        }

        private void HandleTree(Piece piece)
        {
            // 1,lặp và in ra bàn cờ hiện tại
            // 2,tìm tất cả các khả năng (chỗ mà không có quân cờ) (mỗi nhánh là một ô trống)
            // 3,in ra ni của một nhánh bất kỳ (tức đã tìm tất cả các lá của nhánh, tức đã biết được ai thắng trên lá đó)
            // 4,in ra ni của tất cả các nhóm
            // 5,tính Ni (N = tổng tất cả ni) và wi (wi là tổng các lá có kết quả thắng)
            // 6,tính giá trị UCT và chọn nhánh (hay bước đi kế tiếp)        wi                ln(Ni)
            //                                                         UCT = -- + 1.41*sqrt(------)
            //                                                               ni                  ni

            //game.PrintAll();
            game.PlayoutGenerate();
            // bắt đầu game tạo ra nút root
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
                TotalTime.Text = Convert.ToString(Convert.ToInt32(TotalTime.Text) - 1).ToString();
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
            foreach(var piece in StoringForReviewGame)
            {  
             this.Controls.Remove(piece);
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
        private void SaveGame()
        {
           foreach(var item in StoringForReviewGame)
            {

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void TotalTime_Click(object sender, EventArgs e)
        {

        }
        //Dùng List StoringForReviewGame để truy xuất tới phần tử Cuối cùng và xóa Phần Tử cuối cùng
       
        private void guna2CircleButton2_Click(object sender, EventArgs e)
        {
            if (RedoMoves.Count > 0)
            {
                Piece redoPiece = RedoMoves.Last();
                this.Controls.Add(redoPiece);
                game.ChangeWhoRule();
                PieceIndex++;
                StoringForReviewGame.Add(redoPiece);
                RedoMoves.RemoveAt(RedoMoves.Count - 1);

            }
            else
            {
                MessageBox.Show("Không có nước đi để phục hồi.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        
        
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void btnPVsC_Click(object sender, EventArgs e)
        {

            computerPlayer.GetPossibleMoves();
            computerPlayer.MakeMove();
            int x = 19;
            int y = 19;
            Piece piece = game.IsPlaceAPiece(x, y);
            if (piece != null)
            {
                // Cập nhật trạng thái của trò chơi
                // Ví dụ: bạn có thể kiểm tra chiến thắng, thay đổi người chơi hiện tại, v.v.
                game.ChangeWhoRule();
                UpdatingGame();
                IsOnGame = true;
                // Vẽ lại bàn cờ sau nước đi tốt nhất
             }
        }

        private void btnUndo_Click(object sender, EventArgs e)
        {
            if (StoringForReviewGame.Count > 0)
            {
                Piece lastpiece = StoringForReviewGame.Last();
                this.Controls.Remove(lastpiece);
                game.ChangeWhoRule();
                PieceIndex--;
                RedoMoves.Add(lastpiece);
                StoringForReviewGame.RemoveAt(StoringForReviewGame.Count - 1);

            }
            else
            {
                MessageBox.Show("Không có nước đi để hoàn tác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnRedo_Click(object sender, EventArgs e)
        {
            if (RedoMoves.Count > 0)
            {
                Piece redoPiece = RedoMoves.Last();
                this.Controls.Add(redoPiece);
                game.ChangeWhoRule();
                PieceIndex++;
                StoringForReviewGame.Add(redoPiece);
                RedoMoves.RemoveAt(RedoMoves.Count - 1);

            }
            else
            {
                MessageBox.Show("Không có nước đi để phục hồi.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnNewgame_Click(object sender, EventArgs e)
        {
            if (StoringForReviewGame.Count != 0)
            {
                Piece restartpiece = game.IsPlaceAPiece(0, 0);
                if (restartpiece != null)
                {
                    StoringForReviewGame.Add(restartpiece);
                }
                UpdatingGame();
                StoringForReviewGame.Clear();
                IsOnGame = true;

            }
            else
            {
                MessageBox.Show("Đã là ván cờ mới nhất", "Tin nhắn", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
