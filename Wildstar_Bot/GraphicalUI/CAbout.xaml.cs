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

namespace Wildstar_Bot.GraphicalUI
{
    /// <summary>
    /// Interaction logic for About.xaml
    /// </summary>
    public partial class CAbout : UserControl
    {
        public CAbout()
        {
            InitializeComponent();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            //PageController.Switch("MainPage");
            PageController.Switch(typeof(CMainPage));
        }
    }
}
