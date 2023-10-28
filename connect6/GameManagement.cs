using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace connect6
{
    /// <summary>
    /// Đối tượng quản lý trò chơi
    /// </summary>
    
    class GameManagement
    {
        /// <summary>
        /// Đối tượng bàn cờ
        /// </summary>
        public Board Board = new Board();

        /// <summary>
        /// 1 tượng trưng cho bước đầu tiên của Đen hoặc Trắng / 2 tượng trưng cho bước thứ hai của Đen hoặc Trắng
        /// </summary>
        private int NumStep = 1;

        /// <summary>
        /// Xác định xem đây có phải là bước đầu tiên
        /// </summary>
        public bool IsFirst = true;

        /// <summary>
        /// Người Chơi hiện tại
        /// </summary>
        public PieceType CurrentPlayer = PieceType.BLACK;

        public Piece TempStorePiece = null;
        /// <summary>
        /// Liệu quân cờ do người chơi chơi có trở thành quân nhắc nhở hay không
        /// </summary>
        public bool IsRedHint = false;
        /// <summary>
        /// Quân nào thắng
        /// </summary>
        private PieceType WinnerType = PieceType.NON;

        /// <summary>
        /// Hãy để người ngoài có được thông tin chiến thắng trò chơi
        /// </summary>
        public PieceType Winner { get { return WinnerType; } }

        public TreeNode tree = new TreeNode(null, 0, null);

        /// <summary>
        /// Xác định xem vị trí con trỏ có nằm trong phạm vi có thể thả hay không
        /// </summary>
        /// <param name="x">Tọa độ x hiện tại của con trỏ</param>
        /// <param name="y">Tọa độ y hiện tại của con trỏ</param>
        /// <returns>Có hoặc không</returns>
        public bool CanBePlace(int x, int y)
        {
            return Board.CanBePlace(x, y);
        }

        /// <summary>
        /// Xác định xem có thể đặt quân cờ hay không
        /// </summary>
        /// <param name="x">Tọa độ x hiện tại của con trỏ</param>
        /// <param name="y">Tọa độ y hiện tại của con trỏ</param>
        /// <param name="type">Màu của quân cờ khi đến lượt người chơi hiện tại.</param>
        /// <returns>Nếu không còn quân cờ nào khác thì tọa độ thực của thế cờ sẽ được trả về.，Nếu không thì quay lại null</returns>
        public Piece IsPlaceAPiece(int x, int y)
        {
            Piece piece = Board.PlaceAPiece(x, y, CurrentPlayer, IsRedHint);
            
            if (piece != null)
            {
                if (IsRedHint)
                {
                    //Lưu tạm quân cờ gốc cho nước đi này
                    TempStorePiece = piece;

                    CheckWinner();

                    //Trả lại dấu nhắc màu đỏ
                    return Board.RedHintPiece;
                }
                CheckWinner();
                return piece;
            }
            return null;
        }

        /// <summary>
        /// Trao đổi người chơi
        /// </summary>
        public void ChangeWhoRule()
        {
            //Quy tắc sáu tiếng nổ
            if (IsFirst)
            {
                IsFirst = false;
                // CurrentPlayer = PieceType.WHITE;
                IsRedHint = true;
            }
            else if (CurrentPlayer == PieceType.BLACK)
            {
                if (NumStep == 1)
                {
                    NumStep++;
                    IsRedHint = false;
                }
                else if (NumStep == 2)
                {
                    NumStep = 1;
                    //CurrentPlayer = PieceType.WHITE;
                    IsRedHint = true;
                }
            }
            //else if (CurrentPlayer == PieceType.WHITE)
            //{
            //    if (NumStep == 1)
            //    {
            //        NumStep++;
            //        IsRedHint = false;
            //    }
            //    else if (NumStep == 2)
            //    {
            //        NumStep = 1;
            //        CurrentPlayer = PieceType.BLACK;
            //        IsRedHint = true;
            //    }
            //}
            //quy tắc
            //if(IsBlack)
            //{  
            //    IsBlack = false;
            //   return PieceType.BLACK;
            //}
            //else
            //{
            //    IsBlack = true;
            //    return PieceType.WHITE;
            //}
        }

        /// <summary>
        /// Kiểm tra xem chiến thắng
        /// </summary>
  
        private void CheckWinner()
        {
            int centerX = Board.LastPlacedNode.X;
            int centerY = Board.LastPlacedNode.Y;

            //Kiểm tra xem có sáu kết nối theo tám hướng
            for (int dir_x = -1; dir_x <= 1; dir_x++)
            {
                for (int dir_y = -1; dir_y <= 1; dir_y++)
                {
                    //Loại trừ các điểm trung tâm khỏi việc đưa vào tính toán
                    if (dir_x == 0 && dir_y == 0)
                    {
                        continue;
                    }

                    //Ghi lại số lượng quân cờ giống hệt nhau mà bạn nhìn thấy bây giờ
                    int count = 1;
                    while (count < 6)
                    {
                        int target_x = centerX + count * dir_x;
                        int target_y = centerY + count * dir_y;

                        //Kiểm tra xem các màu có giống nhau theo một hướng nhất định tính từ điểm trung tâm hay không.  
                        if (target_x < 0 || target_x > Board.NODE_COUNT ||
                            target_y < 0 || target_y > Board.NODE_COUNT ||
                            Board.GetPieceType(target_x, target_y) != CurrentPlayer)
                        {
                            //Kiểm tra xem các màu có giống nhau theo hướng ngược lại với điểm trung tâm không
                            while (count < 6)
                            {
                                int newTarget_x = target_x + (count + 1) * dir_x * (-1);
                                int newTarget_y = target_y + (count + 1) * dir_y * (-1);
                                if (target_x < 0 || target_x > Board.NODE_COUNT ||
                                    target_y < 0 || target_y > Board.NODE_COUNT ||
                                    Board.GetPieceType(newTarget_x, newTarget_y) != CurrentPlayer)
                                {
                                    //Sau khi kiểm tra hướng ngược lại của điểm trung tâm, sáu mảnh không được kết nối.
                                    break;
                                }
                                count++;
                            }

                            //Sau khi kiểm tra hướng dương và hướng âm của điểm trung tâm, sáu mảnh không được kết nối.
                            break;
                        }
                        count++;
                    }

                    //Phán quyết chiến thắng
                    if (count == 6)
                    {
                        WinnerType = CurrentPlayer;
                    }
                }
            }
        }

        internal void PrintAll()
        {
            Console.WriteLine("\nPrint them all");
            Board.printAll();
        }

        internal void TreeGenerate()
        {
            // với mỗi một ô chưa đánh sẽ tạo một bản sao của bàn cờ
            // bàn cờ đó sẽ lưu vào trong cây

            // tạo bản sao

            
            List<Piece[,]> pieces = Board.Generate();
            foreach(var piece in pieces)
            {
                TreeNode node = new TreeNode(tree, 0, piece);
                for(int i = 0; i < 19; i++)
                {
                    for (int j = 0; j < 19; j++)
                        Console.Write(piece[i, j] != null ? "X" : "_");
                    Console.WriteLine();
                }
                Console.WriteLine();
            }
            


            // CẦN PHẢI XÁC ĐỊNH CHÍNH XÁC MỖI NODE TRONG CÂY CHƯA NHỮNG THÀNH PHẦN GÌ:
            //     1.BÀN CỜ
            //     2.CÁC TRẠNG THÁI LIÊN QUAN NHƯ BƯỚC ĐI CUỐI CÙNG,...

        }
    }
}
