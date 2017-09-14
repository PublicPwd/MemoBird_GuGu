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
                this.DragMove();
            }
        }

        private void button_Close_Click(object sender, RoutedEventArgs e)
        {
            AddedContent.text = string.Empty;
            this.Close();
        }

        private void button_OK_Click(object sender, RoutedEventArgs e)
        {
            AddedContent.text = this.textBox_Content.Text;
            this.Close();
        }
    }
}
