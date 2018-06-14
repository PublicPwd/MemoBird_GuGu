using MemoBird_GuGu.Classes;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace MemoBird_GuGu.Windows
{
    public partial class Window_DeviceDetails : Window
    {
        private DeviceDetails deviceDetails = null;

        public Window_DeviceDetails()
        {
            InitializeComponent();
            TextBox_Name.Focus();
        }

        public Window_DeviceDetails(DeviceDetails deviceDetails)
        {
            InitializeComponent();
            this.deviceDetails = deviceDetails;
            TextBox_Name.Text = deviceDetails.Name;
            TextBox_Id.Text = deviceDetails.Id;
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
            if (deviceDetails == null)
            {
                var devices = from d in DeviceList.Details
                              where d.Name == TextBox_Name.Text
                              select d;
                if (devices.Count() > 0)
                {
                    MessageBox.Show(FindResource("thisnamehasexist").ToString());
                    return;
                }
            }
            else
            {
                DeviceList.Details.Remove(deviceDetails);
            }
            DeviceList.Details.Add(new DeviceDetails(TextBox_Name.Text, TextBox_Id.Text));
            DeviceList.Save();
            Close();
        }
    }
}
