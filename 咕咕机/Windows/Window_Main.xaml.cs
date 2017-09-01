using MemoBird.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace MemoBird
{
    public partial class Window_Main : Window
    {
        public Window_Main()
        {
            InitializeComponent();
        }

        #region Private Function

        /// <summary>
        /// 高亮显示当前点击的选项卡
        /// </summary>
        /// <param name="currentLabel">当前点击的选项卡</param>
        private void HightLightTheCurrentTab(object currentLabel)
        {
            SolidColorBrush brush = new SolidColorBrush(Color.FromRgb(0xF3, 0xF4, 0xF6));
            this.label_Text.Foreground = brush;
            this.label_Image.Foreground = brush;
            this.label_TextAndImage.Foreground = brush;
            this.label_Device.Foreground = brush;

            brush = new SolidColorBrush(Color.FromRgb(0xF7, 0x44, 0x61));
            Label label = currentLabel as Label;
            label.Foreground = brush;

            grid_Nav.Margin = new Thickness(label.Margin.Left, 45, 0, 0);
        }

        #endregion

        #region Event Handlers

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void button_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void button_Minimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void label_MouseEnter(object sender, MouseEventArgs e)
        {
            (sender as Label).FontSize = 25;
        }

        private void label_MouseLeave(object sender, MouseEventArgs e)
        {
            (sender as Label).FontSize = 16;
        }

        private void label_About_MouseEnter(object sender, MouseEventArgs e)
        {
            this.label_About.Content = this.FindResource("about");
        }

        private void label_About_MouseLeave(object sender, MouseEventArgs e)
        {
            this.label_About.Content = this.FindResource("title");
        }

        private void Label_MouseDown(object sender, MouseButtonEventArgs e)
        {
            string requestedCulture = string.Empty;
            if (this.label_Language.Content.Equals("English"))
            {
                requestedCulture = @"Resources\en-us.xaml";
                this.label_Language.Content = "简体中文";
            }
            else
            {
                requestedCulture = @"Resources\zh-cn.xaml";
                this.label_Language.Content = "English";
            }
            List<ResourceDictionary> dictionaryList = new List<ResourceDictionary>();
            foreach (ResourceDictionary dictionary in Application.Current.Resources.MergedDictionaries)
            {
                dictionaryList.Add(dictionary);
            }
            ResourceDictionary resourceDictionary = dictionaryList.FirstOrDefault(d => d.Source.OriginalString.Equals(requestedCulture));
            Application.Current.Resources.MergedDictionaries.Remove(resourceDictionary);
            Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);
        }

        private void label_Text_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.HightLightTheCurrentTab(sender);
        }

        private void label_Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.HightLightTheCurrentTab(sender);
        }

        private void label_TextAndImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.HightLightTheCurrentTab(sender);
        }

        private void label_Device_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.HightLightTheCurrentTab(sender);
        }

        private void label_About_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Window_About window_About = new Window_About();
            window_About.ShowDialog();
        }

        #endregion
    }
}
