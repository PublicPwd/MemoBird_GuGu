using MemoBird_GuGu.Classes;
using MemoBird_GuGu.Utils;
using MemoBird_GuGu.Windows;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace MemoBird_GuGu.Pages
{
    public partial class Page_Device : Page
    {
        public Page_Device()
        {
            InitializeComponent();
        }

        #region Public Function

        /// <summary>
        /// 往 DataGrid 中填充设备信息
        /// </summary>
        public void FillContent()
        {
            if (!DeviceList.DeviceListChanged)
            {
                return;
            }
            foreach (string name in DeviceList.Id.Keys)
            {
                DataGrid_DeviceList.Items.Add(new DataGridRow() { Item = new { col1 = name, col2 = DeviceList.Id[name] } });
            }
            DeviceList.DeviceListChanged = false;
        }

        #endregion

        #region Event Handlers

        private void Button_Add_Click(object sender, RoutedEventArgs e)
        {
            new Window_DeviceDetails().ShowDialog();

            if (DeviceDetails.Name.Length == 0)
            {
                return;
            }

            DataGrid_DeviceList.Items.Add(new DataGridRow() { Item = new { col1 = DeviceDetails.Name, col2 = DeviceDetails.Id } });

            DeviceList.DeviceListChanged = true;
            FileX.SaveDeviceList();
        }

        private void Button_Edit_Click(object sender, RoutedEventArgs e)
        {
            if (DataGrid_DeviceList.Items.Count == 0)
            {
                return;
            }
            int index = DataGrid_DeviceList.SelectedIndex;
            string name = (DataGrid_DeviceList.Columns[0].GetCellContent(DataGrid_DeviceList.Items[index]) as TextBlock).Text;
            string id = (DataGrid_DeviceList.Columns[1].GetCellContent(DataGrid_DeviceList.Items[index]) as TextBlock).Text;

            new Window_DeviceDetails(name, id).ShowDialog();

            if (DeviceDetails.Name.Length == 0)
            {
                return;
            }

            (DataGrid_DeviceList.Columns[0].GetCellContent(DataGrid_DeviceList.Items[index]) as TextBlock).Text = DeviceDetails.Name;
            (DataGrid_DeviceList.Columns[1].GetCellContent(DataGrid_DeviceList.Items[index]) as TextBlock).Text = DeviceDetails.Id;

            DeviceList.DeviceListChanged = true;
            FileX.SaveDeviceList();
        }

        private void Button_Remove_Click(object sender, RoutedEventArgs e)
        {
            if (DataGrid_DeviceList.Items.Count == 0)
            {
                return;
            }
            int index = DataGrid_DeviceList.SelectedIndex;
            string name = (DataGrid_DeviceList.Columns[0].GetCellContent(DataGrid_DeviceList.Items[index]) as TextBlock).Text;
            DeviceList.Id.Remove(name);
            DataGrid_DeviceList.Items.RemoveAt(index);

            DeviceList.DeviceListChanged = true;
            FileX.SaveDeviceList();
        }

        #endregion
    }
}