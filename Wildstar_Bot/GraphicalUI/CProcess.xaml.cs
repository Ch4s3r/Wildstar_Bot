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
    /// Interaction logic for Process.xaml
    /// </summary>
    public partial class CProcess : UserControl, ISwitchable
    {
        private Process[] processlist;

        public CProcess()
        {
            InitializeComponent();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            RefreshProcessList();
        }

        private void btnSet_Click(object sender, RoutedEventArgs e)
        {
            PageController.Switch(typeof(CMainPage),StateMessage.CProcess_CMainPage_setProcess, processlist[lstProcesses.SelectedIndex]);
        }

        private void RefreshProcessList()
        {
            lstProcesses.Items.Clear();
            processlist = Process.GetProcessesByName("wildstar64");
            foreach (Process p in processlist)
            {
                lstProcesses.Items.Add(p.Id + "\t[" + p.ProcessName + "]");
            }
        }

        public void UtilizeState(object data, StateMessage msg)
        {
            switch (msg)
            {
                case StateMessage.CMainPage_CProcess_RefreshProcessList:
                    RefreshProcessList();
                    break;
            }
        }
    }
}
