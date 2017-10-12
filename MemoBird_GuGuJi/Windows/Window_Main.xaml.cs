using MemoBird_GuGuJi.Pages;
using MemoBird_GuGuJi.Utils;
using MemoBird_GuGuJi.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace MemoBird_GuGuJi
{
    public partial class Window_Main : Window
    {
        private Page_Text page_Text = new Page_Text();
        private Page_Image page_Image = new Page_Image();
        private Page_TextAndImage page_TextAndImage = new Page_TextAndImage();
        private Page_QRCode page_QRCode = new Page_QRCode();
        private Page_Device page_Device = new Page_Device();

        public Window_Main()
        {
            InitializeComponent();
            this.frame_Pages.Content = page_Text;
            FileX.LoadDeviceList();
            page_Device.FillContent();
            page_Text.FillContnet();
            page_Image.FillContnet();
            page_QRCode.FillContnet();
            page_TextAndImage.FillContent();
        }

        #region Private Function

        /// <summary>
        /// 高亮显示当前点击的选项卡
        /// </summary>
        /// <param name="currentLabel">当前点击的选项卡</param>
        private void HightLightTheCurrentTab(object currentLabel)
        {
            SolidColorBrush brush = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0xF3, 0xF4, 0xF6));
            this.label_Text.Foreground = brush;
            this.label_Image.Foreground = brush;
            this.label_TextAndImage.Foreground = brush;
            this.label_QRCode.Foreground = brush;
            this.label_Device.Foreground = brush;

            brush = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0xF7, 0x44, 0x61));
            Label label = currentLabel as Label;
            label.Foreground = brush;

            grid_Nav.Margin = new Thickness(label.Margin.Left, 45, 0, 0);

            page_Text.FillContnet();
            page_Image.FillContnet();
            page_TextAndImage.FillContent();
            page_QRCode.FillContnet();

            brush = null;
        }

        /// <summary>
        /// 更改软件显示的语言
        /// </summary>
        private void ChangeTheLanguage()
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

            requestedCulture = string.Empty;
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
            this.label_About.Content = FindResource("about");
        }

        private void label_About_MouseLeave(object sender, MouseEventArgs e)
        {
            this.label_About.Content = FindResource("title");
        }

        private void Label_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.ChangeTheLanguage();
        }

        private void label_Text_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.HightLightTheCurrentTab(sender);
            this.frame_Pages.Content = page_Text;
        }

        private void label_Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.HightLightTheCurrentTab(sender);
            this.frame_Pages.Content = page_Image;
        }

        private void label_TextAndImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.HightLightTheCurrentTab(sender);
            this.frame_Pages.Content = page_TextAndImage;
        }

        private void label_QRCode_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.HightLightTheCurrentTab(sender);
            this.frame_Pages.Content = page_QRCode;
        }

        private void label_Device_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.HightLightTheCurrentTab(sender);
            this.frame_Pages.Content = page_Device;
        }

        private void label_About_MouseDown(object sender, MouseButtonEventArgs e)
        {
            new Window_About().ShowDialog();
        }

        private void Window_Closed(object sender, System.EventArgs e)
        {
            FileX.SaveDeviceList();
        }

        #endregion
    }
}
