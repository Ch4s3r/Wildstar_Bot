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
            PageController.Switch(typeof(CProcess));
        }

        public void UtilizeState(object state)
        {
            if (state.GetType().Equals(typeof(Process)))
            {
                process = (Process)state;
            }
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            if (process == null)
                return;
            VirtualKeyboard kb = new VirtualKeyboard(process);
            kb.Send(Key.A);
        }
    }
}
