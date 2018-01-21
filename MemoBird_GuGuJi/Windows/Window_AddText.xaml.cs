using System.Windows;
using System.Windows.Input;

namespace MemoBird_GuGu.Windows
{
    public partial class Window_AddText : Window
    {
        public string Text = string.Empty;

        public Window_AddText()
        {
            InitializeComponent();
            TextBox_Content.Focus();
        }

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

        private void Button_OK_Click(object sender, RoutedEventArgs e)
        {
            Text = TextBox_Content.Text;
            Close();
        }
    }
}
