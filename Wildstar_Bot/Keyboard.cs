using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Windows.Input;
using System.Threading;
using System.Windows.Forms;

namespace Wildstar_Bot
{
    public class VirtualKeyboard
    {
        #region DllImport

        [DllImport("user32.dll")]
        static extern bool PostMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        static uint WM_KEYDOWN = 0x0100;
        static uint WM_KEYUP = 0x0101;
        static uint WM_CHAR = 0x0102;

        #endregion

        IntPtr pWnd;
        Process process;

        public VirtualKeyboard(Process p)
        {
            this.process = p;
            pWnd = process.MainWindowHandle;
        }

        public void Send(Key k)
        {
            PostMessage(pWnd, WM_KEYDOWN, (int)k, 0);
            PostMessage(pWnd, WM_CHAR, (int)k, 0);
            PostMessage(pWnd, WM_KEYUP, (int)k, 0);
        }

        public void Send(int k)
        {

            Send((Key)k);
        }

        public void Send(Key k, int repeat_x_times = 0)
        {
            for (int i = 0; i < repeat_x_times; i++)
            {
                Send(k);
                Thread.Sleep(100);
            }
        }

        public void Send(int k, int repeat_x_times = 0)
        {

            Send((Key)k, repeat_x_times);
        }
    }
}
