using System;
using System.Windows.Forms;

namespace MyProject // ეს არის შენი პროექტის ნაგულისხმევი namespace
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1()); // ეს გაუშვებს მთავარ ფორმას (Form1.cs)
        }
    }
}
