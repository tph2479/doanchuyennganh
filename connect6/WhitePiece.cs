using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace connect6
{
    /// <summary>
    /// Bộ mẫu quân cờ trắng
    /// </summary>
    class WhitePiece : Piece
    {
        /// <summary>
        /// Đặt giá trị ban đầu liên quan đến mẫu quân cờ
        /// </summary>
        /// <param name="x">Tọa độ x của quân cờ cần đặt</param>
        /// <param name="y">Tọa độ y của quân cờ cần đặt</param>
        public WhitePiece(int x, int y) : base(x, y, false)
        {
            //Đọc hình ảnh quân cờ trắng
            this.Image = Properties.Resources.white;
        }

        /// <summary>
        /// Nói với danh mục phụ huynh rằng hạt này có màu trắng
        /// </summary>
        /// <returns>trắng</returns>
        public override PieceType GetPieceType()
        {
            return PieceType.WHITE;
        }
    }
}
