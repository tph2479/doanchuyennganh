using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace connect6
{
    /// <summary>
    /// nhắc nhở màu đỏ
    /// </summary>
    class RedHintPiece : Piece
    {
        /// <summary>
        /// Đặt giá trị ban đầu liên quan đến mẫu phụ lời nhắc
        /// </summary>
        /// <param name="x">Tọa độ x của quân cờ cần đặt</param>
        /// <param name="y">Tọa độ y của quân cờ cần đặt</param>
        public RedHintPiece(int x, int y) : base(x, y, true)
        {
            //Đọc hình ảnh dấu nhắc màu đỏ
            this.Image = Properties.Resources.red_point;
        }

        /// <summary>
        /// Nói với danh mục phụ huynh rằng đứa trẻ này có màu đỏ
        /// </summary>
        /// <returns>màu đỏ</returns>
        public override PieceType GetPieceType()
        {
            return PieceType.RED;
        }
    }
}
