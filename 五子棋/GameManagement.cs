using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 五子棋
{
    /// <summary>
    /// 遊戲管理物件
    /// </summary>
    class GameManagement
    {
        /// <summary>
        /// 棋盤物件
        /// </summary>
        public Board Board = new Board();

        /// <summary>
        /// 1 代表黑或白方第一步 / 2 代表黑或白方第二步
        /// </summary>
        private int NumStep = 1;

        /// <summary>
        /// 判斷是否為第一步
        /// </summary>
        public bool IsFirst = true;

        /// <summary>
        /// 當前出手方
        /// </summary>
        public PieceType CurrentPlayer = PieceType.BLACK;

       
   
        public Piece TempStorePiece = null;
        /// <summary>
        /// 玩家所下的棋子是否變成提示子
        /// </summary>
        public bool IsRedHint = false;
        /// <summary>
        /// 哪種棋子勝利
        /// </summary>
        private PieceType WinnerType = PieceType.NON;

        /// <summary>
        /// 讓外部取得遊戲勝利資訊
        /// </summary>
        public PieceType Winner { get { return WinnerType; } }

        /// <summary>
        /// 判斷此游標位置是否是在可落子範圍
        /// </summary>
        /// <param name="x">游標目前x座標</param>
        /// <param name="y">游標目前y座標</param>
        /// <returns>是或否</returns>
        public bool CanBePlace(int x, int y)
        {
            return Board.CanBePlace(x, y);
        }

        /// <summary>
        /// 判斷是否可以放置一顆棋子
        /// </summary>
        /// <param name="x">游標目前x座標</param>
        /// <param name="y">游標目前y座標</param>
        /// <param name="type">輪到當前玩家落子的棋子顏色</param>
        /// <returns>若無其他棋子則傳回該位置的真實座標，否則回傳null</returns>
        public Piece IsPlaceAPiece(int x, int y)
        {
            Piece piece = Board.PlaceAPiece(x, y, CurrentPlayer, IsRedHint);
            
            if (piece != null)
            {
                if (IsRedHint)
                {
                    //將此步本來的棋子先暫存起來
                    TempStorePiece = piece;

                    CheckWinner();

                    //回傳紅色提示子
                    return Board.RedHintPiece;
                }
                CheckWinner();
                return piece;
            }
            return null;
        }

        /// <summary>
        /// 玩家出手交換
        /// </summary>
        public void ChangeWhoRule()
        {
            //六子棋規則
            if (IsFirst)
            {
                IsFirst = false;
                CurrentPlayer = PieceType.WHITE;
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
                    CurrentPlayer = PieceType.WHITE;
                    IsRedHint = true;
                }
            }
            else if (CurrentPlayer == PieceType.WHITE)
            {
                if (NumStep == 1)
                {
                    NumStep++;
                    IsRedHint = false;
                }
                else if (NumStep == 2)
                {
                    NumStep = 1;
                    CurrentPlayer = PieceType.BLACK;
                    IsRedHint = true;
                }
            }
            //五子棋規則
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
        /// 檢查是否勝利
        /// </summary>
        private void CheckWinner()
        {
            int centerX = Board.LastPlacedNode.X;
            int centerY = Board.LastPlacedNode.Y;

            //檢查八個方向有無連成六顆
            for (int dir_x = -1; dir_x <= 1; dir_x++)
            {
                for (int dir_y = -1; dir_y <= 1; dir_y++)
                {
                    //排除將中心點也列入計算的情況
                    if (dir_x == 0 && dir_y == 0)
                    {
                        continue;
                    }

                    //紀錄現在看到幾顆相同的棋子
                    int count = 1;
                    while (count < 6)
                    {
                        int target_x = centerX + count * dir_x;
                        int target_y = centerY + count * dir_y;

                        //往中心點的某一方向檢查顏色是否相同.   
                        if (target_x < 0 || target_x > Board.NODE_COUNT ||
                            target_y < 0 || target_y > Board.NODE_COUNT ||
                            Board.GetPieceType(target_x, target_y) != CurrentPlayer)
                        {
                            //往中心點的反方向檢查顏色是否相同
                            while (count < 6)
                            {
                                int newTarget_x = target_x + (count + 1) * dir_x * (-1);
                                int newTarget_y = target_y + (count + 1) * dir_y * (-1);
                                if (target_x < 0 || target_x > Board.NODE_COUNT ||
                                    target_y < 0 || target_y > Board.NODE_COUNT ||
                                    Board.GetPieceType(newTarget_x, newTarget_y) != CurrentPlayer)
                                {
                                    //中心點的反方向檢查完，並未連成六子
                                    break;
                                }
                                count++;
                            }

                            //中心點的正方向和反方向檢查完，並未連成六子
                            break;
                        }
                        count++;
                    }

                    //判斷勝利
                    if (count == 6)
                    {
                        WinnerType = CurrentPlayer;
                    }
                }
            }
        }
    }
}
