using System;
using System.Windows.Forms;
using FacebookWrapper;

namespace BasicFacebookFeatures
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>

        ///Link to Docs Document:
        ///https://docs.google.com/document/d/1j80W-J-jPxyMiLRleCaKGm3SwCp0JWNUNi7dSCZHTyk/edit?usp=sharing
        ///

        [STAThread]
        static void Main()
        {
            Clipboard.SetText("design.patterns20cc");
            FacebookService.s_UseForamttedToStrings = true;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());
        }
    }
}
