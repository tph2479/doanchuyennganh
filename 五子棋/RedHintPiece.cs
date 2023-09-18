using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 五子棋
{
    /// <summary>
    /// 紅色提示子
    /// </summary>
    class RedHintPiece: Piece
    {
        /// <summary>
        /// 設定提示子圖案相關初始值
        /// </summary>
        /// <param name="x">要放置該棋子的x座標</param>
        /// <param name="y">要放置該棋子的y座標</param>
        public RedHintPiece(int x, int y) : base(x, y, true)
        {
            //讀紅色提示子的圖片
            this.Image = Properties.Resources.red_point;
        }

        /// <summary>
        /// 告訴父類別這顆子是紅色的
        /// </summary>
        /// <returns>紅色</returns>
        public override PieceType GetPieceType()
        {
            return PieceType.RED;
        }
    }
}
