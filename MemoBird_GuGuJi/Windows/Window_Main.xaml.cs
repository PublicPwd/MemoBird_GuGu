using MemoBird_GuGu.Classes;
using MemoBird_GuGu.Pages;
using MemoBird_GuGu.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace MemoBird_GuGu
{
    public partial class Window_Main : Window
    {
        public Window_Main()
        {
            InitializeComponent();
            DeviceList.Load();
            Frame_Pages.Content = new Page_Text();
        }

        #region Private Function

        /// <summary>
        /// 高亮显示当前点击的选项卡
        /// </summary>
        /// <param name="currentLabel">当前点击的选项卡</param>
        private void HightLightTheCurrentTab(object currentLabel)
        {
            SolidColorBrush brush = new SolidColorBrush(Color.FromRgb(0xF3, 0xF4, 0xF6));
            Label_Text.Foreground = brush;
            Label_Image.Foreground = brush;
            Label_TextAndImage.Foreground = brush;
            Label_QRCode.Foreground = brush;
            Label_More.Foreground = brush;

            brush = new SolidColorBrush(Color.FromRgb(0xF7, 0x44, 0x61));
            Label label = currentLabel as Label;
            label.Foreground = brush;

            Grid_Nav.Margin = new Thickness(label.Margin.Left, 45, 0, 0);

            brush = null;
        }

        /// <summary>
        /// 更改软件显示的语言
        /// </summary>
        private void ChangeTheLanguage()
        {
            string requestedCulture = string.Empty;
            if (Label_Language.Content.Equals("English"))
            {
                requestedCulture = @"Resources\en-us.xaml";
                Label_Language.Content = "简体中文";
            }
            else
            {
                requestedCulture = @"Resources\zh-cn.xaml";
                Label_Language.Content = "English";
            }
            List<ResourceDictionary> dictionaryList = new List<ResourceDictionary>();
            foreach (ResourceDictionary dictionary in Application.Current.Resources.MergedDictionaries)
            {
                dictionaryList.Add(dictionary);
            }
            ResourceDictionary resourceDictionary = dictionaryList.FirstOrDefault(d => d.Source.OriginalString.Equals(requestedCulture));
            Application.Current.Resources.MergedDictionaries.Remove(resourceDictionary);
            Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);

            requestedCulture = string.Empty;
        }

        #endregion

        #region Event Handlers

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void Button_Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Maximize_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                WindowState = WindowState.Normal;
            }
            else
            {
                WindowState = WindowState.Maximized;
            }
        }

        private void Button_Minimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Label_MouseEnter(object sender, MouseEventArgs e)
        {
            (sender as Label).FontSize = 25;
        }

        private void Label_MouseLeave(object sender, MouseEventArgs e)
        {
            (sender as Label).FontSize = 16;
        }

        private void Label_More_MouseEnter(object sender, MouseEventArgs e)
        {
            Label_Device.Visibility = Visibility.Visible;
            Label_History.Visibility = Visibility.Visible;
            (sender as Label).FontSize = 25;
        }

        private void Label_More_MouseLeave(object sender, MouseEventArgs e)
        {
            (sender as Label).FontSize = 16;
            double x = e.GetPosition(null).X;
            double y = e.GetPosition(null).Y;
            if (x >= 550 && x <= 650 && y <= 150)
            {
                return;
            }
            Label_Device.Visibility = Visibility.Hidden;
            Label_History.Visibility = Visibility.Hidden;
        }

        private void Label_About_MouseEnter(object sender, MouseEventArgs e)
        {
            Label_About.Content = FindResource("about");
        }

        private void Label_About_MouseLeave(object sender, MouseEventArgs e)
        {
            Label_About.Content = FindResource("title");
        }

        private void Label_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ChangeTheLanguage();
        }

        private void Label_Text_MouseDown(object sender, MouseButtonEventArgs e)
        {
            HightLightTheCurrentTab(sender);
            Frame_Pages.Content = new Page_Text();
        }

        private void Label_Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            HightLightTheCurrentTab(sender);
            Frame_Pages.Content = new Page_Image();
        }

        private void Label_TextAndImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            HightLightTheCurrentTab(sender);
            Frame_Pages.Content = new Page_TextAndImage();
        }

        private void Label_QRCode_MouseDown(object sender, MouseButtonEventArgs e)
        {
            HightLightTheCurrentTab(sender);
            Frame_Pages.Content = new Page_QRCode();
        }

        private void Label_Device_MouseDown(object sender, MouseButtonEventArgs e)
        {
            HightLightTheCurrentTab(Label_More);
            Frame_Pages.Content = new Page_Device();
        }

        private void Label_History_MouseDown(object sender, MouseButtonEventArgs e)
        {
            HightLightTheCurrentTab(Label_More);
            Frame_Pages.Content = new Page_History();
        }

        private void Label_About_MouseDown(object sender, MouseButtonEventArgs e)
        {
            new Window_About().ShowDialog();
        }

        #endregion
    }
}
