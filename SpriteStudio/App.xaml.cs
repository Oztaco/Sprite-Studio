using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace SpriteStudio
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Welcome w = new Welcome();
            MainWindow m = new MainWindow();
            if (w.ShowDialog() == true) {
                m.ShowDialog();
            }
            this.Shutdown();
        }
    }
}
