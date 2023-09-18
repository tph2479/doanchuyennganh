using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace 五子棋
{
    /// <summary>
    /// 棋子
    /// </summary>
    abstract class Piece : PictureBox
    {
        //棋子的邊長
        private static int IMAGE_WIDTH ;

        /// <summary>
        /// 棋子基本設定
        /// </summary>
        /// <param name="x">滑鼠游標點擊時的x座標</param>
        /// <param name="y">滑鼠游標點擊時的y座標</param>
        public Piece(int x, int y, bool IsRedHint)
        {
            //更改棋子大小
            if(IsRedHint)
            {
                //紅色提示子大小
                IMAGE_WIDTH = 26;
            }
            else
            {
                //黑白棋大小
                IMAGE_WIDTH = 36;
            }

            //讓棋子背景顏色為透明
            this.BackColor = Color.Transparent;

            //讓棋子圖片依原比例來放大或縮小
            this.SizeMode = PictureBoxSizeMode.StretchImage;

            //棋子出現的位置座標
            this.Location =  new Point(x - IMAGE_WIDTH/2, y - IMAGE_WIDTH / 2);

            //棋子大小
            this.Size = new Size(IMAGE_WIDTH, IMAGE_WIDTH);
        }

        /// <summary>
        /// 問子類別這顆棋子是黑色還是白色
        /// </summary>
        /// <returns></returns>
        public abstract PieceType GetPieceType();
    }
}
