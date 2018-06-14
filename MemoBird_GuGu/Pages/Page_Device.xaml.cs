using MemoBird_GuGu.Classes;
using MemoBird_GuGu.Windows;
using System.Windows;
using System.Windows.Controls;

namespace MemoBird_GuGu.Pages
{
    public partial class Page_Device : Page
    {
        public Page_Device()
        {
            InitializeComponent();
            DataGrid_DeviceList.ItemsSource = DeviceList.Details;
        }

        #region Event Handlers

        private void Button_Add_Click(object sender, RoutedEventArgs e)
        {
            new Window_DeviceDetails().ShowDialog();
        }

        private void Button_Edit_Click(object sender, RoutedEventArgs e)
        {
            var item = DataGrid_DeviceList.SelectedItem as DeviceDetails;
            if (item == null)
            {
                return;
            }
            new Window_DeviceDetails(item).ShowDialog();
        }

        private void Button_Remove_Click(object sender, RoutedEventArgs e)
        {
            var item = DataGrid_DeviceList.SelectedItem as DeviceDetails;
            if (item == null)
            {
                return;
            }
            if (MessageBox.Show($"{FindResource("remove").ToString()}:\n{FindResource("name").ToString()}:{item.Name}\n{FindResource("id").ToString()}:{item.Id}", string.Empty, MessageBoxButton.YesNo) == MessageBoxResult.No)
            {
                return;
            }
            DeviceList.Details.Remove(item);
            DeviceList.Save();
        }

        private void DataGrid_DeviceList_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Button_Edit_Click(null, null);
        }

        #endregion
    }
}