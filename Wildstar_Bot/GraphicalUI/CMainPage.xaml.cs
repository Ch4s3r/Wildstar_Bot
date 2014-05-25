using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;

namespace Wildstar_Bot.GraphicalUI
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class CMainPage : UserControl, ISwitchable
    {
        private Process process;

        public CMainPage()
        {
            InitializeComponent();
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            PageController.Switch(typeof(CAbout));
        }

        private void Process_Click(object sender, RoutedEventArgs e)
        {
            PageController.Switch(typeof(CProcess), StateMessage.CMainPage_CProcess_RefreshProcessList, null);
        }

        public void UtilizeState(object data, StateMessage msg)
        {
            switch (msg)
            {
                case StateMessage.CProcess_CMainPage_setProcess:
                    this.process = (Process)data;
                    Console.WriteLine(process.MainWindowTitle + "\nPID: " + process.Id + "\nHWND: " + process.MainWindowHandle);
                    break;
            }
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            if (process == null)
                throw new ArgumentException("No Process");

            VirtualKeyboard kb = new VirtualKeyboard(process);
            kb.Send(Key.A,10);
            kb.Send(13);
        }
    }
}
