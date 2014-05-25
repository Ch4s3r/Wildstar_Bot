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
using Wildstar_Bot.GraphicalUI;

namespace Wildstar_Bot
{
    class PageController
    {
        private static MainWindow pageSwitcher;        
        private static Dictionary<string, UserControl> pages = new Dictionary<string,UserControl>();
        private static Dictionary<Type, UserControl> pagec = new Dictionary<Type,UserControl>();
        private static bool finalize = false;

        public static void registerSwitcher(MainWindow pageS)
        {
            if (!finalize)
            {
                pageSwitcher = pageS;
                finalize = true;
            }
        }

        public static void registerUserController(UserControl controller, string name)
        {
            if (pages.Keys.Contains(name))
            {
                throw new ArgumentException("PageName [" + name + "] is already registered!");
            }
            if (pages.Values.Contains(controller))
            {
                throw new ArgumentException("UserControl [" + controller + "] is already registered!");
            }
            pages.Add(name, controller);
            pagec.Add(controller.GetType(), controller);
        }

        public static void Switch(string name)
        {
            UserControl uc;
            if (pages.TryGetValue(name, out uc))
            {
                Switch(uc);
            }
            else
            {
                throw new ArgumentException("UserControl for Name [" + name + "] is not registered!");
            }
        }

        public static void Switch(string name, object state)
        {
            UserControl uc;
            if (pages.TryGetValue(name, out uc))
            {
                Switch(uc,state);
            }
            else
            {
                throw new ArgumentException("UserControl for Name [" + name + "] is not registered!");
            }
        }

        public static void Switch(Type type)
        {
            UserControl uc;
            if (pagec.TryGetValue(type, out uc))
            {
                Switch(uc);
            }
            else
            {
                throw new ArgumentException("UserControl for Name [" + type + "] is not registered!");
            }
        }

        public static void Switch(Type type, object state)
        {
            UserControl uc;
            if (pagec.TryGetValue(type, out uc))
            {
                Switch(uc,state);
            }
            else
            {
                throw new ArgumentException("UserControl for Name [" + type + "] is not registered!");
            }
        }

        public static void Switch(UserControl newPage)
        {
            pageSwitcher.Navigate(newPage);
        }

        public static void Switch(UserControl newPage, object state)
        {
            pageSwitcher.Navigate(newPage, state);
        }
    }


}
