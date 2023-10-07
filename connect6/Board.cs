using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace connect6
{
    /// <summary>
    /// chessboard
    /// </summary>
    class Board
    {
        public Board board;
        public Board()
        {

        }
        public Board(Board Board)
        {
            board = Board;
        }
        /// <summary>
        /// coordinates not on point
        /// </summary>
        private static readonly Point NO_MATCH_NODE = new Point(-1, -1);
        private List<Piece> pieces = new List<Piece>();

        /// <summary>
        /// The maximum relative coordinates of chessboard points x and y are 18 (0~18)
        /// </summary>
        public static readonly int NODE_COUNT = 18;

        /// <summary>
        /// border distance of x
        /// </summary>
        private static readonly int OFFSET_X = 205;

        /// <summary>
        /// border distance of y
        /// </summary>
        private static readonly int OFFSET_Y = 102;

        /// <summary>
        /// This numerical range is the range within which moves can be made
        /// </summary>
        private static readonly int NODE_RADUIS = 13;

        /// <summary>
        /// The distance between X-axis points and points (rounded to an integer)
        /// </summary>
        private static readonly int NODE_DISTANCE_X = 38;

        /// <summary>
        /// The distance between Y-axis points (rounded to an integer)
        /// </summary>
        private static readonly int NODE_DISTANCE_Y = 38;

        /// <summary>
        /// Thông tin về vật dụng cờ lưu trữ các quân cờ hiện có trên bàn cờ
        /// </summary>
        private readonly Piece[,] Pieces = new Piece[19, 19];

        /// <summary>
        /// Lưu trữ thông tin về các quân nhắc màu đỏ trên bàn cờ
        /// </summary>
        public Piece RedHintPiece = null;

        /// <summary>
        /// Khi đánh giá sự kết nối của sáu mảnh, vị trí tọa độ tương đối của mảnh cuối cùng
        /// </summary>
        private Point LastPlacedNodePos = NO_MATCH_NODE;

        /// <summary>
        /// đạt được LastPlacedNodePos
        /// </summary>
        public Point LastPlacedNode { get { return LastPlacedNodePos; } }

        /// <summary>
        /// Tìm xem quân cờ ở điểm bàn cờ đó có màu gì
        /// </summary>
        /// <param name="nodePos_x">Tọa độ tương đối trên các điểm bàn cờ x</param>
        /// <param name="nodePos_y">Tọa độ tương đối trên các điểm bàn cờ y</param>
        /// <returns>Trả lại màu của quân cờ</returns>
        public PieceType GetPieceType(int nodePos_x, int nodePos_y)
        {
            //Đầu tiên xác định xem tọa độ tương đối có nằm trên điểm bàn cờ hay không,
            //nếu nhỏ hơn 0 hoặc vượt quá ranh giới thì sẽ được trả về trực tiếp.NON
            if (nodePos_x < 0 || nodePos_x > NODE_COUNT ||
               nodePos_y < 0 || nodePos_y > NODE_COUNT)
            {
                return PieceType.NON;
            }
            else
            {
                //Xác định xem lúc này có quân cờ hay không
                if (Pieces[nodePos_x, nodePos_y] == null)
                {
                    return PieceType.NON;
                }
                else
                {
                    //Nếu có thì trả lại màu của quân cờ.
                    return Pieces[nodePos_x, nodePos_y].GetPieceType();
                }
            }
        }

        /// <summary>
        /// Xác định xem tọa độ được trỏ bởi con trỏ có thể di chuyển được không
        /// </summary>
        /// <param name="x">Tọa độ x hiện tại của con trỏ</param>
        /// <param name="y">Tọa độ Y hiện tại của con trỏ</param>
        /// <returns>Liệu con trỏ có thay đổi thành mũi tên hay không(mũi tên => Có thể di chuyển)</returns>
        public bool CanBePlace(int x, int y)
        {
            //Tìm kiếm tốc độ tương đối của điểm hiện tại gần con trỏ nhất
            Point nodePos = FindTheCloseNode(x, y);

            //Nếu không thì trả lại false
            if (nodePos == NO_MATCH_NODE)
            {
                return false;
            }

            //Kiểm tra xem vị trí hiện tại của con trỏ có nằm trong bàn cờ không，Nếu vậy thì hãy kiểm tra
            if (nodePos.X < 19 && nodePos.Y < 19)
            {
                //Kiểm tra xem có quân cờ ở vị trí hiện tại của con trỏ hay không
                if (Pieces[nodePos.X, nodePos.Y] != null)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Đặt một quân cờ lên bàn cờ
        /// </summary>
        /// <param name="x">Tọa độ x hiện tại của con trỏ<</param>
        /// <param name="y">Tọa độ y hiện tại của con trỏ<</param>
        /// <param name="type">màu đen hoặc màu trắng</param>
        /// <returns>Đối tượng Quân cờ (thông tin liên quan đến quân cờ)</returns>
        public Piece PlaceAPiece(int x, int y, PieceType type, bool IsRedHint)
        {
            //Tìm điểm gần nhất hiện tại của con trỏ
            Point nodePos = FindTheCloseNode(x, y);

            //Nếu không thì trả lại false
            if (nodePos == NO_MATCH_NODE)
            {
                return null;
            }

            //Kiểm tra xem có quân cờ ở vị trí hiện tại của con trỏ hay không
            if (Pieces[nodePos.X, nodePos.Y] != null)
            {
                return null;
            }

            Point realFormPos = new Point();
            realFormPos.X = ConvertToFormPos(nodePos).X;
            realFormPos.Y = ConvertToFormPos(nodePos).Y;

            //Nhập điểm con trỏ sắp đi vào mảng quân cờ để ghi tại điểm bàn cờ đó có quân cờ.
            if (type == PieceType.BLACK)
            {
                if (IsRedHint)
                {
                    RedHintPiece = new RedHintPiece(realFormPos.X, realFormPos.Y);
                }

                Pieces[nodePos.X, nodePos.Y] = new BlackPiece(realFormPos.X, realFormPos.Y);
            }
            else if (type == PieceType.WHITE)
            {
                if (IsRedHint)
                {
                    RedHintPiece = new RedHintPiece(realFormPos.X, realFormPos.Y);
                }

                Pieces[nodePos.X, nodePos.Y] = new WhitePiece(realFormPos.X, realFormPos.Y);
            }

            //Ghi lại vị trí cuối cùng của quân cờ
            LastPlacedNodePos = nodePos;

            return Pieces[nodePos.X, nodePos.Y];
        }

        /// <summary>
        /// Chuyển đổi tọa độ tương đối của vị trí con trỏ hiện tại sang tọa độ thực
        /// </summary>
        /// <param name="nodePos">Tọa độ tương đối của vị trí con trỏ</param>
        /// <returns>tọa độ thực của tọa độ tương đối</returns>
        private Point ConvertToFormPos(Point nodePos)
        {
            //Hãy nhập chính xác khoảng cách giữa các điểm vào đây, nếu không tọa độ tính toán sẽ có “lỗi rất lớn”"
            double node_Distance_x = 37.611;
            double node_Distance_y = 37.666;
            Point pieceRealFormPos = new Point();
            pieceRealFormPos.X = Convert.ToInt32((nodePos.X * node_Distance_x) + OFFSET_X);
            pieceRealFormPos.Y = Convert.ToInt32((nodePos.Y * node_Distance_y) + OFFSET_Y);
            return pieceRealFormPos;
        }

        internal void printAll()
        {
            for (int x = 0; x < 19; x++)
            {
                for (int y = 0; y < 19; y++)
                {
                    Console.Write(Pieces[x, y] != null ? "X" : "0");
                }
                Console.WriteLine();
            }

        }

        /// <summary>
        /// Tìm điểm con trỏ hiện đang ở gần nhất
        /// </summary>
        /// <param name="x">Tọa độ x hiện tại của con trỏ</param>
        /// <param name="y">Tọa độ Y hiện tại của con trỏ</param>
        /// <returns>Trả về tọa độ các điểm liền kề dưới dạng đối tượng điểm</returns>
        private Point FindTheCloseNode(int x, int y)
        {
            int nodeID_X = FindTheCloseNodeAlone(x, OFFSET_X, "x");
            int nodeID_Y = FindTheCloseNodeAlone(y, OFFSET_Y, "y");
            if (nodeID_X == -1)
            {
                return NO_MATCH_NODE;
            }
            if (nodeID_Y == -1)
            {
                return NO_MATCH_NODE;
            }
            return new Point(nodeID_X, nodeID_Y);
        }

        /// <summary>
        /// Tìm riêng các điểm có tọa độ x hoặc y liền kề
        /// </summary>
        /// <param name="pos">Tọa độ x hoặc y hiện tại của con trỏ</param>
        /// <param name="offset">Giới hạn của trục x hoặc y của bảng</param>
        /// <returns>Trả về tọa độ tương đối của trục x hoặc trục y của điểm liền kề</returns>
        private int FindTheCloseNodeAlone(int pos, int offset, string xORy)
        {
            //Xác định xem nên sử dụng khoảng cách giữa các điểm trên trục x hay trục y
            int NODE_DISTANCE;
            if (xORy == "x")
            {
                NODE_DISTANCE = NODE_DISTANCE_X;
            }
            else
            {
                NODE_DISTANCE = NODE_DISTANCE_Y;
            }

            //Trừ đường viền bảng
            pos -= offset;

            //Lấy thương số (biểu thị tọa độ của điểm gần bên trái hoặc phía trên con trỏ hiện tại nhất)
            int quotient = pos / NODE_DISTANCE;

            //Lấy ra phần còn lại (dùng để xác định xem con trỏ hiện tại có thể bị bỏ hay không)
            double remainder = pos % NODE_DISTANCE;

            if (pos < 0)
            {
                //-1 có nghĩa là tọa độ này không thể thấp hơn
                return -1;
            }
            else
            {
                if (remainder <= NODE_RADUIS && quotient <= NODE_COUNT)
                {
                    return quotient;
                }
                else if (remainder >= NODE_DISTANCE - NODE_RADUIS && quotient < NODE_COUNT)
                {
                    return quotient + 1;
                }
                else
                {
                    //-1 có nghĩa là tọa độ này không thể thấp hơn
                    return -1;
                }
            }
        }
        public int CountPieces(PieceType playerType)
        {
            int count = 0;

            foreach (Piece piece in pieces)
            {
                if (piece.GetPieceType() == playerType)
                {
                    count++;
                }
            }

            return count;
        }
        public int CalculateCombos(Board board, PieceType playerType)
        {
            int comboCount = 0;

            // Kiểm tra hàng ngang
            for (int row = 0; row < 19; row++)
            {
                int consecutiveCount = 0;
                for (int col = 0; col < 19; col++)
                {
                    if (board.GetPieceType(col, row) == playerType)
                    {
                        consecutiveCount++;
                        if (consecutiveCount == 6)
                        {
                            comboCount++;
                            break; // Đã tìm thấy combo, thoát vòng lặp
                        }
                    }
                    else
                    {
                        consecutiveCount = 0;
                    }
                }
            }

            // Kiểm tra hàng dọc
            for (int col = 0; col < 19; col++)
            {
                int consecutiveCount = 0;
                for (int row = 0; row < 19; row++)
                {
                    if (board.GetPieceType(col, row) == playerType)
                    {
                        consecutiveCount++;
                        if (consecutiveCount == 6)
                        {
                            comboCount++;
                            break; // Đã tìm thấy combo, thoát vòng lặp
                        }
                    }
                    else
                    {
                        consecutiveCount = 0;
                    }
                }
            }

            // Kiểm tra hàng chéo từ trái trên đến phải dưới
            for (int row = 0; row <= 19; row++)
            {
                for (int col = 0; col <= 19; col++)
                {
                    int consecutiveCount = 0;
                    for (int i = 0; i < 6; i++)
                    {
                        if (board.GetPieceType(col + i, row + i) == playerType)
                        {
                            consecutiveCount++;
                            if (consecutiveCount == 6)
                            {
                                comboCount++;
                                break; // Đã tìm thấy combo, thoát vòng lặp
                            }
                        }
                        else
                        {
                            consecutiveCount = 0;
                        }
                    }
                }
            }

            // Kiểm tra hàng chéo từ trái dưới đến phải trên
            for (int row = 5; row < 19; row++)
            {
                for (int col = 0; col <= 19; col++)
                {
                    int consecutiveCount = 0;
                    for (int i = 0; i < 6; i++)
                    {
                        if (board.GetPieceType(col + i, row - i) == playerType)
                        {
                            consecutiveCount++;
                            if (consecutiveCount == 6)
                            {
                                comboCount++;
                                break; // Đã tìm thấy combo, thoát vòng lặp
                            }
                        }
                        else
                        {
                            consecutiveCount = 0;
                        }
                    }
                }
            }

            return comboCount;
        }
        public int CalculatePosition(Board board, PieceType playerType)
        {
            int positionScore = 0;
         
            // Lặp qua các ô trên bàn cờ và tính điểm cho từng ô
            for (int x = 0; x < 19; x++)
            {
                for (int y = 0; y < 19; y++)
                {
                    int centerScore = Math.Abs(19 / 2 - x) + Math.Abs(19 / 2 - y);
                    int edgeScore = Math.Min(x, y);
                    // Kiểm tra nếu ô đó thuộc về người chơi playerType
                    if (board.GetPieceType(x, y) == playerType)
                    {
                        // Tính điểm cho vị trí này và cộng vào tổng điểm
                        int positionScoreForCell = centerScore-edgeScore;
                        positionScore += positionScoreForCell;
                    }
                }
            }

            return positionScore;
        }

    }
}
