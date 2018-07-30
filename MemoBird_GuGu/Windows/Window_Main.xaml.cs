using MemoBird_GuGu.Classes;
using MemoBird_GuGu.Pages;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MemoBird_GuGu.Windows
{
    public partial class Window_Main : Window
    {
        private Page_Device page_Device = new Page_Device();
        private Page_History page_History = new Page_History();
        private Page_Image page_Image = new Page_Image();
        private Page_QRCode page_QRCode = new Page_QRCode();
        private Page_Text page_Text = new Page_Text();
        private Page_TextAndImage page_TextAndImage = new Page_TextAndImage();

        public Window_Main()
        {
            InitializeComponent();
            DeviceList.Load();
            Label_Text_MouseDown(Label_Text, null);
        }

        #region Private Function

        private void ShowPage(Page page, object label)
        {
            Frame_Pages.Content = page;

            foreach (Label l in StackPanel_Menu.Children)
            {
                l.BorderThickness = new Thickness(0);
            }
            (label as Label).BorderThickness = new Thickness(0, 0, 0, 5);
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

        private void Label_Language_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ChangeTheLanguage();
        }

        private void Label_Text_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ShowPage(page_Text, sender);
        }

        private void Label_Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ShowPage(page_Image, sender);
        }

        private void Label_TextAndImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ShowPage(page_TextAndImage, sender);
        }

        private void Label_QRCode_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ShowPage(page_QRCode, sender);
        }

        private void Label_Device_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ShowPage(page_Device, sender);
        }

        private void Label_History_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ShowPage(page_History, sender);
        }

        private void Label_About_MouseDown(object sender, MouseButtonEventArgs e)
        {
            new Window_About().ShowDialog();
        }

        #endregion
    }
}
