using MemoBird_GuGu.Classes;
using System.Diagnostics;
using System.Reflection;
using System.Windows;
using System.Windows.Input;

namespace MemoBird_GuGu.Windows
{
    public partial class Window_About : Window
    {
        public Window_About()
        {
            InitializeComponent();
            Label_Title.Content += Assembly.GetExecutingAssembly().GetName().Version.ToString();
            TextBox_Log.Text = AboutInfo.Log_zh_cn;
        }

        #region Event Handlers

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void Button_Submit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Label_url1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start(AboutInfo.GuguURL);
        }

        private void Label_url2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start(AboutInfo.QRCoderURL);
        }

        private void Label_url3_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start(AboutInfo.MemoBird_GuGu);
        }

        #endregion
    }
}
