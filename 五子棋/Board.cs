using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace 五子棋
{
    /// <summary>
    /// 棋盤
    /// </summary>
    class Board
    {
        /// <summary>
        /// 不在點上的座標
        /// </summary>
        private static readonly Point NO_MATCH_NODE = new Point(-1, -1);

        /// <summary>
        /// 棋盤點x和y相對座標最大為18(0~18)
        /// </summary>
        public static readonly int NODE_COUNT = 18;

        /// <summary>
        /// x的邊界距離
        /// </summary>
        private static readonly int OFFSET_X = 205;

        /// <summary>
        /// y的邊界距離
        /// </summary>
        private static readonly int OFFSET_Y = 102;

        /// <summary>
        /// 在此數字範圍則為可落子的範圍
        /// </summary>
        private static readonly int NODE_RADUIS = 13;

        /// <summary>
        /// X軸點與點間的距離(四捨五入取整數)
        /// </summary>
        private static readonly int NODE_DISTANCE_X = 38;

        /// <summary>
        /// Y軸點與點間的距離(四捨五入取整數)
        /// </summary>
        private static readonly int NODE_DISTANCE_Y = 38;

        /// <summary>
        /// 存放棋盤上目前有的棋子的棋子物件的資訊
        /// </summary>
        private readonly Piece[,] Pieces = new Piece[19, 19];

        /// <summary>
        /// 存放棋盤上紅色提示子的資訊
        /// </summary>
        public Piece RedHintPiece = null;

        /// <summary>
        /// 判斷六子連線時，最後一顆的相對座標位置
        /// </summary>
        private Point LastPlacedNodePos = NO_MATCH_NODE;

        /// <summary>
        /// 取得LastPlacedNodePos
        /// </summary>
        public Point LastPlacedNode { get { return LastPlacedNodePos; } }

        /// <summary>
        /// 找出該棋盤點上的棋子是什麼顏色的
        /// </summary>
        /// <param name="nodePos_x">棋盤點上的相對座標x</param>
        /// <param name="nodePos_y">棋盤點上的相對座標y</param>
        /// <returns>回傳棋子顏色</returns>
        public PieceType GetPieceType(int nodePos_x, int nodePos_y)
        {
            //先判斷該相對座標是否在棋盤點上，若小於0或超出邊界則直接回傳NON
            if (nodePos_x < 0 || nodePos_x > NODE_COUNT ||
               nodePos_y < 0 || nodePos_y > NODE_COUNT)
            {
                return PieceType.NON;
            }
            else
            {
                //判斷該點有無棋子
                if (Pieces[nodePos_x, nodePos_y] == null)
                {
                    return PieceType.NON;
                }
                else
                {
                    //有的話回傳棋子顏色
                    return Pieces[nodePos_x, nodePos_y].GetPieceType();
                }
            }
        }

        /// <summary>
        /// 判斷游標所指的座標是否可以落子
        /// </summary>
        /// <param name="x">游標目前的x座標</param>
        /// <param name="y">游標目前的Y座標</param>
        /// <returns>是否游標變為箭頭(箭頭 => 可以落子)</returns>
        public bool CanBePlace(int x, int y)
        {
            //找出游標目前最近的點的相對座標
            Point nodePos = FindTheCloseNode(x, y);

            //如果沒有的話，回傳false
            if (nodePos == NO_MATCH_NODE)
            {
                return false;
            }

            //檢查游標目前位置是否在棋盤內，若是的話才進行檢查
            if (nodePos.X < 19 && nodePos.Y < 19)
            {
                //檢查游標目前位置是否有棋子
                if (Pieces[nodePos.X, nodePos.Y] != null)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 放置一顆棋子在棋盤上
        /// </summary>
        /// <param name="x">游標目前的x座標<</param>
        /// <param name="y">游標目前的y座標<</param>
        /// <param name="type">黑棋或白棋</param>
        /// <returns>該顆棋子的物件Piece(棋子的相關資訊)</returns>
        public Piece PlaceAPiece(int x, int y, PieceType type, bool IsRedHint)
        {
            //找出游標目前最近的點
            Point nodePos = FindTheCloseNode(x, y);

            //如果沒有的話，回傳false
            if (nodePos == NO_MATCH_NODE)
            {
                return null;
            }

            //檢查游標目前位置是否有棋子
            if (Pieces[nodePos.X, nodePos.Y] != null)
            {
                return null;
            }

            Point realFormPos = new Point();
            realFormPos.X = ConvertToFormPos(nodePos).X;
            realFormPos.Y = ConvertToFormPos(nodePos).Y;

            //將游標即將要下的點輸入進二為陣列pieces，以紀錄該棋盤點已有棋子
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

            //紀錄最後下棋子的位置
            LastPlacedNodePos = nodePos;

            return Pieces[nodePos.X, nodePos.Y];
        }

        /// <summary>
        /// 將目前游標所在位置的相對座標轉為真實座標
        /// </summary>
        /// <param name="nodePos">游標所在位置的相對座標</param>
        /// <returns>該相對座標的真實座標</returns>
        private Point ConvertToFormPos(Point nodePos)
        {
            //這裡點與點之間的距離請輸入精準，否則算出來的座標會有"極大的誤差"
            double node_Distance_x = 37.611;
            double node_Distance_y = 37.666;

            Point pieceRealFormPos = new Point();
            pieceRealFormPos.X = Convert.ToInt32((nodePos.X * node_Distance_x) + OFFSET_X);
            pieceRealFormPos.Y = Convert.ToInt32((nodePos.Y * node_Distance_y) + OFFSET_Y);
            return pieceRealFormPos;
        }

        /// <summary>
        /// 尋找游標目前最靠近的點
        /// </summary>
        /// <param name="x">游標目前的x座標</param>
        /// <param name="y">游標目前的Y座標</param>
        /// <returns>以point物件的方式回傳鄰近點的座標</returns>
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
        /// 個別尋找x或y座標鄰近的點
        /// </summary>
        /// <param name="pos">游標目前的x或y座標</param>
        /// <param name="offset">該棋盤x或y軸的邊界</param>
        /// <returns>回傳鄰近點的x軸或y軸的相對座標</returns>
        private int FindTheCloseNodeAlone(int pos, int offset, string xORy)
        {
            //判斷要用x軸還是y軸的點與點間的距離
            int NODE_DISTANCE;
            if (xORy == "x")
            {
                NODE_DISTANCE = NODE_DISTANCE_X;
            }
            else
            {
                NODE_DISTANCE = NODE_DISTANCE_Y;
            }

            //減掉棋盤邊界
            pos -= offset;

            //取出商數(代表最靠近目前游標的左邊或上面的點的座標)
            int quotient = pos / NODE_DISTANCE;

            //取出餘數(用來判斷目前游標是否為可落點)
            double remainder = pos % NODE_DISTANCE;

            if (pos < 0)
            {
                //-1代表此座標不可以下
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
                    //-1代表此座標不可以下
                    return -1;
                }
            }
        }
    }
}
