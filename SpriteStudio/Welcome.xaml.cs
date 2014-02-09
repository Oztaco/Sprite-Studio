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
using System.IO;
using Microsoft.Win32;
using System.Xml;
using System.Windows.Forms;

namespace SpriteStudio
{
    /// <summary>
    /// Interaction logic for Welcome.xaml
    /// </summary>
    public partial class Welcome : Window
    {
        public Welcome()
        {
            InitializeComponent();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboCommonSizes.SelectedIndex >= 1) {
                string splitted = ((ComboBoxItem)comboCommonSizes.SelectedItem).Content.ToString().Split('(')[0].Trim();
                string[] broken = splitted.Split('x');
                broken[0].Trim();
                broken[1].Trim();
                nmbrHeight.Value = int.Parse(broken[1]);
                nmbrWidth.Value = int.Parse(broken[0]);
                comboCommonSizes.SelectedIndex = 0;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int height, width;
            height = (int)nmbrHeight.Value;
            width = (int)nmbrWidth.Value;
            nmbrHeight.Value = width;
            nmbrWidth.Value = height;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            comboCommonSizes.SelectedIndex = 0;
            nmbrHeight.ValueSet += SizeChangedHandler;
            nmbrWidth.ValueSet += SizeChangedHandler;
        }

        private void SizeChangedHandler() {
            if (nmbrHeight.Value == nmbrWidth.Value) {
                previewBox.Height = 200;
                previewBox.Width = 200;
            }
            else if (nmbrHeight.Value > nmbrWidth.Value)
            {
                previewBox.Height = 200;
                previewBox.Width = (nmbrWidth.Value / nmbrHeight.Value) * 200; 
            }
            else {
                previewBox.Width = 200;
                previewBox.Height = (nmbrHeight.Value / nmbrWidth.Value) * 200;
            }
            if (previewBox.Width < 5) previewBox.Width = 5;
            if (previewBox.Height < 5) previewBox.Height = 5;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog s = new FolderBrowserDialog();
            string name = "";
            // s.DefaultExt = ".spi";
            // s.Filter = "Sprite Project Index |*.spi";
            if (s.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                if (s.SelectedPath.Length > 35) name = "...\\ " + new DirectoryInfo(s.SelectedPath).Name;
                else  name = s.SelectedPath;

                fileTextBox.Text = name;
                FolderPath = s.SelectedPath;
                btnCreateProj.IsEnabled = true;
            }
        }
        private string FolderPath = "";

        private void btnCreateProj_Click(object sender, RoutedEventArgs e)
        {
            CreateNewProjectFile();
            this.DialogResult = true;
            this.Close();
        }

        private void CreateNewProjectFile()
        {
            CommonControls.AlertBox a = new CommonControls.AlertBox();
            a.ShowDialog();
            //System.Windows.Forms.DialogResult replace = System.Windows.Forms.MessageBox.Show("Note: If any files exi");

            return;
            XmlWriterSettings xs = new XmlWriterSettings();
            xs.Indent = true;
            xs.IndentChars = "  ";
            XmlWriter xw = XmlWriter.Create(FolderPath, xs);

            xw.WriteStartElement("SpriteStudio");
            xw.WriteStartElement("Meta");
            xw.WriteElementString("Version", "1.0");
            xw.WriteElementString("Author", Environment.UserName);
            xw.WriteElementString("Date", DateTime.Now.ToString());
            xw.WriteEndElement();
            xw.WriteStartElement("Preferences");

            xw.WriteEndElement();
            xw.WriteStartElement("Project");
            xw.WriteStartElement("Image");
            xw.WriteAttributeString("Width", nmbrWidth.Value.ToString());
            xw.WriteAttributeString("Height", nmbrHeight.Value.ToString());

            xw.WriteStartElement("Frames");
            xw.WriteStartElement("Frame");
            xw.WriteAttributeString("Duration", "1000");
            xw.WriteStartElement("Layers");
            xw.WriteStartElement("Layer");
            xw.WriteAttributeString("Type", "Raster");
            xw.WriteAttributeString("Opacity", "255");

            xw.WriteEndElement();
            xw.WriteEndElement();
            xw.WriteEndElement();
            xw.Close();
        }
    }
}
