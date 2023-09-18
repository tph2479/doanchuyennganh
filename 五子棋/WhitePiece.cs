using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 五子棋
{
    /// <summary>
    /// 設定白色棋子圖案
    /// </summary>
    class WhitePiece : Piece
    {
        /// <summary>
        /// 設定棋子圖案相關初始值
        /// </summary>
        /// <param name="x">要放置該棋子的x座標</param>
        /// <param name="y">要放置該棋子的y座標</param>
        public WhitePiece(int x, int y) : base(x, y, false)
        {            
                //讀取白棋子的圖片
                this.Image = Properties.Resources.white;
        }

        /// <summary>
        /// 告訴父類別這顆子是白色的
        /// </summary>
        /// <returns>白色</returns>
        public override PieceType GetPieceType()
        {
            return PieceType.WHITE;
        }
    }
}
