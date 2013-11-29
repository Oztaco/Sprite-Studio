using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SpriteStudio
{
    /// <summary>
    /// Interaction logic for EffectHost.xaml
    /// </summary>
    public partial class EffectHost : Window
    {
        public EffectHost()
        {
            InitializeComponent();
        }

        private void InputExpanded(object sender, RoutedEventArgs e) // For both collapsed and expanded events
        {
            expanderOutput.IsExpanded = expanderInput.IsExpanded;
        }

        private void OutputExpanded(object sender, RoutedEventArgs e) // For both collapsed and expanded events
        {
            expanderInput.IsExpanded = expanderOutput.IsExpanded;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.IsEnabled = false;
        }
    }
}
