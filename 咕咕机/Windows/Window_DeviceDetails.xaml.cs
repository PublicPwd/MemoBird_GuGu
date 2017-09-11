using MemoBird.Classes;
using System.Windows;
using System.Windows.Input;

namespace MemoBird.Windows
{
    public partial class Window_DeviceDetails : Window
    {
        string oldName;

        public Window_DeviceDetails()
        {
            InitializeComponent();
            this.oldName = string.Empty;
        }

        public Window_DeviceDetails(string name, string id)
        {
            InitializeComponent();
            this.oldName = name;
            this.textBox_Name.Text = name;
            this.textBox_Id.Text = id;
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
            this.oldName = string.Empty;
            DeviceDetails.deviceName = string.Empty;
            DeviceDetails.deviceId = string.Empty;
            this.Close();
        }

        private void button_OK_Click(object sender, RoutedEventArgs e)
        {
            if (this.textBox_Name.Text.Length == 0)
            {
                MessageBox.Show(FindResource("pleaseinputdevicename").ToString());
                return;
            }
            if (this.textBox_Id.Text.Length == 0)
            {
                MessageBox.Show(FindResource("pleaseinputdeviceid").ToString());
                return;
            }

            if(DeviceList.id.ContainsKey(this.textBox_Name.Text))
            {
                MessageBox.Show(FindResource("thisnamehasexist").ToString());
                return;
            }
            if (this.oldName.Length > 0)
            {
                DeviceList.id.Remove(this.oldName);
            }
            DeviceList.id.Add(this.textBox_Name.Text, this.textBox_Id.Text);
            DeviceDetails.deviceName = this.textBox_Name.Text;
            DeviceDetails.deviceId = this.textBox_Id.Text;
            this.Close();
        }
    }
}
