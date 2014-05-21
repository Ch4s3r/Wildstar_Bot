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
using System.Windows.Shapes;
using System.Diagnostics;

namespace Wildstar_Bot
{
    public delegate void ProcessEventHandler(Process p);
    public partial class frmProcess : Window
    {
        public event ProcessEventHandler setProcessEvent;
        private Process[] processlist;

        public frmProcess()
        {
            InitializeComponent();
            btnRefresh_Click(null, null);
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            lstProcesses.Items.Clear();
            processlist = Process.GetProcessesByName("notepad");
            foreach (Process p in processlist)
            {
                lstProcesses.Items.Add( p.Id +"\t[" + p.ProcessName + "]");
            }
        }

        private void btnSet_Click(object sender, RoutedEventArgs e)
        {
            setProcessEvent(processlist[lstProcesses.SelectedIndex]);
            Close();
        }
        private void lstProcesses_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            btnSet_Click(null, null);
        }
    }
}
