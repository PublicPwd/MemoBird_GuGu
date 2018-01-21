using MemoBird_GuGu.Classes;
using System.Windows;
using System.Windows.Input;

namespace MemoBird_GuGu.Windows
{
    public partial class Window_DeviceDetails : Window
    {
        string oldName;

        public Window_DeviceDetails()
        {
            InitializeComponent();
            oldName = string.Empty;
        }

        public Window_DeviceDetails(string name, string id)
        {
            InitializeComponent();
            oldName = name;
            TextBox_Name.Text = name;
            TextBox_Id.Text = id;
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
            oldName = string.Empty;
            DeviceDetails.SetDeviceDetails(string.Empty, string.Empty);
            Close();
        }

        private void Button_OK_Click(object sender, RoutedEventArgs e)
        {
            if (TextBox_Name.Text.Length == 0)
            {
                MessageBox.Show(FindResource("pleaseinputdevicename").ToString());
                return;
            }
            if (TextBox_Id.Text.Length == 0)
            {
                MessageBox.Show(FindResource("pleaseinputdeviceid").ToString());
                return;
            }
            if (oldName.Length > 0)
            {
                DeviceList.Id.Remove(oldName);
            }
            if(DeviceList.Id.ContainsKey(TextBox_Name.Text))
            {
                MessageBox.Show(FindResource("thisnamehasexist").ToString());
                return;
            }
            DeviceList.Id.Add(TextBox_Name.Text, TextBox_Id.Text);
            DeviceDetails.SetDeviceDetails(TextBox_Name.Text, TextBox_Id.Text);
            Close();
        }
    }
}
