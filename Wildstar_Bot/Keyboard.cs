using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Windows.Input;
using System.Threading;

namespace Wildstar_Bot
{
    public class Keyboard
    {
        #region DllImport
        [DllImport("user32", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern IntPtr GetWindow(IntPtr hwnd, int wFlag);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string className, string windowTitle);

        [DllImport("user32.dll")]
        static extern bool PostMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);

        static uint WM_KEYDOWN = 0x0100;
        static uint WM_KEYUP = 0x0101;
        #endregion

        IntPtr pWnd;
        Process process;

        public Keyboard(Process p){
            this.process = p;
            Console.WriteLine(process.MainWindowTitle);
            pWnd = FindWindowEx(process.MainWindowHandle, IntPtr.Zero, "Edit", null);

            if (pWnd == IntPtr.Zero)
               throw new Exception("Process Mainwindowhandle = null!");
        }

        public void Send(Key k){
            PostMessage((IntPtr)02870828, WM_KEYDOWN, (int)k, 0);
            Thread.Sleep(100);
            PostMessage(pWnd, WM_KEYUP, (int)k, 0);
        }
    }
}
