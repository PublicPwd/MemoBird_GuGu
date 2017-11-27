using MemoBird_GuGuJi.Classes;
using System.Windows;
using System.Windows.Input;

namespace MemoBird_GuGuJi.Windows
{
    public partial class Window_AddText : Window
    {
        public Window_AddText()
        {
            InitializeComponent();
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
            AddedContent.Text = string.Empty;
            Close();
        }

        private void Button_OK_Click(object sender, RoutedEventArgs e)
        {
            AddedContent.Text = TextBox_Content.Text;
            Close();
        }
    }
}
