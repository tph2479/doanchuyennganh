using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace connect6
{
    /// <summary>
    /// Piece
    /// </summary>
    abstract class Piece : PictureBox
    {
        //độ dài cạnh của quân cờ
        private static int IMAGE_WIDTH ;

        /// <summary>
        /// Các thiết lập cơ bản của quân cờ
        /// </summary>
        /// <param name="x">Tọa độ x khi nhấn con trỏ chuột</param>
        /// <param name="y">Tọa độ y khi nhấn con trỏ chuột</param>
        public Piece(int x, int y, bool IsRedHint)
        {
            //Thay đổi kích thước quân cờ
            if (IsRedHint)
            {
                // Kích thước phụ của dấu nhắc màu đỏ
                IMAGE_WIDTH = 26;
            }
            else
            {
                //Kích thước Othello
                IMAGE_WIDTH = 36;
            }

            // Làm màu nền của quân cờ trong suốt
            this.BackColor = Color.Transparent;

            //Cho hình quân cờ được phóng to hoặc thu nhỏ theo tỷ lệ ban đầu
            this.SizeMode = PictureBoxSizeMode.StretchImage;

            //Vị trí tọa độ nơi quân cờ xuất hiện
            this.Location =  new Point(x - IMAGE_WIDTH/2, y - IMAGE_WIDTH / 2);

            //kích thước quân cờ
            this.Size = new Size(IMAGE_WIDTH, IMAGE_WIDTH);
        }

        /// <summary>
        /// Hỏi danh mục con xem tác phẩm này có màu đen hay trắng
        /// </summary>
        /// <returns></returns>
        public abstract PieceType GetPieceType();
    }
}
