using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace 五子棋
{
    /// <summary>
    /// 設定黑色棋子圖案
    /// </summary>
    class BlackPiece : Piece
    {
        /// <summary>
        /// 設定棋子圖案相關初始值
        /// </summary>
        /// <param name="x">要放置該棋子的x座標</param>
        /// <param name="y">要放置該棋子的y座標</param>
        public BlackPiece(int x, int y) : base(x, y, false)
        {          
                //讀取黑棋子的圖片
                this.Image = Properties.Resources.black;
        }

        /// <summary>
        /// 告訴父類別這顆子是黑色的
        /// </summary>
        /// <returns>黑色</returns>
        public override PieceType GetPieceType()
        {
            return PieceType.BLACK;
        }
    }
}
