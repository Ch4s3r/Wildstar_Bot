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
        [DllImport("user32", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern IntPtr GetWindow(IntPtr hwnd, int wFlag);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string className, string windowTitle);

        [DllImport("User32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int uMsg, int wParam, string lParam);

        [DllImport("user32.dll")]
        static extern bool PostMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);

        static uint WM_KEYDOWN = 0x0100;
        static uint WM_KEYUP = 0x0101;
        #endregion

        public delegate bool Win32Callback(IntPtr hwnd, IntPtr lParam);
        [DllImport("user32.Dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnumChildWindows(IntPtr parentHandle, Win32Callback callback, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static public extern IntPtr GetClassName(IntPtr hWnd, System.Text.StringBuilder lpClassName, int nMaxCount);

        private static bool EnumWindow(IntPtr handle, IntPtr pointer)
        {
            GCHandle gch = GCHandle.FromIntPtr(pointer);
            List<IntPtr> list = gch.Target as List<IntPtr>;
            if (list == null)
                throw new InvalidCastException("GCHandle Target could not be cast as List<IntPtr>");
            list.Add(handle);
            return true;
        }

        public static List<IntPtr> GetChildWindows(IntPtr parent)
        {
            List<IntPtr> result = new List<IntPtr>();
            GCHandle listHandle = GCHandle.Alloc(result);
            try
            {
                Win32Callback childProc = new Win32Callback(EnumWindow);
                EnumChildWindows(parent, childProc, GCHandle.ToIntPtr(listHandle));
            }
            finally
            {
                if (listHandle.IsAllocated)
                    listHandle.Free();
            }
            return result;
        }

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);


        IntPtr pWnd;
        Process process;

        public VirtualKeyboard(Process p)
        {
            this.process = p;
            Console.WriteLine(process.MainWindowTitle);
            pWnd = FindWindow("", process.MainWindowTitle);
            pWnd = FindWindowEx(pWnd,IntPtr.Zero,"",process.MainWindowTitle);
            Send(Key.A);
            
            //List<IntPtr> x = GetChildWindows(process.MainWindowHandle);
            //foreach (IntPtr i in x)
            //{
            //    pWnd = FindWindowEx(process.MainWindowHandle, i, null, process.MainWindowTitle);
            //    if (pWnd != IntPtr.Zero)
            //        Send(Key.A);
            //        //throw new Exception("Process Mainwindowhandle = null!");
            //}
            Thread.Sleep(3000);
            SendKeys.SendWait("a");
            

        }

        public void Send(Key k)
        {
            SendMessage(pWnd, 0x000C, 0, "aa");
            //PostMessage((IntPtr)02870828, WM_KEYDOWN, (int)k, 0);
            //Thread.Sleep(100);
            //PostMessage(pWnd, WM_KEYUP, (int)k, 0);
        }
    }
}
