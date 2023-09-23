using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace connect6
{
    /// <summary>
    /// Quân cờ màu đen
    /// </summary>
    class BlackPiece : Piece
    {
        /// <summary>
        /// Đặt giá trị mặc định liên quan đến cờ đen
        /// </summary>
        /// <param name="x">Tọa độ x của quân cờ cần đặt</param>
        /// <param name="y">Tọa độ y của quân cờ cần đặt</param>
        public BlackPiece(int x, int y) : base(x, y, false)
        {
            //Đọc hình ảnh quân cờ đen
            this.Image = Properties.Resources.black;
        }

        /// <summary>
        ///Báo quân này màu đen
        /// </summary>
        /// <returns>Màu đen</returns>
        public override PieceType GetPieceType()
        {
            return PieceType.BLACK;
        }
    }
}
