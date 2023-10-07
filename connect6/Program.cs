using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace connect6
{
    static class Program
    {
        /// <summary>
        /// Màn Hình Chính  của ứng dụng.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmWait());
            //Tree tree = new Tree();
            //tree.Branch.Add(new Tree(1));

            //tree.Branch.Last().Branch.Add(new Tree(2));    tree.Print(tree);
        }
    }
}
