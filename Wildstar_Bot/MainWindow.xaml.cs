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

namespace Wildstar_Bot
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Process process;

        public MainWindow()
        {
            InitializeComponent();

        }

        private void btnProcess_Click(object sender, RoutedEventArgs e)
        {
            lblProcess.Content = "";
            frmProcess frmp = new frmProcess();
            frmp.WindowStartupLocation = this.WindowStartupLocation;
            frmp.setProcessEvent += new ProcessEventHandler(Process_setProcessEvent);
            frmp.ShowDialog();
        }

        void Process_setProcessEvent(Process p)
        {
            lblProcess.Content = p.Id.ToString();
            process = p;
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            if (process != null)
            {
                Keyboard kb = new Keyboard(process);
                kb.Send(Key.A);
            }
        }
    }
}
