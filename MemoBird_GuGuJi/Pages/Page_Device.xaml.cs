using MemoBird_GuGuJi.Classes;
using MemoBird_GuGuJi.Windows;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace MemoBird_GuGuJi.Pages
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
            if (!DeviceList.deviceListChanged)
            {
                return;
            }
            foreach (string name in DeviceList.id.Keys)
            {
                this.dataGrid_DeviceList.Items.Add(new DataGridRow() { Item = new { col1 = name, col2 = DeviceList.id[name] } });
            }
            DeviceList.deviceListChanged = false;
        }

        #endregion

        #region Event Handlers

        private void button_Add_Click(object sender, RoutedEventArgs e)
        {
            new Window_DeviceDetails().ShowDialog();

            if(DeviceDetails.deviceName.Length==0)
            {
                return;
            }

            this.dataGrid_DeviceList.Items.Add(new DataGridRow() { Item = new { col1 = DeviceDetails.deviceName, col2 = DeviceDetails.deviceId } });

            DeviceList.deviceListChanged = true;
        }

        private void button_Edit_Click(object sender, RoutedEventArgs e)
        {
            var item = this.dataGrid_DeviceList.SelectedItem;
            DataRowView dataRowView = item as DataRowView;
            string name = (this.dataGrid_DeviceList.Columns[0].GetCellContent(this.dataGrid_DeviceList.Items[0]) as TextBlock).Text;
            string id = (this.dataGrid_DeviceList.Columns[1].GetCellContent(this.dataGrid_DeviceList.Items[0]) as TextBlock).Text;

            new Window_DeviceDetails(name, id).ShowDialog();

            if (DeviceDetails.deviceName.Length == 0)
            {
                return;
            }

            (this.dataGrid_DeviceList.Columns[0].GetCellContent(this.dataGrid_DeviceList.Items[0]) as TextBlock).Text = DeviceDetails.deviceName;
            (this.dataGrid_DeviceList.Columns[1].GetCellContent(this.dataGrid_DeviceList.Items[0]) as TextBlock).Text = DeviceDetails.deviceId;

            DeviceList.deviceListChanged = true;
        }

        private void button_Remove_Click(object sender, RoutedEventArgs e)
        {
            var item = this.dataGrid_DeviceList.SelectedItem;
            string name = (this.dataGrid_DeviceList.Columns[0].GetCellContent(this.dataGrid_DeviceList.Items[0]) as TextBlock).Text;
            DeviceList.id.Remove(name);
            this.dataGrid_DeviceList.Items.Remove(item);

            DeviceList.deviceListChanged = true;
        }

        #endregion
    }
}
