using MemoBird_GuGuJi.Classes;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace MemoBird_GuGuJi.Windows
{
    public partial class Window_About : Window
    {
        public Window_About()
        {
            InitializeComponent();
            this.ShowLog();
        }

        #region Private Function

        private void ShowLog()
        {
            if (this.button_Submit.Content.Equals("OK"))
            {
                this.textBox_Log.Text = AboutInfo.log_en_us;
            }
            else
            {
                this.textBox_Log.Text = AboutInfo.log_zh_cn;
            }
        }

        #endregion

        #region Event Handlers

        private void Grid_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void button_Submit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void label_url1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start(AboutInfo.guguURL);
        }

        private void label_url2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start(AboutInfo.QRCoderURL);
        }

        #endregion
    }
}
