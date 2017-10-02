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
        }

        private void Grid_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void label_url1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start("https://github.com/memobird/gugu-.net");
        }

        private void label_url2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start("https://github.com/codebude/QRCoder");
        }
    }
}
