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

namespace Wildstar_Bot
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            PageController.registerSwitcher(this);
            
            //Register-Part
            PageController.registerUserController(new Wildstar_Bot.GraphicalUI.CMainPage(), "MainPage");
            PageController.registerUserController(new Wildstar_Bot.GraphicalUI.CAbout(), "About");
            PageController.registerUserController(new Wildstar_Bot.GraphicalUI.CProcess(), "Process");

            //Call MainPage
            PageController.Switch(typeof(Wildstar_Bot.GraphicalUI.CMainPage));// or as PageController.Switch("MainPage");
        }

        public void Navigate(UserControl newPage)
        {
            this.Content = newPage;
        }

        public void Navigate(UserControl newPage, object state)
        {
            this.Content = newPage;
            ISwitchable i = newPage as ISwitchable;

            if (i != null)
            {
                i.UtilizeState(state);
            }
            else
            {
                throw new ArgumentException("NewPage is not Switchable!");
            }
        }
    }
}
